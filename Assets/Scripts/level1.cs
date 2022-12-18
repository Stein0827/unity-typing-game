using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
public class level1 : MonoBehaviour
{
    public GameObject wall_prefab;
    public GameObject banner_prefab;
    public GameObject wallTorch_prefab;     
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
            wallItem(10.0f, 5f*i - 17.5f, 270f);
            GameObject l_wall = Instantiate(wall_prefab, new Vector3(0, 0, 0), Quaternion.identity);
            l_wall.transform.position = new Vector3(-10.0f, 0.0f, 5f*i - 17.5f);
            l_wall.transform.Rotate(0f, 90f, 0f);
            l_wall.transform.localScale = new Vector3(1.25f, 1f, 1f);
            wallItem(-10.0f, 5f*i - 17.5f, 90);

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

    void wallItem(float x, float z, float rotation){
        if(x > 0){
            x = x-0.15f; 
        }
        else{
            x += 0.15f;
        }
        if(Random.value > 0.3){
            if (Random.value < 0.5f){
                GameObject wallTorch = Instantiate(wallTorch_prefab, new Vector3(0, 0, 0), Quaternion.identity);
                wallTorch.transform.position = new Vector3(x, 3.0f, z - 1.5f + Random.value*4);
                wallTorch.transform.Rotate(0f, rotation, 0f);
            }
            else{
                GameObject banner = Instantiate(banner_prefab, new Vector3(0, 0, 0), Quaternion.identity);
                banner.transform.position = new Vector3(x, 3.0f, z - 1.5f + Random.value*4);
                banner.transform.Rotate(0f, rotation, 0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
