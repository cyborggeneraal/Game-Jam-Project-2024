using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameOverController : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    public static gameOverController instance;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null & instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            canvasScpt.canvas.SetActive(true);
        }
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
