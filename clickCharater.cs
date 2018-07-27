using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickCharater : MonoBehaviour {
    public Animator mycharacterAni; 

    void OnMouseDown()
    {
        print("Giggle");
        mycharacterAni.SetTrigger("Giggle");
    }
}
