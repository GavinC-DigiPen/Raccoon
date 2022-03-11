//------------------------------------------------------------------------------
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
    [Tooltip("The aduio inidcator that warns the player they are being seen, from the raccoon")]
    public AudioClip raccoonGaspAudio;
    [Tooltip("The audio that plays when you are caught")]
    public AudioClip caughtAudio;
    [Tooltip("The audio for walking")]
    public AudioClip[] walkNoise;

    private Rigidbody2D myRB;
    private AudioSource myAud;
    private Animator myAnim;
    private GameObject indicator;
    private Animator indicatorAnim;

    private bool isRight = false;
    private bool seesRaccoon = false;
    private bool playingAudio = false;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAud = GetComponent<AudioSource>(); ;
        myAnim = GetComponent<Animator>();
        indicator = transform.GetChild(0).gameObject;
        indicatorAnim = indicator.GetComponent<Animator>();
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
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

                Scaler = indicator.transform.localScale;
                Scaler.x = -1;
                indicator.transform.localScale = Scaler;
            }
            else if (transform.position.x > xLocationRight - marginOfError)
            {
                isRight = false;
                Vector3 Scaler = transform.localScale;
                Scaler.x = 1;
                transform.localScale = Scaler;

                Scaler = indicator.transform.localScale;
                Scaler.x = 1;
                indicator.transform.localScale = Scaler;
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
                myAud.PlayOneShot(raccoonGaspAudio);
            }

            seesRaccoon = true;
            indicatorAnim.SetBool("Seen", true);
            myAnim.SetBool("Moving", false);
            timer += Time.deltaTime;
            if (timer > timeToCapture)
            {
                myAud.PlayOneShot(caughtAudio);
                indicatorAnim.SetBool("Caught", true);
                collision.GetComponent<PlayerController>().enabled = false;
                Animator playerAnim = collision.GetComponent<Animator>();
                playerAnim.SetBool("OnGround", true);
                playerAnim.SetBool("Fast", false);
                playerAnim.SetBool("Moving", false);
                StartCoroutine(EndGame());
                timer = -100;
            }
        }
    }

    // Check if player exited the trigger collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            seesRaccoon = false;
            indicatorAnim.SetBool("Seen", false);
            myAnim.SetBool("Moving", true);
            timer = 0;
        }
    }

    // End the game
    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(indicatorAnim.GetCurrentAnimatorClipInfo(0).Length + 0.1f);
        GameManager.score = -100;
        SceneManager.LoadScene(sceneName);
    }

    public void FootStepSound()
    {
        int index = Random.Range(0, walkNoise.Length);
        myAud.PlayOneShot(walkNoise[index]);
    }
}

