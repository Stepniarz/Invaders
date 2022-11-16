using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int currentScore;
    public static ScoreKeeper instance;

    void Awake() 
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void AddScore(int value)
    {
        currentScore += value;
        Mathf.Clamp(currentScore, 0, int.MaxValue);    //Score can't go below 0
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}
