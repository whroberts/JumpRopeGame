using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScreens : MonoBehaviour
{
    GameObject _menuSound;

    private void Awake()
    {
        _menuSound = GameObject.Find("MenuSound");
        DontDestroyOnLoad(_menuSound);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void ReturnToMenu()
    {
        Destroy(_menuSound);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
