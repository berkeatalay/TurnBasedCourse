    using System;
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class GridSystemVisual : MonoBehaviour
    {
        
        public static GridSystemVisual Instance { get; private set; }
        
        [SerializeField] private Transform gridSystemVisualSinglePrefab;

        private GridSystemVisualSingle[,] _gridSystemVisualSingles;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than one GridSystemVisual " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            _gridSystemVisualSingles = new GridSystemVisualSingle[
                LevelGrid.Instance.GetWidth(),
                LevelGrid.Instance.GetHeight()];
            
            GameObject parentCell = new GameObject("GridCellVisuals");
            for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
            {
                for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
                {
                    GridPosition gp = new(x, z);
                    Transform gridSystemVisualSingeTransform = Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gp),
                        Quaternion.identity, parentCell.transform);
                    
                    _gridSystemVisualSingles[x, z] =
                        gridSystemVisualSingeTransform.GetComponent<GridSystemVisualSingle>();
                }
            }
        }

        private void Update()
        {
            updateGridPositionList();
        }

        public void HideAllGridPosition()
        {
            foreach (GridSystemVisualSingle gridSystemVisualSingle in _gridSystemVisualSingles)
            {
                gridSystemVisualSingle.Hide();
            }
        }

        public void ShowGridPositionList(List<GridPosition> gridPositionsList)
        {
            foreach (GridPosition gridPosition in gridPositionsList)
            {
                _gridSystemVisualSingles[gridPosition.x, gridPosition.z].Show();
            }
        }
        
        public void updateGridPositionList()
        {
            HideAllGridPosition();

            Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
            
            ShowGridPositionList(selectedUnit.GetMoveAction().GetValidActionPositions());
        }


    }
