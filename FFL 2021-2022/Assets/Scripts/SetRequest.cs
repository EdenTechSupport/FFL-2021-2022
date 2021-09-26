using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetRequest : MonoBehaviour
{   public string scoreToSend;
    string ButtonName;
    public List<string> ButtonMask = new List<string>();
 
    public List<string> BestellingList = new List<string>();

    int i;

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
      i++;
      ButtonName = EventSystem.current.currentSelectedGameObject.name; 
      
      EventSystem.current.currentSelectedGameObject.name = " ";
    }
    public void OnButtonSendScore()
    {
        if (scoreToSend == string.Empty)
        {
            print("Geen Items");
        }
        else
        {
            StartCoroutine(SimplePostRequest(scoreToSend));
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

        else
        {
            print("Bestelt");
        }
    }
}
