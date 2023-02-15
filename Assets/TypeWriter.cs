#nullable enable

using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public sealed class TypeWriter : MonoBehaviour
{
    internal Action? onEffectComplete;
    
    [SerializeField, Header("Text Input")]
    private string _textInput;

    [SerializeField, Header("Text Output")]
    private TextMeshProUGUI _targetOutput;

    [Header("Configurations")]
    [SerializeField] private bool _playOnAwake = true;
    [SerializeField] private bool _allowSkipOnInput = false;
    [SerializeField] private float _rate = 0.1f;
    [SerializeField] private string _typeAudioName;

    private bool _active = false;
    private int _textIndex = -1;
    internal int delay;

    private bool IsComplete => _targetOutput.text == _textInput;

    internal void SetInput(string input)
    {
        _textInput = input;
    }

    internal void StartEffect()
    {
        if (delay != 0)
        {
            var alarm = new Alarm(1);
            alarm.SetFor(delay, 0, true, DoTypeWriterEffect);
            return;
        }
        DoTypeWriterEffect();
    }

    private void DoTypeWriterEffect()
    {
        _active = true;

        // Empty target text if it hasn't already...
        _textIndex = -1;
        _targetOutput.text = string.Empty;

        if (_allowSkipOnInput) StartCoroutine(KeyInputListenerCycle());
        StartCoroutine(TypeWriterCycle());
    }

    private void Awake()
    {
        _targetOutput ??= GetComponent<TextMeshProUGUI>();
        if (_playOnAwake == false) return;
        StartEffect();
    }


    private void Next()
    {
        _textIndex++;
    }

    private void UpdateTargetText()
    {
        _targetOutput.text = _textInput[..(_textIndex)];
    }

    private void ReadAllText()
    {
        _textIndex = _textInput.Length - 1;
        UpdateTargetText();
    }

    private void PlayAudio()
    {
        if (_typeAudioName == null) return;
    }

    IEnumerator TypeWriterCycle()
    {
        while(_active)
        {
            Next();
            UpdateTargetText();
            PlayAudio();
            yield return new WaitForSeconds(_rate);
            if(IsComplete)
            {
                // TODO: Have event trigger or something
                onEffectComplete?.Invoke();
                _active = false;
            }
        }
    }

    IEnumerator KeyInputListenerCycle()
    {
        while(_active)
        {
            if (Input.anyKeyDown && IsComplete == false)
                ReadAllText();
            yield return new WaitForSeconds(0.1f);
        }
    }

    internal void Clear()
    {
        _textIndex = -1;
        _textInput = string.Empty;
        _targetOutput.text = _textInput;
    }

    internal void ChangeFontSize(int size)
    {
        _targetOutput.fontSize = size;
    }
}
