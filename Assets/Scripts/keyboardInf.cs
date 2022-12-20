using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class keyboardInf : MonoBehaviour
{
    private string word;
    GameObject[] slime_array;
    Image Q, W, E, R, T, Y, U, I, O, P;
    Image A, S, D, F, G, H, J, K, L;
    Image Z, X, C, V, B, N, M, Comma, Period;
    Image Space;
    Image LF1, LF2, LF3, LF4, LF5;
    Image RF1, RF2, RF3, RF4, RF5;
    Color red, blue, green, orange, purple, pink;
    TextMeshProUGUI cur_word_text;
    float startTime;
    static public int kill_count;
    static public int use_heal;
    static public int use_time;
    // Start is called before the first frame update
    void Start()
    {
        startTime = 0f;
        word = "";
        cur_word_text = GameObject.Find("Current Word").GetComponent<TMPro.TextMeshProUGUI>();
        slime_array = GameObject.FindGameObjectsWithTag("Word");
        green = new Color(0f, 1f, 0f, 0.2f);
        blue = new Color(0f, 0f, 1f, 0.2f);
        purple = new Color(0.5f, 0f, 1f, 0.2f);
        pink = new Color(1f, 0f, 0.5f, 0.2f);
        orange = new Color(1f, 0.5f, 0f, 0.2f);
        red = new Color(1f, 0f, 0f, 0.5f);
        kill_count = 0;
        use_heal = 0;
        use_time = 0;
        // first row
        Q = GameObject.Find("Q").GetComponent<Image>(); W = GameObject.Find("W").GetComponent<Image>(); E = GameObject.Find("E").GetComponent<Image>();
        R = GameObject.Find("R").GetComponent<Image>(); T = GameObject.Find("T").GetComponent<Image>(); Y = GameObject.Find("Y").GetComponent<Image>();
        U = GameObject.Find("U").GetComponent<Image>(); I = GameObject.Find("I").GetComponent<Image>(); O = GameObject.Find("O").GetComponent<Image>();
        P = GameObject.Find("P").GetComponent<Image>();
        // second row
        A = GameObject.Find("A").GetComponent<Image>(); S = GameObject.Find("S").GetComponent<Image>(); D = GameObject.Find("D").GetComponent<Image>();
        F = GameObject.Find("F").GetComponent<Image>(); G = GameObject.Find("G").GetComponent<Image>(); H = GameObject.Find("H").GetComponent<Image>();
        J = GameObject.Find("J").GetComponent<Image>(); K = GameObject.Find("K").GetComponent<Image>(); L = GameObject.Find("L").GetComponent<Image>();
        // third row
        Z = GameObject.Find("Z").GetComponent<Image>(); X = GameObject.Find("X").GetComponent<Image>(); C = GameObject.Find("C").GetComponent<Image>();
        V = GameObject.Find("V").GetComponent<Image>(); B = GameObject.Find("B").GetComponent<Image>(); N = GameObject.Find("N").GetComponent<Image>();
        M = GameObject.Find("M").GetComponent<Image>(); Comma = GameObject.Find("Comma").GetComponent<Image>(); Period = GameObject.Find("Period").GetComponent<Image>();
        // space
        Space = GameObject.Find("Space").GetComponent<Image>();
        // left hand
        LF1 = GameObject.Find("Left Finger 1").GetComponent<Image>(); LF2 = GameObject.Find("Left Finger 2").GetComponent<Image>();
        LF3 = GameObject.Find("Left Finger 3").GetComponent<Image>(); LF4 = GameObject.Find("Left Finger 4").GetComponent<Image>();
        LF5 = GameObject.Find("Left Finger 5").GetComponent<Image>();
        // right hand
        RF1 = GameObject.Find("Right Finger 1").GetComponent<Image>(); RF2 = GameObject.Find("Right Finger 2").GetComponent<Image>();
        RF3 = GameObject.Find("Right Finger 3").GetComponent<Image>(); RF4 = GameObject.Find("Right Finger 4").GetComponent<Image>();
        RF5 = GameObject.Find("Right Finger 5").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // first row
        Q.color = Input.GetKey("q")? red : green; W.color = Input.GetKey("w")? red : blue; E.color = Input.GetKey("e")? red : purple;
        R.color = Input.GetKey("r")? red : pink; T.color = Input.GetKey("t")? red : pink; Y.color = Input.GetKey("y")? red : green;
        U.color = Input.GetKey("u")? red : green; I.color = Input.GetKey("i")? red : blue; O.color = Input.GetKey("o")? red : purple;
        P.color = Input.GetKey("p")? red : pink;
        // second row
        A.color = Input.GetKey("a")? red : green; S.color = Input.GetKey("s")? red : blue; D.color = Input.GetKey("d")? red : purple;
        F.color = Input.GetKey("f")? red : pink; G.color = Input.GetKey("g")? red : pink; H.color = Input.GetKey("h")? red : green;
        J.color = Input.GetKey("j")? red : green; K.color = Input.GetKey("k")? red : blue; L.color = Input.GetKey("l")? red : purple;
        // third row
        Z.color = Input.GetKey("z")? red : green; X.color = Input.GetKey("x")? red : blue; C.color = Input.GetKey("c")? red : purple;
        V.color = Input.GetKey("v")? red : pink; B.color = Input.GetKey("b")? red : pink; N.color = Input.GetKey("n")? red : green;
        M.color = Input.GetKey("m")? red : green; Comma.color = Input.GetKey(",")? red : blue; Period.color = Input.GetKey(".")? red : purple;
        // space
        Space.color = Input.GetKey("space")? red : orange;
        // left hand
        LF1.color = (Input.GetKey("r") || Input.GetKey("t") || Input.GetKey("f") || Input.GetKey("g") || Input.GetKey("v") || Input.GetKey("b"))? red : pink;
        LF2.color = (Input.GetKey("e") || Input.GetKey("d") || Input.GetKey("c"))? red : purple;
        LF3.color = (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("x"))? red : blue;
        LF4.color = (Input.GetKey("q") || Input.GetKey("a") || Input.GetKey("z"))? red : green;
        LF5.color = Input.GetKey("space")? red : orange;
        // right hand
        RF1.color = (Input.GetKey("y") || Input.GetKey("u") || Input.GetKey("h") || Input.GetKey("j") || Input.GetKey("n") || Input.GetKey("m"))? red : green;
        RF2.color = (Input.GetKey("i") || Input.GetKey("k") || Input.GetKey(","))? red : blue;
        RF3.color = (Input.GetKey("o") || Input.GetKey("l") || Input.GetKey("."))? red : purple;
        RF4.color = Input.GetKey("p")? red : pink;
        RF5.color = Input.GetKey("space")? red : orange;

        for(int i = 97; i<123; i++) {
            if(Input.GetKeyDown(((char)i).ToString())) {
                word += (char)i;
                word = word.ToUpper();
                cur_word_text.text = word;
            }
        }
        if(Input.GetKeyDown("backspace") && word != "") {
            word = word.Remove(word.Length - 1, 1);
            cur_word_text.text = word;
            startTime = Time.time;
        }
        if(Input.GetKey("backspace") && ((Time.time - startTime) > 0.5f) && word != "") {
            word = word.Remove(word.Length - 1, 1);
            cur_word_text.text = word;
        }
        if(Input.GetKeyDown("space")) {
            word += " ";
            cur_word_text.text = word;
        }
        slime_array = GameObject.FindGameObjectsWithTag("Word");
        foreach (GameObject slime in slime_array) {
            if(slime.GetComponent<TextMeshPro>().text == word) {
                if (use_heal == 2) {
                    infiniteLvl.health += word.Length;
                } else {
                    infiniteLvl.score += word.Length;
                }
                word = "";
                cur_word_text.text = word;
                Destroy(slime.transform.parent.gameObject);
                kill_count += 1;
            }
        }
        if (word == "HEAL") {
            word = "";
            cur_word_text.text = word;
            if (infiniteLvl.heal > 0) {
            use_heal = 1;
            infiniteLvl.heal -= 1;
            }
        }
        if (word == "TIME") {
            word = "";
            cur_word_text.text = word;
            if (infiniteLvl.time > 0) {
            use_time = 1;
            infiniteLvl.time -= 1;
            }
        }
    }
}