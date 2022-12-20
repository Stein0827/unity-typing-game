using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class hof : MonoBehaviour
{
    public Button main_menu;
    // Start is called before the first frame update
    void Start()
    {
        main_menu.onClick.AddListener(go_back);

        
    }

    void go_back(){
        SceneManager.LoadScene("startScreen");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
