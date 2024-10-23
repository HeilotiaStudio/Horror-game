using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

    public float walkSpeed = 7.0f;
    public float sprintSpeed = 10.0f;
    public float crouchSpeed = 3.5f;
    public float jumpHeight = 2.0f; // Height of the jump
    public float sensitivity = 30.0f;
    private float speed;
    CharacterController character;
    public GameObject cam;
    float moveFB, moveLR;
    float rotX, rotY;
    public bool webGLRightClickRotation = true;
    private float gravity = -9.8f;
    private Vector3 velocity;
    public float originalHeight = 2.0f;  // Default height
    public Vector3 originalCenter = new Vector3(0, 1, 0);  // Default center
    public float crouchHeight = 1.0f;  // Crouched height
    public Vector3 crouchCenter = new Vector3(0, 0.5f, 0);  // Crouched center
    public float ventHidingHeight = 1.0f; // Distance to move up when hiding in a vent
    public float ventAscendSpeed = 0.5f; // Speed of ascending into the vent
    private bool isInVent = false; // Track if player is in a vent
    private bool isHidingInVent = false; // Track if player is hiding in a vent

    void Start(){
        character = GetComponent<CharacterController>();

        // Set initial values if not set in the inspector
        originalHeight = character.height;
        originalCenter = character.center;

        if (Application.isEditor) {
            webGLRightClickRotation = false;
            sensitivity = sensitivity * 1.5f;
        }
    }

    void Update(){
        if (isHidingInVent) {
            // If player is hiding in the vent, only exit hiding if any movement key is pressed
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
                ExitVentHiding();
            }
            return; // Skip the rest of the update logic while hiding
        }

        // Determine speed based on sprinting or walking
        if (Input.GetKey(KeyCode.LeftShift)) {
            speed = sprintSpeed;
        } else {
            speed = walkSpeed;
        }

        // Determine if crouching
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

        // Handle jumping
        if (character.isGrounded) {
            velocity.y = 0f; // Reset velocity when grounded

            if (Input.GetButtonDown("Jump")) {
                if (isInVent) {
                    // Hide in vent by moving up
                    StartCoroutine(HideInVent());
                } else {
                    // Normal jump
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
            }
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
        character.height = crouchHeight;
        character.center = crouchCenter;
        speed = crouchSpeed;
    }

    void StandUp() {
        character.height = originalHeight;
        character.center = originalCenter;
    }

    // Vent hiding logic
    IEnumerator HideInVent() {
        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = originalPosition + new Vector3(0, ventHidingHeight, 0);

        float elapsedTime = 0f;

        while (elapsedTime < ventAscendSpeed) {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / ventAscendSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isHidingInVent = true; // Stay hidden until player presses a movement key
    }

    // Exit hiding mode when a movement key is pressed
    void ExitVentHiding() {
        isHidingInVent = false;
    }

    // Detect if player enters or exits a vent
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Vent")) {
            isInVent = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Vent")) {
            isInVent = false;
        }
    }
}
