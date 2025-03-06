using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public string nextSceneName;
    public Text interactionText; 
    private bool isPlayerNearby = false;

    void Start()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false); 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerNearby = true;
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(true); 
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false); 
            }
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E)) 
        {
            SceneManager.LoadScene(nextSceneName); 
        }
    }
}
