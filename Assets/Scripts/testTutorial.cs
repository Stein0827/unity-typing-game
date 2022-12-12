using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class testTutorial : MonoBehaviour
{
    public Button b1;
    private TextMeshProUGUI b1text; 
    private float shooting_delay; 
    private GameObject projectile_template;
    
    private Image img;
    // Start is called before the first frame update
    void Start()
    {
      
        shooting_delay = 0.5f;  
        projectile_template = (GameObject)Resources.Load("basic slime/Prefab/Slime_01", typeof(GameObject));  // projectile prefab
        b1text = b1.GetComponentInChildren<TextMeshProUGUI>();
        b1.onClick.AddListener(begin);
        img = GameObject.Find("Canvas").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void begin(){
        if(b1text.text == "Start"){
            b1text.text = "Stop";
            img.enabled = false;
        }
        else{
            b1text.text = "Start";
            img.enabled = true;
        }

        StartCoroutine("Spawn");

    }

    private IEnumerator Spawn()
    {
        while (true)
        {            
            if (b1text.text =="Stop")
            {
                Debug.Log("will make slime");
                var starting_pos = new Vector3(Random.Range(-5.0f, 5.0f), 0.0f, 5.0f);
                GameObject new_object = Instantiate(projectile_template, starting_pos, Quaternion.identity);
                // new_object.GetComponent<Apple>().direction = shooting_direction;
                // new_object.GetComponent<Apple>().velocity = projectile_velocity;
                // new_object.GetComponent<Apple>().birth_time = Time.time;
                // new_object.GetComponent<Apple>().birth_turret = transform.gameObject;
            }
            yield return new WaitForSeconds(shooting_delay); // next shot will be shot after this delay
        }
    }
}
