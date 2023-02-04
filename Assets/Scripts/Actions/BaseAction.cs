using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected Unit Unit;
    protected bool IsActive;
    protected Action OnActionComplete;

    protected virtual void Awake()
    {
        Unit = GetComponent<Unit>();
    }

    public abstract string GetActionName();
    public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

    public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositions = GetValidActionPositions();
        return validGridPositions.Contains(gridPosition);
    }

    public abstract List<GridPosition> GetValidActionPositions();

    public virtual int GetActionPointsCost()
    {
        return 1;
    }

}
