using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaScript : MonoBehaviour {

    PlayerHealth playerHealth;
    PlayerBehaviour playerBehaviour;
    
    public GameObject BananaOpenPrefab;
    public GameObject BananaPeeledPrefab;
    public float DecreaseHealthBy;
    public bool setSwordHit = false;
    public bool setStarHit = false;
    public bool setPlayerHit = false;
    private Vector3 angle = new Vector3(30, 0, 0);

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }

    void Update()
    {
        if (!playerBehaviour.isGodEffect) {
            transform.Rotate(angle * Time.deltaTime);
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        if (setSwordHit)
            HitBySword();

        if (setStarHit)
            HitByStar();

        if (setPlayerHit)
            HitPlayer();
    }

    void HitBySword()
    {
        Vector3 position = gameObject.transform.position;
        position.y -= 1.0f;
        position.z += 3.0f;
        GameObject h1 = (GameObject)Instantiate(BananaOpenPrefab, position, gameObject.transform.rotation, gameObject.transform.parent.transform);
        h1.transform.Rotate(0f, 0f, -90f);
        Destroy(gameObject);
    }

    void HitByStar()
    {
        GameObject h1 = (GameObject)Instantiate(BananaPeeledPrefab, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent.transform);
        h1.transform.Rotate(0f, 0f, 0f);
        Destroy(gameObject);
    }

    void HitPlayer()
    {
        if (playerHealth.health > 0 && !playerBehaviour.isGodEffect)
            playerHealth.health -= DecreaseHealthBy;
        setPlayerHit = false;
    }
}
