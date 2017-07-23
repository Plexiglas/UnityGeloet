using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {
    
    public float cameraHeight = 18;
    

    private Transform player;
    private Camera cam;

    private Vector3 playerPos;
    private Vector3 mousePos;
    private Vector3 targetPos;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        // Mouse position in world space, z is not interesting, Vector2 -> Vector3
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraHeight));
        
        // Player position in world space
        playerPos = new Vector3(player.position.x, transform.position.y, player.position.z);

        targetPos = new Vector3((mousePos.x + 3 * playerPos.x) / 4, cameraHeight, (mousePos.z + 3 * playerPos.z) / 4);

        transform.position = targetPos;

        // Camera on player
        //transform.position = playerPos;
    }
    
}
