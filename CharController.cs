using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

    public float walkSpeed = 7.0f;
    public float sprintSpeed = 10.0f;
    public float crouchSpeed = 3.5f;
    public float jumpHeight = 2.0f; 
    public float sensitivity = 30.0f;
    public float headBobAmount = 0.05f;  // Head bobbing amount
    public float fallDamageThreshold = 10.0f; // Threshold for fall damage

    private float speed;
    CharacterController character;
    public GameObject cam;
    float moveFB, moveLR;
    float rotX, rotY;
    private float gravity = -9.8f;
    private Vector3 velocity;
    private Vector3 originalCamPosition;
    private float fallDistance; // Distance fallen before hitting ground

    void Start(){
        character = GetComponent<CharacterController>();
        originalCamPosition = cam.transform.localPosition; // Store original camera position for head bobbing

        if (Application.isEditor) {
            sensitivity *= 1.5f;
        }
    }

    void Update(){
        // Head bobbing when walking
        if (character.isGrounded && (moveFB != 0 || moveLR != 0)) {
            HeadBob();
        } else {
            ResetHeadBob();
        }

        // Determine speed based on sprinting or walking
        if (Input.GetKey(KeyCode.LeftShift)) {
            speed = sprintSpeed;
        } else {
            speed = walkSpeed;
        }

        // Handle crouching
        if (Input.GetKey(KeyCode.LeftControl)) {
            Crouch();
        } else {
            StandUp();
        }

        moveFB = Input.GetAxis("Horizontal") * speed;
        moveLR = Input.GetAxis("Vertical") * speed;

        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY = Input.GetAxis("Mouse Y") * sensitivity;

        Vector3 movement = new Vector3(moveFB, 0, moveLR);
        movement = transform.rotation * movement;

        // Handle jumping and fall damage
        if (character.isGrounded) {
            if (fallDistance > fallDamageThreshold) {
                ApplyFallDamage();
            }
            fallDistance = 0f; // Reset fall distance

            velocity.y = 0f; // Reset velocity when grounded

            if (Input.GetButtonDown("Jump")) {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        } else {
            fallDistance += Mathf.Abs(velocity.y) * Time.deltaTime; // Track falling distance
        }

        velocity.y += gravity * Time.deltaTime; // Apply gravity
        movement.y = velocity.y;

        character.Move(movement * Time.deltaTime);
    }

    void CameraRotation(GameObject cam, float rotX, float rotY){       
        transform.Rotate(0, rotX * Time.deltaTime, 0);
        cam.transform.Rotate(-rotY * Time.deltaTime, 0, 0);
    }

    void Crouch() {
        character.height = 1.0f;
        speed = crouchSpeed;
    }

    void StandUp() {
        character.height = 2.0f;
    }

    void HeadBob() {
        float bobOffset = Mathf.Sin(Time.time * walkSpeed) * headBobAmount;
        cam.transform.localPosition = originalCamPosition + new Vector3(0, bobOffset, 0);
    }

    void ResetHeadBob() {
        cam.transform.localPosition = originalCamPosition;
    }

    void ApplyFallDamage() {
        Debug.Log("Fall damage applied!");
        // Add your fall damage logic here (e.g., reducing health)
    }
}
