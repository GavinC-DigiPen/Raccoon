//------------------------------------------------------------------------------
//
// File Name:	DestroyAtTime.cs
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

public class DestroyAtTime : MonoBehaviour
{
    [Tooltip("The amount of time before the object get destroyed")]
    public float DeathTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DeathTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
