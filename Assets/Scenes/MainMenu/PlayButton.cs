using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void playButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void howtoplayButton ()
    {
        SceneManager.LoadScene("How To Play");
    }
}