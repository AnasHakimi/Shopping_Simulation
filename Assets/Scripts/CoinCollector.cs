using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollector : MonoBehaviour
{
    public Text coinText; // UI Text to display coin count
    public GameObject portal; // Reference to the portal GameObject
    public AudioClip coinSound; // The sound effect to play when a coin is collected
    private AudioSource audioSource; // AudioSource to play the sound

    private void Start()
    {
        // Ensure the portal is initially hidden
        if (portal != null)
        {
            portal.SetActive(false);
        }

        // Initialize the coin count and update the UI
        UpdateCoinText();

        // Get or add an AudioSource component if not already attached
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if not attached
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            GlobalCoinData.coinCount++;
            UpdateCoinText();
            Destroy(other.gameObject);

            // Play the coin sound effect
            if (audioSource != null && coinSound != null)
            {
                audioSource.PlayOneShot(coinSound); // Play sound effect when coin is collected
            }
        }
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + GlobalCoinData.coinCount;
        }

        // Check if coin count has reached 20 and make the portal visible
        if (GlobalCoinData.coinCount >= 20 && portal != null)
        {
            portal.SetActive(true);
        }
    }
}
