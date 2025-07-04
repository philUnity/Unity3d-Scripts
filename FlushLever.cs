using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FlushLever : MonoBehaviour, IInteractable
{
    public AudioClip flushSound;

    private AudioSource audioSource;
    private bool isPlaying = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        if (!isPlaying && flushSound != null)
        {
            audioSource.clip = flushSound;
            audioSource.Play();
            StartCoroutine(ResetAfterFlush());
        }
    }

    public string GetPromptText()
    {
        return isPlaying ? "" : "[E] Flush";
    }

    private System.Collections.IEnumerator ResetAfterFlush()
    {
        isPlaying = true;
        yield return new WaitForSeconds(audioSource.clip.length);
        isPlaying = false;
    }
}
