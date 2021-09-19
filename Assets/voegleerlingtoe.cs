using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voegleerlingtoe : MonoBehaviour
{
    public GameObject leerling;
    public float hoogte;
    RectTransform rT;
    
    // Start is called before the first frame update
    void Start()
    {
        rT = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (ContinuousDemo.scanned == 2)
        {
            Instantiate(leerling, transform);
            print("scanned");
            ContinuousDemo.scanned = 1;
            rT.sizeDelta = new Vector2(rT.sizeDelta.x, rT.sizeDelta.y + 260f);
        }
    }
}
