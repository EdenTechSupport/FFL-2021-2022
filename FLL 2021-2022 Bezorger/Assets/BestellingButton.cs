using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestellingButton : MonoBehaviour
{
    private bool ToggleBool = false;
    public Animator anim;

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
}
