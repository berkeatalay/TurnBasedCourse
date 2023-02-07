using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Unit : MonoBehaviour
{

   private const int ActionPointMax = 2;

   public static event EventHandler OnAnyActionPointsChanged;

   [SerializeField] private bool isEnemy;
   
   private GridPosition _currentGridPosition;
   private MoveAction _moveAction;
   private SpinAction _spinAction;
   private BaseAction[] _baseActionArray;

   private int _actionPoints = 2;

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
   
   public bool IsEnemy()
   {
      return isEnemy;
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
      return (_actionPoints >= baseAction.GetActionPointsCost());
   }

   private void SpendActionPoints(int amount)
   {
      _actionPoints -= amount;
      OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
   }

   public int GetActionPoints()
   {
      return _actionPoints;
   }

   private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
   {
      // ReSharper disable once InvertIf
      if ((isEnemy && !TurnSystem.Instance.IsPlayerTurn()) || (!isEnemy && TurnSystem.Instance.IsPlayerTurn()))
      {
         _actionPoints = ActionPointMax;
         OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
      }
      
   }

   public void Damage()
   {
      Debug.Log(transform + " damaged");
   }

   public Vector3 GetWorldPosition()
   {
      return transform.position;
   }


}
