using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    public Image turboBar;
    public GameObject pausePanel;
	public Text velocimetro;

	public float speed;

	/*
	void Update()
	{
		speed = BallController.currentSpeed;
		velocimetro.text = "Speed:" + speed;
	}
	*/


    public void SetTurboBar(float turbo)
    {
        turboBar.fillAmount = turbo;
    }

	
    public void OpenPausePanel(bool open)                           // Abre el panel de pausa
    {
        pausePanel.SetActive(open);
    }

}
