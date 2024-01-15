using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManagerScript : MonoBehaviour
{
    public UnityEvent difficulty_event;
    public GameObject game_over_screen;
    public GameObject player;
    public GameObject fist;
    public GameObject gun;
    public GameObject bomb;
    private int score;
    [SerializeField] private int score_increment;
    public float boundary;
    public float radius_to_player;
    public float spawn_time;
    public float spawn_timer;
    public float difficulty_lvl;

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
        boundary = 90.0f;
        radius_to_player = 30.0f;
        spawn_time = 4.0f;
        spawn_timer = 0.0f;
        difficulty_lvl = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn_timer < spawn_time) {
            spawn_timer += Time.deltaTime;
        } else {
            spawn_enemy();
            incr_difficulty();
            spawn_timer = 0.0f;
        }

    }
    IEnumerator play_loop_after_intro(AudioSource intro_src) {
        while (intro_src.isPlaying) {
            yield return null;
        }
        audio_manager_script.play_clip("Loop");
    }

    public Vector3 get_pos_close_to_player() {
        Vector3 pos_away_from_player;
        pos_away_from_player = new Vector3(Random.Range(-boundary, boundary), Random.Range(-boundary, boundary), 0);
        float x = pos_away_from_player.x;
        float y = pos_away_from_player.y;
        float player_x = player.transform.position.x;
        float player_y = player.transform.position.y;
        x -= player_x;
        y -= player_y;
        //radius can't be bigger than 50
        if (x*x + y*y > radius_to_player*radius_to_player && x*x + y*y < 3600) {
            return pos_away_from_player;
        } else {
            return get_pos_close_to_player();
        }
    }

    public void spawn_enemy() {
        int type = Random.Range(1, 5);
        if (type == 1) {
            GameObject fist_object = Instantiate(fist, get_pos_close_to_player(), transform.rotation);
            fist_object.GetComponent<EnemyController>().death_event.AddListener(incr_score);
        } else if (type == 2) {
            GameObject gun_object = Instantiate(gun, get_pos_close_to_player(), transform.rotation);
            gun_object.GetComponent<EnemyController>().death_event.AddListener(incr_score);
        } else if (type == 3) {
            GameObject bomb_object = Instantiate(bomb, get_pos_close_to_player(), transform.rotation);
            bomb_object.GetComponent<EnemyController>().death_event.AddListener(incr_score);
        } else if (type == 4) {
            spawn_enemy();
            spawn_enemy();
        }
        //Instantiate()
    }

    private void incr_score() {
        score += score_increment;
    }

    public void incr_difficulty() {
        difficulty_lvl ++;
        //modify detection distances for enemies to be bigger
        //modify enemy speeds to be faster
        //
        difficulty_event.Invoke();
        if (radius_to_player > 12.0f) {
            radius_to_player -= 1.0f;
        }
        if (spawn_time > 1.8f) {
            spawn_time -= 0.15f;
        }
    }

}
