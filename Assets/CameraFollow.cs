using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;        //Public variable to store a reference to the player game object

    private Vector3 offset;          //Private variable to store the offset distance between the player and camera
    // Start is called before the first frame update
    void Start()
    {
        if (player == null) player = this.transform.parent.gameObject;
        this.transform.position = player.transform.position + new Vector3(0,20,-3);
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
        this.transform.rotation = Quaternion.Euler(80, 0, 0);
    }
}
