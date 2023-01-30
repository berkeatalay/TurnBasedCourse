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
            IsActive = false;
            OnActionComplete();
        }

    }

    public void Spin(Action onActionComplete)
    {
        OnActionComplete = onActionComplete;
        IsActive = true;
        _totalSpinAmount = 0f;
    }
}
