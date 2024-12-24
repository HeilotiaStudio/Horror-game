using UnityEngine;
using UnityEngine.UI;

public class DisableObjectOnButtonPress : MonoBehaviour
{
    public Button targetButton; // The button to press
    public GameObject targetObject; // The GameObject to disable

    void Start()
    {
        // Ensure the target object is initially enabled
        targetObject.SetActive(false);
        // Add a listener to the button
        targetButton.onClick.AddListener(OnButtonPress);
    }

    void OnButtonPress()
    {
        // Disable the target object
        targetObject.SetActive(false);
    }
}