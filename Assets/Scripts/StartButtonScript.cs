using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    public GameObject LoadingScreen;
    public GameObject MainMenuScreen;
    public GameObject SelectLevelScreen;
    public int Level;

    private void Start()
    {
        LoadingScreen.SetActive(false);
    }

    public void StartGame()
    {
        LoadingScreen.SetActive(true);
        SceneManager.LoadScene("Level_01");
    }

    public void SelectLevel()
    {
        MainMenuScreen.SetActive(false);
        SelectLevelScreen.SetActive(true);
    }

    public void BackToMainMenu()
    {
        SelectLevelScreen.SetActive(false);
        MainMenuScreen.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void StartLevel()
    {
        LoadingScreen.SetActive(true);
        SceneManager.LoadScene("Level_0" + Level);
    }
}
