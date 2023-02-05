using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ActionButtonUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        [SerializeField] private Button button;
        [SerializeField] private GameObject selectedGameObject;
    
        private BaseAction baseAction;
        public void SetBaseAction(BaseAction baseAction)
        {
            this.baseAction = baseAction;
            textMeshProUGUI.text = baseAction.GetActionName().ToUpper();
            button.onClick.AddListener(() =>
            {
                UnitActionSystem.Instance.SetSelectedAction(baseAction);
            });
        }

        public void UpdateSelectedVisual()
        {
            BaseAction selectedBaseAction = UnitActionSystem.Instance.GetSelectedAction();
            selectedGameObject.SetActive(selectedBaseAction == baseAction);
        }


    
    }
}
