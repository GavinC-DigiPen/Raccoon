using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransition : MonoBehaviour
{
    //varables
    private KeyCode interact = KeyCode.E;
    public GameObject wall;
    public GameObject layer1;
    private GameObject grid;
    private float timer = 0;
    public float countdown = 1;

    // Start is called before the first frame update
    void Start()
    {
        wall.SetActive(false);
        grid = layer1.gameObject.transform.GetChild(0).gameObject;

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
                for (int i = 0; i < 2; ++i)
                {
                    //grid.gameObject.transform.GetChild(i).gameObject.;
                }
                timer = 0;
            }
        }
    }
}
