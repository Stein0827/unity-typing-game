using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class story : MonoBehaviour
{
    public Button skip;
    public TextMeshProUGUI para1;
    public TextMeshProUGUI para2;
    public TextMeshProUGUI para3;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        skip.onClick.AddListener(go_skip);
        para2.enabled = false;
        para3.enabled = false;
        startTime = Time.time;
    }
    
    void go_skip() {
        SceneManager.LoadScene("normalLevel");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > 15f) {
            SceneManager.LoadScene("normalLevel");
        } else if (Time.time - startTime > 10f) {
            para2.enabled = false;
            para3.enabled = true;
        } else if (Time.time - startTime > 5f) {
            para1.enabled = false;
            para2.enabled = true;
        }
    }
}


