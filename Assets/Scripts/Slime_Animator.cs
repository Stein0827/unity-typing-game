using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Slime_Animator : MonoBehaviour {
    
    public int state;
    private Animator animation_controller;
    public float velocity;
    private Camera player_cam;
	// Use this for initialization
	void Start ()
    {
        animation_controller = gameObject.AddComponent<Animator>();
        animation_controller.runtimeAnimatorController = Resources.Load("Slime") as RuntimeAnimatorController;
        velocity = keyboardInf.curr_vel;
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

        if (transform.position.y > 1f) {
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        }
        AnimatorStateInfo animation_state = animation_controller.GetCurrentAnimatorStateInfo(0);
        if (v.magnitude < 2)
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
                    infiniteLvl.health -= 20;
                    Destroy(gameObject);
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
                    }
                }
            }
        }
    }

    public void setState(int stateNum) {
        state = stateNum;
    }

    public int getState() {
        return state;
    }
}