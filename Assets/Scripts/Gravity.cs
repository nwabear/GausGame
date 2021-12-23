using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gravity : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;

    public GameObject player;
    public GameObject gaus_gun;
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
    public bool shoot_test = false;
    Vector2 force;
    // Start is called before the first frame update
    public void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        // player = m_Rigidbody.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xVelocity = 0f;
        moving_right = false;
        moving_left = false;

        if((Input.GetKey(KeyCode.D) && onGround()) || test_right) {
            moving_right = true;
        }

        if((Input.GetKey(KeyCode.A) && onGround()) || test_left) {
            moving_left = true;
        }

        if(moving_right) {
            xVelocity += 6.0f;
        }

        if(moving_left) {
            xVelocity -= 6.0f;
        }
        m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x + xVelocity, m_Rigidbody.velocity.y - 0.25f);
        if(onGround()) {
            m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x / 2, m_Rigidbody.velocity.y);
        }
        if((m_Rigidbody.velocity.x > 6f && shootdelay < 180 && onGround()) || test) {
            m_Rigidbody.velocity = new Vector2(3f, m_Rigidbody.velocity.y);
        }
        if((m_Rigidbody.velocity.x < -6f && shootdelay < 180 && onGround()) || test) {
            m_Rigidbody.velocity = new Vector2(-3f, m_Rigidbody.velocity.y);
        }
        if((Input.GetKey(KeyCode.W) && onGround()) || test_up) {
            m_Rigidbody.velocity = new Vector2(xVelocity, 0.0f);
            m_Rigidbody.AddForce(jump * transform.up, ForceMode2D.Impulse);
            shootdelay = 50;
        }

        if(Input.GetKey(KeyCode.Mouse0) || shoot_test) {
            Shoot();
        }

        if(gaus_gun.scene.IsValid()) {
            RotateGun();
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

    void RotateGun() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;
        Vector3 objectPos;
        if(!test) {
            objectPos = Camera.main.WorldToScreenPoint(gaus_gun.transform.position);
        } else {
            objectPos = player.transform.up * 10;
        }
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        gaus_gun.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -1f);
        gaus_gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if(angle > 90 || angle < -90) {
            gaus_gun.transform.localScale = new Vector3(-0.13f, -0.1f, 1f);
        } else {
            gaus_gun.transform.localScale = new Vector3(-0.13f, 0.1f, 1f);
        }
    }

    public void Shoot() {
        // Vector3 startPos = player.transform.position;
        Vector3 startPos = player.transform.position;
        // startPos = new Vector3(startPos.x - 0.4f, startPos.y + 0.6f, startPos.z);
        // Debug.DrawLine(startPos, startPos * 2, Color.blue, 100f);
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
        // Debug.DrawLine(startPos, endPos, Color.black, 100f);
        Vector2 direction = (endPos - startPos).normalized;
        Vector2 inverse_direction = (startPos - endPos).normalized;

        // Debug.DrawLine(startPos, direction, Color.black, 10000f);
        // RaycastHit hit;
        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, 10f);
        if(hit) {
            // Debug.Log(hit.transform.name + " was hit!");
            if(shootdelay <= 0 && onGround()) {
                shootdelay = 200;
                moving_left = false;
                moving_right = false;
                Debug.Log("shoot! : " + hit.distance);
                float power = 0f;
                if(hit.distance < 4) {
                    power = 20f - (hit.distance * 2);
                }
                // m_Rigidbody.AddForce(inverse_direction * (20f - hit.distance), ForceMode2D.Impulse);
                m_Rigidbody.AddForce(inverse_direction * (power), ForceMode2D.Impulse);
            }
        }
    }
}
