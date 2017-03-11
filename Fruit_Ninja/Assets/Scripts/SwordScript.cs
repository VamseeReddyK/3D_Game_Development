using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Apple")) {
            GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>()[2].Play();
            other.gameObject.GetComponent<AppleScript>().setSwordHit = true;
        }

        if (other.gameObject.CompareTag("Kiwi")) {
            GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>()[2].Play();
            other.gameObject.GetComponent<KiwiScript>().setSwordHit = true;
        }

        if (other.gameObject.CompareTag("Banana")) {
            GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>()[2].Play();
            other.gameObject.GetComponent<BananaScript>().setSwordHit = true;
        }

        if (other.gameObject.CompareTag("BananaOpened")) {
            GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>()[2].Play();
            other.gameObject.GetComponent<BananaOpenedScript>().setSwordHit = true;
        }

        if (other.gameObject.CompareTag("Pineapple")) {
            GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>()[2].Play();
            other.gameObject.GetComponent<PineappleScript>().setSwordHit = true;
        }

        if (other.gameObject.CompareTag("Strawberry")) {
            GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>()[2].Play();
            other.gameObject.GetComponent<StrawberryScript>().setSwordHit = true;
        }

        if (other.gameObject.CompareTag("Thorn")) {
            GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>()[2].Play();
            other.gameObject.GetComponent<ThornScript>().setSwordHit = true;
        }

        if (other.gameObject.CompareTag("Star")) {
            GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>()[2].Play();
            other.gameObject.GetComponent<StarScript>().setSwordHit = true;
        }

        if (other.gameObject.CompareTag("Pear")) {
            GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>()[2].Play();
            other.gameObject.GetComponent<PearScript>().setPlayerHit = true;
        }
    }
}
