using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookObjects : MonoBehaviour {

    PlayerBehaviour playerBehaviour;

    Vector3 playerPosition;
    Vector3 rayDirection;
    RaycastHit rayCastHit;
    Vector3 playerDestination;
    void Start()
    {
        playerBehaviour = GetComponent<PlayerBehaviour>();
    }
    // Update is called once per frame
    void Update () {

        if (playerBehaviour.isHookEnabled)
        {            
            playerPosition = transform.position;
            playerPosition.y += 2.0f;
            rayDirection = playerBehaviour.gestureController._wristRight - playerBehaviour.gestureController._shoulderRight;
            rayDirection.z = -rayDirection.z;

            if (Physics.Linecast(playerPosition, rayDirection * 500f, out rayCastHit))
            {
                DetectCollider();
            }
        }
	}
    
    void DetectCollider()
    {
        Debug.DrawLine(playerPosition, rayDirection * 500f, Color.red);
        
        if (rayCastHit.collider.gameObject.CompareTag("Apple") || rayCastHit.collider.gameObject.CompareTag("Kiwi") || rayCastHit.collider.gameObject.CompareTag("Banana") ||
            rayCastHit.collider.gameObject.CompareTag("Star") || rayCastHit.collider.gameObject.CompareTag("Pear") || rayCastHit.collider.gameObject.CompareTag("Pineapple"))
        {
            Debug.Log(rayCastHit.collider.gameObject);
            var capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
            gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.5f, 0.16f);
            gameObject.GetComponent<CapsuleCollider>().height = 1.0f;

            playerDestination = rayCastHit.collider.gameObject.transform.position;
            playerDestination.z -= 2.0f;
            transform.Translate(playerDestination * 5.0f * Time.deltaTime);
            GetComponents<AudioSource>()[6].Play();
        }
        else
        {
            var capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
            gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1f, 0.16f);
            gameObject.GetComponent<CapsuleCollider>().height = 2.0f;
        }
        GetComponent<LineRenderer>().SetPosition(0, playerPosition);
        GetComponent<LineRenderer>().SetPosition(1, rayDirection * 500f);
    }
}
