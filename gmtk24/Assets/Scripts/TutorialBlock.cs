using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBlock : MonoBehaviour
{
    private Animator animator;
    private RectTransform rt;
    // Start is called before the first frame update
    void Start() {
        animator = gameObject.GetComponent<Animator>();
        rt = GetComponent<RectTransform>();
    }

    public void ValidateSwipe() {
        Camera.main.GetComponent<GameManager>().BeginButton();
        Toss();
    }

    void Toss() {
        animator.SetTrigger("Toss");
    }
}
