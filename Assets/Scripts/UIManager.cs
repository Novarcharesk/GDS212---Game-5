using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text dialogueText;       // Reference to the Text element for displaying dialogue
    public Button nextButton;       // Reference to the UI button to progress through dialogue
    public string[] dialogues;      // Array of dialogue strings
    private int currentIndex = 0;  // Index to keep track of the current dialogue
    private bool isSequenceComplete = false; // Flag to track if the UI sequence is complete

    private void Start()
    {
        // Check if the UI sequence has already been completed
        if (!isSequenceComplete)
        {
            // Initialize the UI elements and subscribe to the button click event
            dialogueText.text = dialogues[currentIndex];
            nextButton.onClick.AddListener(OnNextButtonClick);
        }
        else
        {
            // Deactivate the UI elements if the sequence is complete
            dialogueText.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
        }
    }

    private void OnNextButtonClick()
    {
        // Check if there's more dialogue to display
        if (currentIndex < dialogues.Length - 1)
        {
            currentIndex++;
            dialogueText.text = dialogues[currentIndex];
        }
        else
        {
            // Mark the sequence as complete and deactivate the UI elements
            isSequenceComplete = true;
            dialogueText.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);

            // You can implement other actions here, e.g., saving the game state.
            Debug.Log("End of dialogue. Sequence complete.");
        }
    }

    // You can add a method to reset the UI sequence when starting a new game
    public void ResetSequence()
    {
        isSequenceComplete = false;
        currentIndex = 0;
        dialogueText.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        dialogueText.text = dialogues[currentIndex];
    }
}