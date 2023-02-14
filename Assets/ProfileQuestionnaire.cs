using Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class ProfileQuestionnaire : MonoBehaviour
{
    [SerializeField]
    private TypeWriter typeWriter;

    [SerializeField]
    private ProfileInputField _inputField;

    [SerializeField]
    private GameObject _promptObject;

    private int _questionIndex = -1;

    EventCall _receivePlayerNameCallback;
    EventCall _receiveNumberCallback;
    EventCall _receiveFinalResponseCallback;

    EventCall[] _events;

    private readonly string[] Questions =
    {
        "What is your name?",
        "What's your favorite number? Pick between 1 and 100",
        "If you had the chance to change one's faith," +
            " even if they've done wrong," +
            " even if it puts you at risk," +
            " would you do it?"
    };

    private void Awake()
    {
        SetupEvents();
    }

    private void OnEnable()
    {
        typeWriter.onEffectComplete = OnComplete;
        NextQuestion();
    }

    public void OnComplete()
    {
        _events[_questionIndex].Trigger();
        _events[_questionIndex].Reset();
    }

    private void SetupEvents()
    {
        TMP_InputField.SubmitEvent submitName = new TMP_InputField.SubmitEvent();
        TMP_InputField.SubmitEvent submitNumber = new TMP_InputField.SubmitEvent();

        submitName.AddListener(CalculateStateInfluencePercentage);
        submitNumber.AddListener(EnhanceStat);

        _receivePlayerNameCallback = EventManager.AddEvent(9000, string.Empty, () => EnableInputField(7, submitName));
        _receiveNumberCallback = EventManager.AddEvent(9001, string.Empty, () => EnableInputField(3, submitNumber));
        _receiveFinalResponseCallback = EventManager.AddEvent(9002, string.Empty, EnablePromptField);

        _events = new EventCall[]
        {
            _receivePlayerNameCallback,
            _receiveNumberCallback,
            _receiveFinalResponseCallback
        };
    }

    private void EnableInputField(int maxCharacters, TMP_InputField.SubmitEvent onSubmit)
    {
        _inputField.gameObject.Enable();
        _inputField.characterLimit = maxCharacters;
        _inputField.onSubmit = onSubmit;
        _inputField.Select();
    }

    private void DisableInputField()
    {
        _inputField.text = string.Empty;
        _inputField.onSubmit = null;
        _inputField.gameObject.Disable();
    }

    private void EnablePromptField()
    {
        _promptObject.Enable();
    }

    private void DisablePromptField()
    {
        _promptObject.Disable();
    }

    private void NextQuestion()
    {
        _questionIndex++;
        typeWriter.SetInput(Questions[_questionIndex]);
        typeWriter.StartEffect();
    }

    private void Clear()
    {
        typeWriter.Clear();
        _receivePlayerNameCallback = null;
        _receiveNumberCallback = null;
        _receiveFinalResponseCallback = null;
    }

    #region Player Stat Modifier Methods
    private void CalculateStateInfluencePercentage(string input)
    {
        Clear();
        DisableInputField();
        // TODO: Do all the calculation stuff that you are suppose to do.
        NextQuestion();
    }

    private void EnhanceStat(string input)
    {
        Clear();
        DisableInputField();
        // TODO: Parse input (1 - 100) to StatVariable type
        NextQuestion();
    }

    public void ApplyTrustAmplifier()
    {
        DisablePromptField();
        // TODO: Add increase to stat
        Clear();
    } 
    #endregion
}
