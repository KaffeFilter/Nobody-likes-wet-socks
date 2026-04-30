using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using TMPro;


public class GameMainManager : MonoBehaviour
{
    public static GameMainManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public void Gameover()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        yield return null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
