using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private float _timer;


    private void Start()
    {
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

    }

    private void Update()
    {

        if (TurnSystem.Instance.IsPlayerTurn()) return;

        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            TurnSystem.Instance.NextTurn();
        }

    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        _timer = 2f;
    }
    
}
