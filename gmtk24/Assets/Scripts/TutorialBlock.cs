using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBlock : MonoBehaviour
{
    private Camera mainCamera;
    private Animator animator;
    private RectTransform rt;
    // Start is called before the first frame update
    void Start() {
        animator = gameObject.GetComponent<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rt = GetComponent<RectTransform>();
    }

    public void ValidateSwipe() {
        mainCamera.GetComponent<GameManager>().BeginButton();
        Toss();
    }

    void Toss() {
        animator.SetTrigger("Toss");
    }
}
