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
    public GameObject crate_prefab;
    public GameObject pot_prefab;
    public GameObject table_prefab;
    public GameObject rock_prefab;   
    public GameObject pot_particle_prefab;  
    // Start is called before the first frame update
    void Start()
    {
        generateWalls();
        generateFloor();
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

    void generateFloor(){
        for (int i=0; i < 90; i = i + 15){
            int which = Random.Range(1, 5);
            float x = Random.Range(4.0f, 7.0f);
            float y = Random.Range(6.0f, 8.0f);
            generateItem(which, x, y + i);
            which = Random.Range(1, 5);
            x = Random.Range(-4.0f, -7.0f);
            y = Random.Range(6.0f, 8.0f);
            generateItem(which, x, y + i);
            which = Random.Range(1, 5);
            x = Random.Range(4.0f, 7.0f);
            y = Random.Range(10.0f, 12.0f);
            generateItem(which, x, y + i);
            which = Random.Range(1, 5);
            x = Random.Range(-4.0f, -7.0f);
            y = Random.Range(10.0f, 12.0f);
            generateItem(which, x, y + i);
        }
    }

    void generateItem(int which, float x, float y){
        switch (which)
        {
            case 1:
                tableAddons(x, y);
                break;
            case 2:
                GameObject crate = Instantiate(crate_prefab, new Vector3(0, 0, 0), Quaternion.identity);
                crate.transform.position = new Vector3(x, 0.4571295f, y);
                crate.transform.Rotate(0f, Random.Range(0.0f, 360.0f), 0f);
                break;
            case 3:
                GameObject rock = Instantiate(rock_prefab, new Vector3(0, 0, 0), Quaternion.identity);
                rock.transform.position = new Vector3(x, 0.5f, y);
                rock.transform.Rotate(0f, Random.Range(0.0f, 360.0f), 0f);
                break;
            case 4:
                GameObject pot = Instantiate(pot_prefab, new Vector3(0, 0, 0), Quaternion.identity);
                pot.transform.position = new Vector3(x, 0.0f, y);
                pot.transform.Rotate(0f, Random.Range(0.0f, 360.0f), 0f);
                GameObject particle = Instantiate(pot_particle_prefab, new Vector3(0, 0, 0), Quaternion.identity);
                particle.transform.position = new Vector3(x, 0.6652534f, y);
                break;
        }
    }

    void tableAddons(float x, float z){
        if(Random.value > 0.5f){
            GameObject table = Instantiate(table_prefab, new Vector3(0, 0, 0), Quaternion.identity);
            table.transform.position = new Vector3(x, 0.0f, z);
        }
        else{
            GameObject table = Instantiate(table_prefab, new Vector3(0, 0, 0), Quaternion.identity);
            table.transform.position = new Vector3(x, 0.0f, z);
            table.transform.Rotate(0f, 90f, 0f);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
