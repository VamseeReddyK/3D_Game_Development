using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearScript : MonoBehaviour {

	PlayerHealth playerhealth;
	PlayerBehaviour playerbehaviour;
	public GameObject Explosion;

    public float IncHealthBy;
	public bool setSwordHit = false;
	public bool setStarHit = false;
	public bool setPlayerHit = false;
	
	// Use this for initialization
	void Start () 
	{
		playerhealth = GameObject.FindGameObjectWithTag("Player").GetComponent <PlayerHealth> ();
		playerbehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent <PlayerBehaviour> ();
        transform.GetChild(0).gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (!playerbehaviour.isGodEffect) {
            gameObject.GetComponent<AnimationScript>().enabled = true;
            //transform.GetChild(0).gameObject.SetActive(false);
        } else {
            gameObject.GetComponent<AnimationScript>().enabled = false;
            //transform.GetChild(0).gameObject.SetActive(true);
        }

        if (setSwordHit || setStarHit) 
		{
            HitBySwordOrStar();
		}

		if (setPlayerHit)
		{
			HitByPlayer ();
	    }

     }

    void HitBySwordOrStar()
	{
        Instantiate(Explosion, transform.position,transform.rotation);
        if (playerhealth.health <300f)
            playerhealth.health += IncHealthBy;
        Destroy(gameObject);
	}

    void HitByPlayer()
    {
        Instantiate(Explosion, transform.position, transform.rotation);
        if (playerhealth.health > 0 && !playerbehaviour.isGodEffect)
            playerhealth.health = 0;
        setPlayerHit = false;
        Destroy(gameObject);
    }
}