//------------------------------------------------------------------------------
//
// File Name:	GameManager.cs
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
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //allow this component to be grabbed from anywhere and make sure only one exists
    public static GameManager Instance;

    // Variables
    public static UnityEvent ScoreUpdate = new UnityEvent();
    private static int _score = 0;
    public static int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            ScoreUpdate.Invoke();
        }
    }

    public static UnityEvent MaxScoreUpdate = new UnityEvent();
    private static int _maxScore = 0;
    public static int maxScore
    {
        get
        {
            return _maxScore;
        }
        set
        {
            _maxScore = value;
            MaxScoreUpdate.Invoke();
        }
    }

    public static UnityEvent InWallUpdate = new UnityEvent();
    private static bool _inWall = false;
    public static bool inWall
    {
        get
        {
            return _inWall;
        }
        set
        {
            _inWall = value;
            InWallUpdate.Invoke();
        }
    }

    // When made make sure this is the only manager, and make the manager persistant through levels
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        
    }
}
