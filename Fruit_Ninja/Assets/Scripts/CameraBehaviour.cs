using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    PlayerBehaviour playerBehavior;

    private Transform lookAt;
    private Vector3 offSet;

    private Vector3 moveVector;

    private float transition = 0.0f;
    private float animationDuration = 3.0f;
    private Vector3 animationOffset = new Vector3(0, 5, 5);

	// Use this for initialization
	void Start () {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        offSet = transform.position - lookAt.position;
        playerBehavior = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
	}

    // Update is called once per frame
    void Update() {
        moveVector = lookAt.position + offSet;

        //X - Left and Right
        moveVector.x = 0;

        //Y - Up and Down
        moveVector.y = Mathf.Clamp(moveVector.y, 3, 1000);
        
        if (transition > 1.0f)
        {
            transform.position = moveVector;
        }
        else
        {
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            transform.LookAt(lookAt.position, Vector3.up);
        }
	}
}
