using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour {

	// Use this for initialization
	public Canvas QuitMenu;
	public Button StartText;
	public Button ExitText;
	void Start () 
	{
		QuitMenu = QuitMenu.GetComponent <Canvas> ();
		StartText = StartText.GetComponent <Button> ();
		ExitText = ExitText.GetComponent <Button> ();
		QuitMenu.enabled = false;
		
	}
	
	// Update is called once per frame
	public void ExitPress ()
	{
		QuitMenu.enabled = true;
		StartText.enabled = false;
		ExitText.enabled = false;
	}

	public void NoPress ()
	{
		QuitMenu.enabled = false;
		StartText.enabled = true;
		ExitText.enabled = true;


	}

	public void StartLevel ()
	{
		SceneManager.LoadScene ("Fruit_Ninja", LoadSceneMode.Single);
	}

	public void ExitGame ()
	{
		Application.Quit ();
	}
}
