//------------------------------------------------------------------------------
//
// File Name:	MenuTransitionButtonSound.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    Raccoon
// Course:	    WANIC VGP2
//
// Copyright ©️ 2022 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTransitionButtonSound : MonoBehaviour
{
    //[Tooltip("The button sound")]
    //public AudioClip buttonSound;
    [Tooltip("Scenes Audio Source")]
    public AudioSource sceneSource;

    // Plays the button's sound
    public void ButtonSound()
    {
        //if (sceneSource != null || buttonSound != null)
        //{
        //    sceneSource.PlayOneShot(buttonSound);
        //}
    }
}
