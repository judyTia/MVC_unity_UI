using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class allWords : MonoBehaviour {

     Dictionary<string, word> wordsDic =
           new Dictionary<string, word>();
    Stack<word> randomLeftHistory = new Stack<word>();
    Stack<word> randomRight = new Stack<word>();
    private static allWords _instance;

    public static allWords Instance { get { return _instance; } }


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
    public Dictionary<string, word> getAllwordsDic()
    {
        return wordsDic;
    }
    public void add(word mywrod)
    {
        wordsDic.Add(mywrod.getEnSpelling(), mywrod);
    }
    public List<string> OrignalList()
    {
        List<string> keyList = wordsDic.Keys.ToList();
        //showList(keyList); // test
        return keyList;
    }
    public List<string> sortWords()
    {
        List<string> keyList = wordsDic.Keys.ToList();
        keyList.Sort();
        //showList(keyList); // test
        return keyList;
    }
    public List<string> favoriateList()
    {
        List<string> keyList = wordsDic.Keys.ToList();
        List<string> copyList = new List<string>();
        foreach (string o in keyList.ToArray())
        {
            if (wordsDic[o].getFavorite())
            {
                copyList.Add(o);
            }
        }
        return copyList;
    }
    public word RandomOneWord()
    {
        List<string> keyList = wordsDic.Keys.ToList();
        int r = Mathf.FloorToInt(keyList.Count * Random.value);
        string oneWordkey = keyList[r];
        return wordsDic[oneWordkey];
    }
    
    public word ForwardRandomWord(word oneword)
    {
        word mywordkey;
        if (randomRight.Count == 0)
        {
            mywordkey = RandomOneWord();
        }
        else
        {
            mywordkey = randomRight.Pop();
        }
        if (oneword != null)
        {
            randomLeftHistory.Push(oneword);
        }
        return mywordkey;
    }
   
    public word BackRandomOneWord(word oneword)
    {
        word mywordkey;
        if(randomLeftHistory.Count== 0)
        {
            mywordkey = RandomOneWord();  
        }
        else
        {
            mywordkey = randomLeftHistory.Pop();
        }
        randomRight.Push(oneword);
      
        return mywordkey;
    }
    public word findOneWord(string mykey)
    {
        return wordsDic[mykey];
    }
    public void changeOneWordFavorite(string mywordKey)
    {
        wordsDic[mywordKey].changeFavorite();
    }
    public void showList(List<string> mykeys)
    {
        foreach (string o in mykeys)
        {
            print(wordsDic[o].getSpelling());
        }
    }

}
