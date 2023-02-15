#nullable enable

using UnityEngine;
using UnityEngine.UI;

using static SharedData.Constants;

public sealed class ProfileSlot : MonoBehaviour
{
    [SerializeField]
    private Button _profileSlotButtonComponent;

    [SerializeField]
    private ProfileStatus _slotStatus = ProfileStatus.Empty;
    
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
            PlayerDataSerializationSystem.LoadPlayerDataState(ID, out _playerState);
            SetUpButtonStatus();
        }
    }

    private PlayerDataState? _playerState;

    private void Awake()
    {
        _profileSlotButtonComponent ??= GetComponent<Button>();
    }

    private void SetUpButtonStatus()
    {
        _slotStatus = _playerState == null ? ProfileStatus.Empty : _playerState.Status;
        _profileSlotButtonComponent.interactable = !(_slotStatus == ProfileStatus.Unknown || _slotStatus == ProfileStatus.PassedOn);
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
            _ => throw new System.NotImplementedException()
        };
        EventManager.TriggerEvent(eventId);
    }

    public void OnDelete()
    {
        EventManager.TriggerEvent(EVT_DeleteProfile);
    }
}
