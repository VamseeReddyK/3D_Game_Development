using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleScript : MonoBehaviour {

    PlayerHealth playerHealth;
    PlayerBehaviour playerBehaviour;
    
    public GameObject HalfApplePrefab;
	public GameObject Cookie;

    public float DecreaseHealthBy;
    public bool setSwordHit = false;
    public bool setStarHit = false;
    public bool setPlayerHit = false;
    private Vector3 angle = new Vector3(0,30,0);

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        if (!playerBehaviour.isGodEffect) {
            transform.Rotate(angle * Time.deltaTime);
            transform.GetChild(0).gameObject.SetActive(false);
        } else {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        if (setSwordHit || setStarHit)
            HitBySwordOrStar();

        if (setPlayerHit)
            HitByPlayer();
    }

    void HitBySwordOrStar()
    {
        GameObject h1 = (GameObject)Instantiate(HalfApplePrefab, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent.transform);
        GameObject h2 = (GameObject)Instantiate(HalfApplePrefab, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent.transform);
        GameObject a1 = (GameObject)Instantiate(Cookie, gameObject.transform.position, gameObject.transform.rotation);

        h2.transform.Rotate(0f, 180f, 0f);
        h2.GetComponent<Rigidbody>().AddForce(-transform.up * 10f);
        h1.GetComponent<Rigidbody>().AddForce(-transform.up * 10f);
        a1.GetComponent<Rigidbody>().AddForce(GameObject.FindGameObjectWithTag("Player").transform.forward * 100f);

        Destroy(gameObject);
    }

    void HitByPlayer()
    {
        if (playerHealth.health > 0 && !playerBehaviour.isGodEffect)
            playerHealth.health -= DecreaseHealthBy;
        setPlayerHit = false;
    }
}
