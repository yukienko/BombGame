using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager :Singleton<EventManager>
{
    public enum Events
    {
        StopTimeItem,
        AllDeleteItem,
        GameOver,
    }

    private Dictionary<Events, UnityEvent> eventDictionary = new Dictionary<Events, UnityEvent>();

    public static void StartListening(Events eventName, UnityAction listener)
    {
        UnityEvent newEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out newEvent))
        {
            newEvent.AddListener(listener);
        }
        else
        {
            newEvent = new UnityEvent();
            newEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, newEvent);
        }
    }

    public static void StopListening(Events eventName, UnityAction listener)
    {
        UnityEvent newEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out newEvent))
        {
            newEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(Events eventName)
    {
        UnityEvent newEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out newEvent))
        {
            newEvent.Invoke();
        }
    }
}
