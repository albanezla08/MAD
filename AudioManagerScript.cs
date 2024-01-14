using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public float volume = 0.5f;
    public bool loop;
}

public class AudioManagerScript : MonoBehaviour
{
    [SerializeField] Sound[] sounds;
    Dictionary<string, Sound> sounds_dict = new Dictionary<string, Sound>();
    void Awake()
    {
        for (int i = 0; i < sounds.Length; i++) {
            sounds_dict.Add(sounds[i].name, sounds[i]);
        }
    }
    public AudioSource play_clip(string name) {
        AudioSource src = gameObject.AddComponent<AudioSource>();
        Sound sound = sounds_dict[name];
        if (sound == null) {
            Debug.Log("sound does not exist");
            return null;
        }
        src.clip = sound.clip;
        src.volume = sound.volume;
        src.loop = sound.loop;
        src.Play();
        return src;
    }
}