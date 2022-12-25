using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
   [SerializeField] private Transform gridDebugObjectPrefab;
   private GridSystem _gridSystem;

   private void Awake()
   {
      _gridSystem = new GridSystem(10, 10,2f);
      _gridSystem.CreateDebugObject(gridDebugObjectPrefab);
   }
}
