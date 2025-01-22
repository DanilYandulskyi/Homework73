using UnityEngine;

[RequireComponent(typeof(GoldUI))]
public class GoldUIView : MonoBehaviour
{
    [SerializeField] private Base _base;

    private GoldUI _ui;

    private void Awake()
    {
        _ui = GetComponent<GoldUI>();
        _ui.UpdateText(0);
    }

    private void OnEnable()
    {
        _base.GoldAmountChanged += IncreaseGoldAmount;
    }

    private void IncreaseGoldAmount()
    {
        _ui.UpdateText(_base.CollectedResounrseAmount);
    }
}
