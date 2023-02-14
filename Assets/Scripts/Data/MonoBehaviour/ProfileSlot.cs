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
    internal int id;

    private void Awake()
    {
        _profileSlotButtonComponent ??= GetComponent<Button>();
    }

    private void Start()
    {
        SetUpButtonStatus();
    }

    private void SetUpButtonStatus()
    {
        _profileSlotButtonComponent.interactable = !(_slotStatus == ProfileStatus.Unknown || _slotStatus == ProfileStatus.PassedOn);
    }

    public void OnSelect()
    {
        GameManager.UpdateActivePlayerID(id);
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
