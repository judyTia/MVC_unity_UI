using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createAnimation : MonoBehaviour {
    public RectTransform myblackMenuOutside;
    public RectTransform myblackMenuShakeSide;
    public RectTransform myblackMenuInside;
    public RectTransform myblackMenu;

    public RectTransform myblackbordPosOutside;
    public RectTransform myblackbordPosInside;
    public RectTransform myblackbord;

    public RectTransform myPaperPosBegin;
    public RectTransform myPaperPosMiddle;
    public RectTransform myPaperPosEnd;
    public RectTransform myPaper;

    public RectTransform myFavoriteBegin;
    public RectTransform myFavoriteEnd;
    public RectTransform myfavorite;

    private float PosX;
    private float PosY;
    private float PosZ;
    private float shakeStartTime;
    public float shakeTime = 3;
    public float shakeAmount = 5;
    public float shakeSpeed = 10;
    public float speed = 100;
    public float favoriateSpeed = 50;
    Vector3 myoutside;
    Vector3 myinside;
    Vector3 BeginPos; // for favorite begin animation
    bool isFirst = true;
    bool playMenuDown = false;
    bool playMenuShake = false;
    bool playShakeToEnd = false;
    bool playBlackbroadDown = false;
    bool playBlackbroadUp = false;
    bool playPaperinside = false;
    bool playPaperOutside = false;
    bool playReversePaperinside = false;
    bool playReversePaperOutside = false;
    bool playFavorite = false;
    bool isplay = false;
    private float journeyLength;
    public float ScaleSize = 0.5f;
    private float ScaleSpeed;
    private float startTime;
    private Vector3 orignalScale = new Vector3(1, 1, 1);
    private Vector3 ScaleAxis = new Vector3(1, 1, 0);
    bool endMove = false;
    float duringTime = float.MaxValue;
    float fracJourney = 0;
    float existStarEnd = 0.3f;

    public delegate void EndAnimationDelegate();
    public EndAnimationDelegate myEndAnimationMethod;


    private static createAnimation _instance;

    public static createAnimation Instance { get { return _instance; } }


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
        myEndAnimationMethod = endAnim;
       myoutside = myblackbordPosOutside.position;
       myinside = myblackbordPosInside.position;
        myPaper.gameObject.SetActive(false);
        myblackbord.gameObject.SetActive(false);
        PosX = myblackMenuShakeSide.position.x;
        PosY = myblackMenuShakeSide.position.y;
        PosZ = myblackMenuShakeSide.position.z;
        MenuDown();
    }
	
	// Update is called once per frame
	void Update () {
        if(playFavorite)
        {
            animationfavarite(myfavorite, myFavoriteBegin, myFavoriteEnd, ref playFavorite);
        }
        if(playMenuDown)
        {
            animationPanel(myblackMenu, myblackbordPosOutside.position, myblackMenuShakeSide.position, ref playMenuDown, shakeMenuBoard, speed);
        }
        if(playMenuShake)
        {
            if (!playShakeToEnd)
            {
                float offestY = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
                myblackMenu.position = new Vector3(PosX, PosY + offestY, PosZ);
            }
            if ((Time.time - shakeStartTime) > shakeTime)
            {
                playShakeToEnd = true;
                animationPanel(myblackMenu, myblackMenu.position, myblackMenuInside.position, ref playMenuShake, endShake,shakeSpeed);
            }
        }
        if (playBlackbroadDown)
        {
            animationPanel(myblackbord, myblackbordPosOutside.position, myblackbordPosInside.position,ref playBlackbroadDown, null,speed);
           
        }
        if(!playBlackbroadDown && playBlackbroadUp)
        {
            animationPanel(myblackbord, myblackbordPosInside.position, myblackbordPosOutside.position,ref playBlackbroadUp, endboradAni,speed);
            
        }
        if(playPaperinside)
        {
            animationPanel(myPaper, myPaperPosBegin.position, myPaperPosMiddle.position, ref playPaperinside, endPaperOpen,speed);
        }
        if(!playPaperinside&&playPaperOutside)
        {
            animationPanel(myPaper, myPaper.position, myPaperPosEnd.position, ref playPaperOutside, endani,speed);

        }
        if (playReversePaperinside)
        {
            animationPanel(myPaper, myPaper.position, myPaperPosBegin.position,  ref playReversePaperinside, endPaperReverseOpen,speed);
        }
        if (!playReversePaperinside && playReversePaperOutside)
        {
            animationPanel(myPaper, myPaperPosEnd.position, myPaperPosMiddle.position,  ref playReversePaperOutside, endPaperReverseLeave, speed);

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
            {
             openBlackBoardAni();
            }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            closeBlackBoardAni();
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            openPaper();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            addFavorite();
        }

    }
    public void addFavorite()
    {
        playFavorite = true;
        isplay = true;
    }
    public void MenuDown()
    {
        myblackMenu.position = myblackMenuOutside.position;
        playMenuDown = true;
        isplay = true;
    }
    public void shakeMenuBoard()
    {
        playMenuShake = true;
        isplay = true;
        shakeStartTime = Time.time;
    }
    public void endShake()
    {
        playShakeToEnd = false;
    }
    public bool getPlaying()
    {
        return isplay;
    }
    public void openBlackBoardAni()
    {
        myblackbord.gameObject.SetActive(true);
        playBlackbroadDown = true;
        isplay = true;
    }

    public void closeBlackBoardAni()
    {

        playBlackbroadUp = true;
        isplay = true;
    }
    public void openPaper()
    {
        myPaper.gameObject.SetActive(true);
        playPaperinside = true;
        isplay = true;
    }
    public void leavePaper()
    {
        playPaperOutside = true;
        isplay = true;
    }
    public void reverseOpenPaper()
    {
        playReversePaperinside = true;
        isplay = true;
    }
    public void reverseOutPaper()
    {
        playReversePaperOutside = true;
        isplay = true;
    }
    // AniObj object that moves, fromRightnow is the startPos, endRightnow is the endPos,
    //playAni is the bool value which control the animation play or not, method is when the animation ends call the method, speed is the speed of the animation
    void animationPanel(RectTransform AniObj, Vector3 fromRightnow, Vector3 endRightnow,ref bool playAni, EndAnimationDelegate method,float speed)
    {


        Vector3 direction = (endRightnow - fromRightnow).normalized;
        if (isFirst)
        {
            AniObj.position = fromRightnow;
            isFirst = false;
            startTime = Time.time;
            journeyLength = Vector3.Distance(fromRightnow, endRightnow);

        }
        else if (isplay&& isFirst==false)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            AniObj.Translate(direction * Time.deltaTime * speed, Space.Self);

                if (fracJourney>=1)
                {

                    playAni = false;
                    isplay = false;
                    isFirst = true;
                    if (method != null)
                    {
                        method();
                    }
            }

        }
    }

    void animationfavarite(RectTransform AniObj, RectTransform fromRightnow, RectTransform endRightnow, ref bool playAni)
    {
        Vector3 direction = (endRightnow.position - fromRightnow.position).normalized;
        Vector3 ScaleDir = (endRightnow.localScale - fromRightnow.localScale).normalized;
      
        if (isFirst)
        {
            AniObj.gameObject.SetActive(true);
            AniObj.position = fromRightnow.position;
            BeginPos = fromRightnow.position;
            isFirst = false;
            startTime = Time.time;
            journeyLength = Vector3.Distance(fromRightnow.position, endRightnow.position);
            // journeyScale = Vector3.Distance(fromRightnow.localScale, new Vector3(0.5f,0.5f,1));
            float expendTime = journeyLength / favoriateSpeed;
            ScaleSpeed = ScaleSize / expendTime;
        }
        else if (isplay && isFirst == false)
        {
           
          
            if (!endMove)
            { 
            float distCovered = (Time.time - startTime) * favoriateSpeed;
             fracJourney = distCovered / journeyLength;
            AniObj.Translate(direction * Time.deltaTime * favoriateSpeed, Space.Self);
            // AniObj.localScale = new Vector3(-1,-1,0) * ScaleSpeed*Time.deltaTime;
            float distScale = (Time.time - startTime) * ScaleSpeed;
            AniObj.localScale = orignalScale - ScaleAxis * distScale;
            }
            if (fracJourney >= 1&& !endMove)
            {
                endMove = true;
                duringTime = Time.time;
                AniObj.gameObject.SetActive(false);
                AniObj.position = BeginPos;
                AniObj.localScale = new Vector3(1, 1, 1);
                endRightnow.gameObject.SetActive(true);
               

            }
            if (Time.time - duringTime > existStarEnd)
            {
                endRightnow.gameObject.SetActive(false);
                playAni = false;
                isplay = false;
                isFirst = true;
                endMove = false;
                fracJourney = 0;
                duringTime = float.MaxValue;
            }
        }
    }
    void endAnim()
    {
        print("delegate hahaha");
 //       playBlackbroadUp = true;
 //       isplay = true;
    }
    //end paper leave left
    public void endani()
    {
        controller.Instance.finishedleavepanelAni();
    }
    //end board
    public void endboradAni()
    {
        controller.Instance.finishedBoardAni();
    }
    //end paper open left
    public void endPaperOpen()
    {
        controller.Instance.finishedPaperOpen();
    }
    //end paper revers open right
    public void endPaperReverseOpen()
    {
        controller.Instance.finishedReversePaperOpen();
    }
    public void endPaperReverseLeave()
    {
        controller.Instance.finishedReversePaperLeave();
    }

}
