using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    [HideInInspector]public AudioSource source;

    public AudioClip clip;
    [Range(0,1)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    public bool loop;   
}
