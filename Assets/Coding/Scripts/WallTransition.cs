using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class WallTransition : MonoBehaviour
{
    //varables
    private KeyCode interact = KeyCode.E;
    public GameObject wall;
    public GameObject layer1;
    private GameObject grid;
    private GameObject enemy;
    private bool inWall = true;
    private Tilemap tileSet;
    private Color defualtColor = new Color(1, 1, 1, 1);
    private Color defualtEnemyColor = new Color(1, 0.3f, 0.3f, 1);
    private Color transparentColor = new Color(1, 1, 1, 0.3f);
    private Color transparentEnemyColor = new Color(1, 0.3f, 0.3f, 0.3f);
    private float timer = 0;
    public float countdown = 1;


    // Start is called before the first frame update
    void Start()
    {
        wall.SetActive(false);

    }


    private void Update()
    {
        timer += Time.deltaTime;
    }

    //check if the player wants to go in the wall
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKey(interact))
        {
            if (timer > countdown)
            {
                wall.SetActive(!wall.activeInHierarchy);
                inWall = !inWall;

                //loops through all children inside parent(layer1)
                for (int i = 0; i < layer1.transform.childCount; ++i)
                {
                    //checks if child is a Grid
                    if (layer1.transform.GetChild(i).gameObject.CompareTag("Grid"))
                    {
                        //sets the loaction of grid
                        grid = layer1.gameObject.transform.GetChild(i).gameObject;

                        //if it is a Grid then change the Alpha value on each tilemap within grid
                        for (int j = 0; j < grid.transform.childCount; ++j)
                        {
                            if (inWall)
                            {
                                grid.transform.GetChild(j).GetComponent<Tilemap>().color = defualtColor;
                                grid.transform.GetChild(j).GetComponent<TilemapCollider2D>().enabled = true;
                            }
                            else
                            {
                                grid.transform.GetChild(j).GetComponent<Tilemap>().color = transparentColor;
                                grid.transform.GetChild(j).GetComponent<TilemapCollider2D>().enabled = false;
                            }
                        }
                    }

                    //if child of parent(layer1) is an enemy
                    if (layer1.transform.GetChild(i).gameObject.CompareTag("Enemy"))
                    {
                        //sets the loaction of enemy
                        enemy = layer1.gameObject.transform.GetChild(i).gameObject;

                        if (inWall)
                        {
                            enemy.GetComponent<SpriteRenderer>().color = defualtEnemyColor;
                        }
                        else
                        {
                            enemy.GetComponent<SpriteRenderer>().color = transparentEnemyColor;
                        }
                    }
                }
                timer = 0;
            }
        }
    }
}