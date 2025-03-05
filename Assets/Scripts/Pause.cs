using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public bool isPaused;
    public void PauseUnpause()
    {
        if (isPaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            isPaused = false;
        }
        else
        {
            isPaused = true;
        }
    }
}
