using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingMaster : MonoBehaviour {

    public float ventHidingHeight = 1.0f;  // Distance to move up when hiding in a vent
    public float ventAscendSpeed = 0.5f;   // Speed of ascending into the vent
    public float ventDescendSpeed = 0.5f;  // Speed of descending from the vent
    private bool isInVent = false;         // Whether the player is near or in a vent
    private bool isHidingInVent = false;   // Whether the player is currently hidden in a vent
    private bool isAtVentTop = false;      // Whether the player is at the top of the vent

    private CharacterController characterController;
    private Vector3 velocity = Vector3.zero;
    private float gravity = -9.8f;

    void Start() {
        characterController = GetComponent<CharacterController>();
    }

    void Update() {
        // Trigger climbing when the space bar is pressed (for ascending)
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isInVent && !isHidingInVent && !isAtVentTop) {
                StartCoroutine(HideInVent());  // Ascend
            }
        }

        // Only descend when the "E" key is pressed
        if (isAtVentTop && Input.GetKeyDown(KeyCode.E)) {
            StartCoroutine(ExitVentHiding());  // Descend
        }

        // Disable gravity and movement when at the top
        if (!isAtVentTop) {
            ApplyGravity();
        }
    }

    // Vent hiding logic for ascending
    public IEnumerator HideInVent() {
        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = originalPosition + new Vector3(0, ventHidingHeight, 0);

        float elapsedTime = 0f;
        while (elapsedTime < ventAscendSpeed) {
            Vector3 newPos = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / ventAscendSpeed));
            characterController.Move(newPos - transform.position);  // Use Move to ascend
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isHidingInVent = true; // Mark the player as hidden in the vent
        isAtVentTop = true;    // Mark the player as at the top of the vent
        velocity = Vector3.zero; // Stop velocity to prevent falling
        characterController.enabled = false;  // Disable character movement while at the top
    }

    // Exit hiding mode by descending from the vent
    public IEnumerator ExitVentHiding() {
        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = originalPosition - new Vector3(0, ventHidingHeight, 0);

        characterController.enabled = true;  // Re-enable character movement before descending

        float elapsedTime = 0f;
        while (elapsedTime < ventDescendSpeed) {
            Vector3 newPos = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / ventDescendSpeed));
            characterController.Move(newPos - transform.position);  // Use Move to descend
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isHidingInVent = false; // Mark the player as no longer hidden in the vent
        isAtVentTop = false;    // Mark the player as not at the top
    }

    // Detect if the player is near or enters a vent
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Vent")) {
            isInVent = true;
        }
    }

    // Detect if the player exits a vent area
    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Vent")) {
            isInVent = false;
        }
    }

    // Apply gravity when not climbing or at the top
    void ApplyGravity() {
        if (characterController.isGrounded) {
            velocity.y = 0f;  // Reset velocity when grounded
        } else {
            velocity.y += gravity * Time.deltaTime;  // Apply gravity
        }
        characterController.Move(velocity * Time.deltaTime);
    }
}
