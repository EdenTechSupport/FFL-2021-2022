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
    public RectTransform UI;
    public byte Lines = 0;

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
        BestellingArr = Bestelling.Substring(1).Replace("Eden Spijker:Heinoseweg6", "").Split(':');
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
                if(j == BestellingArr.Length -2)
                {
                    AddText(BestellingArr[i], Score);
                }
            }
        }
    }

    private void AddText(string Product, int j)
    {
        if (!text.text.Contains(Product))
        {
            text.text += "-" + Product + " " + j+ "x\n";
            Lines++;
        }
        Score = 0;
    }

    public void StartAnimation(float time)
    {
        if(Lines > 0)StartCoroutine(Animation(300 + Lines * 80,time));
    }
    IEnumerator Animation(int height, float time)
    {
        yield return new WaitForSeconds(0.01f);

        UI.sizeDelta = UI.sizeDelta  + new Vector2(0 ,height / time * 0.01f);
        if (UI.sizeDelta.y < height) StartCoroutine(Animation(height, time));
        else Lines = 0;
    }
}

