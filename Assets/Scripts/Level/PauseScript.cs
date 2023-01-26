using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseCanvas;

    private bool isPaused = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isPaused)
        {
            EnterPause();
        } else if(Input.GetKeyDown(KeyCode.P) && isPaused)
        {
            ExitPause();
        }
    }

    private void EnterPause()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void ExitPause()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }
}
