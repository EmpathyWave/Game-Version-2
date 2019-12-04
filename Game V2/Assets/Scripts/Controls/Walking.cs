using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
//janky ass system to get walking working in the game
//please help me
{

    public float maxSpeed;
    public float minSpeed;
    public float speed;
    public float scale;

    public GameObject global;
    public GameObject map;
    public Rigidbody2D rb;

    void Start()
    {
        speed = minSpeed;
        rb = GetComponent<Rigidbody2D>();
        global = GameObject.Find("Game Manager");
        global = GameObject.Find("Map Manager");
        
    }


    void FixedUpdate()
    {
        if (global.GetComponent<Global>().currentGS == Global.GameState.Walking)
        {
            Controls();
            Check();
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void Check()
    {
        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }

        if (speed < minSpeed)
        {
            speed = minSpeed;
        }
    }

    void Controls()
    {
        if (Input.GetKey(KeyCode.A)) //left
        {
            //speed += scale;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.velocity = transform.TransformVector(-1,0,0) * speed;
        } 
        else if (Input.GetKey(KeyCode.D)) //right
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.velocity = transform.TransformVector(1, 0, 0) * speed;
            //speed += scale;
            //rb.velocity = Vector2.right * speed * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.M))
        {
            global.GetComponent<Global>().currentGS = Global.GameState.MViewing;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if(Input.GetKeyUp(KeyCode.A))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            global.GetComponent<Global>().currentGS = Global.GameState.MViewing;
            map.GetComponent<MapController>().current_char = other.tag;
        }
    }
}

