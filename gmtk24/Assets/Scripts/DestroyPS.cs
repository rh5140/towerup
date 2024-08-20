using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPS : MonoBehaviour
{
    private Camera mainCamera;
    private float lifetime = 1.5f;

    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        AudioSource audioSrc = gameObject.GetComponent<AudioSource>();
        audioSrc.clip = mainCamera.GetComponent<AudioManager>().Meow();
        audioSrc.Play();
        StartCoroutine(CleanUp());
    }

    IEnumerator CleanUp() {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
