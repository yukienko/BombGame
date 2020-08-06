using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public bool isCatch;

    void Start()
    {
        animator = GetComponent<Animator>();
        isCatch = false;
    }

    void Update()
    {

    }

    //TapEffect.csからつかんでるときの処理をここに反映させてあげる
    public void Catch()
    {
        if (isCatch)
        {
            animator.SetBool("Catch", true);
        }
        else
        {
            animator.SetBool("Catch", false);
        }
    }
}
