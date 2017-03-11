using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyScript : MonoBehaviour {

    GestureController gestureController;
    PlayerBehaviour playerBehaviour;
    private Vector3 lastDirection;
    public GameObject playerCamera;
    //CharacterController character;

    private float h;
    private float v;

    // Use this for initialization
    void Start () {
        playerBehaviour = GetComponent<PlayerBehaviour>();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerBehaviour.isFlying)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
            //Vector3 moveVector = Rotating(h, v);
            Vector3 moveVector = Vector3.forward* v;
            //GetComponent<Rigidbody>().AddForce((direction * 100), ForceMode.Acceleration);
            /*if (moveVector.z != 0.0f || gestureController.Run() != Vector3.zero)
            {
                anim.SetBool("run", true);
                hookStick.SetActive(false);
                playerSward.SetActive(false);
                if (moveVector.z == 0.0f)
                {
                    moveVector.x = gestureController.Run().x * speed;
                    moveVector.z = gestureController.Run().z * speed;
                    moveVector = moveVector * Time.deltaTime;
                }
            }*/
            transform.Translate(moveVector);
        }
    }

    Vector3 Rotating(float horizontal, float vertical)
    {
        // Calculate target direction based on direction key.
        Vector3 targetDirection = Vector3.forward * vertical;

        // Rotate the player to the correct fly position.
        if ((Mathf.Abs(horizontal) > 0.1 || Mathf.Abs(vertical) > 0.1) && targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            targetRotation *= Quaternion.Euler(90, 0, 0);

            Quaternion newRotation = Quaternion.Slerp(GetComponent<Rigidbody>().rotation, targetRotation, 3.0f * Time.deltaTime);

            GetComponent<Rigidbody>().MoveRotation(newRotation);
            lastDirection = targetDirection;
        }

        // Rotate the player to stand position when flying & idle.
        if (!(Mathf.Abs(horizontal) > 0.9 || Mathf.Abs(vertical) > 0.9))
        {
            Repositioning();
        }

        // Return the current fly direction.
        return targetDirection;
    }

    // Put the player on a standing up position based on last direction faced.
    public void Repositioning()
    {
        if (lastDirection != Vector3.zero)
        {
            lastDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(lastDirection);
            Quaternion newRotation = Quaternion.Slerp(GetComponent<Rigidbody>().rotation, targetRotation, 3.0f * Time.deltaTime);
            GetComponent<Rigidbody>().MoveRotation(newRotation);
        }
    }
}
