using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
   
   public static LevelGrid Instance { get; private set; }

   [SerializeField] private Transform gridDebugObjectPrefab;
   
   private GridSystem _gridSystem;

   private void Awake()
   {
      if (Instance != null)
      {
         Debug.LogError("There is more than one UnitActionSystem " + transform + " - " + Instance);
         Destroy(gameObject);
         return;
      }
      Instance = this;
      
      _gridSystem = new GridSystem(10, 10,2f);
      _gridSystem.CreateDebugObject(gridDebugObjectPrefab);
   }
   
   public void SetUnitAtGridPosition(GridPosition gridPosition, Unit unit)
   {
      GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
      gridObject.SetUnit(unit);
      
   }

   public Unit GetUnitAtGridPosition(GridPosition gridPosition)
   {
      GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
      return gridObject.GetUnit();
   }

   public void ClearUnitAtGridPosition(GridPosition gridPosition)
   {
        GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
        gridObject.SetUnit(null);
   }

   public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
   {
      ClearUnitAtGridPosition(fromGridPosition);
      SetUnitAtGridPosition(toGridPosition,unit);
   }

   public GridPosition GetGridPosition(Vector3 worldPosition) => _gridSystem.GetGridPosition(worldPosition);
}
