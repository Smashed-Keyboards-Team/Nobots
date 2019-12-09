using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    public Image turboBar;
    public GameObject pausePanel;
	public GameObject gameOverPanel;
	public Text velocimetro;
	public MouseLock mouseLock;

	void Start()
	{
		mouseLock = GetComponent<MouseLock>();
	}
	//public float speed;

	/*
	void Update()
	{
		speed = BallController.currentSpeed;
		velocimetro.text = "Speed:" + speed;
	}
	*/

	//Modificar barra de turbo
    public void SetTurboBar(float turbo)
    {
        turboBar.fillAmount = turbo * 2;
    }

	//Abrir el panel de pausa
    public void OpenPausePanel(bool open)                           
    {
        pausePanel.SetActive(open);
    }

	public void OpenGameOverPanel(bool open)                           
    {
        gameOverPanel.SetActive(open);
    }
}
