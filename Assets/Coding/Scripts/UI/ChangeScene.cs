//------------------------------------------------------------------------------
//
// File Name:	ChangeScene.cs
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
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [Tooltip("Name of the scene you want this script to load")]
    public string sceneName;
    [Tooltip("The button sound")]
    public AudioClip buttonSound;
    [Tooltip("Scenes Audio Source")]
    public AudioSource sceneSource;

    //Function to be called that loads new scene
    public void ChangeSceneNow()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
        //if (sceneSource == null || buttonSound == null)
        //{
        //    SceneManager.LoadScene(sceneName);
        //}
        //else
        //{
        //    StartCoroutine(WaitForSound());
        //}
    }

    // Coroutine to wait for sound to be played
    private IEnumerator WaitForSound()
    {
        sceneSource.PlayOneShot(buttonSound);
        yield return new WaitForSeconds(buttonSound.length);
        SceneManager.LoadScene(sceneName);
    }
    
}
