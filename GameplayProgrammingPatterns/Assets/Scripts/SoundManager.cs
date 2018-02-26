using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public GameObject genericAudioSource;

    static private SoundManager instance;
    static public SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SoundManager();
                Debug.Log("new");
            }
            return instance;
        }
    }

   
    public void GenerateSourceAndPlay(AudioClip clip, float volume, float pitch, Vector3 position)
    {
        GameObject specialAudioSource = Instantiate(genericAudioSource);
        AudioSource source = specialAudioSource.GetComponent<AudioSource>();
        specialAudioSource.transform.position = position;
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
        Destroy(specialAudioSource, clip.length);

    }
}
