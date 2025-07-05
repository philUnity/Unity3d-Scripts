using UnityEngine;
using TMPro;

public class FoodPickupAndEat : MonoBehaviour, IInteractable
{
    public string foodName = "Pizza";
    public float eatDuration = 3f;
    public AudioClip chewingSound;
    public Transform playerHand;
    public TextMeshProUGUI holdPromptUI; // assegnalo dal canvas

    private bool isHeld = false;
    private bool isEating = false;
    private float eatTimer = 0f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if (chewingSound != null)
        {
            audioSource.clip = chewingSound;
            audioSource.playOnAwake = false;
            audioSource.loop = true;
        }
    }

    public string GetPromptText()
    {
        return isHeld ? "" : "[E] Pick up " + foodName;
    }

    public void Interact()
    {
        if (!isHeld)
            PickUpFood();
    }

    void Update()
    {
        if (!isHeld)
        {
            if (holdPromptUI != null)
                holdPromptUI.gameObject.SetActive(false);
            return;
        }

        // Mostra sempre il prompt mentre tieni il cibo
        if (holdPromptUI != null)
        {
            holdPromptUI.text = "[RBM] Hold to Eat " + foodName;
            holdPromptUI.gameObject.SetActive(true);
        }

        // Mangia tenendo premuto il tasto destro
        if (Input.GetMouseButton(1))
        {
            if (!isEating)
            {
                isEating = true;
                eatTimer = 0f;
                if (chewingSound != null && !audioSource.isPlaying)
                    audioSource.Play();
            }

            eatTimer += Time.deltaTime;

            if (eatTimer >= eatDuration)
                FinishEating();
        }
        else if (isEating)
        {
            isEating = false;
            eatTimer = 0f;
            audioSource.Stop();
        }
    }

    private void PickUpFood()
    {
        isHeld = true;

        if (playerHand != null)
        {
            transform.SetParent(playerHand);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        Collider col = GetComponent<Collider>();
        if (col) col.enabled = false;
    }

    private void FinishEating()
    {
        isHeld = false;
        isEating = false;

        if (audioSource.isPlaying)
            audioSource.Stop();

        if (holdPromptUI != null)
            holdPromptUI.gameObject.SetActive(false);

        Destroy(gameObject);
    }
}
