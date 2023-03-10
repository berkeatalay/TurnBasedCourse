using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    private float _totalSpinAmount;
    

    private void Update()   
    {
        if (!IsActive) return;
        
        float spinAddAmount = 360 * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
        _totalSpinAmount += spinAddAmount;

        if (_totalSpinAmount >= 360f)
        {
            ActionComplete();
        }

    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);
        _totalSpinAmount = 0f;
    }
    
    public override string GetActionName()
    {
        return "Spin";
    }

    public override List<GridPosition> GetValidActionPositions()
    {
        GridPosition unitGridPosition = Unit.GetGridPosition();
        return new List<GridPosition>()
        {
            unitGridPosition
        };

    }

    public override int GetActionPointsCost()
    {
        return 0;
    }
}
