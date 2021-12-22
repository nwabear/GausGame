using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject player;
    public GameObject camera;
    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y >= 1.5) {
            camera.transform.position = new Vector3(camera.transform.position.x, player.transform.position.y, camera.transform.position.z);
        }
    }
}
