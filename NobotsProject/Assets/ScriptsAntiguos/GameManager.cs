using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour


{
    public float life;
    public float maxLife = 100;
    private HUD hud;
    public GameObject pausePanel;
    private bool pause;

    // Start is called before the first frame update
    void Start()
    {
        life = maxLife;
        hud = FindObjectOfType<HUD>();											//	HUD
    }

    private void Update()
    {
        /*
		if (life <= 0)											//Vida
        {
            life = 100;
            GameOver();
        }
		*/
    }

    public void GetDamage(float damage)
    {
        life -= damage;

        hud.SetLifeBar(life / maxLife);
    }

	
    public void SetPause(bool pause)                                        // Funcion de pausa
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
	/*
    private void GameOver()
    {
        SceneManager.LoadScene(2);
    }
	*/
}

