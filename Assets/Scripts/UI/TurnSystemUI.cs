using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace UI
{
    public class TurnSystemUI : MonoBehaviour
    {
        [SerializeField] private Button endTurnButton;
        [SerializeField] private TextMeshProUGUI turnButtonText;
        [SerializeField] private GameObject enemyTurnVisualObject;

        private void Start()
        {
            endTurnButton.onClick.AddListener(() =>
            {
                TurnSystem.Instance.NextTurn();
            });
            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
            UpdateTurnNumberText();
            UpdateEnemyTurnVisual();
            UpdateEndTurnButtonVisibility();
        }

        private void UpdateTurnNumberText()
        {
            turnButtonText.text = "TURN " + TurnSystem.Instance.GetTurnNumber();

        }
        
        private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
        {
            UpdateTurnNumberText();
            UpdateEnemyTurnVisual();
            UpdateEndTurnButtonVisibility();

        }

        private void UpdateEnemyTurnVisual()
        {
            enemyTurnVisualObject.SetActive(!TurnSystem.Instance.IsPlayerTurn());
        }
        
        private void UpdateEndTurnButtonVisibility()
        {
            endTurnButton.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
        }
    }
}
