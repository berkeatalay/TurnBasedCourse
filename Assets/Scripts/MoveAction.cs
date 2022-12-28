using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    
    [SerializeField] private Animator unitAnimator;

    private Vector3 _targetPosition;

    private void Awake()
    {
        _targetPosition = transform.position;
    }

    private void Update()
    {
        const float stoppingDistance = .1f;         

        const float moveSpeed = 4f;
        const float rotateSpeed = 10f;

        if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
        {      
            unitAnimator.SetBool("isWalking",true);
            Vector3 moveDirection = (_targetPosition - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward,moveDirection,Time.deltaTime * rotateSpeed);
            transform.position += moveDirection * (Time.deltaTime * moveSpeed);
        }
        else
        {
            unitAnimator.SetBool("isWalking",false);

        }
    }


    public void Move(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }
}
 