using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Prefab DB")]
public class PrefabDB : ScriptableObject
{
    
    [SerializeField]
    private GameObject genericAudioSource;
    public GameObject GenericAudioSource { get { return genericAudioSource; } }

    [SerializeField]
    private GameObject chicken;
    public GameObject Chicken { get { return chicken; } }

    [SerializeField]
    private GameObject duck;
    public GameObject Duck { get { return duck; } }

    [SerializeField]
    private GameObject sheep;
    public GameObject Sheep { get { return sheep; } }
}

