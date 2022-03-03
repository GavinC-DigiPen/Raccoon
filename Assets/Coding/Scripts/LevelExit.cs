//------------------------------------------------------------------------------
//
// File Name:	LevelExit.cs
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

public class LevelExit : MonoBehaviour
{
    [Tooltip("Name of the scene you want this script to load")]
    public string sceneName;
    [Tooltip("The amount of time you need to hold down the button to exit")]
    public float timeToExit = 1.0f;

    private KeyCode interactKey = KeyCode.E;
    private SignReading popUpScript;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        popUpScript = GetComponent<SignReading>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (GameManager.score > 0)
        {
            popUpScript.IsActive(true);
        }
        else
        {
            popUpScript.IsActive(false);
        }
    }

    // Check if player is in exit area
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.score > 0)
            {
                if (Input.GetKey(interactKey))
                {
                    if (timer > timeToExit)
                    {
                        SceneManager.LoadScene(sceneName);
                        timer = 0;
                    }
                }
            }
        }
    }
}
