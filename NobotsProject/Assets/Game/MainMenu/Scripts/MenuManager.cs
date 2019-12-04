using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void MenuButton()
    {
        SceneManager.LoadScene(1); //Cargar menu principal
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(2); //Cargar gameplay / reiniciar
    }

    public void ExitGame()
    {
        Application.Quit(); //Salir del juego
    }
}
