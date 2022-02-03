using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestellingButton : MonoBehaviour
{
    private bool ToggleBool = false;
    public Animator anim;
    public RectTransform Bestelling;

    private void Start()
    {
        anim.enabled = false;
    }

    public void ToggleAnim()
    {
        anim.enabled = true;
        ToggleBool = !ToggleBool;
        if(ToggleBool)anim.Play("Pressed");
        else anim.Play("Normal");
    }

    IEnumerator Animation(int height, float time)
    {
        yield return new WaitForSeconds(0.01f);

        Bestelling.sizeDelta = Bestelling.sizeDelta + new Vector2(height / time * 0.01f ,0);
        if(Bestelling.sizeDelta.x < height)StartCoroutine(Animation(250, 1));
    }
}
