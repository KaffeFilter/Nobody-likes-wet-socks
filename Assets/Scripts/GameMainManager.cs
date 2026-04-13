using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


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
        Application.Quit();
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
}
