using DG.Tweening;
using Extensions;
using System;
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

    [Header("Tweening Effects (Question)")]
    [SerializeField] private FadeInTween _fadeInTween;
    [SerializeField] private FadeOutTween _fadeOutTween;
    [SerializeField] private TranslationTween _translationTween;

    [Header("Tweening Effects (ProfileInputField)")]
    [SerializeField] private CanvasGroupFadeInTween _inputFieldFadeInTween;
    [SerializeField] private CanvasGroupFadeOutTween _inputFieldFadeOutTween;
    [SerializeField] private TranslationTween _inputFieldTranslationTween;

    [Header("Tweening Effects (Prompt)")]
    [SerializeField] private CanvasGroupFadeInTween _promptFadeInTween;
    [SerializeField] private CanvasGroupFadeOutTween _promptFadeOutTween;
    [SerializeField] private TranslationTween _promptTranslationTween;

    private int _questionIndex = -1;

    private EventCall _receivePlayerNameCallback;
    private EventCall _receiveNumberCallback;
    private EventCall _receiveFinalResponseCallback;

    private EventCall[] _events;

    private readonly string[] Questions =
    {
        "What is your name?",
        "What's your favorite number? Pick between 0 and 99",
        "If you had the chance to change one's faith," +
            " even if they've done wrong," +
            " even if it puts you at risk," +
            " would you do it?",
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
        TMP_InputField.SubmitEvent submitName = new();
        TMP_InputField.SubmitEvent submitNumber = new();

        submitName.AddListener(CalculateStateInfluencePercentage);
        submitNumber.AddListener(EnhanceStat);

        _receivePlayerNameCallback = EventManager.AddEvent(9000, string.Empty, () => EnableInputField(7, submitName));
        _receiveNumberCallback = EventManager.AddEvent(9001, string.Empty, () => EnableInputField(2, submitNumber));
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
        _inputField.allowNumbers = false;
        _inputField.allowLetters = false;

        _inputField.allowLetters = _questionIndex == 0;
        _inputField.allowNumbers = _questionIndex == 1;

        _inputField.characterLimit = maxCharacters;
        _inputField.onSubmit = onSubmit;
        _inputField.Select();
        
        _inputFieldFadeInTween.DoFadeInTweening();
        _inputFieldTranslationTween.DoTranslationTweeningTo();
    }

    private void DisableInputField()
    {
        _inputFieldFadeOutTween.DoFadeOutTweening();
        _inputFieldTranslationTween.DoTranslationTweeningReturn();
        _inputFieldFadeOutTween.OnEffectComplete = () =>
        {
            _inputField.text = string.Empty;
            _inputField.onSubmit = null;
            _inputField.gameObject.Disable();
        };
    }

    private void EnablePromptField()
    {
        _promptObject.Enable();
        _promptFadeInTween.DoFadeInTweening();
        _promptTranslationTween.DoTranslationTweeningTo();
    }

    private void DisablePromptField()
    {
        _promptFadeOutTween.DoFadeOutTweening();
        _promptTranslationTween.DoTranslationTweeningReturn();
        _promptFadeOutTween.OnEffectComplete = () =>
        {
            _promptObject.Disable();
        };
    }

    private void NextQuestion()
    {
        _questionIndex++;
        _translationTween.DoTranslationTweeningReturn();
        _fadeOutTween.DoFadeOutTweening();
        _fadeOutTween.OnEffectComplete = () =>
        {
            _translationTween.DoTranslationTweeningTo();
            _fadeInTween.DoFadeInTweening(); 
            Clear();
            if (_questionIndex > Questions.Length - 1) return;
            typeWriter.SetInput(Questions[_questionIndex]);
            typeWriter.StartEffect();
        };
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
        DisableInputField();
        // TODO: Do all the calculation stuff that you are suppose to do.
        GameManager.PlayerName = input;



        NextQuestion();
    }

    private void EnhanceStat(string input)
    {
        DisableInputField();
        // TODO: Parse input (1 - 100) to StatVariable type
        var number = Convert.ToInt32(input);
        NextQuestion();
    }

    public void ApplyTrustAmplifier()
    {
        DisablePromptField();
        _translationTween.to = 130;
        _translationTween.DoTranslationTweeningTo();
        _fadeOutTween.DoFadeOutTweening();
        _fadeOutTween.OnEffectComplete = Clear;

        // TODO: Add increase to stat
    } 
    #endregion
}
