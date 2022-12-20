using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class hof : MonoBehaviour
{
    public Button main_menu;
    public TMP_Text hall_of_fame;
    private string path;
    public Dictionary<string, int> scores;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("score"));
        Debug.Log(PlayerPrefs.GetString("nick"));
        main_menu.onClick.AddListener(go_back);
        path = @"Assets/Resources/score_board.txt";
        scores = new Dictionary<string, int>();
        if (new FileInfo(path).Length != 0)   
        {
            add_to_dict();
        }
        hall_of_fame_IO();
    }

    void go_back(){
        SceneManager.LoadScene("startScreen");
    }

    void hall_of_fame_IO() {
        if (!scores.ContainsKey(PlayerPrefs.GetString("nick"))) {
            scores.Add(PlayerPrefs.GetString("nick"), PlayerPrefs.GetInt("score"));
            string curr_score = PlayerPrefs.GetString("nick") + "\t" + PlayerPrefs.GetInt("score");
            File.AppendAllText(path, curr_score + System.Environment.NewLine);
        }
        else {
            if (scores[PlayerPrefs.GetString("nick")] < PlayerPrefs.GetInt("score")) {
                scores[PlayerPrefs.GetString("nick")] = PlayerPrefs.GetInt("score");
                string curr_score = PlayerPrefs.GetString("nick") + "\t" + PlayerPrefs.GetInt("score");
                File.AppendAllText(path, curr_score + System.Environment.NewLine);
            }
        }
        
        List<KeyValuePair<string, int>> sorted_dict = scores.ToList();

        sorted_dict.Sort(
            delegate(KeyValuePair<string, int> pair1, KeyValuePair<string, int> pair2) {
                return -pair1.Value.CompareTo(pair2.Value);
            }
        );

        string final_score = "";
        // Debug.Log(sorted_dict[0]);
        int num = 0;
        foreach (var i in sorted_dict) {
            if (num >= 5) {
                break;
            }
            final_score += i.Key + "\t" + i.Value + System.Environment.NewLine;
            num++;
        }

        hall_of_fame.text = final_score;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void add_to_dict()
    {
        string curr_line = "";
        using (StreamReader sr = new StreamReader(path))
        {
            while ((curr_line = sr.ReadLine()) != null) {
                string[] tokens = curr_line.Split("\t");
                Debug.Log(tokens);
                int curr_line_score = System.Int32.Parse(tokens[1]);
                Debug.Log(curr_line_score);
                if (!scores.ContainsKey(tokens[0])) {
                    scores.Add(tokens[0], curr_line_score);
                }
                else if (scores[tokens[0]] < curr_line_score) {
                    scores[tokens[0]] = curr_line_score;
                }
            }
        }
    }
}
