using UnityEngine;
using UnityEngine.UI;

public class EnableObjectOnButtonPress : MonoBehaviour
{
    public Button targetButton; // The button to press
    public GameObject targetObject; // The GameObject to enable

    private void OnEnable()
    {
        // Add a listener to the button
        targetButton.onClick.AddListener(OnButtonPress);
    }

    private void OnDisable()
    {
        // Remove the listener to avoid potential memory leaks
        targetButton.onClick.RemoveListener(OnButtonPress);
    }

    void OnButtonPress()
    {
        // Enable the target object if it's not already active
        if (!targetObject.activeSelf)
        {
            targetObject.SetActive(true);
        }
    }
}
