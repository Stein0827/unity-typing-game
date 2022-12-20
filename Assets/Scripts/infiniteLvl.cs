using TMPro;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Rnd = System.Random;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class infiniteLvl : MonoBehaviour
{
    public Button play_pause;
    public Button return_to_menu;
    public TextMeshProUGUI paragraph;
    public TextMeshProUGUI health_gui;
    public TextMeshProUGUI score_gui;
    public TextMeshProUGUI time_gui;
    public TextMeshProUGUI heal_gui;
    private TextMeshProUGUI play_pause_text;
    
    private float spawning_delay; 
    private GameObject mainCamera;
    private List<GameObject> slime_templates;
    private GameObject slime_template;

    static Rnd rnd;
    string[] words_easy;
    string[] words_complex35;
    string[] words_complex50;
    string[] words_complex55;
    string[] words_complex80;
    string[] words_complex95;
    int chance, count;
    float heal_startTime, time_startTime;

    static public int health;
    static public int score;
    static public int time;
    static public int heal;
    
    // Start is called before the first frame update
    void Start()
    {
        spawning_delay = 1.5f;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        slime_templates = new List<GameObject>();
        slime_templates.Add((GameObject)Resources.Load("basic slime/Prefabs/Slime_01", typeof(GameObject)));
        slime_templates.Add((GameObject)Resources.Load("basic slime/Prefabs/Slime_01_King", typeof(GameObject)));
        slime_templates.Add((GameObject)Resources.Load("basic slime/Prefabs/Slime_01_MeltalHelmet", typeof(GameObject)));
        slime_templates.Add((GameObject)Resources.Load("basic slime/Prefabs/Slime_01_Viking", typeof(GameObject)));
        slime_templates.Add((GameObject)Resources.Load("basic slime/Prefabs/Slime_02", typeof(GameObject)));
        slime_templates.Add((GameObject)Resources.Load("basic slime/Prefabs/Slime_03 King", typeof(GameObject)));
        slime_templates.Add((GameObject)Resources.Load("basic slime/Prefabs/Slime_03 Leaf", typeof(GameObject)));
        slime_templates.Add((GameObject)Resources.Load("basic slime/Prefabs/Slime_03 Sprout", typeof(GameObject)));
        slime_templates.Add((GameObject)Resources.Load("basic slime/Prefabs/Slime_03", typeof(GameObject)));

        slime_template = slime_templates[0];
        play_pause_text = play_pause.GetComponentInChildren<TextMeshProUGUI>();
        play_pause.onClick.AddListener(begin);
        return_to_menu.onClick.AddListener(main_menu);

        words_complex35 = File.ReadAllLines(@"Assets/Words/american-words.35.txt");
        words_complex50 = File.ReadAllLines(@"Assets/Words/american-words.50.txt");
        words_complex55 = File.ReadAllLines(@"Assets/Words/american-words.55.txt");
        words_complex80 = File.ReadAllLines(@"Assets/Words/american-words.80.txt");
        words_complex95 = File.ReadAllLines(@"Assets/Words/american-words.95.txt");
        words_easy = File.ReadAllLines(@"Assets/Words/wordlist.txt");
        rnd = new Rnd();
        health = 100; chance = 100;
        score = 0; count = 0; heal = 0; time = 0;
        heal_startTime = 0f; time_startTime = 0f;
    }

    void Update()
    {
        if (keyboardInf.kill_count == 10) {
            int r = rnd.Next(slime_templates.Count);
            slime_template = slime_templates[r];
            if (chance>15) {
                chance -= 5;
            }
            keyboardInf.kill_count = 0;
            count += 10;
        }
        if (count == 20) {
            if (rnd.Next(2) == 0) {
                time += 1;
            } else {
                heal += 1;
            }
            count = 0;
        }
        if (health == 0) {
            Time.timeScale = 0;
            paragraph.enabled = true;
            paragraph.text = "You have died.";
            paragraph.color = new Color(1f, 0f, 0f);
            play_pause_text.text = "Try Again";
            return_to_menu.gameObject.SetActive(true);
            StopCoroutine("Spawn");
        }
        if (keyboardInf.use_heal == 1) {
            keyboardInf.use_heal = 2;
            heal_startTime = Time.time;
        }
        if (keyboardInf.use_heal == 2 && (Time.time - heal_startTime) > 10f) {
            keyboardInf.use_heal = 0;
            heal_startTime = 0f;
        }
        if (keyboardInf.use_time == 1) {
            keyboardInf.use_time = 2;
            time_startTime = Time.time;
            Time.timeScale = 0.5f;
        }
        if (keyboardInf.use_time == 2 && (Time.time - time_startTime) > 5f) {
            keyboardInf.use_time = 0;
            time_startTime = 0f;
            Time.timeScale = 1f;
        }
        health_gui.text = "HP: " + health.ToString();
        score_gui.text = "Score: " + score.ToString();
        heal_gui.text = "[HEAL] : " + heal.ToString();
        time_gui.text = "[TIME] : " + time.ToString();
    }

    void begin() {
        if(health == 0) {
            SceneManager.LoadScene("infiniteLevel");
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
                var starting_pos = new Vector3(Random.Range(-24.0f, 24.0f), 0.0f, 24.0f);
                if (GameObject.FindGameObjectsWithTag("Slime").Length < 5)
                {
                    GameObject slime = Instantiate(slime_template, starting_pos, Quaternion.identity);
                    slime.tag = "Slime";
                    slime.transform.LookAt(mainCamera.transform);
                    slime.AddComponent<Slime_Animator>();

                    GameObject pln  = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    pln.transform.Rotate(-90, 0, 0);
                    pln.transform.localScale = new Vector3(0.6f, 1f, 0.1f);
                    pln.transform.position = slime.transform.position + new Vector3(0f, 1.5f, 0.1f);
                    pln.transform.SetParent(slime.transform);

                    GameObject txtBox = new GameObject("Text");
                    txtBox.tag = "Word";
                    txtBox.transform.position = slime.transform.position;
                    txtBox.transform.position += new Vector3(0f, 1.5f, 0f);
                    txtBox.transform.SetParent(slime.transform);
                    
                    TextMeshPro t = txtBox.AddComponent<TextMeshPro>();
                    t.color = new Color(0, 0, 0);
                    int r = rnd.Next(chance);
                    if(r==0) {
                        r = rnd.Next(words_complex95.Length);
                        while (words_complex95[r].Contains("'")) {
                            r = rnd.Next(words_complex95.Length);
                        }
                        t.text = words_complex95[r].ToUpper();
                    } else if(r<=3) {
                        r = rnd.Next(words_complex80.Length);
                        while (words_complex80[r].Contains("'")) {
                            r = rnd.Next(words_complex80.Length);
                        }
                        t.text = words_complex80[r].ToUpper();
                    } else if(r<=6) {
                        r = rnd.Next(words_complex55.Length);
                        while (words_complex55[r].Contains("'")) {
                            r = rnd.Next(words_complex55.Length);
                        }
                        t.text = words_complex55[r].ToUpper();
                    } else if(r<=9) {
                        r = rnd.Next(words_complex50.Length);
                        while (words_complex50[r].Contains("'")) {
                            r = rnd.Next(words_complex50.Length);
                        }
                        t.text = words_complex50[r].ToUpper();
                    } else if(r<=12) {
                        r = rnd.Next(words_complex35.Length);
                        while (words_complex35[r].Contains("'")) {
                            r = rnd.Next(words_complex35.Length);
                        }
                        t.text = words_complex35[r].ToUpper();
                    } else {
                        r = rnd.Next(words_easy.Length);
                        while (words_easy[r].Contains("'")) {
                            r = rnd.Next(words_easy.Length);
                        }
                        t.text = words_easy[r].ToUpper();
                    }
                    t.alignment = TextAlignmentOptions.Center;
                    t.fontSize = 5;
                    RectTransform rt = t.GetComponent<RectTransform>();
                    rt.sizeDelta = new Vector2(7, 1);       
                }
            }
            yield return new WaitForSeconds(spawning_delay);
        }
    }
}