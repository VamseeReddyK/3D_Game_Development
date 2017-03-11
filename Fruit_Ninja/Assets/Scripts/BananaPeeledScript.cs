using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaPeeledScript : MonoBehaviour {

    PlayerBehaviour playerBehaviour;
    PlayerHealth playerHealth;
    public float IncHealthBy = 10f;
    public bool setPlayerHit = false;

    private Vector3 angle = new Vector3(30, 0, 0);

    void start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }

    void Update()
    {
        //if(!playerBehaviour.isGodEffect)
            transform.Rotate(angle * Time.deltaTime);

        if (setPlayerHit)
            HitByPlayer();
    }

    void HitByPlayer()
    {
        //playerHealth.health += IncHealthBy;
        Destroy(gameObject);
    }

}
