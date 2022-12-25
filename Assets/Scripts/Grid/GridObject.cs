
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridPosition _gridPosition;
    private GridSystem _gridSystem;
    private Unit _unit;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        _gridSystem = gridSystem;
        _gridPosition = gridPosition;
    }
    
    public override string ToString()
    {
        return _gridPosition.ToString() + "\n" + _unit;
    }

    public void SetUnit(Unit unit)
    {
        _unit = unit;
    }
    public Unit GetUnit()
    {
        return _unit;
    }
}
