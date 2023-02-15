using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

using Array = System.Array;

using static SharedData.Constants;

public sealed class ProfileQuestionnaire : MonoBehaviour
{
    [SerializeField]
    private TypeWriter _typeWriter;

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
    private EventCall _profileCreationCompleteionCallback;

    private EventCall[] _events;

    // Newly Created Stats for the player
    EntityStats _createdStats;

    private readonly string[] QuestionnaireContent =
    {
        "What is your name?",
        "What's your favorite number? Pick between 0 and 99",
        "If you had the chance to change one's faith," +
            " even if they've done wrong," +
            " even if it puts you at risk," +
            " would you do it?",
        "Your Existance Has Been Acknowledged"
    };

    private void Awake()
    {
        SetupEvents();
    }

    private void OnEnable()
    {
        _typeWriter.onEffectComplete = OnComplete;
        _createdStats = new EntityStats();
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
        _profileCreationCompleteionCallback = EventManager.AddEvent(9999, string.Empty, CompleteProfileCreationProcess);

        _events = new EventCall[]
        {
            _receivePlayerNameCallback,
            _receiveNumberCallback,
            _receiveFinalResponseCallback,
            _profileCreationCompleteionCallback
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
        _fadeOutTween.OnEffectComplete = FadeInAndDisplay;
    }

    private void FadeInAndDisplay()
    {
        // If we are a tthe last "question"
        if (_questionIndex == QuestionnaireContent.Length - 1)
        {
            _translationTween.from = 30;
            _translationTween.to = 0;

            _fadeInTween.delay = 3;
            _translationTween.delay = 3;
            _typeWriter.delay = 3;

            _typeWriter.ChangeFontSize(64); 
        }

        _translationTween.DoTranslationTweeningTo();
        _fadeInTween.DoFadeInTweening();
        Clear();

        if (_questionIndex > QuestionnaireContent.Length - 1) return;

        _typeWriter.SetInput(QuestionnaireContent[_questionIndex]);
        _typeWriter.StartEffect();
    }

    private void CompleteProfileCreationProcess()
    {
        var alarm = new Alarm(1);
        GameSceneManager.Prepare(SI_HeavensPlaza);
        alarm.SetFor(5f, 0, true, () =>
        {
            _translationTween.DoTranslationTweeningReturn();
            _fadeOutTween.DoFadeOutTweening();
            _fadeOutTween.OnEffectComplete = ClearAndStartGame;
        });
    }

    private void Clear()
    {
        _typeWriter.Clear();
        _receivePlayerNameCallback = null;
        _receiveNumberCallback = null;
        _receiveFinalResponseCallback = null;
    }

    private void ClearAndStartGame()
    {
        var activeIndex = GameManager.ActiveProfileIndex;
        Clear();
        StatsSystem.SetPlayerStatsState(_createdStats);
        ItemSystem.Save();
        SkillSystem.Save();
        MadoSystem.Save();
        DeitySystem.Save();
        StatsSystem.Save();
        GameManager.Save();

        // Mark Player as Alive
        PlayerDataSerializationSystem.PlayerDataStateSet[activeIndex].UpdateProfileStatus(ProfileStatus.Alive);

        // Finalize and Save data
        PlayerDataSerializationSystem.SavePlayerDataState(activeIndex);

        // Load the previously prepared scene
        GameSceneManager.Deploy();
    }

    #region Player Stat Modifier Methods
    private void CalculateStateInfluencePercentage(string input)
    {
        DisableInputField();
        // TODO: Get sum of character ascii
        List<int> asciiValues = new(input.Length);
        foreach (char character in input)
        {
            asciiValues.Add(character);
            _createdStats[(StatVariable)Array.IndexOf(input.ToArray(), character)] = character;
        }
        var sum = asciiValues.Sum();

        int index = 0;

        // Start modifying our stats
        _createdStats[StatVariable.Attack].IncreaseThisBy(asciiValues[index++], BonusModificationType.PercentageOf).IncreaseThisBy(sum, BonusModificationType.PercentageOf);
        _createdStats[StatVariable.Defense].IncreaseThisBy(asciiValues[index++], BonusModificationType.PercentageOf).IncreaseThisBy(sum, BonusModificationType.PercentageOf);
        _createdStats[StatVariable.Poise].IncreaseThisBy(asciiValues[index++], BonusModificationType.PercentageOf).IncreaseThisBy(sum, BonusModificationType.PercentageOf);
        _createdStats[StatVariable.Agility].IncreaseThisBy(asciiValues[index++], BonusModificationType.PercentageOf).IncreaseThisBy(sum, BonusModificationType.PercentageOf);
        _createdStats[StatVariable.SpecialAttack].IncreaseThisBy(asciiValues[index++], BonusModificationType.PercentageOf).IncreaseThisBy(sum, BonusModificationType.PercentageOf);
        _createdStats[StatVariable.SpecialDefense].IncreaseThisBy(asciiValues[index++], BonusModificationType.PercentageOf).IncreaseThisBy(sum, BonusModificationType.PercentageOf);
        _createdStats[StatVariable.Trust].IncreaseThisBy(asciiValues[index], BonusModificationType.PercentageOf).IncreaseThisBy(sum, BonusModificationType.PercentageOf);

        GameManager.PlayerName = input;
        NextQuestion();
    }

    private void EnhanceStat(string input)
    {
        StatVariable[] variables =
        {
            StatVariable.Attack,
            StatVariable.Defense,
            StatVariable.Poise,
            StatVariable.Agility,
            StatVariable.SpecialAttack,
            StatVariable.SpecialDefense,
            StatVariable.Trust
        };

        DisableInputField();
        // TODO: Parse input (1 - 100) to StatVariable type
        var number = Convert.ToInt32(input);
        var selectedVariable = variables[number % variables.Length];
        _createdStats[selectedVariable] = _createdStats[selectedVariable].IncreaseThisBy(number % (variables.Length * Two), BonusModificationType.Whole);
        NextQuestion();
    }

    public void ApplyTrustAmplifier()
    {
        DisablePromptField();
        // TODO: Add increase to stat
        _createdStats[StatVariable.Trust] = _createdStats[StatVariable.Trust].IncreaseThisBy(10, BonusModificationType.PercentageOf);
        NextQuestion();
    }
    #endregion
}
