using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
public class level1 : MonoBehaviour
{
    public GameObject wall_prefab;     
    // Start is called before the first frame update
    void Start()
    {
        generateWalls();
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
