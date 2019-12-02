using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float turbo;
    public float maxTurbo = 100;
    private HUD hud;
    public GameObject pausePanel;
    private bool pause;

	public float turboCurrentCd;
	public float turboCd;

	public float coreHp = 100;

    void Start()
    {
        turbo = maxTurbo;

		turboCurrentCd = 0;

		//Encontrar HUD
        hud = FindObjectOfType<HUD>();											
    }

	void Update()
    {
		//Recarga de turbo
		if (turbo < maxTurbo)  
		{
			turboCurrentCd = turboCurrentCd + 1 * Time.deltaTime;
			if(turboCurrentCd >= turboCd)
			{
				turbo = turbo + 0.2f;
			}
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
			GameOver();
		}
    }

	//Funcion para gastar turbo
    public void Turbo()
    {
        turbo -= 1;

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
        else
        {
            Time.timeScale = 1f;
        }
    }
	
    private void GameOver()
    {
        SceneManager.LoadScene(1);
    }	
}

