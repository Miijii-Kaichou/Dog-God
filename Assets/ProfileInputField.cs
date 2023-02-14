using Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ProfileInputField : MonoBehaviour
{
    internal int characterLimit;
    internal TMP_InputField.SubmitEvent onSubmit;
    internal string text;

    private int _previousCharacterSlotIndex = 0;
    [SerializeField] private int _characterSlotIndex = 0;
    private CharacterSlot[] characterSlots;
    private const int MaxCharacters = 12;

    internal void Select()
    {
        var index = -1;
        foreach(var slot in characterSlots)
        {
            index++;
            if (index < characterLimit)
            {
                slot.gameObject.Enable();
                slot.Clear();
                continue;
            }
            slot.gameObject.Disable();
        }
        _characterSlotIndex = 0;

        StartCoroutine(InputCycle());
    }

    private void OnEnable()
    {
        characterSlots ??= GetComponentsInChildren<CharacterSlot>();
        foreach (var slot in characterSlots) { slot.gameObject.Disable(); }     
    }

    void NextCharacter()
    {
        
        _previousCharacterSlotIndex = _characterSlotIndex;
        if (_characterSlotIndex == characterLimit - 1) return;
        _characterSlotIndex++;
        SelectSlot();
    }

    void PreviousCharacter()
    {
        _previousCharacterSlotIndex = _characterSlotIndex;
        if (_characterSlotIndex == 0) return;
        _characterSlotIndex--;
        SelectSlot();
    }

    void SelectSlot()
    {
        characterSlots[_characterSlotIndex].Select();

        if (_characterSlotIndex == _previousCharacterSlotIndex) return;
        characterSlots[_previousCharacterSlotIndex].Deselect();
    }

    IEnumerator InputCycle()
    {
        SelectSlot();
        while(true)
        {
            ReadKeyInput();
            ReadDirectionInput();
            yield return null;
        }
    }

    void ReadKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            RemoveAndStepBack();
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            FeedAndGo(' ');
            return;
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            Process();
            onSubmit.Invoke(text);
            return;
        }

        if(Input.anyKeyDown && Input.inputString.Length != 0)
        {
            FeedAndGo(Input.inputString[0]);
            return;
        }
    }

    private void Process()
    {
        StringBuilder builder = new StringBuilder();
        for(int i = 0; i < characterLimit; i++)
        {
            var slot = characterSlots[i];
            if (i > characterLimit - 1) continue;
            builder.Append(slot.Text);
        }
        text = builder.ToString().ToLower();
    }

    private void FeedAndGo(char input)
    {
        characterSlots[_characterSlotIndex].Enter(input, out bool result);
        if (result == false) return;
        NextCharacter();
    }

    private void RemoveAndStepBack()
    {
        characterSlots[_characterSlotIndex].Clear();
        PreviousCharacter();
    }

    private void ReadDirectionInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) PreviousCharacter();
        if (Input.GetKeyDown(KeyCode.RightArrow)) NextCharacter();
    }
}
