using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class wepsaveing : MonoBehaviour
{
    //so u keep wep when switching rooms
    public List<GameObject> eqwep;

    private void OnEnable()
    {
        if (gameState.eqwep >= 1 && gameState.eqwep <= eqwep.Count)
        {
            for (int i = 0; i < eqwep.Count; i++)
            {
                eqwep[i].SetActive(i == gameState.eqwep - 1);
                SaveGame.Save();

            }
        }
    }
}
