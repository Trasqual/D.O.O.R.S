using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public delegate void EventHandlerDelegate(object eventData);

    private Dictionary<Type, List<EventHandlerDelegate>> _listeners = new();

    #region Typed functions
    public void AddListener(Type eventType, EventHandlerDelegate eventHandler)
    {
        if (_listeners.TryGetValue(eventType, out List<EventHandlerDelegate> eventListeners))
        {
            if (!eventListeners.Contains(eventHandler)) // make sure we dont add duplicate
            {
                eventListeners.Add(eventHandler);
            }
        }
        else
        {
            eventListeners = new List<EventHandlerDelegate>() { eventHandler };
            _listeners.Add(eventType, eventListeners);
        }
    }

    public void RemoveListener(Type eventType, EventHandlerDelegate eventHandler)
    {
        if (_listeners.TryGetValue(eventType, out List<EventHandlerDelegate> eventListeners))
        {
            eventListeners.Remove(eventHandler);
        }
    }

    public void RemoveAllListeners(Type eventType)
    {
        _listeners.Remove(eventType);
    }

    public bool IsListening(Type eventType, EventHandlerDelegate eventHandler)
    {
        if (_listeners.TryGetValue(eventType, out List<EventHandlerDelegate> eventListeners))
        {
            return eventListeners.Contains(eventHandler);
        }
        return false;
    }

    public bool AnyListening(Type eventType)
    {
        if (_listeners.TryGetValue(eventType, out List<EventHandlerDelegate> eventListeners))
        {
            return eventListeners.Count > 0;
        }
        return false;
    }

    public void TriggerEvent(Type eventType, object eventData = null)
    {
        if (_listeners.TryGetValue(eventType, out List<EventHandlerDelegate> eventListeners))
        {
            foreach (var listener in eventListeners.ToList())
            {
                try // ensure all the listents are called
                {
                    listener.Invoke(eventData);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
    }
    #endregion

    #region Generics
    public void AddListener<T>(EventHandlerDelegate eventHandler)
    {
        AddListener(TypedClass<T>.TypeOf, eventHandler);
    }

    public void RemoveListener<T>(EventHandlerDelegate eventHandler)
    {
        RemoveListener(TypedClass<T>.TypeOf, eventHandler);
    }

    public void RemoveAllListeners<T>()
    {
        RemoveAllListeners(TypedClass<T>.TypeOf);
    }

    public bool IsListening<T>(EventHandlerDelegate eventHandler)
    {
        return IsListening(TypedClass<T>.TypeOf, eventHandler);
    }

    public bool AnyListening<T>()
    {
        return AnyListening(TypedClass<T>.TypeOf);
    }

    public void TriggerEvent<T>(object eventData = null)
    {
        TriggerEvent(TypedClass<T>.TypeOf, eventData);
    }

    private static class TypedClass<T>
    {
        public static readonly Type TypeOf = typeof(T);
    }
    #endregion
}
