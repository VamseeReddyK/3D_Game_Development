using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleScript : MonoBehaviour {

	public float thornFrequency= 2f;
	public float thornSpeed = 40f;
	public GameObject Thorn ;
    PlayerHealth playerHealth;
    PlayerBehaviour playerBehaviour;
    public float DecreaseHealthBy;
    public bool setSwordHit = false;
    public bool setStarHit = false;
    public bool setPlayerHit = false;

    private Vector3 angle = new Vector3(0, 30, 0);

    // Use this for initialization
    void Start () 
	{
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        InvokeRepeating("shootThorn", thornFrequency, thornFrequency);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        if (!playerBehaviour.isGodEffect)
        {
            //transform.Rotate(angle * Time.deltaTime);
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        if (setSwordHit || setStarHit)
            HitBySwordOrStar();

        if (setPlayerHit)
            HitPlayer();
    }

	void shootThorn ()
	{
        float distance = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, gameObject.transform.position);
        if (Mathf.Abs(distance) < 10 && !playerBehaviour.isGodEffect)
        {
            Instantiate(Thorn, transform.position, Thorn.transform.rotation, transform);
        }
	}

    void HitBySwordOrStar()
    {
        if (playerHealth.health < 300)
            playerHealth.health += DecreaseHealthBy;
        Destroy(gameObject);
    }

    void HitPlayer()
    {
        if (playerHealth.health > 0 && !playerBehaviour.isGodEffect)
            playerHealth.health -= DecreaseHealthBy;
        setPlayerHit = false;
    }
}
