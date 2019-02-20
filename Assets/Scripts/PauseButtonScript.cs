using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtonScript : MonoBehaviour
{
    public GameObject PauseButton;
    public GameObject PausePanel;

    public void PauseGame()
    {
        Time.timeScale = 0.0f;

        PauseButton.SetActive(false);
        PausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;

        PausePanel.SetActive(false);
        PauseButton.SetActive(true);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu_screen");
    }
}
