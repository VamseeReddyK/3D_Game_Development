using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour {
    
    Animator anim;
    public GestureController gestureController;
    VoiceRecognizer voiceRecognizer;
    public GameObject God;
    public GameObject hookStick;
    public GameObject playerSward;
    public GameObject directionalLight;

    public bool isDead;
    public bool isFlying;
    public bool isThrowing;
    public bool isGodEffect;
    public bool isHookEnabled = false;

    //private CharacterController character;
    private float speed = 5.0f;
    private Vector3 moveVector;
    private bool set = false;
    
    // Use this for initialization
    void Start () {
        isDead = false;
        isFlying = false;
        isThrowing = false;
        isGodEffect = false;
        isHookEnabled = false;
        God.SetActive(false);
        hookStick.SetActive(false);
        playerSward.SetActive(false);
        anim = GetComponent<Animator>();
        gestureController = GetComponent<GestureController>();
        voiceRecognizer = GetComponent<VoiceRecognizer>();
    }
    
    // Update is called once per frame
    void Update () {

        moveVector = Vector3.zero;
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        moveVector.z = Input.GetAxisRaw("Vertical") * speed;
        moveVector = moveVector * Time.deltaTime;

        if (isDead)
        {
            if (!set) {
                set = true;
                GetComponents<AudioSource>()[3].Play();
                anim.SetBool("dead", true);
                StartCoroutine(EndSceneAfter(3.0f));
            }
            return;
        }
        
        //If hook enabled he can fly    
        if (Input.GetKeyDown(KeyCode.F) || gestureController.isFlyActive && !isHookEnabled)
        {
            isFlying = !isFlying;
            anim.SetBool("fly", isFlying);
            hookStick.SetActive(false);
            playerSward.SetActive(false);
            if (isFlying)
            {
                transform.Translate(0f, 10.0f, 0f);
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            }
        }

        //If not flying can do everything else
        if (isFlying)
        {
            moveVector = Vector3.forward * gestureController.Run().z;
            transform.Translate(moveVector);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.T) || gestureController.isThrowActive && !isHookEnabled)
            {                
                isThrowing = true;
                anim.SetBool("throw", true);
            }
            else
                anim.SetBool("throw", false);

            if (Input.GetKeyDown(KeyCode.P) || gestureController.isSliceActive && !isHookEnabled)
            {
                anim.SetBool("slice", true);
                playerSward.SetActive(true);
                hookStick.SetActive(false);
            }
            else
                anim.SetBool("slice", false);

            if (Input.GetKeyDown(KeyCode.O) || gestureController.isAttackActive && !isHookEnabled)
            {
                anim.SetBool("attack", true);
                playerSward.SetActive(true);
                hookStick.SetActive(false);
            }
            else
                anim.SetBool("attack", false);

            if (Input.GetKeyDown(KeyCode.I) || gestureController.isAttackActive && !isHookEnabled) {
                //anim.SetBool("jump_attack", true);
                playerSward.SetActive(true);
                hookStick.SetActive(false);
            } else
                anim.SetBool("jump_attack", false);

            if (Input.GetKeyDown(KeyCode.L) || gestureController.isDuckActive && !isHookEnabled)
            {
                anim.SetBool("duck", true);
                hookStick.SetActive(false);
                playerSward.SetActive(false);
                var capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
                gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.5f, 0.16f);
                gameObject.GetComponent<CapsuleCollider>().height = 1.0f;
                StartCoroutine(ResetPlayerCollider(2.0f));
            }
            else
                anim.SetBool("duck", false);

            if (Input.GetKeyDown(KeyCode.H) || gestureController.isHookActive)
            {
                //anim.SetBool("hook", true);
                //hookStick.SetActive(true);
                isHookEnabled = !isHookEnabled;
                playerSward.SetActive(false);
            }
            else
                anim.SetBool("hook", false);
            
            if (Input.GetKeyDown(KeyCode.G) || voiceRecognizer.isFruitGodActive && !isHookEnabled)
            {
                StartCoroutine(EnableGodEffectAfter(2));
                anim.SetBool("fruitGod", true);
            }
            else
                anim.SetBool("fruitGod", false);

            if (moveVector.z != 0.0f || gestureController.Run() != Vector3.zero && !isHookEnabled)
            {
                anim.SetBool("run", true);
                hookStick.SetActive(false);
                playerSward.SetActive(false);
                if (moveVector.z == 0.0f){
                    moveVector.x = gestureController.Run().x * speed;
                    moveVector.z = gestureController.Run().z * speed;
                    moveVector = moveVector * Time.deltaTime;
                }
            }
            else
                anim.SetBool("run", false);

            if (Input.GetKeyDown(KeyCode.J) || gestureController.isJumpActive && !isHookEnabled)
            {
                anim.SetBool("jump", true);
                moveVector = transform.TransformDirection(Vector3.up) * 2;
            }
            else
                anim.SetBool("jump", false);

            transform.Translate(moveVector);
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Apple")) {
            collision.collider.gameObject.GetComponent<AppleScript>().setPlayerHit = true;
        }

        if (collision.collider.gameObject.CompareTag("Kiwi")) {
            collision.collider.gameObject.GetComponent<KiwiScript>().setPlayerHit = true;
        }

        if (collision.collider.gameObject.CompareTag("Banana")) {
            collision.collider.gameObject.GetComponent<BananaScript>().setPlayerHit = true;
        }

        if (collision.collider.gameObject.CompareTag("BananaOpened")) {
            collision.collider.gameObject.GetComponent<BananaOpenedScript>().setPlayerHit = true;
        }

        if (collision.collider.gameObject.CompareTag("BananaPeeled")) {
            collision.collider.gameObject.GetComponent<BananaPeeledScript>().setPlayerHit = true;
        }

        if (collision.collider.gameObject.CompareTag("Pineapple")) {
            collision.collider.gameObject.GetComponent<PineappleScript>().setPlayerHit = true;
        }

        if (collision.collider.gameObject.CompareTag("Strawberry")) {
            collision.collider.gameObject.GetComponent<StrawberryScript>().setPlayerHit = true;
        }

        if (collision.collider.gameObject.CompareTag("Thorn")) {
            collision.collider.gameObject.GetComponent<ThornScript>().setPlayerHit = true;
        }

        if (collision.collider.gameObject.CompareTag("Star")) {
            GetComponents<AudioSource>()[0].Play();
            collision.collider.gameObject.GetComponent<StarScript>().setPlayerHit = true;
        }

        if (collision.collider.gameObject.CompareTag("Pear")) {
            GetComponents<AudioSource>()[4].Play();
            collision.collider.gameObject.GetComponent<PearScript>().setPlayerHit = true;
        }
    }

    IEnumerator EnableGodEffectAfter(float time)
    {
        yield return new WaitForSeconds(time);
        isGodEffect = true;
        directionalLight.SetActive(false);
        GetComponents<AudioSource>()[7].Pause();
        GetComponents<AudioSource>()[5].Play();
        StartCoroutine(DiableGodEffectAfter(10));
    }

    IEnumerator DiableGodEffectAfter(float time)
    {
        yield return new WaitForSeconds(time);
        isGodEffect = false;
        //God.SetActive(false);
        GetComponents<AudioSource>()[7].Play();
        GetComponents<AudioSource>()[5].Stop();
        directionalLight.SetActive(true);
    }
    
    IEnumerator ResetPlayerCollider(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1f, 0.16f);
        gameObject.GetComponent<CapsuleCollider>().height = 2.0f;
    }

    IEnumerator EndSceneAfter(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
