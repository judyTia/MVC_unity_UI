using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VersionOfWord { English = 0,French = 1, Spanish = 2 };
[System.Serializable]
public class word{
    private string sound;
    public string spelling_EN;
    public string spelling_FR;
    public string spelling_SP;
    public float delaySound;
    private string symbol;
    private string animationCharater;
    private bool favorite;

    private static VersionOfWord Version = VersionOfWord.English;
    public void Setword()
    {

        string TrimEN = spelling_EN.Replace(" ", "");
        sound = "EN_" + TrimEN;
        symbol = "SignsAll_" + TrimEN;

        string lowermywrodname = TrimEN.ToLower();
        animationCharater = char.ToUpper(lowermywrodname[0]) + lowermywrodname.Substring(1);
        //animationCharater = "samuel@Anim_" + newname;
        favorite = false;
       

    }
    public string getEnSpelling()
    {
        return spelling_EN;
    }

    public string getSpelling()
    {
        if (Version == VersionOfWord.English)
        {
            return spelling_EN;
        }
        else if(Version == VersionOfWord.French)
        {
            return spelling_FR;
        }
        else
        {
            return spelling_SP;
        }
    }
    public bool getFavorite()
    {
        return favorite;
    }
    public VersionOfWord getVersion()
    {
        return Version;
    }
    public string getSymbol()
    {
        return symbol;
    }
    public string getanimationCharater()
    {
        return animationCharater;
    }
    public string getsound()
    {
        string TrimEN = spelling_EN.Replace(" ", "");
        
        if (Version == VersionOfWord.English)
        {
            sound = "EN_" + TrimEN;
            return sound;
        }
        else if (Version == VersionOfWord.French)
        {
            sound = "FR_" + TrimEN;
            return sound;
        }
        else
        {
            sound = "SP_" + TrimEN;
            return sound;
        }
    }
    public void changeFavorite()
    {
        favorite = !favorite;
    }

    public void changeVersion()
    {
        
        if (Version == VersionOfWord.English)
        {
            sound = "FR_" + spelling_EN;
            sound = sound.Replace(" ", "");
            Version = VersionOfWord.French;
        }
        else if (Version == VersionOfWord.French)
        {
            sound = "SP_" + spelling_EN;
            sound = sound.Replace(" ", "");
            Version = VersionOfWord.Spanish;
        }
        else
        {
            sound = "EN_" + spelling_EN;
            sound = sound.Replace(" ", "");
            Version = VersionOfWord.English;
        }
    }

    // Use this for initialization
    /*
     void Start () {

     }

     void playSound()
     {
         if(sound!=null)
         {
             sound.Play();
         }
     }
     void playAni()
     {

     }
     void showSymbol()
     {

     }
     */

}
