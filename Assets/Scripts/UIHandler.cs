using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour {
    
    void Start()
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "LEVEL: " + (SceneManager.GetActiveScene().buildIndex).ToString();
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

}
