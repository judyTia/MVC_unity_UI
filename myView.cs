using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
public class myView : MonoBehaviour {
    public enum PanelOfState { CateogoryOpen = 0, SortOpen = 1, FavoriteOpen = 2, NoOneOpen = 4 , CateogoryClose = 5, SortClose = 6, FavoriteClose = 7 };
    public enum PaperOfState { PaperOpen = 0,PaperClose = 1, PaperNone = 2}; // Parperclose if a Paper is not open yet, but will be open after end of the animation
    public ScrollRect myScrollRect;
    public Animator myOpenAni;
    //paper
    public Animator myparperAni;
    public Image mysymbol;
    public Text myspelling;
    public Image myEN_FR;
    public Image myfavorite_click;
    public Image myfavorite_ani;
    public AudioSource myWordSound;
    private bool alradyhaspaper;

    //character animation
    public Animator mycharacterAni;
    //assign papaer things

   
    //assign the first positon of black and paper
    public Transform blackboardPos;
    public Transform paperPos;


    private Sprite sprite_EN;
    private Sprite sprite_FR;
    private Sprite sprite_ES;

    private Sprite sprite_Favorite_white;
    private Sprite sprite_Favorite_yellow;
    private Sprite sprite_Favorite_whiteyellow;



    private PaperOfState myPaperOfState;

    Dictionary<string, Sprite> singalAll =
         new Dictionary<string, Sprite>();
    //
    public GameObject myPrefab;
    public Transform myPrefabParent;


    private PanelOfState myPanelOfState;

    Dictionary<string, GameObject> GameObjectsDic =
          new Dictionary<string, GameObject>();
    private static myView _instance;

    public static myView Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {


        if (myScrollRect == null)
        {
            print("not OK");
        }
        else
        {
            myScrollRect.verticalNormalizedPosition = 1;
        }
        alradyhaspaper = false;

         sprite_EN = Resources.Load<Sprite>("images/En_SpeechBubble");
         sprite_FR = Resources.Load<Sprite>("images/FR_SpeechBubble");
         sprite_ES = Resources.Load<Sprite>("images/ES_SpeechBubble");

         sprite_Favorite_white = Resources.Load<Sprite>("images/FavoritesIcon_White");
         sprite_Favorite_yellow = Resources.Load<Sprite>("images/StarOn");
        sprite_Favorite_whiteyellow = Resources.Load<Sprite>("images/FavoritesIcon_Yellow");
        Sprite[] SpritesData = Resources.LoadAll<Sprite>("images/SignsAll_001_128x128");
        for (int i = 0; i < SpritesData.Length; i++)
        {
            singalAll.Add(SpritesData[i].name, SpritesData[i]);
        }

        myPanelOfState = PanelOfState.NoOneOpen;
        myPaperOfState = PaperOfState.PaperNone;
        
    }


