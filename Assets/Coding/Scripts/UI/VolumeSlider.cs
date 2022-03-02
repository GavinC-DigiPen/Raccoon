//------------------------------------------------------------------------------
//
// File Name:	VolumeSlider.cs
// Author(s):	Gavin Cooper (gavin.cooper@digipen.edu)
// Project:	    Empty-Jar Drill Press
// Course:	    WANIC VGP2
//
// Copyright © 2022 DigiPen (USA) Corporation.
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [Tooltip("The audio mixer")]
    public AudioMixer mixer;
    [Tooltip("The name of the mixer being effected")]
    public string mixerName;

    private Slider slider;

    private float currentVolume;

    // Start is called before the first frame update
    void Start()
    {
        mixer.GetFloat(mixerName, out currentVolume);

        slider = GetComponent<Slider>();
        slider.value = Mathf.Pow(10, currentVolume / 20);
    }

    //Function to set volume
    public void SetVolume(float volume)
    {
        mixer.SetFloat(mixerName, Mathf.Log10(volume) * 20);
    }
}