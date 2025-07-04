using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class TVToggle : MonoBehaviour, IInteractable
{
    public Renderer screenRenderer; // Schermo della TV (dove va il video)
    public Material offMaterial;    // Materiale da mostrare a TV spenta
    public Material videoMaterial;  // Materiale con texture del video

    private VideoPlayer videoPlayer;
    private bool isOn = false;
    private double savedTime = 0;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = true;

        if (screenRenderer != null && offMaterial != null)
            screenRenderer.material = offMaterial;
    }

    public void Interact()
    {
        if (isOn)
        {
            savedTime = videoPlayer.time;
            videoPlayer.Pause();
            if (screenRenderer && offMaterial)
                screenRenderer.material = offMaterial;
        }
        else
        {
            videoPlayer.time = savedTime;
            videoPlayer.Play();
            if (screenRenderer && videoMaterial)
                screenRenderer.material = videoMaterial;
        }

        isOn = !isOn;
    }

    public string GetPromptText()
    {
        return isOn ? "[E] Turn OFF TV" : "[E] Turn ON TV";
    }
}
