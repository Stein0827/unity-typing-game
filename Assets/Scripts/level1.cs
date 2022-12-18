using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using Rnd = System.Random;
using UnityEngine.SceneManagement;
using TMPro;

public class level1 : MonoBehaviour
{
    public GameObject wall_prefab;
    string path = @"Assets/Words/american-words.35.txt";
    string[] readText;
    static Rnd rnd;
    public Button play_pause;
    public Button return_to_menu;
    private TextMeshProUGUI play_pause_text; 
    private float shooting_delay; 
    private GameObject projectile_template;
    private GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        generateWalls();
        rnd = new Rnd();
        readText = File.ReadAllLines(path);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        shooting_delay = 1.5f;  
        projectile_template = (GameObject)Resources.Load("basic slime/Prefab/Slime_01", typeof(GameObject));  // projectile prefab
        play_pause_text = play_pause.GetComponentInChildren<TextMeshProUGUI>();
        play_pause.onClick.AddListener(begin);
        return_to_menu.onClick.AddListener(main_menu);
    }

    void generateWalls(){
        for (int i = 1; i<27; i++){
            GameObject r_wall = Instantiate(wall_prefab, new Vector3(0, 0, 0), Quaternion.identity);
            r_wall.transform.position = new Vector3(10.0f, 0.0f, 5f*i - 17.5f);
            r_wall.transform.Rotate(0f, 270f, 0f);
            r_wall.transform.localScale = new Vector3(1.25f, 1f, 1f);
            GameObject l_wall = Instantiate(wall_prefab, new Vector3(0, 0, 0), Quaternion.identity);
            l_wall.transform.position = new Vector3(-10.0f, 0.0f, 5f*i - 17.5f);
            l_wall.transform.Rotate(0f, 90f, 0f);
            l_wall.transform.localScale = new Vector3(1.25f, 1f, 1f);
        }
        for (int i=1; i < 5; i++) {
            GameObject end_wall = Instantiate(wall_prefab, new Vector3(0, 0, 0), Quaternion.identity);
            end_wall.transform.position = new Vector3(12.5f - 5f*i, 0.0f, 115f);
            end_wall.transform.Rotate(0f, 180f, 0f);
            end_wall.transform.localScale = new Vector3(1.25f, 1f, 1f);
        }

        for (int i=15; i < 105; i = i + 15){
            for (int j = 0; j < 5; j++){
                GameObject mid_wall = Instantiate(wall_prefab, new Vector3(0, 0, 0), Quaternion.identity);
                mid_wall.transform.position = new Vector3(-8f + 4f*j, 0.0f, i);
                mid_wall.transform.Rotate(0f, 180f, 0f);
                // end_wall.transform.localScale = new Vector3(1.25f, 1f, 1f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void begin() {
        if(play_pause_text.text == "Start"){
            play_pause_text.text = "Pause";
            return_to_menu.gameObject.SetActive(false);
            StartCoroutine("Spawn");
        } else{
            play_pause_text.text = "Start";
            return_to_menu.gameObject.SetActive(true);
            StopCoroutine("Spawn");
            foreach (GameObject slime in GameObject.FindGameObjectsWithTag("Word")) {
                Destroy(slime.transform.parent.gameObject);
            }
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
                var starting_pos = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z) + new Vector3(Random.Range(-5.0f, 5.0f), 0.0f, 20.0f);
                if (GameObject.FindGameObjectsWithTag("Slime").Length < 3)
                {
                    GameObject new_object = Instantiate(projectile_template, starting_pos, Quaternion.identity);
                    GameObject obj = new GameObject("Text");
                    TextMeshPro t = obj.AddComponent<TextMeshPro>();
                    t.color = new Color(0, 0, 0);
                    RectTransform rt = t.GetComponent<RectTransform>();
                    rt.sizeDelta = new Vector2(5, 2);
                    int r = rnd.Next(readText.Count());
                    while (readText[r].Length > 6) {
                        r = rnd.Next(readText.Count());
                    }        
                    t.text = readText[r];
                    t.alignment = TextAlignmentOptions.Center;
                    t.fontSize = 5;
                    t.color = new Color(1f, 1f, 1f);
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