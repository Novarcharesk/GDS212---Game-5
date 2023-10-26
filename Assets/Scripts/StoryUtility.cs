using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryUtility : MonoBehaviour
{

    public GameObject scene1;
    public GameObject scene2;
    public GameObject scene3;
    public GameObject scene4;
    public GameObject scene5;
    public GameObject scene6;
    public GameObject scene7;
    public GameObject scene8;
    public GameObject scene9;
    public GameObject scene10;
    public GameObject scene11;
    public GameObject scene12;

    public TextMeshProUGUI nextButton;

    private int sceneIndex = 1;

    public void NextScene()
    {
        scene1.SetActive(false);
        scene2.SetActive(false);
        scene3.SetActive(false);
        scene4.SetActive(false);
        scene5.SetActive(false);
        scene6.SetActive(false);
        scene7.SetActive(false);
        scene8.SetActive(false);
        scene9.SetActive(false);
        scene10.SetActive(false);
        scene11.SetActive(false);
        scene12.SetActive(false);

        sceneIndex++;

        if (sceneIndex == 1)
        {
            scene1.SetActive(true);
        }
        else if (sceneIndex == 2)
        {
            scene2.SetActive(true);
        }
        else if (sceneIndex == 3)
        {
            scene3.SetActive(true);
        }
        else if (sceneIndex == 4)
        {
            scene4.SetActive(true);
        }
        else if (sceneIndex == 5)
        {
            scene5.SetActive(true);
        }
        else if (sceneIndex == 6)
        {
            scene6.SetActive(true);
        }
        else if (sceneIndex == 7)
        {
            scene7.SetActive(true);
        }
        else if (sceneIndex == 8)
        {
            scene8.SetActive(true);
        }
        else if (sceneIndex == 9)
        {
            scene9.SetActive(true);
        }
        else if (sceneIndex == 10)
        {
            scene10.SetActive(true);
        }
        else if (sceneIndex == 11)
        {
            scene11.SetActive(true);
        }
        else if (sceneIndex == 12)
        {
            scene12.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
