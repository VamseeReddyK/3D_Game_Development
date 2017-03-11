using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    PlayerBehaviour playerBehaviour;
    public Text healthText;
    public Text deathText;
    public float health;

    private float difficultyLevel = 1;
    private float scoreToNextLevel = 10;
    private float maxDifficultyLevel = 10;
    private int i = 0;

	// Use this for initialization
	void Start () {
        health = 300;
        playerBehaviour = GetComponent<PlayerBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {

        if (GetComponent<PlayerBehaviour>().isDead)
        {
            //GameObject.FindGameObjectWithTag("DeathMenu").SetActive(true);
            //deathText.text = ((int)health).ToString();
            return;
        }

        if (i > 100)
        {
            if (!playerBehaviour.isGodEffect)
                health -= Time.deltaTime;
            if (health < 0)
                health = 0;
            if (health > 300)
                health = 300;
        }
        healthText.text = ((int)health).ToString();

        if (health == 0)
            playerBehaviour.isDead = true;

        if (!playerBehaviour.isGodEffect)
            i++;
	}
    
    public void Restart()
    {
        //SceneManager.LoadScene
    }    
}
