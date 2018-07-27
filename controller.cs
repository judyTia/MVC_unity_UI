using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour {
    allWords myword;
    myView myview;
    word oneword;
    public word[] addword;
    private static controller _instance;

    public static controller Instance { get { return _instance; } }


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
    // Use this for initialization
    void Start () {
        myword = allWords.Instance;
        myview = myView.Instance;
        oneword = null;
        /*
        word AngryWord = new word("Angry", "En colère", "enojado", VersionOfWord.English);
        word AfterWord = new word("After", "Après", "Después", VersionOfWord.English);
        word AppleWord = new word("Apple", "Pomme", "manzana", VersionOfWord.English);
        word AgainWord = new word("Again", "Encore", "De nuevo", VersionOfWord.English);
        word ALotWord = new word("A Lot", "Beaucoup", "Mucho", VersionOfWord.English);
       
        myword.add(AngryWord);
        myword.add(AfterWord);
        myword.add(AppleWord);
        myword.add(AgainWord);
        myword.add(ALotWord);
        */

        int lengh = addword.Length;
        for(int i=0;i<lengh;i++)
        {
            addword[i].Setword();
            myword.add(addword[i]);
        }
        myview.setUpItem();

    }
    public void changeCurrentWord(word mWord)
    {
        oneword = mWord;
    }
    public word getCurrentWord()
    {
        return oneword;
    }
    public List<string> orignalWords()
    {
        return myword.OrignalList();
    }
    public List<string> sortWords()
    {
        return myword.sortWords();
    }
    public word randomOneWord()
    {
        return myword.RandomOneWord();
    }
    
    public void category()
    {
        myview.Cataglories(orignalWords());
    }
    public void sort()
    {
        myview.SortWords(sortWords());
    }
    public void Randomword()
    {

        oneword = myword.ForwardRandomWord(oneword);
        myview.Randomword(oneword);
    }
    public void Favoriate()
    {
        myview.Faouvriate(myword.favoriateList());
    }
    public void changeFavorite()
    {
        print("controller");
        oneword.changeFavorite();
        print(oneword.getFavorite());
        myview.addFavoriteAnimation(oneword.getFavorite(), oneword.getEnSpelling());
    }
    public void changeVersion()
    {
        oneword.changeVersion();
        myview.changePaper(oneword);
    }
    public void SoundoneWord()
    {
        myview.playWordSound(oneword);
    }
    public void hideboradshowWord(string mykey)
    {
        oneword = myword.findOneWord(mykey);
       
    }
    public void ReverseOpenPaper()
    {

        createAnimation.Instance.reverseOpenPaper();
    }

    // for end animation
    //end board close
    public void finishedBoardAni()
    {
        myview.finishedBoardAni(oneword);
    }
    //end paper leave left
    public void finishedleavepanelAni()
    {

        myview.ShowPaper(oneword);

    }
    //end paper open left
    public void finishedPaperOpen()
    {
        myview.finishedOpenPaper(oneword);
    }
    //end paper reverse open right
    public void finishedReversePaperOpen()
    {
        oneword = myword.BackRandomOneWord(oneword);
        myview.ShowReversePaper(oneword);
    }
    //end papaer reverse leave right
    public void finishedReversePaperLeave()
    {
        myview.finishedOpenPaper(oneword);
    }
}
