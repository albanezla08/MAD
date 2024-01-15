using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject game_over_screen;
    public GameObject player;
    
    public void game_over() {
        player.SetActive(false);
        game_over_screen.SetActive(true);
    }

    public void restart_game() {
        //when using buttons, you will have to go in unity and add an event to the button.
        //Then drop the game object with this script (not the script) in the event in the button, then select a function
        //for it to perform on click. In this case, we want to select this function
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
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
