using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gravity : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;

    GameObject player;
    public float m_Thrust = 70f;
    public float grav_force = 0.0f;
    public float jump;
    public int on_ground = 0;
    public bool moving_right = false;
    public bool moving_left = false;
    public bool test_right = false;
    public bool test_left = false;
    public bool test_up = false;
    public int shootdelay = 0;
    public bool test = false;
    Vector2 force;
    // Start is called before the first frame update
    public void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        player = m_Rigidbody.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xVelocity = 0f;
        moving_right = false;
        moving_left = false;

        if((Input.GetKey(KeyCode.RightArrow) && onGround()) || test_right) {
            moving_right = true;
        }

        if((Input.GetKey(KeyCode.LeftArrow) && onGround()) || test_left) {
            moving_left = true;
        }

        if(moving_right) {
            xVelocity += 3.0f;
        }

        if(moving_left) {
            xVelocity -= 3.0f;
        }
        m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x + xVelocity, m_Rigidbody.velocity.y - 0.25f);
        if(onGround()) {
            m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x / 2, m_Rigidbody.velocity.y);
        }
        if(m_Rigidbody.velocity.x > 3f && shootdelay < 280 && onGround()) {
            m_Rigidbody.velocity = new Vector2(3f, m_Rigidbody.velocity.y);
        }
        if(m_Rigidbody.velocity.x < -3f && shootdelay < 280 && onGround()) {
            m_Rigidbody.velocity = new Vector2(-3f, m_Rigidbody.velocity.y);
        }
        if((Input.GetKey(KeyCode.UpArrow) && onGround()) || test_up) {
            m_Rigidbody.velocity = new Vector2(xVelocity, 0.0f);
            m_Rigidbody.AddForce(jump * transform.up, ForceMode2D.Impulse);
        }

        if(Input.GetKey(KeyCode.Mouse0)) {
            Shoot();
        }

        shootdelay--;
    }

    public bool onGround() {
        return on_ground > 0;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "ground") {
            on_ground++;
        }

        if(collision.gameObject.tag == "slope") {
            moving_left = false;
            moving_right = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.tag == "ground") {
            on_ground--;
        }
    }

    public void Shoot() {
        Vector3 startPos = player.transform.position;
        // Vector3 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Vector3 endPos = -player.transform.up * 10;
        Vector3 endPos;
        if(!test) {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  
        } else {
            endPos = player.transform.up * 10;
        }
        endPos.z = 0;
        // Vector2 heading = endPos - startPos;
        // float distance = heading.magnitude;
        Vector2 direction = (endPos - startPos).normalized;
        Vector2 inverse_direction = (startPos - endPos).normalized;

        // Debug.DrawLine(startPos, direction, Color.black, 10000f);
        // RaycastHit hit;
        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, 10f);
        if(hit) {
            // Debug.Log(hit.transform.name + " was hit!");
            if(shootdelay <= 0) {
                shootdelay = 300;
                moving_left = false;
                moving_right = false;
                Debug.Log("shoot!");
                m_Rigidbody.AddForce(inverse_direction * (30f - hit.distance), ForceMode2D.Impulse);
            }
        }
    }
}
