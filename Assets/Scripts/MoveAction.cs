using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private int maxDistance = 4;

    private Vector3 _targetPosition;
    private Unit _unit;
    private void Awake()
    {
        _unit = GetComponent<Unit>();
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
    
    public void Move(GridPosition targetGridPosition)
    {
        _targetPosition = LevelGrid.Instance.GetWorldPosition(targetGridPosition);
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositions = GetValidActionPositions();
        return validGridPositions.Contains(gridPosition);
    }
    public List<GridPosition> GetValidActionPositions()
    {
        List<GridPosition> validGridPositions = new List<GridPosition>();
        GridPosition unitGridPosition = _unit.GetGridPosition();
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
}
 