using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class startGame : MonoBehaviour
{
    public Button hof;
    public Button infinite;
    public Button normal;
    public Button tutorial;
    private string nname;
    public TMP_InputField nickname;
    // Start is called before the first frame update
    void Start()
    {
        // hof.gameObject.SetActive(false);
        hof.onClick.AddListener(go_hof);
        infinite.onClick.AddListener(go_infinite);
        normal.onClick.AddListener(go_normal);
        tutorial.onClick.AddListener(go_tutorial);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void go_tutorial(){
        if(nickname.text == ""){
            nname = "anonymous";
        }
        else{
            nname = nickname.text;
        }
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("nick", nname);
        SceneManager.LoadScene("Tutorial Level");
    }
    void go_infinite(){
        if(nickname.text == ""){
            nname = "anonymous";
        }
        else{
            nname = nickname.text;
        }
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("nick", nname);
        SceneManager.LoadScene("Tutorial Level");
    }
    void go_hof(){
        if(nickname.text == ""){
            nname = "anonymous";
        }
        else{
            nname = nickname.text;
        }
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("nick", nname);
        SceneManager.LoadScene("Tutorial Level");
    }
    void go_normal(){
        if(nickname.text == ""){
            nname = "anonymous";
        }
        else{
            nname = nickname.text;
        }
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("nick", nname);
        SceneManager.LoadScene("Tutorial Level");
    }
}
