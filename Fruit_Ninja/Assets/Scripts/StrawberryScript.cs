using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawberryScript : MonoBehaviour {

    PlayerBehaviour playerBehaviour;
    PlayerHealth playerHealth;

    public bool setPlayerHit = false;
    public bool setSwordHit = false;
    public bool setStarHit = false;
    public float DecHealthBy;

    private Vector3 angle = new Vector3(0, 30, 0);
    private Vector3 strawberryPosition;
    
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        if (!playerBehaviour.isGodEffect)
        {
            GetComponent<AnimationScript>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(false);
            
        }else
        {
            GetComponent<AnimationScript>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(true);
        }
        
        if (setSwordHit || setStarHit)
            HitBySwordOrStar();

        if (setPlayerHit)
            HitPlayer();
        
    }

    void HitBySwordOrStar()
    {
        if (playerHealth.health < 300)
        {
            playerHealth.health += DecHealthBy;
        }
        Destroy(gameObject);
    }

    void HitPlayer()
    {
        if (playerHealth.health > 0 && !playerBehaviour.isGodEffect) {
            playerHealth.health -= DecHealthBy;
        }
        setPlayerHit = false;
    }
}
