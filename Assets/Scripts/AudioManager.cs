using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip helicopterSound;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioSource audioSource;
    public static AudioManager Instance { get; private set; }
    
    private void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: keep it between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip); 
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public void PlayHelicopter()
    {
        if (!audioSource.isPlaying)
        {
            PlaySound(helicopterSound);
        }
    }

    public void PlayExplosion()
    {
        PlaySound(explosionSound);
    }
    
}
    
