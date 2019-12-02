using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene(2); //Cargar gameplay
    }

    public void ExitGame()
    {
        Application.Quit(); //Salir del juego
    }
}
