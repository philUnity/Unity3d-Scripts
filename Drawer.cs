using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Drawer : MonoBehaviour, IInteractable
{
    public Vector3 openOffset = new Vector3(0.2f, 0f, 0f);
    public float moveSpeed = 2f;

    public AudioClip openSound;
    public AudioClip closeSound;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;
    private bool isMoving = false;

    private AudioSource audioSource;

    void Start()
    {
        closedPosition = transform.localPosition;
        openPosition = closedPosition + openOffset;

        audioSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        if (!isMoving)
            StartCoroutine(MoveDrawer());
    }

    public string GetPromptText()
    {
        return isMoving ? "" : isOpen ? "[E] Close drawer" : "[E] Open drawer";
    }

    private System.Collections.IEnumerator MoveDrawer()
    {
        isMoving = true;

        Vector3 targetPosition = isOpen ? closedPosition : openPosition;

        //Play sound
        if (audioSource != null)
        {
            audioSource.clip = isOpen ? closeSound : openSound;
            if (audioSource.clip != null)
                audioSource.Play();
        }

        while (Vector3.Distance(transform.localPosition, targetPosition) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * moveSpeed);
            yield return null;
        }

        transform.localPosition = targetPosition;
        isOpen = !isOpen;
        isMoving = false;
    }
}
