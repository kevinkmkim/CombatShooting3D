using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustAnimationPosition : MonoBehaviour
{
    [SerializeField] private float adjustAmount;

    #region Properties
    private Animator animator;
    private Vector3 initPosition;
    private float nextNormalizedTime = 1.0f;
    #endregion

    void Start()
    {
        initPosition = GetComponent<Transform>().position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= nextNormalizedTime) {
            nextNormalizedTime++;
            GetComponent<Transform>().position = initPosition;
            Debug.Log("looped");
        }
        else 
        {
            GetComponent<Transform>().position -= new Vector3(0f,adjustAmount,0f);
        }
    }
}