    public Sprite GetSpriteByName(string name)
    {
        if (singalAll.ContainsKey(name))
            return singalAll[name];
        else
            return null;
    }
    public void setUpItem()
    {
        Dictionary<string, word> mywordsDic = allWords.Instance.getAllwordsDic();
        List<string> keys = mywordsDic.Keys.ToList();
        foreach (string o in keys)
        {
            GameObject myword = Instantiate(myPrefab, myPrefabParent) as GameObject;
            GameObjectsDic.Add(o, myword);
            Button mybutton = myword.GetComponent<Button>();
           
            Transform childTrans = myword.transform.GetChild(0);
            Text myText = childTrans.GetComponent<Text>();
            myText.text = o;
            Transform childImageTrans = myword.transform.GetChild(1);
            Image myImage = childImageTrans.GetComponent<Image>();
            bool mybuttionfavoriate = mywordsDic[o].getFavorite();
            if (mybuttionfavoriate)
            {
                myImage.sprite = sprite_Favorite_whiteyellow;
            }
            else
            {
                myImage.sprite = sprite_Favorite_white;
            }


            mybutton.onClick.AddListener(() => Mybutton(o));
        }
    }
    void Mybutton(string mykey)
    {
        print(mykey);
        // isCategory = false;
        myPaperOfState = PaperOfState.PaperClose;
        controller mycontroller = controller.Instance;
        mycontroller.hideboradshowWord(mykey);
        ShowPanel(false);
        myPanelOfState = PanelOfState.NoOneOpen;

    }
    public PanelOfState getMyPanelState()
    {
        return myPanelOfState;
    }
    bool isStart = true;
    public void Cataglories(List<string> mykeys)
    {
        // print("category");
        /*
        if (isStart)
        {
            blackboardPos.position = blackboardPos.position + Vector3.up * (Screen.width / 20);
            paperPos.position = paperPos.position + Vector3.right * (Screen.height / 20);
            isStart = false;
        }
        */
        if (!createAnimation.Instance.getPlaying())
        {
            if (myPanelOfState == PanelOfState.NoOneOpen || myPanelOfState == PanelOfState.CateogoryClose)
            {

                int index = 0;

                foreach (string mykey in mykeys)
                {
                    //print("get: " +mykey);
                    GameObjectsDic[mykey].transform.SetSiblingIndex(index++);
                    GameObjectsDic[mykey].SetActive(true);
                }

                ShowPanel(true);
                myPanelOfState = PanelOfState.CateogoryOpen;
            }
            else if (myPanelOfState == PanelOfState.CateogoryOpen)
            {
                ShowPanel(false);
                myPanelOfState = PanelOfState.NoOneOpen;
            }
            else
            {
                ShowPanel(false);
                myPanelOfState = PanelOfState.CateogoryClose;
            }
            //blackboardPos.gameObject.SetActive(true);
        }
    }
    public void Faouvriate(List<string> mykeys)
    {
        if (!createAnimation.Instance.getPlaying())
        {
            //int index = 0;
            if (myPanelOfState == PanelOfState.NoOneOpen || myPanelOfState == PanelOfState.FavoriteClose)
            {

                List<string> allobjectList = GameObjectsDic.Keys.ToList<string>();
                foreach (string mykey in allobjectList)
                {
                    //print("get: " +mykey);
                    //  GameObjectsDic[mykey].transform.SetSiblingIndex(index++);
                    if (!mykeys.Contains(mykey))
                    {
                        GameObjectsDic[mykey].SetActive(false);
                    }
                    else
                    {
                        GameObjectsDic[mykey].SetActive(true);
                    }
                }
                myPanelOfState = PanelOfState.FavoriteOpen;
                ShowPanel(true);
            }
            else if (myPanelOfState == PanelOfState.FavoriteOpen)
            {
                ShowPanel(false);
                myPanelOfState = PanelOfState.NoOneOpen;
            }
            else
            {
                ShowPanel(false);
                myPanelOfState = PanelOfState.FavoriteClose;
            }
        }

    }

    public void SortWords(List<string> mykeys)
    {
        //print("sort");
        if (!createAnimation.Instance.getPlaying())
        {
            if (myPanelOfState == PanelOfState.NoOneOpen || myPanelOfState == PanelOfState.SortClose)
            {

                int index = 0;
                foreach (string mykey in mykeys)
                {
                    GameObjectsDic[mykey].transform.SetSiblingIndex(index++);
                    GameObjectsDic[mykey].SetActive(true);
                }
                myPanelOfState = PanelOfState.SortOpen;
                ShowPanel(true);
            }
            else if (myPanelOfState == PanelOfState.SortOpen)
            {
                ShowPanel(false);
                myPanelOfState = PanelOfState.NoOneOpen;
            }
            else
            {
                ShowPanel(false);
                myPanelOfState = PanelOfState.SortClose;
            }
        }
    }
    public void ShowPanel(bool isOpen)
    {

        if (isOpen)
        {
            if (myOpenAni.enabled)
            {
                myOpenAni.SetTrigger("open");
            }
            else
            {
                createAnimation.Instance.openBlackBoardAni();
            }
        }
        else
        {
            if (myOpenAni.enabled)
            {
                myOpenAni.SetTrigger("close");
            }
            else
            {
                createAnimation.Instance.closeBlackBoardAni();
            }
        }
    }
    
