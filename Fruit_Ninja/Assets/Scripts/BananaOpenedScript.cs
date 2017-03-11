using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaOpenedScript : MonoBehaviour {

    PlayerBehaviour playerBehaviour;
    PlayerHealth playerHealth;
    
    public GameObject BananaPeeledPrefab;
    public float DecHealthBy;
    public bool setSwordHit = false;
    public bool setStarHit = false;
    public bool setPlayerHit = false;

    private Vector3 angle = new Vector3(0, 30, 0);

    void Start()
    {
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        
    }

    void Update()
    {
        if (!playerBehaviour.isGodEffect)
        {
            transform.Rotate(angle * Time.deltaTime);
        }

        if (setSwordHit || setStarHit)
            HitBySwordOrStar();

        if (setPlayerHit)
            HitPlayer();
    }

    void HitBySwordOrStar()
    {
        Vector3 position = gameObject.transform.position;
        position.y += 1.0f;
        position.z += 3.0f;
        GameObject h1 = (GameObject)Instantiate(BananaPeeledPrefab, position, gameObject.transform.rotation, gameObject.transform.parent.transform);
        h1.transform.Rotate(0f, 0f, -90f);
        if (playerHealth.health < 300f)
            playerHealth.health += 10;
        Destroy(gameObject);
    }

    void HitPlayer()
    {
        if (playerHealth.health > 0 && !playerBehaviour.isGodEffect)
            playerHealth.health -= DecHealthBy;
        setPlayerHit = false;
    }
}
