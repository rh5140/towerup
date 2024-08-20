using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPS : MonoBehaviour
{
    private float lifetime = 1.5f;

    void Awake()
    {
        AudioSource audioSrc = gameObject.GetComponent<AudioSource>();
        audioSrc.clip = Camera.main.GetComponent<AudioManager>().Meow();
        audioSrc.Play();
        StartCoroutine(CleanUp());
    }

    IEnumerator CleanUp() {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
