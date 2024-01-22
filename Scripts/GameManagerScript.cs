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
    [SerializeField] private UIManager ui_manager;
    private int score = 0;
    [SerializeField] private int score_increment = 10;
    public float boundary;
    public float radius_to_player;
    public float spawn_time;
    public float spawn_timer;
    public float difficulty_lvl;
    private bool is_game_over;
    public bool is_tutorial = false;
    public bool is_paused = false;
    [SerializeField] GameObject pause_panel;
    public void game_over() {
        is_game_over = true;
        player.SetActive(false);
        game_over_screen.SetActive(true);
        audio_manager_script.stop_clip("Intro");
        audio_manager_script.stop_clip("Loop");
    }

    public void restart_game() {
        //when using buttons, you will have to go in unity and add an event to the button.
        //Then drop the game object with this script (not the script) in the event in the button, then select a function
        //for it to perform on click. In this case, we want to select this function
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void pause_game() {
        pause_panel.SetActive(true);
        is_paused = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }
    public void resume_game() {
        pause_panel.SetActive(false);
        is_paused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
    [SerializeField] AudioManagerScript audio_manager_script;
    // Start is called before the first frame update
    void Start()
    {
        boundary = 90.0f;
        radius_to_player = 30.0f;
        spawn_time = 4.0f;
        spawn_timer = 0.0f;
        difficulty_lvl = 0;
        if (!is_tutorial) {
            AudioSource intro_src = audio_manager_script.play_clip("Intro");
            StartCoroutine(play_loop_after_intro(intro_src));
            spawn_enemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (is_game_over) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            if (is_paused) {
                resume_game();
            } else {
                pause_game();
            }
        }
        if (is_tutorial) {
            return;
        }
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
        if (is_game_over) {
            yield break;
        }
        audio_manager_script.play_clip("Loop");
        StartCoroutine(make_sure_intro_isnt_playing(intro_src));
    }

    IEnumerator make_sure_intro_isnt_playing(AudioSource intro_src) {
        while (true) {
            if (intro_src.isPlaying) {
                audio_manager_script.stop_clip("Loop");
                StartCoroutine(play_loop_after_intro(intro_src));
                yield break;
            }
            yield return null;
        }
    }

    public Vector3 get_pos_close_to_player() {
        return get_pos_close_to_player_helper(3);
    }

    Vector3 get_pos_close_to_player_helper(int attempts) {
        Vector3 pos_away_from_player;
        pos_away_from_player = new Vector3(Random.Range(-boundary, boundary), Random.Range(-boundary, boundary), 0);
        float x = pos_away_from_player.x;
        float y = pos_away_from_player.y;
        float player_x = player.transform.position.x;
        float player_y = player.transform.position.y;
        x -= player_x;
        y -= player_y;
        //radius can't be bigger than 50
        // if (x*x + y*y > radius_to_player*radius_to_player && x*x + y*y < 3600) {
        //     return pos_away_from_player;
        // } else {
        //     return get_pos_close_to_player_helper(attempts - 1);
        // }

        // make enemies spawn farther away
        if (Vector2.Distance(pos_away_from_player, player.transform.position) < 15 && attempts > 0) {
            return get_pos_close_to_player_helper(attempts - 1);
        } else {
            return pos_away_from_player;
        }
    }

    public void spawn_enemy() {
        // int type = Random.Range(1, 5);
        // if (type == 1) {
        //     GameObject fist_object = Instantiate(fist, get_pos_close_to_player(), transform.rotation);
        //     fist_object.GetComponent<EnemyController>().death_event.AddListener(incr_score);
        // } else if (type == 2) {
        //     GameObject gun_object = Instantiate(gun, get_pos_close_to_player(), transform.rotation);
        //     gun_object.GetComponent<EnemyController>().death_event.AddListener(incr_score);
        // } else if (type == 3) {
        //     GameObject bomb_object = Instantiate(bomb, get_pos_close_to_player(), transform.rotation);
        //     bomb_object.GetComponent<EnemyController>().death_event.AddListener(incr_score);
        // } else if (type == 4) {
        //     spawn_enemy();
        //     spawn_enemy();
        // }
        // version without recursion
        int type = Random.Range(1, 4);
        if (type == 1) {
            GameObject fist_object = Instantiate(fist, get_pos_close_to_player(), transform.rotation);
            fist_object.GetComponent<EnemyController>().death_event.AddListener(incr_score);
        } else if (type == 2) {
            GameObject gun_object = Instantiate(gun, get_pos_close_to_player(), transform.rotation);
            gun_object.GetComponent<EnemyController>().death_event.AddListener(incr_score);
        } else if (type == 3) {
            GameObject bomb_object = Instantiate(bomb, get_pos_close_to_player(), transform.rotation);
            bomb_object.GetComponent<EnemyController>().death_event.AddListener(incr_score);
        }
        //Instantiate()
    }

    public void incr_score() {
        score += score_increment;
        ui_manager.update_score(score);
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

    public void show_need_weapon() {
        ui_manager.show_need_weapon();
    }

}
