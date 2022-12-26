using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private  float moveSpeed = 10f;
    [SerializeField] private  float rotationSpeed = 100f;
    [SerializeField] private  float zoomAmount = 1f;
    [SerializeField] private  float zoomSpeed = 5f;
    private const float MinFollowUOffset = 2f;
    private const float MaxFollowUOffset = 12f;
    private Vector3 _targetFollowOffset;
    private CinemachineTransposer _cinemachineTransposer;
    
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        _cinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        _targetFollowOffset = _cinemachineTransposer.m_FollowOffset;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleMovement()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = 1f;
        }

        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += inputMoveDir * (moveSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = 1f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }


        transform.eulerAngles += rotationVector * (rotationSpeed * Time.deltaTime);
    }

    private void HandleZoom()
    {
        
        switch (Input.mouseScrollDelta.y)
        {
            case > 0:
                _targetFollowOffset.y -= zoomAmount;
                break;
            case < 0:
                _targetFollowOffset.y += zoomAmount;
                break;
        }

        _targetFollowOffset.y = Mathf.Clamp(_targetFollowOffset.y, MinFollowUOffset, MaxFollowUOffset);

        _cinemachineTransposer.m_FollowOffset = Vector3.Lerp(_cinemachineTransposer.m_FollowOffset, _targetFollowOffset, Time.deltaTime * zoomSpeed);

    }
}
