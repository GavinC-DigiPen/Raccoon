//------------------------------------------------------------------------------
//
// File Name:	Collectible.cs
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

[System.Serializable]
public struct FoodItems
{
    [Tooltip("The sprite")]
    public Sprite foodSprite;
    [Tooltip("The collection sound")]
    public AudioClip foodPickUpNoise;
};

public class Collectible : MonoBehaviour
{
    [Tooltip("The number of points gained from picking up the food")]
    public int points = 1;
    [Tooltip("The different foods")]
    public FoodItems[] food;
    [Tooltip("The gameobject that will be summoned when collected")]
    public GameObject SpawnOnPickUp;

    private int activeIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        activeIndex = Random.Range(0, food.Length);
        GetComponent<SpriteRenderer>().sprite = food[activeIndex].foodSprite;
    }

    // When collectible collides with object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.score += points;
            AudioSource PAud = collision.gameObject.GetComponent<AudioSource>();
            if(PAud != null)
            {
                PAud.PlayOneShot(food[activeIndex].foodPickUpNoise);
            }
            if(SpawnOnPickUp != null)
            {
                Instantiate(SpawnOnPickUp, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
