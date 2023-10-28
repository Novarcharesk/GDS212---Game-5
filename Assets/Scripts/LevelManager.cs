using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Level Configuration")]
    public Compound requiredCompound;
    
    [Header("References")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private Animator transitionAnimator;

    private AsyncOperation levelLoad;

    private void Start()
    {
        if (SceneManager.GetSceneByName("Background Scene").isLoaded == false)
        {
            levelLoad = SceneManager.LoadSceneAsync("Background Scene", LoadSceneMode.Additive);
        }
    }

    //private void FixedUpdate()
    //{
    //    if (levelLoad != null && levelLoad.isDone)
    //    {
    //        levelLoad = null;
    //        // TODO: only start transition animation once complete
    //    }
    //}

    public void CheckWinCondition()
    {
        if (requiredCompound.IsAssembled())
        {
            Debug.Log("You win!");
            winScreen.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel(Button button)
    {
        button.interactable = false;
        switch (requiredCompound.name)
        {
            case Compound.CompoundType.Water:
                StartCoroutine(LoadLevel("Level 2"));
                break;
            case Compound.CompoundType.Methane:
                StartCoroutine(LoadLevel("Level 3"));
                break;
            case Compound.CompoundType.Ammonia:
                StartCoroutine(LoadLevel("Level 4"));
                break;
            case Compound.CompoundType.Methanol:
                StartCoroutine(LoadLevel("Level 5"));
                break;
            case Compound.CompoundType.AmmoniumHydroxide:
                StartCoroutine(LoadLevel("Level 6"));
                //SceneManager.LoadScene("Menu");
                break;
            case Compound.CompoundType.AcetateAcid:
                StartCoroutine(LoadLevel("Menu"));
                break;
        }
    }

    IEnumerator LoadLevel(string levelName)
    {
        transitionAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelName);
    }
}
