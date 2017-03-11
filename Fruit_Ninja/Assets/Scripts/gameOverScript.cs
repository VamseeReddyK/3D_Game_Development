using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverScript : MonoBehaviour {

	// Use this for initialization
	public void gameOverLevel ()
	{
		SceneManager.LoadScene ("MainMenu", LoadSceneMode.Single);
	}
}
