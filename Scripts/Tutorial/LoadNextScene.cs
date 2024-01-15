using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            load_scene(2);
        }
    }
    void load_scene(int index) {
        SceneManager.LoadScene(index);
    }
}
