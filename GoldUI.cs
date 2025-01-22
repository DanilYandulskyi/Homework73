using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GoldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    
    public void UpdateText(int goldAmount)
    {
        _text.text = $"Gold - {goldAmount}";
    }
}
