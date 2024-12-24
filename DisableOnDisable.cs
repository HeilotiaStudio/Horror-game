using UnityEngine;

public class DisableOnDisable : MonoBehaviour
{
    private void OnDisable()
    {
        // Ensure the object is disabled whenever the parent is disabled
        gameObject.SetActive(false);
    }
}