    public void ShowPaper(word myword)
    {

        changePaper(myword);
        if (myparperAni.enabled)
        {
            myparperAni.SetTrigger("open");
        }
        else
        {
            createAnimation.Instance.openPaper();
        }
    }
    public void ShowReversePaper(word myword)
    {
        changePaper(myword);
        createAnimation.Instance.reverseOutPaper();
    }
    public void changePaper(word myword)
    {
        string mykeyname = myword.getEnSpelling();
        string mykeytrimname = mykeyname.Replace(" ", "");
        string mysymbolname = myword.getSymbol();
        mysymbol.sprite = GetSpriteByName(mysymbolname);
        myspelling.text = myword.getSpelling();
        myEN_FR.sprite = getVersionSprite(myword.getVersion());
        myfavorite_click.sprite = getFavoriteSprite(myword.getFavorite());
        AudioClip nowwordAudio = Resources.Load<AudioClip>("Signs" + mykeytrimname);

    }
    public void finishedBoardAni(word oneword)
    {
        if (myPaperOfState == PaperOfState.PaperClose)
        {
            myPanelOfState = PanelOfState.NoOneOpen;
            Randomword(oneword);
        }
        else if(myPanelOfState == PanelOfState.CateogoryClose)
        {
            controller.Instance.category();
        }
        else if(myPanelOfState == PanelOfState.SortClose)
        {
            controller.Instance.sort();
        }
        else if(myPanelOfState == PanelOfState.FavoriteClose)
        {
            controller.Instance.Favoriate();
        }
    }
    public Sprite getVersionSprite(VersionOfWord Version)
    {
        if (Version == VersionOfWord.English)
        {
            return sprite_EN;
        }
        else if (Version == VersionOfWord.French)
        {
            return sprite_FR;
        }
        else
        {
            return sprite_ES;
        }
    }
    public Sprite getFavoriteSprite(bool isFavorite)
    {
        if(isFavorite)
        {
            return sprite_Favorite_yellow;
        }
        else
        {
            return sprite_Favorite_white;
        }
    }
    public void addFavoriteAnimation(bool isFavorite,string mykey)
    {

        GameObject myword = GameObjectsDic[mykey];
        Transform childImageTrans = myword.transform.GetChild(1);
        Image myImage = childImageTrans.GetComponent<Image>();
        myImage.sprite = getFavoriteSprite(isFavorite);
        if (isFavorite)
        {
            //myparperAni.SetTrigger("favoriate");  
            myImage.sprite = sprite_Favorite_whiteyellow;
            createAnimation.Instance.addFavorite();
        }
        else
        {
            myImage.sprite = sprite_Favorite_white;
        }

        myfavorite_click.sprite = getFavoriteSprite(isFavorite);
    }
    // sound and animation
    public void playWordSound(word myword)
    {
        print("sound");
        if (mycharacterAni != null)
        {
            mycharacterAni.SetTrigger(myword.getanimationCharater());
        }
        string mykeyname = myword.getsound();
        AudioClip nowwordAudio = Resources.Load<AudioClip>("Signs/" + mykeyname);
        myWordSound.clip = nowwordAudio;
        myWordSound.PlayDelayed(myword.delaySound);

    }

    public void finishedOpenPaper(word myword)
    {
        playWordSound(myword);

    }
    public void Quiztootherscene()
    {
        SceneManager.LoadScene("testforeventsystem");
    }
    public void MainuScene()
    {
      
        print("Mainu");
    }
    public void ScorllButton(int number)
    {
        print(number);
    }
    //show a specified word, if there is already exist one, leave one then show. 
    public void Randomword(word myword)
    {
        // paperPos.gameObject.SetActive(true);
        print("random");
        if (!createAnimation.Instance.getPlaying())
        {
            if (myPanelOfState != PanelOfState.NoOneOpen)
            {
                myPaperOfState = PaperOfState.PaperClose;
                ShowPanel(false);
            }
            else
            {
                if (alradyhaspaper)
                {
                    if (myparperAni.enabled)
                    {
                        myparperAni.SetTrigger("leave");
                    }
                    else
                    {
                        createAnimation.Instance.leavePaper();
                    }

                }
                else
                {
                    if (myparperAni.enabled)
                    {
                        // myparperAni.SetTrigger("open");
                        ShowPaper(myword);
                    }
                    else
                    {
                        //createAnimation.Instance.openPaper();
                        ShowPaper(myword);
                    }
                }
                alradyhaspaper = true;
                myPaperOfState = PaperOfState.PaperOpen;
            }
        }
    }
}
