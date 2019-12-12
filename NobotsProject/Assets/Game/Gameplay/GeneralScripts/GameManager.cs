using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float turbo;
    public float maxTurbo = 100;
    private PlayerController playerController;
    private HUD hud;
    public GameObject pausePanel;
    private bool pause;
	private bool dead;
	public bool win;

	public float winTime = 3;
	private float winCounter;

	public float coreHp = 100;

	public float turboRecharge = 3;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").
            GetComponent<PlayerController>();

        //Encontrar HUD
        hud = FindObjectOfType<HUD>();

        turbo = maxTurbo;											
    }

	void Update()
    {
		//Recarga de turbo
		if (turbo < maxTurbo)  
		{
			turbo += turboRecharge * Time.deltaTime;
		}
		
		//Ajustes de turbo maximo y minimo
		if (turbo <= 0)  
		{
			turbo = 0;
		}
		if (turbo >= 100)  
		{
			turbo = 100;
		}

		if (coreHp <= 0)
		{
			win = true;
			Win();
		}
    }

	//Funcion para gastar turbo
    public void TurboBarRefresh()
    {
        hud.SetTurboBar(turbo / maxTurbo);
    }

	//Funcion para hacer daño al nucleo
	public void CoreDamage(float damage)
    {
        coreHp -= damage;
    }
	
	// Funcion de pausa
    public void SetPause(bool pause)                                        
    {
        this.pause = pause;

        hud.OpenPausePanel(pause);

        if (pause)
        {
            Time.timeScale = 0f;                                            // Velocidad del juego
        }
        else if(pause == false)
        {
            Time.timeScale = 1f;
        }
    }
	
    public void Win()
    {
        hud.OpenWinPanel(win);
		if (winCounter <= winTime)
		{
			winCounter += Time.deltaTime;
		}
		else
		{
			hud.mouseLock.ShowCursor();
			SceneManager.LoadScene(1);
		}
    }
	
	public void GameOver(bool dead)
    {
        this.dead = dead;
		hud.OpenGameOverPanel(dead);
    }	
}

