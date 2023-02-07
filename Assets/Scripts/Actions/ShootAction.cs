using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShootAction : BaseAction
{
    [Header("Timer Settings")]
    [SerializeField] private float shootingStateTimer = .1f;
    [SerializeField] private float coolOffStateTimer = .5f;
    [SerializeField] private float aimingStateTimer = 1f;    
    
    [Header("Action Settings")]
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private int maxShootDistance = 7;
    
    private State _state;
    private bool _canShoot;
    private Unit _targetUnit;
    private float _stateTimer;

    private void Update()   
    {
        if (!IsActive) return;

        _stateTimer -= Time.deltaTime;
        switch (_state)
        {
            case State.Aiming:
                Turn();
                break;
            case State.Shooting:
                if (_canShoot)
                {
                    Shoot();
                    _canShoot = false;
                }
                break;
            case State.CoolOff:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (_stateTimer <= 0f)
        {
            NextState();
        }

    }

    private void Shoot()
    {
        _targetUnit.Damage();
    }
    
    private void Turn()
    {
        Vector3 aimDirection = (_targetUnit.GetWorldPosition() - Unit.GetWorldPosition()).normalized; 
        transform.forward = Vector3.Lerp(transform.forward,aimDirection,Time.deltaTime * rotateSpeed);
    }

    private void NextState()
    {
        switch (_state)
        {
            case State.Aiming:
                if (_stateTimer <= 0f)
                {
                    _state = State.Shooting;
                    _stateTimer = shootingStateTimer;
                }
                break;
            case State.Shooting:
                if (_stateTimer <= 0f)
                {
                    _state = State.CoolOff;
                    _stateTimer = coolOffStateTimer;
                }
                break;
            case State.CoolOff:
                if (_stateTimer <= 0f)
                {
                    ActionComplete();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override string GetActionName()
    {
        return "Shoot";
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);

        _targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
        
        _stateTimer = aimingStateTimer;
        _state = State.Aiming;

        _canShoot = true;
    }

    public override List<GridPosition> GetValidActionPositions()
    {
        var validGridPositions = new List<GridPosition>();
        GridPosition unitGridPosition = Unit.GetGridPosition();
        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > maxShootDistance) continue;
                
                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) continue;

                Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);
                
                if (targetUnit.IsEnemy() == Unit.IsEnemy()) continue; // Both units are in same team since we are using same logic for enemy & player units
                
                validGridPositions.Add(testGridPosition);

            }
        }

        return validGridPositions;
    }

    private enum  State 
    {
        Aiming,
        Shooting,
        CoolOff
    }
}