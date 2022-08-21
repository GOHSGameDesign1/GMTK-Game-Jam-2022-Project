using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pausePanel;
    public InputAction pauseAction;
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PausePressed(InputAction.CallbackContext context)
    {
        if (!isPaused)
        {
            PauseGame();
        } else
        {
            Resume();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pausePanel.SetActive(false);
    }

    private void OnEnable()
    {
        pauseAction.Enable();
        pauseAction.performed += PausePressed;
    }

    private void OnDisable()
    {
        pauseAction.performed -= PausePressed;
        pauseAction.Disable();
    }
}
