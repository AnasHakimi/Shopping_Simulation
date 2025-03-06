using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main Menu Canvas")]
    public GameObject mainMenuCanvas; // Assign the main menu Canvas GameObject in the Inspector

    private bool isMenuOpen = false;

    void Update()
    {
        // Toggle the main menu when Escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMainMenu();
        }
    }

    private void ToggleMainMenu()
    {
        if (mainMenuCanvas == null)
        {
            Debug.LogWarning("Main Menu Canvas is not assigned in the Inspector!");
            return;
        }

        isMenuOpen = !isMenuOpen;
        UpdateMenuState();
    }

    private void UpdateMenuState()
    {
        mainMenuCanvas.SetActive(isMenuOpen);

        Time.timeScale = isMenuOpen ? 0f : 1f;
        Cursor.lockState = isMenuOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isMenuOpen;

        Debug.Log($"Main Menu Active: {isMenuOpen}, Time Scale: {Time.timeScale}");
    }

    public void ResumeGame()
    {
        if (!isMenuOpen) return;

        isMenuOpen = false;
        UpdateMenuState();
        Debug.Log("Game Resumed");
    }

    public void ExitToMainMenu()
    {
        GlobalCoinData.coinCount = 0;
        Time.timeScale = 1f;
        Debug.Log("Exiting to Main Menu Scene...");
        SceneManager.LoadScene(0);
    }
}
