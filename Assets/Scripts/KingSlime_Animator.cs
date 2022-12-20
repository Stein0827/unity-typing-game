using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class KingSlime_Animator : MonoBehaviour
{
    
    public int state;
    private Animator animation_controller;
    public float velocity;
    public float health;
    private Camera player_cam;

    static public bool won;
	// Use this for initialization
	void Start ()
    {
        won = false;
        // paragraph = GameObject.Find("Paragraph").GetComponent<TextMeshProUGUI>();
        // return_to_menu = GameObject.Find("Leave Button").GetComponent<Button>();
        health = 120f;
        velocity = 1;
        animation_controller = gameObject.AddComponent<Animator>();
        animation_controller.runtimeAnimatorController = Resources.Load("Slime") as RuntimeAnimatorController;
        // if (keyboardInf.curr_vel == 0) {
        //     velocity = 1;
        // } else {
        //     velocity = keyboardInf.curr_vel;
        // }
        player_cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(state);
        Vector3 v = player_cam.transform.position - transform.position;
        Vector3 d = v / v.magnitude;
        transform.position += new Vector3(d.x, 0f, d.z) * velocity * Time.deltaTime;
        animation_controller.SetInteger("state", state);
        velocity = (120-health)/20 + 1f;
        if (transform.position.y > 1f) {
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        }
        AnimatorStateInfo animation_state = animation_controller.GetCurrentAnimatorStateInfo(0);
        if (v.magnitude < 3)
        {
            state = 1;
            animation_controller.SetInteger("state", state);
            // Debug.Log(animation_controller.GetCurrentAnimatorClipInfo(0)[0].clip.name);
            if (animation_controller.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Attack")
            {
                velocity = 0f;
                Debug.Log("in if statement");
                if (animation_state.normalizedTime >= 0.65f)
                {
                    canvasL1.health -= 20;
                    transform.position = new Vector3(0f, 0f, 140f);
                    state = 0;
                }
            }
            // Destroy(gameObject);
        }
        else {
            animation_controller.SetInteger("state", state);
            if (animation_controller.GetInteger("state") == 2) {
                Debug.Log(animation_controller.GetInteger("state"));
                velocity = 0f;
                Debug.Log(animation_controller.GetCurrentAnimatorClipInfo(0)[0].clip.name);
                if (animation_controller.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Damage_02")
                {
                    velocity = 0f;
                    if (animation_state.normalizedTime >= 0.65f)
                    {
                        Destroy(gameObject);
                        won = true;
                    }
                }
            }
        }
    }

    void main_menu() {
        SceneManager.LoadScene("startScreen");
    }


    public void setState(int stateNum) {
        state = stateNum;
    }

    public int getState() {
        return state;
    }
}
