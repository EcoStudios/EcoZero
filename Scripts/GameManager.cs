using System.Collections;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject loadingScreenPrefab;

    void Start()
    {
        Instance = this;
    }

    // If enum value is less than 0, then the game will be paused.
    public enum GameState
    {
        ActiveInGameMenu = -3,
        ActiveInGameUI = -2,
        Paused = -1,
        ActiveInMainMenu = 0,
        Playing = 1
    }
    
    public static GameState ActiveGameState = GameState.Playing;
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    // Loading up the game
    public void LoadToGame()
    {
        // Loading the scene
        GameObject loadingScreen = Instantiate(loadingScreenPrefab);
        loadingScreen.SetActive(true);
        loadingScreen.GetComponentInChildren<Camera>().gameObject.SetActive(false);
        StartCoroutine(LoadScene(1, loadingScreen));

        // Do whatever else
        DisableCursor();
        ActiveGameState = GameState.Playing;
    }

    // Loading the mainmenu
    public void LoadToMainMenu()
    {
        PlayerManager.PlayerGameObj.SetActive(false); // so the main cam is off, for cam on loadingscreen to be active
        // Loading the scene
        GameObject loadingScreen = Instantiate(loadingScreenPrefab);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadScene(0, loadingScreen));
        
        // Do whatever else
        EnableCursor();
        ActiveGameState = GameState.ActiveInMainMenu;
    }

    private IEnumerator LoadScene(int sceneIndex, GameObject loadingScreen)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncOperation!.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            int progressPercent = (int)(progress * 100);
            loadingScreen.transform.Find("Loading Screen").Find("PercentTXT")
                .GetComponent<TMP_Text>().text = progressPercent + "%";
            loadingScreen.GetComponentInChildren<Slider>().value = progress;
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(sceneIndex));
    }


    void Update()
    {
        // Pausing the world time
        if (ActiveGameState < 0)
        {
            Time.timeScale = 0;
        }
        else
        {
            if (Time.timeScale == 0) Time.timeScale = 1.0f;
        }

        // Changing gamestates automatically.
        ActiveGameState = SceneManager.GetActiveScene().buildIndex == 0 
            ? GameState.ActiveInMainMenu : GameState.Playing;
    }
    
    // Quality of life methods 
    public static void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public static void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


}
