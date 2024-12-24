using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public GameObject welcomeText; // Reference to the initial welcome text
    public GameObject additionalText; // Reference to the additional text you want to show
    public Button learnNowButton; // Reference to the initial "Learn Now" button
    public Button[] showButtons; // Array of buttons you want to show
    public Image[] showImages; // Array of images you want to show

    private void Start()
    {
        // Initially, hide the additional text, buttons, and images
        additionalText.SetActive(false);

        foreach (Button button in showButtons)
        {
            button.gameObject.SetActive(false);
        }

        foreach (Image image in showImages)
        {
            image.gameObject.SetActive(false);
        }
    }

    public void OnButtonClick()
    {
        // Hide the initial "Learn Now" button and welcome text
        learnNowButton.gameObject.SetActive(false);
        welcomeText.gameObject.SetActive(false);

        // Show the additional text, buttons, and images
        additionalText.SetActive(true);

        foreach (Button button in showButtons)
        {
            button.gameObject.SetActive(true);
        }

        foreach (Image image in showImages)
        {
            image.gameObject.SetActive(true);
        }
    }
}