using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    public Image lifeBar;
    public GameObject pausePanel;

	void Start()
	{
		
	}


    public void SetLifeBar(float life)
    {
        lifeBar.fillAmount = life;
    }

    public void OpenPausePanel(bool open)                           // Abre el panel de pausa
    {
        pausePanel.SetActive(open);
    }
}
