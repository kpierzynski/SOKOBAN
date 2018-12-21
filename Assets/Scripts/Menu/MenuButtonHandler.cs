using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonHandler : MonoBehaviour
{

    public Button button;

    void Start()
    {
        if (PlayerPrefs.GetInt("1", -1) != -1)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        //PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
