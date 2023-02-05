using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Unit : MonoBehaviour
{

   private const int ActionPointMax = 2;

   public static event EventHandler OnAnyActionPointsChanged;
   
   private GridPosition _currentGridPosition;
   private MoveAction _moveAction;
   private SpinAction _spinAction;
   private BaseAction[] _baseActionArray;

   private int actionPoints = 2;

   private void Awake()
   {
      _moveAction = GetComponent<MoveAction>();
      _spinAction = GetComponent<SpinAction>();
      _baseActionArray = GetComponents<BaseAction>();
   }


   private void Start()
   {
      _currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
      LevelGrid.Instance.AddUnitAtGridPosition(_currentGridPosition, this);

      TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
   }

   private void Update()
   {

      GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

      if (newGridPosition != _currentGridPosition)
      {
         LevelGrid.Instance.UnitMovedGridPosition(this, _currentGridPosition, newGridPosition);
         _currentGridPosition = newGridPosition;
      }

   }

   public MoveAction GetMoveAction()
   {
      return _moveAction;
   }

   public SpinAction GetSpinAction()
   {
      return _spinAction;
   }

   public GridPosition GetGridPosition()
   {
      return _currentGridPosition;
   }

   public BaseAction[] GetBaseActionArray()
   {
      return _baseActionArray;
   }

   public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
   {
      if (!CanSpendActionPointsToTakeAction(baseAction)) return false;
      
      SpendActionPoints(baseAction.GetActionPointsCost());

      return true;
   }

   public bool CanSpendActionPointsToTakeAction(BaseAction baseAction)
   {
      return (actionPoints >= baseAction.GetActionPointsCost());
   }

   private void SpendActionPoints(int amount)
   {
      actionPoints -= amount;
      OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
   }

   public int GetActionPoints()
   {
      return actionPoints;
   }

   private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
   {
      actionPoints = ActionPointMax;
      OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);

   }


}
