using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Services
{



    private static EventManager _eventManager;
    public static EventManager EventManager
    {
        get
        {
            Debug.Assert(_eventManager != null);
            return _eventManager;
        }
        set
        {
            _eventManager = value;
        }
    }

    private static Game_Manager _game_Manager;
    public static Game_Manager Game_Manager
    {
        get
        {
            Debug.Assert(_game_Manager != null);
            return _game_Manager;
        }
        set
        {
            _game_Manager = value;
        }
    }

    private static SoundManager _soundManager;
    public static SoundManager SoundManager
    {
        get
        {
            Debug.Assert(_soundManager != null);
            return _soundManager;
        }
        set
        {
            _soundManager = value;
        }
    }

    private static TaskManager _taskManager;
    public static TaskManager TaskManager
    {
        get
        {
            Debug.Assert(_taskManager != null);
            return _taskManager;
        }
        set
        {
            _taskManager = value;
        }
    }

    private static PrefabDB _prefabDB;
    public static PrefabDB PrefabDB
    {
        get
        {
            Debug.Assert(_prefabDB != null);
            return _prefabDB;
        }
        set
        {
            _prefabDB = value;
        }
    }
}
