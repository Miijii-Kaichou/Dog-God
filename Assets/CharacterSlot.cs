using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _character;
    public string Text => _character.text;

    [SerializeField]
    private Image background;

    private Color _default;
    [SerializeField] private Color _unselectedColor;
    [SerializeField] private Color _selectedColor;

    private void Awake()
    {
        _character ??= GetComponentInChildren<TextMeshProUGUI>();
        background ??= GetComponentInChildren<Image>();

        _default = background.color;
        Deselect();
    }

    public void Select()
    {
        background.color = _selectedColor;
    }

    public void Deselect()
    {
        background.color = _unselectedColor;
    }

    public void Clear()
    {
        _character.text = string.Empty;
    }

    internal void Enter(char input, bool allowLetter, bool allowNumber, out bool result)
    {
        result = ((char.IsLetter(input) || input == ' ') && allowLetter) || (char.IsDigit(input) && allowNumber);
        if (result == false) return;
        _character.text = input.ToString();
    }
}
