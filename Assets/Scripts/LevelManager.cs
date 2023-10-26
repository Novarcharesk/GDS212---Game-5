using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level Configuration")]
    public Compound requiredCompound;
    
    [Header("References")]
    [SerializeField] private GameObject winScreen;

    private void Start()
    {
        if (SceneManager.GetSceneByName("Background Scene").isLoaded == false)
        {
            SceneManager.LoadSceneAsync("Background Scene", LoadSceneMode.Additive);
        }
    }

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

    public void NextLevel()
    {
        switch (requiredCompound.name)
        {
            case Compound.CompoundType.Water:
                SceneManager.LoadScene("Level 2");
                break;
            case Compound.CompoundType.Methane:
                SceneManager.LoadScene("Level 3");
                break;
            case Compound.CompoundType.Ammonia:
                SceneManager.LoadScene("Level 4");
                break;
            case Compound.CompoundType.Methanol:
                SceneManager.LoadScene("Level 5");
                break;
            case Compound.CompoundType.AmmoniumHydroxide:
                //SceneManager.LoadScene("Level 6");
                SceneManager.LoadScene("Menu");
                break;
            case Compound.CompoundType.AcetateAcid:
                SceneManager.LoadScene("Menu");
                break;
        }
    }
}
