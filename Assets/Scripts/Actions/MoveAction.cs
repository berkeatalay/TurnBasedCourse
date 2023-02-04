using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private int maxDistance = 4;

    private Vector3 _targetPosition;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    
    protected override void Awake()
    {
        base.Awake();
        _targetPosition = transform.position;
    }

    private void Update()
    {
        if (!IsActive) return;
        const float stoppingDistance = .1f;         

        const float moveSpeed = 4f;
        const float rotateSpeed = 10f;
        Vector3 moveDirection = (_targetPosition - transform.position).normalized;

        if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
        {      
            unitAnimator.SetBool(IsWalking,true);
            transform.position += moveDirection * (Time.deltaTime * moveSpeed);
        }
        else
        {
            unitAnimator.SetBool(IsWalking,false);
            IsActive = false;
            OnActionComplete();
        }
        transform.forward = Vector3.Lerp(transform.forward,moveDirection,Time.deltaTime * rotateSpeed);

    }
    
    public override void TakeAction(GridPosition targetGridPosition, Action onActionComplete)
    {
        OnActionComplete = onActionComplete;
        _targetPosition = LevelGrid.Instance.GetWorldPosition(targetGridPosition);
        IsActive = true;
    }
    
    public override List<GridPosition> GetValidActionPositions()
    {
        List<GridPosition> validGridPositions = new List<GridPosition>();
        GridPosition unitGridPosition = Unit.GetGridPosition();
        for (int x = -maxDistance; x <= maxDistance; x++)
        {
            for (int z = -maxDistance; z <= maxDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;
                if (unitGridPosition == testGridPosition) continue;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) continue;
                
                validGridPositions.Add(testGridPosition);

            }
        }

        return validGridPositions;
    }

    public override string GetActionName()
    {
        return "Move";
    }
}
 