using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetRequest : MonoBehaviour
{
    public string Bestelling;
    string ButtonName;
    public List<string> ButtonMask = new List<string>();

    readonly string getURL = "https://ffl2021-2022.000webhostapp.com/UWR_Tut_Get.php";
    readonly string postURL = "https://ffl2021-2022.000webhostapp.com/UWR_Tut_Post.php";

    private void Start()
    {
  
    }

    void Update()
    {
      if(!ButtonMask.Contains(EventSystem.current.currentSelectedGameObject.name)) m_ButtonName();
      print(ButtonName);
    }

    void m_ButtonName()
    {
      ButtonName = EventSystem.current.currentSelectedGameObject.name;
    }
    public void OnButtonGetScore()
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
            print("error");
        }
    }

    public void OnButtonSendScore()
    {
        if (Bestelling == string.Empty)
        {
            print("empty string");
        }
        else
        {
            StartCoroutine(SimplePostRequest(Bestelling));
        }
    }

    IEnumerator SimplePostRequest(string curScore)
    {
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
        wwwForm.Add(new MultipartFormDataSection("curScoreKey", curScore));

        UnityWebRequest www = UnityWebRequest.Post(postURL, wwwForm);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
    }
}
