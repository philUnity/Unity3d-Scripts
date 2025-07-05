using UnityEngine;

public class ReadableNote : MonoBehaviour, IInteractable
{
    public GameObject noteUIPanel;       // UI panel
    public GameObject playerMovement;    // script that manages the player movement (optional)   

    private bool isReading = false;

    void Start()
    {
        if (noteUIPanel != null)
            noteUIPanel.SetActive(false);
    }

    public string GetPromptText()
    {
        return isReading ? "[E] Put Back Note" : "[E] Read Note";
    }

    public void Interact()
    {
        if (!isReading)
        {
            ShowNote();
        }
        else
        {
            HideNote();
        }
    }

    private void ShowNote()
    {
        if (noteUIPanel != null)
            noteUIPanel.SetActive(true);

        if (playerMovement != null)
            playerMovement.SetActive(false); // block movement

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isReading = true;
    }

    private void HideNote()
    {
        if (noteUIPanel != null)
            noteUIPanel.SetActive(false);

        if (playerMovement != null)
            playerMovement.SetActive(true); // riattiva movimento

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isReading = false;
    }
}
