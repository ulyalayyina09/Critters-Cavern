using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public static InGameMenu instance;
    
    [Header("UI Panels")]
    public GameObject pausePanel;
    public GameObject minimapPanel;

    private bool isPaused = false;
    private bool isMinimapOpen = false;

    void Awake()
    {
        // Setup Singleton
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        // 1. Logika Pause (Tombol Space)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }

        // 2. Logika Minimap (Tombol M)
        // Minimap hanya bisa dibuka kalau game LAGI TIDAK PAUSE
        if (Input.GetKeyDown(KeyCode.M) && !isPaused)
        {
            ToggleMinimap();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f; // Menghentikan seluruh waktu/pergerakan di game
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f; // Menjalankan kembali waktu game
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // PENTING: Waktu harus di-reset ke 1 sebelum pindah scene!
        SceneManager.LoadScene("MainMenu"); // Ganti dengan nama Scene Main Menu kamu
    }

    // --- FUNGSI MINIMAP ---
    public void ToggleMinimap()
    {
        isMinimapOpen = !isMinimapOpen;
        minimapPanel.SetActive(isMinimapOpen);
    }
}
