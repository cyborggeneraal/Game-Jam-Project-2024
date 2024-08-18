using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameObject canvas;
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CloseGameOver()
    {
        gameObject.SetActive(false);
        canvasScpt.canvas.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
