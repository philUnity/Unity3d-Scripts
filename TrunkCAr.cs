using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TrunkCAr : MonoBehaviour, IInteractable
{
    public float openAngle = 90f;
    public float openSpeed = 3f;
    public bool isOpen = false;

    public AudioClip openSound;
    public AudioClip closeSound;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isMoving = false;
    private AudioSource audioSource;

    private Animator animator; // opzionale

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(openAngle, 0f, 0f));

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>(); // opzionale
    }

    public void Interact()
    {
        if (!isMoving)
            StartCoroutine(ToggleDoor());
    }

    public string GetPromptText()
    {
        return isMoving ? "" : isOpen ? "[E] Close Trunk" : "[E] Open Trunk";
    }

    private System.Collections.IEnumerator ToggleDoor()
    {
        isMoving = true;
        Quaternion targetRotation = isOpen ? closedRotation : openRotation;

        //Play sound
        if (audioSource != null)
        {
            audioSource.clip = isOpen ? closeSound : openSound;
            if (audioSource.clip != null)
                audioSource.Play();
        }

        //Trigger animation (se usi Animator invece di rotazione)
        if (animator != null)
        {
            animator.SetTrigger(isOpen ? "Close" : "Open");
            yield return new WaitForSeconds(0.5f); // aspetta l'animazione
        }
        else
        {
            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
                yield return null;
            }

            transform.rotation = targetRotation;
        }

        isOpen = !isOpen;
        isMoving = false;
    }
}
