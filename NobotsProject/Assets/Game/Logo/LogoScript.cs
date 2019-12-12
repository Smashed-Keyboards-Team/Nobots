using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogoScript : MonoBehaviour
{
	//public Image logo;
	private float counter;
	public int sceneChange;
	//private Color transparency;

    void Start()
    {
        /*
		transparency = logo.GetComponent<Image>().color; 
		transparency.a = 0f;
		logo.GetComponent<Image>().color = transparency;
		*/
    }

	void Update()
    {
 
		//transparency.a = transparency.a + 100;
		counter += Time.deltaTime;
		

		if (counter >= sceneChange)
		{
			SceneManager.LoadScene(1);
		}
    }

    
}
