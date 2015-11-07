using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    // UI audio clips
    public AudioClip hoverButton;
    public AudioClip clickButton;

    // The source to play audio clips through
    private AudioSource audioSource;

    void Awake()
    {
        //Ensures that there is only ever one SoundManager
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //Sets the reference for the audio manager to the one attached to this gameobject
        audioSource = GetComponent<AudioSource>();
    }

    // Called externally. Plays the sound given.
    public void PlaySound(string sound)
    {
        AudioClip clip = null;

        switch (sound)
        {
            case "hover":
                clip = hoverButton;
                break;
            case "click":
                clip = clickButton;
                break;
        }

        if(clip)
            audioSource.PlayOneShot(clip);
    }
}
