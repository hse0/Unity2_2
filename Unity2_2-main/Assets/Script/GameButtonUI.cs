using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.ParticleSystem;

public class GameButtonUI : MonoBehaviour
{
    
    public void StartGame(string StartGame)
    {
        SceneManager.LoadScene("MainGame");
    }
    
    public void GameSetting_1(string GameSetting)
    {
        SceneManager.LoadScene("GameSet _1");
    }

    public void  ExitGame(string ExitGame)
    {
        SceneManager.LoadScene("Exit");
    }
    public void Thefirstscreen(string Thefirstscreen)
    {
        SceneManager.LoadScene("StartGameScene");
    }

    public void GameSetting_2(string GameSetting)
    {
        SceneManager.LoadScene("GameSet _2");
    }

}
