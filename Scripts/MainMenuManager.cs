using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public AudioManagerScript audio_manager;
    public GameObject credits_panel;
    public void load_tutorial() {
        SceneManager.LoadScene(1);
    }
    public void show_credits() {
        credits_panel.SetActive(true);
    }
    public void hide_credits() {
        credits_panel.SetActive(false);
    }
    // void Update() {
    //     if (Input.GetMouseButtonDown(0)) {
    //         audio_manager.play_clip("Gun Shoot");
    //     }
    // }
}
