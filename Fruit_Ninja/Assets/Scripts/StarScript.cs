using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour {

    PlayerBehaviour playerBehaviour;
    public bool setPlayerHit = false;
    public bool setSwordHit = false;
    private Vector3 angle = new Vector3(0, 30, 0);

    // Use this for initialization
    void Start () {
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        if (transform.parent == null)
        {
            gameObject.GetComponent<AnimationScript>().enabled = false;
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            StartCoroutine(DestroyObject(5.0f));
        }
        //transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        if (transform.parent == null)
            transform.Rotate(angle * Time.deltaTime);
        else{

            if (!playerBehaviour.isGodEffect)
            {
                gameObject.GetComponent<AnimationScript>().enabled = true;
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                gameObject.GetComponent<AnimationScript>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(true);
            }
            if (setSwordHit || setPlayerHit)
                HitBySwordOrPlayer();
        }
    }

    void HitBySwordOrPlayer()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<ThrowScript>().stars < 5)
            GameObject.FindGameObjectWithTag("Player").GetComponent<ThrowScript>().stars += 1;
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Apple")) {
            other.gameObject.GetComponent<AppleScript>().setStarHit = true;
        }

        if (other.gameObject.CompareTag("Kiwi")) {
            other.gameObject.GetComponent<KiwiScript>().setStarHit = true;
        }

        if (other.gameObject.CompareTag("Banana")) {
            other.gameObject.GetComponent<BananaScript>().setStarHit = true;
        }

        if (other.gameObject.CompareTag("BananaOpened")) {
            other.gameObject.GetComponent<BananaOpenedScript>().setStarHit = true;
        }

        if (other.gameObject.CompareTag("Pineapple")) {
            other.gameObject.GetComponent<PineappleScript>().setStarHit = true;
        }

        if (other.gameObject.CompareTag("Strawberry")) {
            other.gameObject.GetComponent<ThornScript>().setStarHit = true;
        }

        if (other.gameObject.CompareTag("Thorn")) {
            other.gameObject.GetComponent<ThornScript>().setStarHit = true;
        }

        if (other.gameObject.CompareTag("Pear"))
        {
            other.gameObject.GetComponent<PearScript>().setStarHit = true;
        }
    }

    IEnumerator DestroyObject(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    
}
