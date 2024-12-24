using UnityEngine;

public class QuitButton : MonoBehaviour
{
    // This method will be called when the button is clicked
    public void QuitApplication()
    {
        // This line quits the application
        Application.Quit();

        // Note: The application won't quit when running in the Unity Editor
    }
}