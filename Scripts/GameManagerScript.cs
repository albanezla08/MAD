using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player;
    [SerializeField] AudioManagerScript audio_manager_script;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource intro_src = audio_manager_script.play_clip("Intro");
        StartCoroutine(play_loop_after_intro(intro_src));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator play_loop_after_intro(AudioSource intro_src) {
        while (intro_src.isPlaying) {
            yield return null;
        }
        audio_manager_script.play_clip("Loop");
    }
}
