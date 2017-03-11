using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookStickScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Apple")) {
            //other.gameObject.GetComponent<AppleScript>().setSwordHit = true;
        }

        if (other.gameObject.CompareTag("Kiwi")) {
            //other.gameObject.GetComponent<KiwiScript>().setSwordHit = true;
        }

        if (other.gameObject.CompareTag("Banana")) {
            //other.gameObject.GetComponent<BananaScript>().setSwordHit = true;
        }

        if (other.gameObject.CompareTag("BananaOpened")) {
            //other.gameObject.GetComponent<BananaOpenedScript>().setSwordHit = true;
        }

        if (other.gameObject.CompareTag("Pineapple")) {
            //other.gameObject.GetComponent<PineappleScript>().setSwordHit = true;
        }

        if (other.gameObject.CompareTag("Strawberry")) {
            //other.gameObject.GetComponent<StrawberryScript>().hookStick = gameObject;
            //other.gameObject.GetComponent<StrawberryScript>().setHookStickHit = true;
        }

        if (other.gameObject.CompareTag("Thorn")) {
            //other.gameObject.GetComponent<ThornScript>().setSwordHit = true;
        }
    }
}
