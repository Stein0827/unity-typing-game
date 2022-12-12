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
    // private TextMeshPro te
    
    private Image img;
    // Start is called before the first frame update
    void Start()
    {
      
        shooting_delay = 10.5f;  
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
                GameObject obj = new GameObject("Text");
                TextMeshPro t = obj.AddComponent<TextMeshPro>();
                t.color = new Color(0, 0, 0);
                RectTransform rt = t.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(5, 2);                
                t.text = "new text set";
                t.alignment = TextAlignmentOptions.Center;
                t.fontSize = 3 ;
                obj.transform.position = new_object.transform.position;
                obj.transform.position += new Vector3(0f, 1f, 0f);
                obj.transform.SetParent(new_object.transform);
                new_object.tag = "Slime";
                // TextMeshProUGUI  t = new_object.AddComponent<TextMeshProUGUI>();
                // new_object   .tag = "Slime";
                // new_object.AddComponent<Canvas>();
                // Canvas myCanvas = new_object.GetComponent<Canvas> ();
                // myCanvas.renderMode = RenderMode.WorldSpace;
                // TextMeshPro myText = new Text();
                // myText.text = "hello";
                // myText.transform.SetParent = (new_object.transform, false);
                // Text myText;
                // Text myText.text = "hello";
                // myText.transform.SetParent = (myCanvas.transform, false);
                // TextMesh theText = new_object.transform.GetComponent<TextMesh>();
                // Set the text of the TextMeshPro (that is, of the second variable)
                // theText.text = "hello";
                // t.text = "new text set";
                // t.fontSize = 30;
                // t.transform.localEulerAngles += new Vector3(90, 0, 0);
                // t.transform.localPosition += new Vector3(56f, 3f, 40f);
                // new_object.GetComponent<Apple>().direction = shooting_direction;
                // new_object.GetComponent<Apple>().velocity = projectile_velocity;
                // new_object.GetComponent<Apple>().birth_time = Time.time;
                // new_object.GetComponent<Apple>().birth_turret = transform.gameObject;
            }
            yield return new WaitForSeconds(shooting_delay); // next shot will be shot after this delay
        }
    }
}
