using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowScript : MonoBehaviour
{
    Animator anim;
    PlayerBehaviour playerBehaviour;
    public GameObject starPrefab;
    public GameObject starPosition;
    public int stars;
    public Text scoreText;

    // Use this for initialization
    void Start()
    {
        stars = 0;
        playerBehaviour = GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "  " + stars;
        if (playerBehaviour.isThrowing)
        {
            StartCoroutine(ThrowObject(0.3f));
            playerBehaviour.isThrowing = false;
        }
    }

    IEnumerator ThrowObject(float time)
    {
        yield return new WaitForSeconds(time);
        if (stars > 0)
        {
            stars--;
            GameObject h1 = (GameObject)Instantiate(starPrefab, starPosition.transform.position, starPosition.transform.rotation);
            h1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            h1.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);
            GetComponents<AudioSource>()[8].Play();
        }
    }
}

