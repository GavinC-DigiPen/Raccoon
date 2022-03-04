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
    private bool inWall = false;
    private Tilemap tileSet;
    private Color defualtGridColor;
    private Color defualtEnemyColor;
    private Color transparentGridColor;
    private Color transparentEnemyColor;
    private float timer = 0;
    public float countdown = 1;
    private bool haveSet = false;


    // Start is called before the first frame update
    void Start()
    {
        wall.SetActive(false);
        
    }


    private void Update()
    {
        timer += Time.deltaTime;

        if (haveSet == false)
        {
            for (int i = 0; i < layer1.transform.childCount; ++i)
            {

                if (layer1.transform.GetChild(i).gameObject.CompareTag("Grid"))
                {
                    grid = layer1.gameObject.transform.GetChild(i).gameObject;

                    for (int j = 0; j < grid.transform.childCount; ++j)
                    {
                        //sets the loaction of grid, and its colors
                        defualtGridColor = grid.transform.GetChild(j).GetComponent<Tilemap>().color;
                        transparentGridColor = new Color(defualtGridColor.r, defualtGridColor.g, defualtGridColor.b, 0.3f);
                    }
                }

                //if child of parent(layer1) is an enemy
                if (layer1.transform.GetChild(i).gameObject.CompareTag("Enemy"))
                {
                    //sets the loaction of enemy and its colors
                    enemy = layer1.gameObject.transform.GetChild(i).gameObject;
                    defualtEnemyColor = enemy.GetComponent<SpriteRenderer>().color;
                    transparentEnemyColor = new Color(defualtEnemyColor.r, defualtEnemyColor.g, defualtEnemyColor.b, 0.3f);

                }
            }
            haveSet = true;
        }
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
                GameManager.inWall = inWall;

                //loops through all children inside parent(layer1)
                for (int i = 0; i < layer1.transform.childCount; ++i)
                {
                    //checks if child is a Grid
                    if (layer1.transform.GetChild(i).gameObject.CompareTag("Grid"))
                    {
                        //sets the loaction of grid, and its colors
                        


                        //if it is a Grid then change the Alpha value on each tilemap within grid
                        for (int j = 0; j < layer1.transform.GetChild(i).transform.childCount; ++j)
                        {
                            grid = layer1.gameObject.transform.GetChild(i).gameObject;

                            if (!inWall)
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

                        if (!inWall)
                        {
                            enemy.GetComponent<SpriteRenderer>().color = defualtEnemyColor;
                            enemy.GetComponent<BoxCollider2D>().enabled = true;
                            enemy.GetComponent<PolygonCollider2D>().enabled = true;
                        }
                        else
                        {
                            enemy.GetComponent<SpriteRenderer>().color = transparentEnemyColor;
                            enemy.GetComponent<BoxCollider2D>().enabled = false;
                            enemy.GetComponent<PolygonCollider2D>().enabled = false;
                        }
                    }

                    //collectables
                    Collectible[] collectables = FindObjectsOfType<Collectible>();
                    for (int j = 0; j < collectables.Length; j++)
                    {
                        if (!inWall)
                        {
                            Color CC = collectables[j].gameObject.GetComponent<SpriteRenderer>().color;
                            CC = new Color(CC.r, CC.g, CC.b, 0.3f);
                            collectables[j].gameObject.GetComponent<BoxCollider2D>().enabled = true;
                        }
                        else
                        {
                            Color CC = collectables[i].gameObject.GetComponent<SpriteRenderer>().color;
                            CC = new Color(CC.r, CC.g, CC.b, 1);
                            collectables[j].gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        }
                    }
                }
                timer = 0;
            }
        }
    }
}