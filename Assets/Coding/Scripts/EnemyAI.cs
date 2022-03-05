﻿//------------------------------------------------------------------------------
//
// File Name:	EnemyAI.cs
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
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    [Tooltip("The left x limit")]
    public float xLocationLeft;
    [Tooltip("The right x limit")]
    public float xLocationRight;
    [Tooltip("The margine of error for being close enough to xLocations")]
    public float marginOfError = 0.25f;
    [Tooltip("The enemies speed")]
    public float speed = 20.0f;
    [Tooltip("Name of the scene you want this script to load when caught")]
    public string sceneName;
    [Tooltip("Time before player is captured")]
    public float timeToCapture = 0.5f;
    [Tooltip("The sprite indicator that tells the player they are caught")]
    public Sprite caughtIndicator;
    [Tooltip("The aduio inidcator that warns the player they are being seen")]
    public AudioClip warningAudio;
    [Tooltip("The aduio inidcator that warns the player they are being seen, from the raccoon")]
    public AudioClip raccoonGaspAudio;
    [Tooltip("The audio that plays when you are caught")]
    public AudioClip caughtAudio;
    [Tooltip("The audio for walking")]
    public AudioClip walk;
    [Tooltip("The audio for walking while in the wall")]
    public AudioClip walkInWall;
    [Tooltip("The audio that automatically playes randomly")]
    public AudioClip[] idleAudio;
    [Tooltip("The time range for how often the idle audio will play")]
    public Vector2 idleAudioTimeRange;

    private Rigidbody2D myRB;
    private AudioSource myAud;
    private Animator myAnim;
    private GameObject indicator;

    private bool isRight = false;
    private bool seesRaccoon = false;
    private bool playingAudio = false;
    private float timer = 0;
    private float audioTime = 0;
    private float audioTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAud = GetComponent<AudioSource>(); ;
        myAnim = GetComponent<Animator>();

        indicator = gameObject.transform.GetChild(0).gameObject;

        indicator.SetActive(false);
        myAud.clip = walkInWall;

        audioTimer = Random.Range(idleAudioTimeRange.x, idleAudioTimeRange.y);
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        myAud.clip = GameManager.inWall ? walkInWall : walk;
        GameManager.raccoonSeen = seesRaccoon;

        if (!seesRaccoon)
        {
            if (isRight)
            {
                myRB.AddForce(Vector2.right * speed);
            }
            else
            {
                myRB.AddForce(Vector2.left * speed);
            }

            if (transform.position.x < xLocationLeft + marginOfError)
            {
                isRight = true;
                Vector3 Scaler = transform.localScale;
                Scaler.x = -1;
                transform.localScale = Scaler;
            }
            else if (transform.position.x > xLocationRight - marginOfError)
            {
                isRight = false;
                Vector3 Scaler = transform.localScale;
                Scaler.x = 1;
                transform.localScale = Scaler;
            }

            if (audioTimer > audioTime && !playingAudio)
            {
                int index = Random.Range(0, idleAudio.Length);
                myAud.PlayOneShot(idleAudio[index]);
                audioTime = Random.Range(idleAudioTimeRange.x, idleAudioTimeRange.y) + idleAudio[index].length;
                audioTimer = 0;
            }
            else
            {
                audioTimer += Time.deltaTime;
            }
        }
    }

    // Check if player is in trigger collider
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!seesRaccoon && !playingAudio)
            {
                playingAudio = true;
                myAud.Stop();
                myAud.Play();
                myAud.PlayOneShot(warningAudio);
                StartCoroutine(RaccoonGaspAudio());
            }

            seesRaccoon = true;
            myAnim.SetBool("Moving", false);
            timer += Time.deltaTime;
            if (timer > timeToCapture)
            {
                indicator.GetComponent<SpriteRenderer>().sprite = caughtIndicator;
                indicator.SetActive(true);
                myAud.PlayOneShot(caughtAudio);
                StartCoroutine(EndGame());
                timer = -100;
            }
            else
            {
                indicator.SetActive(true);
            }
        }
    }

    // Check if player exited the trigger collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            seesRaccoon = false;
            myAnim.SetBool("Moving", true);
            timer = 0;
            indicator.SetActive(false);
        }
    }

    // The audio for the raccoon gasping
    private IEnumerator RaccoonGaspAudio()
    {
        yield return new WaitForSeconds(warningAudio.length + 0.1f);
        myAud.PlayOneShot(raccoonGaspAudio);
        playingAudio = false;
    }

    // End the game
    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(caughtAudio.length);
        GameManager.score = -100;
        SceneManager.LoadScene(sceneName);
    }
}
