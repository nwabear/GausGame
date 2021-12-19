using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gravity : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;
    public float m_Thrust = 70f;
    public float grav_force = 0.0f;
    public float jump;
    public bool on_ground = false;
    public bool moving_right = false;
    public bool moving_left = false;
    public bool test_right = false;
    public bool test_left = false;
    public bool test_up = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xVelocity = 0.0f;
        if(on_ground) {
            moving_right = false;
            moving_left = false;
        }

        if((Input.GetKey(KeyCode.RightArrow) && on_ground) || test_right) {
            moving_right = true;
        }

        if((Input.GetKey(KeyCode.LeftArrow) && on_ground) || test_left) {
            moving_left = true;
        }

        if(moving_right) {
            xVelocity += 10.0f;
        }

        if(moving_left) {
            xVelocity -= 10.0f;
        }

        m_Rigidbody.velocity = new Vector2(xVelocity, m_Rigidbody.velocity.y - 0.25f);
        if((Input.GetKey(KeyCode.UpArrow) && on_ground) || test_up) {
            m_Rigidbody.velocity = new Vector2(xVelocity, 0.0f);
            m_Rigidbody.AddForce(jump * transform.up, ForceMode2D.Impulse);
            on_ground = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "ground") {
            on_ground = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.tag == "ground") {
            on_ground = false;
        }
    }
}
