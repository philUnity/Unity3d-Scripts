using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LightSwitch : MonoBehaviour, IInteractable
{
    [Header("Light and Emission")]
    public Light targetLight;
    public Renderer emissionRenderer;        // Oggetto visivo (lampadina ecc.)
    public Material emissionMaterial;        // Materiale usato dalla lampada
    public Color emissionColor = Color.white;

    [Header("Audio")]
    public AudioClip switchSound;

    private AudioSource audioSource;
    private bool isOn = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (targetLight != null)
            targetLight.enabled = isOn;

        UpdateEmission();
    }

    public void Interact()
    {
        isOn = !isOn;

        if (targetLight != null)
            targetLight.enabled = isOn;

        UpdateEmission();
        PlaySound();
    }

    public string GetPromptText()
    {
        return isOn ? "[E] Turn Light OFF" : "[E] Turn Light ON";
    }

    private void PlaySound()
    {
        if (audioSource != null && switchSound != null)
        {
            audioSource.clip = switchSound;
            audioSource.Play();
        }
    }

    private void UpdateEmission()
    {
        if (emissionRenderer != null && emissionMaterial != null)
        {
            if (isOn)
            {
                emissionMaterial.EnableKeyword("_EMISSION");
                emissionMaterial.SetColor("_EmissionColor", emissionColor);
            }
            else
            {
                emissionMaterial.SetColor("_EmissionColor", Color.black);
            }
        }
    }
}
