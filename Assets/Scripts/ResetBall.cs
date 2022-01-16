using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBall : MonoBehaviour
{
    public int delay_time = 300;
    public int cur_time = -1;
    public SpriteRenderer ball;

    public Gravity player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(cur_time == 0) {
            ball.enabled = true;
        }
        if(cur_time > -1) {
            cur_time--;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        cur_time = delay_time;
        ball.enabled = false;
        player.can_shoot = true;
        player.shootdelay = 0;
    }
}
