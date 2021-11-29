using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetRequest : MonoBehaviour
{
    string SelectedGameobject;
    string SelectedGameobject1;
    bool CoRoutineRunning;
    public GameObject rect;
    public bool Scrolling;
    public string Bestelling;
    string bestelling2;
    string ButtonName;
    string ButtonName2;
    public List<string> ButtonMask = new List<string>();
    public List<string> BoodSchappen = new List<string>();
    string[] BoodSchappenArr;

    readonly string postURL = "https://ffl2021-2022.000webhostapp.com/UWR_Tut_Post.php";
    public float PrevRect;

    private void Start()
    {
        ButtonMask.Add(null);
    }

    void Update()
    {
        if(SelectedGameobject != EventSystem.current.currentSelectedGameObject.name && SelectedGameobject1 != EventSystem.current.currentSelectedGameObject.name)
        {
            SelectedGameobject = EventSystem.current.currentSelectedGameObject.name;
            SelectedGameobject1 = EventSystem.current.currentSelectedGameObject.name;
        }
        //print(rect.transform.position.y);
        if (!ButtonMask.Contains(SelectedGameobject) && CoRoutineRunning == false)
        {
            PrevRect = rect.transform.position.y;
            StartCoroutine(ExecuteAfterTime(0.5f));
            CoRoutineRunning = true;
        }

        //print(ButtonName);
        if (ButtonName != ButtonName2)
        {
            ButtonName2 = ButtonName;
            BoodSchappen.Add(ButtonName);
        }

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

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        if (rect.transform.position.y == PrevRect)
        {
            ButtonName = SelectedGameobject;
            SelectedGameobject = null;
        }
        else SelectedGameobject = null;
        CoRoutineRunning = false;
    }

}
