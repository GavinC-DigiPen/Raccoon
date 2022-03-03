//------------------------------------------------------------------------------
//
// File Name:	ScoreBar.cs
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

public class ScoreBar : MonoBehaviour
{
    private RectTransform bar;
    private Image imageOne;
    private Image imageTwo;
    private Image imageThree;

    // Start is called before the first frame update
    void Start()
    {
        bar = gameObject.transform.GetChild(0).GetChild(2).GetComponent<RectTransform>();
        imageOne = gameObject.transform.GetChild(0).GetChild(3).GetComponent<Image>();
        imageTwo = gameObject.transform.GetChild(0).GetChild(4).GetComponent<Image>();
        imageThree = gameObject.transform.GetChild(0).GetChild(5).GetComponent<Image>();

        imageOne.color = Color.black;
        imageTwo.color = Color.black;
        imageThree.color = Color.black;

        StartCoroutine(WaitForLoading());
    }
    
    private IEnumerator WaitForLoading()
    {
        yield return new WaitForSeconds(0.1f);

        Collectible[] collectables = FindObjectsOfType<Collectible>();
        int maxScore = 0;
        for (int i = 0; i < collectables.Length; i++)
        {
            maxScore += collectables[i].points;
        }
        GameManager.maxScore = maxScore;
        GameManager.score = 0;

        UpdateBar();
        GameManager.ScoreUpdate.AddListener(UpdateBar);
    }

    // Update progress bar
    private void UpdateBar()
    {
        bar.sizeDelta = new Vector2(((float)GameManager.score / (float)GameManager.maxScore) * 100f, 100);
        if (bar.sizeDelta.x > 100)
        {
            bar.sizeDelta = new Vector2(100, 100);
        }
        bar.localPosition = new Vector2((bar.rect.width - 100f) / 2f, 0);

        UpdateImages();
    }

    // Update images
    private void UpdateImages()
    {
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
