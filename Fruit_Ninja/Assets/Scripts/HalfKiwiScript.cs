using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfKiwiScript : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        StartCoroutine(DestroyAfter(3f));
    }

    IEnumerator DestroyAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
