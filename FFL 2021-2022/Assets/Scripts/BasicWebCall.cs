using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BasicWebCall : MonoBehaviour
{
    public string Bestelling;
    public string[] BestellingArr;
    public Text text;
    readonly string getURL = "https://ffl2021-2022.000webhostapp.com/UWR_Tut_Get.php";
    readonly string postURL = "https://ffl2021-2022.000webhostapp.com/UWR_Tut_Post.php";

    private void Start()
    {
        text.text = "";
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
                BestellingArr = Bestelling.Substring(1).Split(':');
                //Split();
            }    
        }
    }

    private void Split()
    {
        BestellingArr = Bestelling.Substring(1).Split(':');
        for(int i = 0;i<BestellingArr.Length;i++)
        {
            text.text += "\n" + BestellingArr[i];
        }
    }
}
