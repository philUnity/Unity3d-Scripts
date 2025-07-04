using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RadioToggle : MonoBehaviour, IInteractable
{
    private AudioSource audioSource;
    private bool isOn = false;
    private float savedTime = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    public void Interact()
    {
        if (isOn)
        {
            savedTime = audioSource.time; // Salva il punto in cui siamo
            audioSource.Stop();
        }
        else
        {
            audioSource.time = savedTime; // Riprendi da lì
            audioSource.Play();
        }

        isOn = !isOn;
    }

    public string GetPromptText()
    {
        return isOn ? "[E] Turn Off Radio" : "[E] Turn On Radio";
    }
}
