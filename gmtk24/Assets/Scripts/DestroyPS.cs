using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPS : MonoBehaviour
{
    private float lifetime = 0.5f;

    void Awake()
    {
        StartCoroutine(CleanUp());
    }

    IEnumerator CleanUp() {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
