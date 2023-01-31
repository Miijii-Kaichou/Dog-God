using UnityEngine;
using UnityEngine.UI;

public class ActionSystem : GameSystem
{
    private bool _isPerformingAction = false;
    public bool IsPerformingAction => _isPerformingAction;
    public bool IsCategorySelected => _selectedPrimarySlot != -1;

    private bool _slotUsed = false;
    private int _previousSelectedPrimary = 0;
    private int _selectedPrimarySlot = -1;
    private int _selectedSecondarySlot = -1;

    private const int MaxSlots = 4;

    RuntimeActionSystem RuntimeActionSystem;

    private readonly KeyCode[] _primaryKeys = new KeyCode[MaxSlots]
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
    };

    private readonly KeyCode[] _secondaryKeys = new KeyCode[MaxSlots]
    {
        KeyCode.Q,
        KeyCode.W,
        KeyCode.E,
        KeyCode.R
    };

    public PlayerHUD PlayerHUD { get; private set; }

    protected override void OnInit()
    { 
        RuntimeActionSystem ??= GameManager.GetSystem<RuntimeActionSystem>();

    }

    protected override void Main()
    {
        if (Input.GetKey(KeyCode.Space) && _slotUsed == false)
        {
            AccessActionOptions();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _slotUsed = false;
            CloseActionOptions();
        }
    }

    private void CloseActionOptions()
    {
        if (_isPerformingAction == true)
        {
            PlayerHUD.ResetActionUi();
            _isPerformingAction = false;
            _previousSelectedPrimary = 0;
            _selectedPrimarySlot = -1;
            _selectedSecondarySlot = -1;
        }
    }

    private void AccessActionOptions()
    {
        if (_isPerformingAction == false)
        {
            _isPerformingAction = true;
            PlayerHUD.UpdateSlotIndex(_previousSelectedPrimary);
        }
        ListenForPrimaryInput();
        ListenForSecondaryInput();
    }

    private void ListenForPrimaryInput()
    {
        for (int primaryKeyIndex = 0; primaryKeyIndex < MaxSlots; primaryKeyIndex++)
        {
            if (Input.GetKeyDown(_primaryKeys[primaryKeyIndex]))
            {
                _previousSelectedPrimary = Mathf.Clamp(_selectedPrimarySlot, 0, MaxSlots);
                _selectedPrimarySlot = primaryKeyIndex;
                OpenActionView();
            }
        }
    }

    private void OpenActionView()
    {
        var difference = _selectedPrimarySlot - _previousSelectedPrimary;
        PlayerHUD.UpdateTargetAngle(difference);
        PlayerHUD.UpdateSlotIndex(_selectedPrimarySlot);
    }

    private void ListenForSecondaryInput()
    {
        if (_selectedPrimarySlot == -1) return;
        for (int secondaryKeyIndex = 0; secondaryKeyIndex < MaxSlots; secondaryKeyIndex++)
        {
            if (Input.GetKeyDown(_secondaryKeys[secondaryKeyIndex])) ExecuteAction(secondaryKeyIndex, _selectedPrimarySlot);
        }
    }

    private void ExecuteAction(int actionSlotNumber, int fromCategorySlotNumber)
    {
        _selectedSecondarySlot = actionSlotNumber;

        Debug.Log($"Item Number {actionSlotNumber} from Category {fromCategorySlotNumber} used!");

        _slotUsed = true;
        RuntimeActionSystem[fromCategorySlotNumber].UseActionItem(actionSlotNumber);

        CloseActionOptions();
    }


    public void RegisterPlayerHUD(PlayerHUD playerHUD)
    {
        PlayerHUD = playerHUD;
    }
}
