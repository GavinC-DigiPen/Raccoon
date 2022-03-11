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
    public AudioClip calmMusic;
    public AudioClip anxiousMusicCue;
    public AudioClip wallMusic;
    private AudioSource myAud;
    private bool isPlayingCalm = true;
    private bool isPlayingWall = false;

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

            StartCoroutine(WaitForCue());
        }
        if (GameManager.inWall == true && isPlayingWall == false)
        {
            isPlayingCalm = false;
            isPlayingWall = true;
            myAud.Stop();
            myAud.clip = wallMusic;
            myAud.Play();
        }
        if (GameManager.inWall == false && isPlayingCalm == false)
        {
            isPlayingCalm = true;
            isPlayingWall = false;
            myAud.Stop();
            myAud.clip = calmMusic;
            myAud.Play();
            
            StartCoroutine(WaitForCue());
        }
    }

    //waits for cue to finish
    private IEnumerator WaitForCue()
    {
        yield return new WaitForSeconds(myAud.clip.length);

        myAud.clip = calmMusic;
        myAud.Play();
    }
}
