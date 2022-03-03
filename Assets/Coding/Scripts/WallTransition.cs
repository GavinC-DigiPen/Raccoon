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
    private Color defualtGridColor;
    private Color defualtEnemyColor;
    private Color transparentGridColor;
    private Color transparentEnemyColor;
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
                        //sets the loaction of grid, and its colors
                        grid = layer1.gameObject.transform.GetChild(i).gameObject;
                        defualtGridColor = grid.GetComponent<Tilemap>().color;
                        transparentGridColor = new Color(defualtGridColor.r, defualtGridColor.g, defualtGridColor.b, 0.3f);


                        //if it is a Grid then change the Alpha value on each tilemap within grid
                        for (int j = 0; j < grid.transform.childCount; ++j)
                        {
                            if (inWall)
                            {
                                grid.transform.GetChild(j).GetComponent<Tilemap>().color = defualtGridColor;
                                grid.transform.GetChild(j).GetComponent<TilemapCollider2D>().enabled = true;
                            }
                            else
                            {
                                grid.transform.GetChild(j).GetComponent<Tilemap>().color = transparentGridColor;
                                grid.transform.GetChild(j).GetComponent<TilemapCollider2D>().enabled = false;
                            }
                        }
                    }

                    //if child of parent(layer1) is an enemy
                    if (layer1.transform.GetChild(i).gameObject.CompareTag("Enemy"))
                    {
                        //sets the loaction of enemy and its colors
                        enemy = layer1.gameObject.transform.GetChild(i).gameObject;
                        defualtEnemyColor = enemy.GetComponent<SpriteRenderer>().color;
                        transparentEnemyColor = new Color(defualtEnemyColor.r, defualtEnemyColor.g, defualtEnemyColor.b, 0.3f);

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