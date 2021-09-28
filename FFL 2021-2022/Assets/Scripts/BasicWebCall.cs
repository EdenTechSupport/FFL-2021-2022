using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BasicWebCall : MonoBehaviour
{
    public string Bestelling;
    public string[] BestellingArr;
    public GameObject[] Prefabs;
    readonly string getURL = "https://ffl2021-2022.000webhostapp.com/UWR_Tut_Get.php";
    readonly string postURL = "https://ffl2021-2022.000webhostapp.com/UWR_Tut_Post.php";

    private void Start()
    {
        InvokeRepeating("GetScore", 0f, 2f);
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
        for(int i = 0;i<BestellingArr.Length;i++)
        {
             if(Prefabs.Contains.(BestellingArr[i]))
             {
                 print("huts");
             }
        }
    }
}
