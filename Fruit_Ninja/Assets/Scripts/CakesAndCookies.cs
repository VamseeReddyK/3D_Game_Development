using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakesAndCookies : MonoBehaviour {

    PlayerHealth playerHealth;
    public int IncHealthBy;
    private Vector3 angle = new Vector3(0, 30, 0);

    void Start ()
	{
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent <PlayerHealth>();
	}

    void Update()
    {
        transform.Rotate(angle * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag ("Player")) 
		{
            other.gameObject.GetComponents<AudioSource>()[1].Play();
            playerHealth.health += IncHealthBy;
			Destroy (gameObject);
		}
	}
}
