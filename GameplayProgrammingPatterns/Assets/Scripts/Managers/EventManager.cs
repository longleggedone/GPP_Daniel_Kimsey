using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager {

    static private EventManager instance;
    static public EventManager Instance{
        get 
        {
            if(instance == null)
            {
                instance = new EventManager();
            }
            return instance;
        }
    }
    private Dictionary<Type, Event.Handler> registeredHandlers = new Dictionary<Type, Event.Handler>();



    public void Register<T>(Event.Handler handler) where T : Event
    {
        Type type = typeof(T);
        if(registeredHandlers.ContainsKey(type))
        {
            registeredHandlers[type] += handler;
        } 
        else
        {
            registeredHandlers[type] = handler;
        }
    }

    public void Unregister<T>(Event.Handler handler) where T : Event
    {
        Type type = typeof(T);
        Event.Handler handlers;
        if(registeredHandlers.TryGetValue(type, out handlers))
        {
            handlers -= handler;
            if(handlers == null)
            {
                registeredHandlers.Remove(type);
            }
            else
            {
                registeredHandlers[type] = handlers;    
            }
        }
    }

    public void FireEvent(Event e)
    {
        Type type = e.GetType();
        Event.Handler handlers;

        if (registeredHandlers.TryGetValue(type, out handlers))
        {
            handlers(e);
        }
    }
	 
}


public abstract class Event
{

    public delegate void Handler(Event e);
}


public class EnemyDeathEvent : Event 
{
    public readonly Enemy deadEnemy;

    public EnemyDeathEvent(Enemy enemy)
    {
        deadEnemy = enemy;
    }
}

