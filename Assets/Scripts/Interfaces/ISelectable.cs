public interface ISelectable
{
    EventCall OnNextPress { get; set; }
    EventCall OnPreviousPress { get; set; }
    EventCall OnConfirmPress { get; set; }
    EventCall OnCancelPress { get; set; }
    Audio cursorSound { get; set; }
    Audio confirmSound { get; set; }
    Audio cancelSound { get; set; }
    Audio finalConfirmationSound { get; set; }
    int selectionIndex { get; set; }
    void SelectionCycle();
}