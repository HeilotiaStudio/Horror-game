using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour
{
    public Animator openandclose;
    public bool open;
    public Transform Player;
    public float interactionDistance = 3.0f; // Distance at which the player can interact with the door

    void Start()
    {
        open = false;
    }

    void Update()
    {
        float dist = Vector3.Distance(Player.position, transform.position);

        if (dist < interactionDistance && Input.GetMouseButtonDown(0))
        {
            ToggleDoor();
        }
    }

    void ToggleDoor()
    {
        if (open)
        {
            StartCoroutine(closing());
        }
        else
        {
            StartCoroutine(opening());
        }
    }

    IEnumerator opening()
    {
        print("you are opening the door");
        openandclose.Play("Opening");
        open = true;
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator closing()
    {
        print("you are closing the door");
        openandclose.Play("Closing");
        open = false;
        yield return new WaitForSeconds(.5f);
    }
}
