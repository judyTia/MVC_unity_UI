using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endAnimations : MonoBehaviour {

    //end paper leave
    public void endani()
    {
        controller.Instance.finishedleavepanelAni();
    }
    //end board
    public void endboradAni()
    {
        controller.Instance.finishedBoardAni();
    }
    //end paper open
    public void endPaperOpen()
    {
        controller.Instance.finishedPaperOpen();
    }

}
