using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundMusicManager : MonoBehaviour
{
    public AudioClip backgroundMusic; 
    private AudioSource audioSource; 

    private void Awake()
    {
        
        if (FindObjectsOfType<BackgroundMusicManager>().Length > 1)
        {
            Destroy(gameObject); 
        }
        else
        {
            
            DontDestroyOnLoad(gameObject);

           
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>(); 
            }

            
            audioSource.clip = backgroundMusic;
            audioSource.loop = true; 
            audioSource.Play();
        }
    }
}
