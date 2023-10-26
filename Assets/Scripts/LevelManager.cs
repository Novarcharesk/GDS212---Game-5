using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Configuration")]
    public Compound requiredCompound;
    
    [Header("References")]
    [SerializeField] private GameObject winScreen;

    public void CheckWinCondition()
    {
        if (requiredCompound.IsAssembled())
        {
            Debug.Log("You win!");
            winScreen.SetActive(true);
        }
    }
}
