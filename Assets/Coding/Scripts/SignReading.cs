//------------------------------------------------------------------------------
//
// File Name:	SignReading.cs
// Author(s):	Ryan Schepplar
//              Gavin Cooper (gavin.cooper)
// Project:	    Raccoon
// Course:	    WANIC VGP2
//
// Copyright ©️ 2022 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignReading : MonoBehaviour
{
    public GameObject hidenObject;

    private bool isActive = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && isActive)
        {
            hidenObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hidenObject.SetActive(false);
        }
    }

    public void IsActive(bool value)
    {
        isActive = value;
    }
}
