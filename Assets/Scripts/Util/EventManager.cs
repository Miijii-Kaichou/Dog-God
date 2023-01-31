using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Random = UnityEngine.Random;

[Serializable]
public delegate void CallBackMethod();

public static class EventManager
{
    internal static void Empty()
    {
        Events.Clear();
    }

    //This associated an event with
    static List<EventCall> Events = new();

    public static readonly Action ExemptAction = () => { return; };

    /// <summary>
    /// Add a new event with a uniqueID, name, and defined listeners
    /// </summary>
    /// <param name="uniqueID"></param>
    /// <param name="name"></param>
    /// <param name="listeners"></param>
    public static EventCall AddEvent(int uniqueID, string name, params CallBackMethod[] listeners)
    {

        EventCall newEvent = new EventCall(uniqueID, name);

        if (listeners.Length == 0) return newEvent;

        if (listeners.Length <= 1)
        {
            newEvent.AddNewListener(listeners[0]);
            Events.Add(newEvent);
            return newEvent;
        }

        foreach (CallBackMethod listener in listeners)
        {
            newEvent.AddNewListener(listener, true);
        }
        Events.Add(newEvent);
        return newEvent;
    }

    /// <summary>
    /// Add a new event with a uniqueID, name, and defined listeners
    /// </summary>
    /// <param name="uniqueID"></param>
    /// <param name="name"></param>
    /// <param name="listeners"></param>
    public static EventCall AddEvent(EventCall newEvent)
    {
        return AddEvent(newEvent.UniqueID, newEvent.EventCode, newEvent.listeners);
    }

    /// <summary>
    /// Remove an event based on it's eventCode
    /// </summary>
    /// <param name="eventCode"></param>
    public static void RemoveEvent(string eventCode)
    {
        for (int idIndex = 0; idIndex < Events.Count; idIndex++)
        {
            //If we found the event with this eventCode, remove it
            if (eventCode == Events[idIndex].GetEventCode())
            {
                //Now delete the event itself
                Events.Remove(Events[idIndex]);
                return;
            }
        }
    }

    /// <summary>
    /// Remove an event based on it's uniqueID
    /// </summary>
    /// <param name="eventCode"></param>
    public static void RemoveEvent(int uniqueId)
    {
        for (int idIndex = 0; idIndex < Events.Count; idIndex++)
        {
            //If we found the event with this eventCode, remove it
            if (uniqueId.Equals(Events[idIndex].GetUniqueID()))
            {
                //Now delete the event itself
                Events.Remove(Events[idIndex]);
            }
        }
    }

    /// <summary>
    /// Remove an event 
    /// </summary>
    /// <param name="eventCode"></param>
    public static void RemoveEvent(EventCall @event)
    {
        try
        {
            for (int idIndex = 0; idIndex < Events.Count; idIndex++)
            {
                //If we found the event with this eventCode, remove it
                if (@event != null && @event.Equals(Events[idIndex]))
                {
                    //Now delete the event itself
                    Events.Remove(Events[idIndex]);
                }
            }
        }
        catch
        {
            return;
        }
    }

    /// <summary>
    /// Retuns all events of this event code
    /// </summary>
    /// <param name="eventCode"></param>
    /// <returns></returns>
    public static EventCall[] FindEventsOfEventCode(string eventCode)
    {
        List<EventCall> foundEvents = new List<EventCall>();
        for (int idIndex = 0; idIndex < Events.Count; idIndex++)
        {
            //If we found the event with this eventCode, remove it
            if (eventCode.Equals(Events[idIndex].GetEventCode()))
            {
                //Add it to our discorvered events
                foundEvents.Add(Events[idIndex]);
            }
        }

        //Return the foundEvents
        return foundEvents.ToArray();
    }

    /// <summary>
    /// Check if all events of this kind have been triggered
    /// </summary>
    /// <param name="events"></param>
    /// <returns></returns>
    public static bool HaveAllTriggered(this EventCall[] events)
    {
        foreach (EventCall @event in events)
        {
            if (!@event.HasTriggered()) return false;
        }

        return true;
    }

    public static void TriggerEvent(int uniqueId)
    {
        for (int idIndex = 0; idIndex < Events.Count; idIndex++)
        {
            //If we found the event with this eventCode, remove it
            if (uniqueId.Equals(Events[idIndex].GetUniqueID()) && Events[idIndex].IsAttentive)
            {
                //Trigger events of this uniqueID
                Events[idIndex].Trigger();
                continue;
            }

            Events[idIndex].ResetTriggerState();
        }
    }

    public static void TriggerEvent(string eventCode)
    {
        for (int idIndex = 0; idIndex < Events.Count; idIndex++)
        {
            //If we found the event with this eventCode, remove it
            if (eventCode.Equals(Events[idIndex].GetEventCode()) && Events[idIndex].IsAttentive)
            {
                //Trigger events of this eventCode
                Events[idIndex].Trigger();
                continue;
            }

            Events[idIndex].ResetTriggerState();
        }
    }

    /// <summary>
    /// Returns all events of different IDs and EventCodes
    /// </summary>
    /// <returns></returns>
    public static EventCall[] GetAllEvents() => Events.ToArray();

    /// <summary>
    /// Will watch for a certain condition to be met before executing
    /// an event
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="if"></param>
    /// <param name="else"></param>
    /// <returns></returns>
    public static bool Watch(bool condition, Action @if, Action @else)
    {
        if (condition)
        {
            @if?.Invoke();
            return @if != null && condition;
        }
        @else?.Invoke();
        return @else != null && condition;
    }

    /// <summary>
    /// Will watch for a certain condition to be met before executing
    /// an event
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="if"></param>
    /// <param name="results"></param>
    public static bool Watch(bool condition, Action @if, out bool result)
    {
        result = Watch(condition, @if, null);
        return result;
    }

    /// <summary>
    /// Will watch for a certain condition to be met before executing
    /// an event
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="if"></param>
    /// <param name="else"></param>
    /// <returns></returns>
    public static bool Watch(bool condition, EventCall @if, EventCall @else)
    {
        if (condition)
        {
            @if?.Trigger();
            return @if != null && condition;
        }
        @else?.Trigger();
        return @else != null && condition;
    }

    /// <summary>
    /// Will watch for a certain condition to be met before executing
    /// an event
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="if"></param>
    /// <param name="results"></param>
    public static bool Watch(bool condition, EventCall @if, out bool results)
    {
        results = Watch(condition, @if, null);
        return results;
    }

    public static void DebugEventList()
    {
        foreach (EventCall @event in GetAllEvents())
        {
            Debug.Log($"Event{@event.UniqueID}: \"{@event.EventCode}\"; Listeners: {@event.listeners.GetInvocationList().Length}");
        }
    }

    public static int FreeID => GetAnyFreeID();
    static int GetAnyFreeID()
    {
        const int MIN_RANGE = 100;
        const int MAX_RANGE = 999;
        int _randomNum = Random.Range(MIN_RANGE, MAX_RANGE);
        if (Events.Where(evt => evt.UniqueID.Equals(_randomNum)).Any()) return GetAnyFreeID();
        return _randomNum;
    }
}
