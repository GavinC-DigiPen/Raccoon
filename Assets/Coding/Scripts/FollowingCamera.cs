//------------------------------------------------------------------------------
//
// File Name:	FollowCamera.cs
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

public class FollowingCamera : MonoBehaviour
{
    [Tooltip("The tag of the camera's target")]
    public string targetTag = "Player";
    [Tooltip("The speed the camera snaps to the target")]
    public float snapSpeed = 0.5f;
    [Tooltip("The offset of the camera")]
    public Vector2 cameraOffset;
    [Tooltip("The amount of time the camera shakes")]
    public float shakeTime = 0;
    [Tooltip("The magnitude of the shake")]
    public float shakeMagnitude = 0;

    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag(targetTag);
    }

    // Update is called once per frame
    void Update()
    {
        //testing
       /* if(Input.GetKeyDown(KeyCode.G))
        {
            TriggerShake(1, 1);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            TriggerShake(1, 3);
        }*/
    }

    private void FixedUpdate()
    {
        Vector3 newPos = target.transform.position;
        newPos += (Vector3)cameraOffset;
        newPos.z = transform.position.z;

        if(shakeTime > 0)
        {
            newPos += Random.insideUnitSphere.normalized * shakeMagnitude;
            shakeTime -= Time.fixedDeltaTime;
        }
        else
        {
            shakeTime = 0;
            shakeMagnitude = 0;
        }



        transform.position = Vector3.Lerp(transform.position, newPos, snapSpeed);
    }

    public void TriggerShake(float time, float magnitude)
    {
        if(shakeTime < time)
        {
            shakeTime = time;
        }
        if(shakeMagnitude < magnitude)
        {
            shakeMagnitude = magnitude;
        }
    }
}
