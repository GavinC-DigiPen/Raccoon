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
    [Tooltip("The sprite indicator that warns the player they are being seen")]
    public Sprite warningIndicator;
    [Tooltip("The sprite indicator that tells the player they are caught")]
    public Sprite caughtIndicator;
    [Tooltip("The aduio inidcator that warns the player they are being seen")]
    public AudioClip warningAudio;

    private Rigidbody2D myRB;
    private AudioSource myAud;
    private Animator myAnim;
    private GameObject indicator;

    private bool isRight = false;
    private bool seesRaccoon = false;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAud = GetComponent<AudioSource>(); ;
        myAnim = GetComponent<Animator>();

        indicator = gameObject.transform.GetChild(0).gameObject;

        indicator.SetActive(false);
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
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
        }
    }

    // Check if player is in trigger collider
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!seesRaccoon)
            {
                myAud.PlayOneShot(warningAudio);
            }

            seesRaccoon = true;
            myAnim.SetBool("Moving", false);
            timer += Time.deltaTime;
            if (timer > timeToCapture)
            {
                indicator.GetComponent<SpriteRenderer>().sprite = caughtIndicator;
                indicator.SetActive(true);
                StartCoroutine(EndGame());
            }
            else
            {
                indicator.GetComponent<SpriteRenderer>().sprite = warningIndicator;
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

    // End the game
    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.score = -100;
        SceneManager.LoadScene(sceneName);
    }
}

