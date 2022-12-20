using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using Rnd = System.Random;

public class canvasL1 : MonoBehaviour
{
    public Button play_pause;
    public Button return_to_menu;
    public TextMeshProUGUI paragraph;
    public TextMeshProUGUI health_gui;
    private TextMeshProUGUI play_pause_text; 
    private float spawning_delay; 
    private GameObject slime_template;
    private GameObject mainCamera;
    string path;
    string[] readText;
    static Rnd rnd;
    public GameObject column_prefab;
    private float room_num;

    private float new_camera_z;
    static public int health;
    static public int defeated;
    private GameObject king_slime;
    
    // Start is called before the first frame update
    void Start()
    {
        // room_num = 1;
        new_camera_z = 16f;
        defeated = 0;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        spawning_delay = 1.5f;  
        slime_template = (GameObject)Resources.Load("basic slime/Prefabs/Slime_01", typeof(GameObject));  // projectile prefab
        king_slime = ((GameObject)Resources.Load("basic slime/Prefabs/Slime_01_King", typeof(GameObject)));
        play_pause_text = play_pause.GetComponentInChildren<TextMeshProUGUI>();
        play_pause.onClick.AddListener(begin);
        return_to_menu.onClick.AddListener(main_menu);
        path = @"Assets/Words/american-words.35.txt";
        readText = File.ReadAllLines(path);
        rnd = new Rnd();
        health = 100;
    }

    void Update() {
       if (health == 0) {
            Time.timeScale = 0;
            paragraph.enabled = true;
            paragraph.text = "You have died.";
            paragraph.color = new Color(1f, 0f, 0f);
            play_pause_text.text = "Try Again";
            return_to_menu.gameObject.SetActive(true);
            StopCoroutine("Spawn");
        }
        health_gui.text = "HP: " + health.ToString();
        if (GameObject.FindGameObjectsWithTag("Slime").Length == 0 && defeated >= room_num*2+1 && mainCamera.transform.position.z <= new_camera_z){ 
            // mainCamera.transform.localPosition = Vector3.MoveTowards (mainCamera.transform.localPosition, new Vector3(0f, 0f, Mathf.Floor((mainCamera.transform.position.z + 15)/15)), Time.deltaTime * 10);
            mainCamera.transform.position += new Vector3(0f,0f,1f) * Time.fixedDeltaTime;
        }
    }

    void begin() {
        if(health == 0) {
            SceneManager.LoadScene("normalLevel");
        }
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
                room_num = Mathf.Floor(mainCamera.transform.position.z/15) + 1;
                var starting_pos = mainCamera.transform.position + new Vector3(Random.Range(-5.0f, 5.0f), -1.88f, 15.0f);
                if (GameObject.FindGameObjectsWithTag("Slime").Length < 2 && defeated <= room_num*2+1 && mainCamera.transform.position.z < 300f)                
                {
                    GameObject slime = Instantiate(slime_template, starting_pos, Quaternion.identity);
                    slime.tag = "Slime";
                    slime.transform.LookAt(mainCamera.transform);
                    slime.AddComponent<Slime_Animator>();

                    GameObject txtBox = new GameObject("Text");
                    txtBox.tag = "Word";
                    txtBox.transform.position = slime.transform.position;
                    txtBox.transform.position += new Vector3(0f, 1.5f, 0f);
                    txtBox.transform.SetParent(slime.transform);

                    GameObject pln  = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    pln.transform.Rotate(-90, 0, 0);
                    pln.transform.localScale = new Vector3(0.6f, 1f, 0.1f);
                    pln.transform.position = slime.transform.position + new Vector3(0f, 1.5f, 0.1f);
                    pln.transform.SetParent(txtBox.transform);
                    
                    TextMeshPro t = txtBox.AddComponent<TextMeshPro>();
                    t.color = new Color(0, 0, 0);
                    int r = rnd.Next(readText.Length);
                    while (readText[r].Contains("'")) {
                        r = rnd.Next(readText.Length);
                    }
                    t.text = readText[r].ToUpper();
                    t.alignment = TextAlignmentOptions.Center;
                    t.fontSize = 5;
                    RectTransform rt = t.GetComponent<RectTransform>();
                    rt.sizeDelta = new Vector2(7, 1);
                }

                if(mainCamera.transform.position.z >=  new_camera_z){
                   defeated = 0;
                   new_camera_z += 15;
                }
                
                if(defeated >= room_num*2+1 && GameObject.FindGameObjectsWithTag("Slime").Length > 0){
                    foreach (GameObject door in GameObject.FindGameObjectsWithTag("door")){
                        if (door.transform.position == new Vector3(0.0f, 0.0f, Mathf.Floor(mainCamera.transform.position.z) + 15)){
                            Destroy(door);
                        }
                    }

                    GameObject l_column = Instantiate(column_prefab, new Vector3(0, 0, 0), Quaternion.identity);
                    l_column.transform.position = new Vector3(-2f, 0f, mainCamera.transform.position.z + 15);
                    GameObject r_column = Instantiate(column_prefab, new Vector3(0, 0, 0), Quaternion.identity);
                    r_column.transform.position = new Vector3(2f, 0f, mainCamera.transform.position.z + 15);
                    if(mainCamera.transform.position.z == 0){
                        new_camera_z = Mathf.Floor(mainCamera.transform.position.z + 15);
                    }
                }

                if(mainCamera.transform.position.z == 300f){

                }

                // if (GameObject.FindGameObjectsWithTag("Slime").Length == 0){
                //     mainCamera.transform.position += Vector3.forward*Time.deltaTime;
                // }

            }
            yield return new WaitForSeconds(spawning_delay);
        }
        
    }
}