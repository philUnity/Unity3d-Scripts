using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FridgeDoor : MonoBehaviour, IInteractable
{
    public float openAngle = 100f;         // How far the door opens
    public float openSpeed = 2f;           // Speed of the door animation
    public bool startOpen = false;         // Optional: start the game with door open

    public AudioClip openSound;
    public AudioClip closeSound;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen;
    private bool isMoving = false;

    private AudioSource audioSource;

    void Start()
    {
        closedRotation = transform.localRotation;
        openRotation = Quaternion.Euler(transform.localEulerAngles + new Vector3(0f, openAngle, 0f));

        isOpen = startOpen;
        if (isOpen)
            transform.localRotation = openRotation;

        audioSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        if (!isMoving)
            StartCoroutine(ToggleDoor());
    }

    public string GetPromptText()
    {
        return isMoving ? "" : isOpen ? "[E] Close fridge" : "[E] Open fridge";
    }

    private System.Collections.IEnumerator ToggleDoor()
    {
        isMoving = true;
        Quaternion targetRotation = isOpen ? closedRotation : openRotation;

        //Sound
        if (audioSource != null)
        {
            audioSource.clip = isOpen ? closeSound : openSound;
            if (audioSource.clip != null)
                audioSource.Play();
        }

        while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.1f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }

        transform.localRotation = targetRotation;
        isOpen = !isOpen;
        isMoving = false;
    }
}
