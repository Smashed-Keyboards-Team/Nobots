using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GodMode : MonoBehaviour
{
    public GameManager gm;
	public HUD hud;
	public PlayerController pc;
	public GameObject godPanel;
	private bool pause;

	public void God_Logo()
    {
        SceneManager.LoadScene(0); //Cargar logo
    }

	public void God_MainMenu()
    {
        SceneManager.LoadScene(1); //Cargar menu principal
    }

    public void God_TestPlayground()
    {
        SceneManager.LoadScene(2); //Cargar gameplay / reiniciar
		Time.timeScale = 1f; 
    }

	public void God_Win() //Ganar partida
    {
		gm.Win();
		God_ExitGodMenu();
    }

	public void God_Lose() //Perder partida
    {
        gm.GameOver(true);
		God_ExitGodMenu();
    }

	public void God_DestroyCore() //Destruir nucleo
    {
        gm.CoreDamage(10000000000);
		God_ExitGodMenu();
    }

	public void God_DestroyPlayer() //Destruir player
    {
		pc.PlayerDead();
		God_ExitGodMenu();
    }

	public void God_HealCore() //Curar nucleo
    {
		hud.mouseLock.LockCursor();
		gm.coreHp = 100;
		God_ExitGodMenu();
    }

	public void God_InvulnerablePlayer() //Player Invulnerable
    {
		hud.mouseLock.LockCursor();
		pc.godInvulnerable = true;
		God_ExitGodMenu();
    }

	public void God_FreeMovement() //Movimiento sin restricciones
    {
		hud.mouseLock.LockCursor();
		pc.isBall = false;
		pc.godInvulnerable = true;
		pc.rb.useGravity = false;
		pc.rb.isKinematic = true;
		God_ExitGodMenu();
    }

	public void God_ExitGodMenu() //Salir del menu God
    {
        Time.timeScale = 1f; 
		godPanel.SetActive(false);
    }
}
