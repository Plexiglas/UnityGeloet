using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    // Handling
    public float walkSpeed = 5;
    public float runSpeed = 8;

    // System
    private Quaternion targetRotation;

    // Components
    private CharacterController controller;
    private Camera cam;
    private bool jumpPressed = false;
    public Gun gun;

    void Start () {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        input.y = (Input.GetButton("Jump")) ? 20 : 0;
        controller.Move(input * Time.deltaTime);
    }
    
	
	void Update () {
        //controlWASD();

        controlMouse();
        
        if(Input.GetButtonDown("Shoot"))
        {
            gun.Shoot();
        }
        else if(Input.GetButton("Shoot"))
        {
            gun.ShootAuto();
        }
	}

    void controlMouse()
    {
        // Look
        Vector3 mousePos = Input.mousePosition;
        mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
        
        targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x, 0, transform.position.z));
        transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, 4500 * Time.deltaTime);
        

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        // Movement
        Vector3 motion = input;
        // Quakehack (strafe increase etc)
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7F : 1;
        // Sprinting
        motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
        // Jumping
        motion.y = (Input.GetButton("Jump")) ? 20 : 0;
        /*
        if (Input.GetButton("Jump") && !jumpPressed)
        {
            jumpPressed = true;
            motion.y = 20;
            controller.Move(motion * Time.deltaTime);
            if (transform.position == )
                jumpPressed = false;
        }
        */
        // Quick gravity, always pulls down
        motion += Vector3.up * -10;

        controller.Move(motion * Time.deltaTime);
    }
    
}
