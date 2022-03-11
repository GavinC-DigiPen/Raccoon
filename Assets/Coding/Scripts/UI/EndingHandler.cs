//------------------------------------------------------------------------------
//
// File Name:	EndingHandler.cs
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
using UnityEngine.UI;

public class EndingHandler : MonoBehaviour
{
    [Tooltip("Caught ending sound")]
    public AudioClip audioCaught;
    [Tooltip("Bad ending audio")]
    public AudioClip audioBad;
    [Tooltip("Okay ending audio")]
    public AudioClip audioOkay;
    [Tooltip("Best ending audio")]
    public AudioClip audioAmazing;

    private Image endImage;
    private AudioSource myAud;

    private Image imageOne;
    private Image imageTwo;
    private Image imageThree;

    // Start is called before the first frame update
    void Start()
    {
        endImage = GetComponent<Image>();
        myAud = GetComponent<AudioSource>();

        imageOne = gameObject.transform.GetChild(0).GetComponent<Image>();
        imageTwo = gameObject.transform.GetChild(1).GetComponent<Image>();
        imageThree = gameObject.transform.GetChild(2).GetComponent<Image>();

        imageOne.color = Color.black;
        imageTwo.color = Color.black;
        imageThree.color = Color.black;

        if (GameManager.score >= GameManager.maxScore * (1.0f / 3.0f) - 1f)
        {
            imageOne.color = Color.white;
        }
        if (GameManager.score >= GameManager.maxScore * (2.0f / 3.0f) - 1f)
        {
            imageTwo.color = Color.white;
        }
        if (GameManager.score == GameManager.maxScore)
        {
            imageThree.color = Color.white;
        }
    }
}
