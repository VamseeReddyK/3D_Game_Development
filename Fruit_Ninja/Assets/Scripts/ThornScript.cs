using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornScript : MonoBehaviour {

    PlayerHealth playerHealth;
    PlayerBehaviour playerBehaviour;
    
    public float DecreaseHealthBy;
    public bool setSwordHit = false;
    public bool setStarHit = false;
    public bool setPlayerHit = false;

	// Use this for initialization
	void Start () {
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!playerBehaviour.isGodEffect)
        {
            float distance = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.parent.transform.position);
            if (distance > 10)
                Destroy(gameObject);
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, Time.deltaTime);
        }

        if (setSwordHit || setStarHit)
            HitBySwordOrStar();

        if (setPlayerHit)
            HitByPlayer();
    }

    void HitBySwordOrStar()
    {
        if (playerHealth.health < 300 && !playerBehaviour.isGodEffect)
            playerHealth.health += DecreaseHealthBy;
        Destroy(gameObject);
    }

    void HitByPlayer()
    {
        if (playerHealth.health > 0 && !playerBehaviour.isGodEffect)
            playerHealth.health -= DecreaseHealthBy;
        Destroy(gameObject);
    }
}
