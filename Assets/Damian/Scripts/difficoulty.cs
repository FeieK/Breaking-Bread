using UnityEngine;
using static gameState;
using UnityEngine.UI;
using TMPro;

//set this to smt so u can make a dropdown for dificoulty
public class difficoulty : MonoBehaviour
{
    public TMP_Dropdown difficultyDrop;

    void Start()
    {
        //sends the value to the index wich sets the enum
        difficultyDrop.onValueChanged.AddListener(OnDifficultyChanged);
    }

    void OnDifficultyChanged(int index)
    {
        gameState.difficulty = (Difficulty)index;
    }
}