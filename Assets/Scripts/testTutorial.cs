using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class testTutorial : MonoBehaviour
{
    public Button play_pause;
    public Button return_to_menu;
    public TextMeshProUGUI paragraph;
    private TextMeshProUGUI play_pause_text; 
    private float spawning_delay; 
    private GameObject slime_template;
    private GameObject mainCamera;
    private Animator slime_animated;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        spawning_delay = 1.5f;  
        slime_template = (GameObject)Resources.Load("basic slime/Prefabs/Slime_01", typeof(GameObject));  // projectile prefab
        play_pause_text = play_pause.GetComponentInChildren<TextMeshProUGUI>();
        play_pause.onClick.AddListener(begin);
        return_to_menu.onClick.AddListener(main_menu);
    }

    void begin() {
        if(play_pause_text.text == "Start"){
            play_pause_text.text = "Pause";
            paragraph.enabled = false;
            return_to_menu.gameObject.SetActive(false);
            StartCoroutine("Spawn");
            Time.timeScale = 1;
        } else{
            play_pause_text.text = "Start";
            paragraph.enabled = true;
            return_to_menu.gameObject.SetActive(true);
            StopCoroutine("Spawn");
            Time.timeScale = 0;
        }
    }

    void main_menu() {
        SceneManager.LoadScene("startScreen");
    }

    private IEnumerator Spawn()
    {
        while (true)
        {            
            if (play_pause_text.text == "Pause")
            {
                var starting_pos = new Vector3(Random.Range(-5.0f, 5.0f), 0.0f, 4.0f);
                if (GameObject.FindGameObjectsWithTag("Slime").Length < 3)
                {
                    GameObject slime = Instantiate(slime_template, starting_pos, Quaternion.identity);
                    slime.tag = "Slime";
                    slime.transform.LookAt(mainCamera.transform);
                    slime.AddComponent<Slime_Animator>();

                    GameObject pln  = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    pln.transform.Rotate(-90, 0, 0);
                    pln.transform.localScale = new Vector3(0.1f, 1f, 0.1f);
                    pln.transform.position = slime.transform.position + new Vector3(0f, 1.5f, 0.1f);
                    pln.transform.SetParent(slime.transform);

                    GameObject txtBox = new GameObject("Text");
                    txtBox.tag = "Word";
                    txtBox.transform.position = slime.transform.position;
                    txtBox.transform.position += new Vector3(0f, 1.5f, 0f);
                    txtBox.transform.SetParent(slime.transform);
                    
                    TextMeshPro t = txtBox.AddComponent<TextMeshPro>();
                    t.color = new Color(0, 0, 0);
                    t.text = ((char)(int)Random.Range(97f, 123f)).ToString().ToUpper();
                    t.alignment = TextAlignmentOptions.Center;
                    t.fontSize = 5;
                    RectTransform rt = t.GetComponent<RectTransform>();
                    rt.sizeDelta = new Vector2(5, 1);
                }
            }
            yield return new WaitForSeconds(spawning_delay);
        }
    }
}