using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class leerling : MonoBehaviour
{
    public Text text;

    
    // Start is called before the first frame update
    void Start()
    {
        text.text = ContinuousDemo.scanwaarde;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
