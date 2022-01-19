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
    List<string> DuplicateBoodschappen = new List<string>();
    string[] BoodSchappenArr;
    public GameObject[] Counters;

    readonly string postURL = "https://ffl2021-2022.000webhostapp.com/UWR_Tut_Post.php";
    public float PrevRect;

    private void Start()
    {
        ButtonMask.Add(null);
        Counters = GameObject.FindGameObjectsWithTag("Counter");
    }
    
    void Update()
    {
        if (!ButtonMask.Contains(SelectedGameobject) && CoRoutineRunning == false)
        {
            PrevRect = rect.transform.position.y;
            StartCoroutine(ExecuteAfterTime(0.5f));
            CoRoutineRunning = true;
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
            addBoodschappen();
        }
        else SelectedGameobject = null;
        CoRoutineRunning = false;
    }

    void addCounters()
    {
        for (int i = 0; i < Counters.Length; i++)
        {
            string cntrs = Counters[i].GetComponent<Text>().text;
            int b = int.Parse(cntrs);
            if (BoodSchappen.Contains(Counters[i].name)&&Counters[i].name == ButtonName)
            {
                b++;
                Counters[i].GetComponent<Text>().text = b.ToString();
            }
        }
    }
    void addBoodschappen()
    {
        BoodSchappen.Add(ButtonName);
        addCounters();
    }
    public void setGameobject()
    {
        SelectedGameobject = EventSystem.current.currentSelectedGameObject.name;
    }

}
