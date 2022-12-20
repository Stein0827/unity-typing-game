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
    private TextMeshProUGUI play_pause_text; 
    private float shooting_delay; 
    private GameObject projectile_template;
    private GameObject mainCamera;
    string path;
    string[] readText;
    static Rnd rnd;
    public GameObject column_prefab;
    private float room_num;

    private float new_camera_z;

    static public int defeated;
    
    // Start is called before the first frame update
    void Start()
    {
        // room_num = 1;
        new_camera_z = 16f;
        defeated = 0;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        shooting_delay = 1.5f;  
        projectile_template = (GameObject)Resources.Load("basic slime/Prefabs/Slime_01", typeof(GameObject));  // projectile prefab
        play_pause_text = play_pause.GetComponentInChildren<TextMeshProUGUI>();
        play_pause.onClick.AddListener(begin);
        return_to_menu.onClick.AddListener(main_menu);
        path = @"Assets/Words/american-words.35.txt";
        readText = File.ReadAllLines(path);
        rnd = new Rnd();
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
                Debug.Log(defeated);
                room_num = Mathf.Floor(mainCamera.transform.position.z/15) + 1;
                var starting_pos = mainCamera.transform.position + new Vector3(Random.Range(-5.0f, 5.0f), -1.88f, 15.0f);
                if (GameObject.FindGameObjectsWithTag("Slime").Length < 2 && defeated <= room_num*2+1 && mainCamera.transform.position.z < 200f)
                {
                    GameObject new_object = Instantiate(projectile_template, starting_pos, Quaternion.identity);
                    GameObject obj = new GameObject("Text");
                    GameObject pln  = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    pln.transform.Rotate(90, 0, 0);
                    pln.transform.localScale = new Vector3(0.3f, 1f, 0.1f);
                    TextMeshPro t = obj.AddComponent<TextMeshPro>();
                    t.color = new Color(0f, 0f, 0f);
                    RectTransform rt = t.GetComponent<RectTransform>();
                    rt.sizeDelta = new Vector2(5, 2);
                    int r = rnd.Next(readText.Length);
                    while (readText[r].Contains("'")) {
                        r = rnd.Next(readText.Length);
                    }
                    t.text = readText[r].ToLower();
                    t.alignment = TextAlignmentOptions.Center;
                    t.fontSize = 5;
                    obj.transform.position = new_object.transform.position + new Vector3(0f, 1f, 0f);
                    obj.transform.SetParent(new_object.transform);
                    pln.transform.position = new_object.transform.position + new Vector3(0f, 1f, -0.1f);
                    pln.transform.SetParent(new_object.transform);
                    new_object.tag = "Slime";
                    obj.tag = "Word";
                    new_object.transform.LookAt(mainCamera.transform);
                    obj.transform.Rotate(0, 180, 0);
                    new_object.AddComponent<Slime_Animator>();
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

                // if (GameObject.FindGameObjectsWithTag("Slime").Length == 0){
                //     mainCamera.transform.position += Vector3.forward*Time.deltaTime;
                // }

            }
            yield return new WaitForSeconds(shooting_delay); // next shot will be shot after this delay
        }
        
    }

    void Update(){
        if (GameObject.FindGameObjectsWithTag("Slime").Length == 0 && defeated >= room_num*2+1 && mainCamera.transform.position.z <= new_camera_z){ 
            // mainCamera.transform.localPosition = Vector3.MoveTowards (mainCamera.transform.localPosition, new Vector3(0f, 0f, Mathf.Floor((mainCamera.transform.position.z + 15)/15)), Time.deltaTime * 10);
            // Debug.
            mainCamera.transform.position += new Vector3(0f,0f,1f) * Time.fixedDeltaTime;
        }
    }
}