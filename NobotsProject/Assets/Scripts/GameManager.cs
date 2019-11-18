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

    void Start()
    {
        turbo = maxTurbo;
        hud = FindObjectOfType<HUD>();											//	HUD
    }

	void Update()
    {
		if (turbo < maxTurbo)  
		{
			turbo = turbo + 0.2f;
		}
		if (turbo <= 0)  
		{
			turbo = 0;
		}
		if (turbo >= 100)  
		{
			turbo = 100;
		}
    }

    public void Turbo()
    {
        turbo -= 1;

        hud.SetTurboBar(turbo / maxTurbo);
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
        SceneManager.LoadScene(2);
    }	
}

