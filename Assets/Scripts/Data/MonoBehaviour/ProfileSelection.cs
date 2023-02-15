#nullable enable

using Extensions;
using System;
using UnityEngine;

using Array = System.Array;

using static SharedData.Constants;

public sealed class ProfileSelection : MonoBehaviour
{
    //All referenced ProfileSlots
    private ProfileSlot[] _profileSlots;

    private void Start()
    {
        SetupEvents();
        InitalizedSlots();
    }

    private void InitalizedSlots()
    {
        _profileSlots = GetComponentsInChildren<ProfileSlot>();
        foreach (var slot in _profileSlots)
        {
            slot.ID = Array.IndexOf(_profileSlots, slot);
        }
    }

    private void SetupEvents()
    {
        EventManager.AddEvent(EVT_CreateProfile, string.Empty, CreateNewProfile);
        EventManager.AddEvent(EVT_LoadProfile, string.Empty, ContinueWithProfile);
        EventManager.AddEvent(EVT_ResurrectProfile, string.Empty, ResurrectProfile);
        EventManager.AddEvent(EVT_DeleteProfile, string.Empty, DeleteProfile);
    }

    private void CreateNewProfile()
    {
        var gameObject = PersistentObjectHierarchy.Find("TitleCanvas");
        if (gameObject == null)
        {
            Debug.Log("Test Case Failed...");
        }
        gameObject.Disable();
        GameSceneManager.LoadScene(SI_ProfileCreation);
    }

    private void ContinueWithProfile()
    {
        // Load Player Data
        PlayerDataSerializationSystem.LoadPlayerDataState(GameManager.ActiveProfileIndex, out GameManager.PlayerDataState);
        //PlayerDataSerializationSystem.OnLoadDone = LoadLastSavedScene;
    }

    private void ResurrectProfile()
    {
        // If you have enough grace to resurrect a profile,
        // validate this action
        Debug.Log("Resurrecting Profile");
    }

    private void DeleteProfile()
    {
        // Prompt the user if they would like to delete this.
        // Also let them know that any progress made in that will be lost
        Debug.Log("Deleting Profile");
    }
}