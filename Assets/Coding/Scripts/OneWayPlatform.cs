//------------------------------------------------------------------------------
//
// File Name:	OneWayPlatform.cs
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

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D myEffector;
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        myEffector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") >= 0)
        {
            myEffector.rotationalOffset = 0;
            waitTime = 0.5f;
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            if (waitTime < 0)
            {
                myEffector.rotationalOffset = 180f;
                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
