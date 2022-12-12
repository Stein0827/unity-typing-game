using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class testTutorial : MonoBehaviour
{
    public Button b1;
    public TextMeshProUGUI paragraph;
    private TextMeshProUGUI b1text; 
    private float shooting_delay; 
    private GameObject projectile_template;
    private GameObject mainCamera;
    public int health;
    // private TextMeshPro te
    
    private Image img;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        shooting_delay = 1.5f;  
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
            paragraph.enabled = false;
        }
        else{
            SceneManager.LoadScene("startScreen");
        }

        StartCoroutine("Spawn");

    }

    private IEnumerator Spawn()
    {
        while (true)
        {            
            if (b1text.text =="Stop")
            {
                var starting_pos = new Vector3(Random.Range(-5.0f, 5.0f), 0.0f, 5.0f);
                if (GameObject.FindGameObjectsWithTag("Slime").Length == 0)
                {
                    GameObject new_object = Instantiate(projectile_template, starting_pos, Quaternion.identity);
                    GameObject obj = new GameObject("Text");
                    TextMeshPro t = obj.AddComponent<TextMeshPro>();
                    t.color = new Color(0, 0, 0);
                    RectTransform rt = t.GetComponent<RectTransform>();
                    rt.sizeDelta = new Vector2(5, 2);                
                    t.text = ((char)(int)Random.Range(97f, 123f)).ToString();
                    t.alignment = TextAlignmentOptions.Center;
                    t.fontSize = 3 ;
                    obj.transform.position = new_object.transform.position;
                    obj.transform.position += new Vector3(0f, 1f, 0f);
                    obj.transform.SetParent(new_object.transform);
                    new_object.tag = "Slime";
                    obj.tag = "Word";
                    new_object.transform.LookAt(mainCamera.transform);
                    obj.transform.Rotate(0f, 180f, 0f);
                    new_object.AddComponent<Slime_Animator>();
                }
            }
            yield return new WaitForSeconds(shooting_delay); // next shot will be shot after this delay
        }
    }
}
