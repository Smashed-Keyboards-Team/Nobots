using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    public Image turboBar;
    public GameObject pausePanel;
	public Text velocimetro;

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

}
