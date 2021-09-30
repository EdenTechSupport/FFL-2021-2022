using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetRequest : MonoBehaviour
{
    public string Bestelling;
    string bestelling2;
    string ButtonName;
    string ButtonName2;
    public List<string> ButtonMask = new List<string>();
    public List<string> BoodSchappen = new List<string>();
    string[] BoodSchappenArr;

    readonly string getURL = "https://ffl2021-2022.000webhostapp.com/UWR_Tut_Get.php";
    readonly string postURL = "https://ffl2021-2022.000webhostapp.com/UWR_Tut_Post.php";

    private void Start()
    {
  
    }

    void Update()
    {
      if(!ButtonMask.Contains(EventSystem.current.currentSelectedGameObject.name)) m_ButtonName();
      print(ButtonName);
      if(ButtonName != ButtonName2) 
      {
        ButtonName2 = ButtonName;
        BoodSchappen.Add(ButtonName);
      }
    }

    void m_ButtonName()
    {
      ButtonName = EventSystem.current.currentSelectedGameObject.name;
    }
    public void OnButtonSendScore()
    {
     
      if(Bestelling != bestelling2)
      {
        BoodSchappenArr = BoodSchappen.ToArray();
        for(int i = 0; i < BoodSchappenArr.Length; i++)
        {
          Bestelling += ":" + BoodSchappenArr[i];
        }
        bestelling2 = Bestelling;
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
      Bestelling = null;
      ClearArray();
        
    }

    public void ClearArray()
    {
      BoodSchappen.Clear();
      for(int i = 0;i<BoodSchappenArr.Length;i++) BoodSchappenArr[i] = null;
    }
}
