using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System;
using UnityEngine.Events;

public class GenerateLevelSelector : MonoBehaviour
{
    public Button button;
    public GameObject holder;

    int levelNumbers;

    void Start()
    {

        levelNumbers = SceneManager.sceneCountInBuildSettings - 1;
        Vector2 buttonSize = button.GetComponent<RectTransform>().sizeDelta;

        int i;
        Button[] buttons = new Button[levelNumbers];

        for (i = 0; i < levelNumbers; i++)
        {
            buttons[i] = Instantiate<Button>(button, new Vector3(buttonSize.x * i, 0, 0), Quaternion.identity);
            buttons[i].transform.SetParent(holder.transform, false);
        }

        for (i = 0; i < levelNumbers; i++)
        {
            Button tmp = buttons[i];
            buttons[i].GetComponentInChildren<Text>().text = (i + 1).ToString();
            buttons[i].GetComponent<Button>().onClick.AddListener(() => SelectLevel(tmp));

            ColorBlock cb;
            switch (PlayerPrefs.GetInt((i + 1).ToString(), -1))
            {
                case -1:
                    cb = buttons[i].GetComponent<Button>().colors;
                    cb.normalColor = new Color(255, 180, 180, 255) / 255;
                    buttons[i].GetComponent<Button>().colors = cb;
                    buttons[i].GetComponent<Button>().onClick.RemoveAllListeners();
                    break;

                case 0:
                    cb = buttons[i].GetComponent<Button>().colors;
                    cb.normalColor = new Color(180, 180, 255, 255) / 255;
                    buttons[i].GetComponent<Button>().colors = cb;
                    break;

                case 1:
                    cb = buttons[i].GetComponent<Button>().colors;
                    cb.normalColor = new Color(180, 255, 180, 255) / 255;
                    buttons[i].GetComponent<Button>().colors = cb;
                    break;
            }
        }

        holder.GetComponent<RectTransform>().sizeDelta = new Vector2(i * buttonSize.x, buttonSize.y);
    }

    void SelectLevel(Button button)
    {
        SceneManager.LoadScene(int.Parse(button.GetComponentInChildren<Text>().text));
    }
}
