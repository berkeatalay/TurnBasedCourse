using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Unit : MonoBehaviour
{

   [SerializeField] private Animator unitAnimator;
   private Vector3 _targetPosition;
   private GridPosition _currentGridPosition;

   private void Awake()
   {
      _targetPosition = transform.position;
   }

   private void Start()
   {
      _currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
      LevelGrid.Instance.SetUnitAtGridPosition(_currentGridPosition, this);
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
      
      GridPosition newGridPosition = _currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

      if (newGridPosition != _currentGridPosition)
      {
         Debug.Log("new" + newGridPosition);
         Debug.Log("current" +_currentGridPosition);
         LevelGrid.Instance.UnitMovedGridPosition(this, _currentGridPosition, newGridPosition);
         _currentGridPosition = newGridPosition;
      }

   }
   
   

   public void Move(Vector3 targetPosition)
   {
      _targetPosition = targetPosition;
   }
}
