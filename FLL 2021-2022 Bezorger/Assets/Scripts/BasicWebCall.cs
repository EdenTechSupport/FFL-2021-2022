using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BasicWebCall : MonoBehaviour
{
    public string Bestelling;
    public string[] BestellingArr;
    List<int> BestellingList = new List<int>();
    public Text text;
    readonly string getURL = "https://ffl2021-2022.000webhostapp.com/UWR_Tut_Get.php";
    readonly string postURL = "https://ffl2021-2022.000webhostapp.com/UWR_Tut_Post.php";
    int Score;

    private void Start()
    {
        InvokeRepeating("GetScore", 0f, 5f);
    }
    

    public void GetScore()
    {
        StartCoroutine(SimpleGetRequest());
    }

    IEnumerator SimpleGetRequest()
    {
        UnityWebRequest www = UnityWebRequest.Get(getURL);

        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }

        else
        {
            if(www.downloadHandler.text != Bestelling)
            {
                Bestelling = www.downloadHandler.text;
                Split();
            }    
        }
    }

    private void Split()
    {
        BestellingArr = Bestelling.Substring(1).Split(':');
        text.text = null;
        CountArray();
    }

    private void CountArray()
    {
        for (int i = 0; i < BestellingArr.Length; i++)
        {
            for (int j = 0; j < BestellingArr.Length; j++)
            {
                if (BestellingArr[i].Contains(BestellingArr[j]))
                {
                    Score++;
                }
                if(j == BestellingArr.Length -1)
                {
                    AddText(BestellingArr[i], Score);
                }
            }
        }
    }

    private void AddText(string Product, int j)
    {
        if(!text.text.Contains(Product)) text.text += Product + " " + j+ "\n";
        Score = 0;
    }
}

