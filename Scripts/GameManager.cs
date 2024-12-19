using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        Paused,
        Playing
    }
    
    public GameState gameState = GameState.Playing;

    public void QuitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        if (gameState == GameState.Paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            if (Time.timeScale == 0) Time.timeScale = 1.0f;
        }
    }


}
