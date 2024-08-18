using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void CloseGameOver()
    {
        Object.Destroy(gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
