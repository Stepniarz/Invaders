using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
    ScoreKeeper scoreKeeper;

    void Start() 
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void LoadGame()
    {
        if(scoreKeeper == null)
        {
            scoreKeeper = FindObjectOfType<ScoreKeeper>();
        }
        scoreKeeper.ResetScore();
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("Game Over", sceneLoadDelay));
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
