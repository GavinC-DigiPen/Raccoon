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
    [Tooltip("Caught ending image")]
    public Sprite spriteCaught;
    [Tooltip("Bad ending image")]
    public Sprite spriteBad;
    [Tooltip("Okay ending image")]
    public Sprite spriteOkay;
    [Tooltip("Best ending image")]
    public Sprite spriteBest;
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

    // Start is called before the first frame update
    void Start()
    {
        endImage = GetComponent<Image>();
        myAud = GetComponent<AudioSource>();

        if (GameManager.score == GameManager.maxScore)
        {
            endImage.sprite = spriteBest;
            myAud.PlayOneShot(audioAmazing);
        }
        else if (GameManager.score >= GameManager.maxScore * (2.0f / 3.0f) - 1f)
        {
            endImage.sprite = spriteOkay;
            myAud.PlayOneShot(audioOkay);
        }
        else if (GameManager.score >= 0)
        {
            endImage.sprite = spriteBad;
            myAud.PlayOneShot(audioBad);
        }
        else
        {
            endImage.sprite = spriteCaught;
            myAud.PlayOneShot(audioCaught);
        }
    }
}
