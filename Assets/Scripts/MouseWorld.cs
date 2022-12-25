using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{

    private static MouseWorld _instance;
    
    [SerializeField] private LayerMask mousePlaneLmMask;
    private static Camera _camera;
    

    private void Awake()
    {
        _instance = this;
        _camera = Camera.main;
    }
 
    public static Vector3 GetPosition()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray,out RaycastHit raycastHit, float.MaxValue, _instance.mousePlaneLmMask);
        return raycastHit.point;
    }
}
