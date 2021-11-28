using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public void ChangeRect(float y)
    {
        GetComponent<RectTransform>().sizeDelta = new Vector3(transform.localScale.x, y, transform.localScale.z);
    }
}
