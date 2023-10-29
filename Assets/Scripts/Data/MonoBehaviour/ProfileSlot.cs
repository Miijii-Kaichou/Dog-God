#nullable enable

using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static SharedData.Constants;

public sealed class ProfileSlot : MonoBehaviour
{
    [SerializeField]
    private Button _profileSlotButtonComponent;

    [SerializeField]
    private ProfileStatus _slotStatus = ProfileStatus.Empty;

    [Space, Header("Profile Information Objects")]
    [SerializeField] private TextMeshProUGUI _profileName;
    [SerializeField] private TextMeshProUGUI _lastRecordedHealth;
    [SerializeField] private TextMeshProUGUI _currentLevel;

    [Space, Header("Profile Colors")]
    [SerializeField] private Color _colorBrandNew;
    [SerializeField] private Color _colorActive;

    private int _id;
    public int ID
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
            if (_playerState != null) return;
            PlayerDataSerializationSystem.LoadPlayerDataState(ID, OnPlayerDataLoaded);
        }
    }

    private void OnPlayerDataLoaded(PlayerDataState? state)
    {
        _playerState = state;

        UpdateProfileName();
        SetUpButtonStatus();
    }

    private void UpdateProfileName()
    {
        _profileName.text = _playerState?.gameState.identifier;
    }

    private PlayerDataState? _playerState;

    private void Awake()
    {
        _profileSlotButtonComponent ??= GetComponent<Button>();
    }

    private void SetUpButtonStatus()
    {
        _slotStatus = _playerState == null ? ProfileStatus.Empty : _playerState.Status;
        Debug.Log(_playerState);
        _profileSlotButtonComponent.interactable = !(_slotStatus == ProfileStatus.Unknown || _slotStatus == ProfileStatus.PassedOn);

        ColorBlock buttonColorBlock = _profileSlotButtonComponent.colors;

        if (_slotStatus == ProfileStatus.Alive)
        {
            buttonColorBlock.normalColor = _colorActive;
            _profileSlotButtonComponent.colors = buttonColorBlock;
            return;
        }

        if (_slotStatus == ProfileStatus.Empty)
        {
            buttonColorBlock.normalColor = _colorBrandNew;
            _profileSlotButtonComponent.colors = buttonColorBlock;
            return;
        }
    }

    public void OnSelect()
    {
        GameManager.UpdateActivePlayerID(ID);
        int eventId = _slotStatus switch
        {
            ProfileStatus.Unknown => -One,
            ProfileStatus.Empty => EVT_CreateProfile,
            ProfileStatus.Alive => EVT_LoadProfile,
            ProfileStatus.PassedOn => EVT_ResurrectProfile,
            _ => -1
        };
        EventManager.TriggerEvent(eventId);
    }

    public void OnDelete()
    {
        EventManager.TriggerEvent(EVT_DeleteProfile);
    }
}
