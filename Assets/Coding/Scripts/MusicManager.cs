/*********************************
 * By: Andrew kitzan
 * last edit: 3/3/2022
 * desc: plays the music in the game
 * ******************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip calmMusicCue;
    public AudioClip calmMusic;
    public AudioClip anxiousMusicCue;
    private AudioSource myAud;

    // Start is called before the first frame update
    void Start()
    {
        myAud = GetComponent<AudioSource>();
        myAud.clip = calmMusic;
        myAud.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.raccoonSeen && myAud.clip.name == calmMusic.name)
        {
            myAud.Stop();
            myAud.clip = anxiousMusicCue;
            myAud.Play();

            StartCoroutine(WaitForMusic());
        }
    }

    //waits for music to finish
    private IEnumerator WaitForMusic()
    {
        yield return new WaitForSeconds(myAud.clip.length);

        myAud.clip = calmMusicCue;
        myAud.Play();

        StartCoroutine(waitForCue());
    }

    //waits for cue to finish
    private IEnumerator waitForCue()
    {
        yield return new WaitForSeconds(myAud.clip.length);

        myAud.clip = calmMusic;
        myAud.Play();
    }
}
