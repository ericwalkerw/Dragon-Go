using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    

    [Header("Pause")]
    [SerializeField] private GameObject pauseGameScreen;

    [Header("END")]
    [SerializeField] private GameObject endGame;
    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseGameScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGameScreen.activeInHierarchy)
            {
                PauseGame(false);
            }
            else
                PauseGame(true);
        }
    }


    #region Game Over
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }   
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }  
    public void Quit()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        pauseGameScreen.SetActive(status);

        if (status)
        {
            Time.timeScale = 0;
        }
        else 
            Time.timeScale = 1;
    }

    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }
    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    #endregion

    #region End
    public void ENDGAME()
    {
        endGame.SetActive(true);
    }
    #endregion
}
