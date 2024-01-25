using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIScript : MonoBehaviour
{
    public void NewGame()
    {
        GameManager.Instance.LoadNewGame();
    }

    public void Exit()
    {
        GameManager.Instance.QuitGame();
    }
}
