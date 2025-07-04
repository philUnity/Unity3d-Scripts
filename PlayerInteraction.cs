// PlayerInteraction.cs
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask interactLayer;
    public Camera playerCamera;

    public TextMeshProUGUI interactionText;

    private IInteractable currentInteractable;

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactionText.text = interactable.GetPromptText();
                interactionText.gameObject.SetActive(true);
                currentInteractable = interactable;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentInteractable.Interact();
                }
                return;
            }
        }

        interactionText.gameObject.SetActive(false);
        currentInteractable = null;
    }
}
