using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameManager gm;
	public HUD hud;
	private bool pause;


	public void MenuButton()
    {
        SceneManager.LoadScene(1); //Cargar menu principal
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(2); //Cargar gameplay / reiniciar
		Time.timeScale = 1f; 
    }

    public void ExitGame()
    {
        Application.Quit(); //Salir del juego
    }

	public void ResumeButton()
    {
        Time.timeScale = 1f; //Salir del menu al gameplay
		gm.SetPause(pause);
		hud.mouseLock.LockCursor();
    }
}
