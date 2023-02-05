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

        private void Start()
        {
            endTurnButton.onClick.AddListener(() =>
            {
                TurnSystem.Instance.NextTurn();
            });
            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
            UpdateTurnNumberText();

        }

        private void UpdateTurnNumberText()
        {
            turnButtonText.text = "TURN " + TurnSystem.Instance.GetTurnNumber();

        }
        
        private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
        {
            UpdateTurnNumberText();
        }
    }
}
