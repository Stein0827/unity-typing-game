using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class story : MonoBehaviour
{
    public Button skip;
    // Start is called before the first frame update
    void Start()
    {
        skip.onClick.AddListener(go_skip);
    }
    
    void go_skip() {
        SceneManager.LoadScene("normalLevel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}