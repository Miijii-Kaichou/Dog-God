using Extensions;
using System;

[Serializable]
public class EventCall
{
    public int UniqueID { get; private set; }
    public string EventCode { get; private set; }
    public bool Triggered { get; private set; }

    private bool _isAttentive;
    public bool IsAttentive
    {
        get
        {
            return listeners.GetInvocationList().Length != 0;
        }

        private set
        {
            _isAttentive = value;
        }
    }

    public CallBackMethod listeners;

    public EventCall(int initUID, string initEventCode)
    {

        UniqueID = initUID;

        //Null-Checking
        this.EventCode = string.IsNullOrEmpty(initEventCode) ? "Unassigned" : initEventCode;

        Triggered = false;
    }

    /// <summary>
    /// Return the uniqueId given to this event
    /// </summary>
    /// <returns></returns>
    public int GetUniqueID() => UniqueID;

    /// <summary>
    /// Return the eventCode given to this event
    /// </summary>
    /// <returns></returns>
    public string GetEventCode() => EventCode;

    public void AddNewListener(CallBackMethod listener, bool multicast = false)
    {
        if (listener == null)
            listeners = new CallBackMethod(listener);

        if (multicast)
            listeners += listener;
        else
            listeners = listener;

        HasListerners();
    }

    public void RemoveListener(CallBackMethod listener)
    {
        if (listeners != null)
            listeners -= listener;
    }

    /// <summary>
    /// Trigger this event, executing all listeners assigned to it.
    /// </summary>
    public void Trigger()
    {
        listeners?.Invoke();
        return;
    }

    public void Trigger(bool discardAfter = false)
    {
        Trigger();
        if (discardAfter) EventManager.RemoveEvent(this);
    }

    /// <summary>
    /// Set HasTriggered to false, as if it hasn't been triggered
    /// </summary>
    public void ResetTriggerState() => Triggered = default;

    /// <summary>
    /// Removes all listeners from Event Call (Nullifies)
    /// </summary>
    public void Reset()
    {
        if (!HasListerners()) return;
        listeners.Nullify();
    }

    /// <summary>
    /// Returns if this even has been triggered
    /// </summary>
    public bool HasTriggered()
    {
        return Triggered;
    }

    /// <summary>
    /// Returns if this event has listeners.
    /// This is the same as checking if an event "IsAttentive"
    /// </summary>
    /// <returns></returns>
    public bool HasListerners()
    {
        return _isAttentive;
    }

    ~EventCall()
    {
        EventManager.RemoveEvent(this);
    }
}
