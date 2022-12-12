using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Slime_Animator : MonoBehaviour {

    // private Animator animation_controller;
    private float velocity;
    private float walking_velocity;
    private Camera player_cam;
	// Use this for initialization
	void Start ()
    {
        // animation_controller = gameObject.GetComponent<Animator>();
        // Debug.Log(animation_controller);
        // animation_controller.runtimeAnimatorController = Resources.Load("Slime") as RuntimeAnimatorController;
        velocity = 1f;
        player_cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = player_cam.transform.position - transform.position;
        Vector3 d = v / v.magnitude;
        transform.position += new Vector3(d.x, 0f, d.z) * velocity * Time.deltaTime;

        if (transform.position.y > 1f) {
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        }

        if (v.magnitude < 2)
        {
            // set animator to attack
            // change velocity to slow down
            // animation_controller.SetInteger("state", 1);
            // AnimatorStateInfo animation_state = animation_controller.GetCurrentAnimatorStateInfo(0);
            // if (animation_controller.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Rig_Attack")
            // {
            //     velocity = 0.1f;
            //     if (animation_state.normalizedTime >= 0.75f)
            //     {
            //         Destroy(gameObject);
            //     }
            // }
            Destroy(gameObject);
        }
        // else {
        //     animation_controller.SetInteger("state", 0);
        // }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Main Camera")
        {
            Destroy(gameObject);
        }
    }
}

