//------------------------------------------------------------------------------
//
// File Name:	QuitButton.cs
// Author(s):	Ryan Schepplar
//              Jeremy Kings
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

public class QuitButton : MonoBehaviour
{
    [Tooltip("The button sound")]
    public AudioClip buttonSound;
    [Tooltip("Scenes Audio Source")]
    public AudioSource sceneSource;

    // Function to close game
    public void QuitNow()
    {
        if (sceneSource == null || buttonSound == null)
        {
            #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
        else
        {
            StartCoroutine(WaitForSound());
        }
    }
        // Coroutine to wait for sound to be played
        private IEnumerator WaitForSound()
        {
            sceneSource.PlayOneShot(buttonSound);
            yield return new WaitForSeconds(buttonSound.length);

            #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
    }
}
