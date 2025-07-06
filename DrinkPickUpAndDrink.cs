using UnityEngine;
using TMPro;

public class DrinkPickUpAndDrink : MonoBehaviour, IInteractable
{
    public string drinkName = "Cola";
    public float drinkDuration = 3f;
    public AudioClip gulpSound;
    public Transform playerHand;
    public TextMeshProUGUI holdPromptUI; // assegnalo dal Canvas

    private bool isHeld = false;
    private bool isDrinking = false;
    private float drinkTimer = 0f;

    private AudioSource audioSource;

    void Start()
    {
        // Aggiungi un AudioSource se non presente
        audioSource = gameObject.AddComponent<AudioSource>();
        if (gulpSound != null)
        {
            audioSource.clip = gulpSound;
            audioSource.playOnAwake = false;
            audioSource.loop = true;
        }
    }

    public string GetPromptText()
    {
        return isHeld ? "" : "[E] Pick up " + drinkName;
    }

    public void Interact()
    {
        if (!isHeld)
            PickUpDrink();
    }

    void Update()
    {
        if (!isHeld)
        {
            if (holdPromptUI != null)
                holdPromptUI.gameObject.SetActive(false);
            return;
        }

        // Mostra prompt mentre bevi
        if (holdPromptUI != null)
        {
            holdPromptUI.text = "[Right Click] Hold to Drink " + drinkName;
            holdPromptUI.gameObject.SetActive(true);
        }

        // Bevi tenendo premuto tasto destro
        if (Input.GetMouseButton(1))
        {
            if (!isDrinking)
            {
                isDrinking = true;
                drinkTimer = 0f;

                if (gulpSound != null && !audioSource.isPlaying)
                    audioSource.Play();
            }

            drinkTimer += Time.deltaTime;

            if (drinkTimer >= drinkDuration)
                FinishDrinking();
        }
        else if (isDrinking)
        {
            isDrinking = false;
            drinkTimer = 0f;
            audioSource.Stop();
        }
    }

    private void PickUpDrink()
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

    private void FinishDrinking()
    {
        isHeld = false;
        isDrinking = false;

        if (audioSource.isPlaying)
            audioSource.Stop();

        if (holdPromptUI != null)
            holdPromptUI.gameObject.SetActive(false);

        Destroy(gameObject); // Scompare dopo aver bevuto
    }
}
