
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _amountText;

    public ResourceType Type { get; private set; }

    public void Set(ResourceType type, Sprite sprite, uint amount)
    {
        Type = type;
        _icon.sprite = sprite;
        _icon.enabled = sprite != null;
        _amountText.text = amount.ToString();
        gameObject.SetActive(amount > 0);
    }

    public void SetAmount(uint amount)
    {
        _amountText.text = amount.ToString();
        gameObject.SetActive(amount > 0);
    }
}


