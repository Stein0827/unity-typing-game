using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    Image Q, W, E, R, T, Y, U, I, O, P;
    Image A, S, D, F, G, H, J, K, L;
    Image Z, X, C, V, B, N, M;
    Color white, red;
    // Start is called before the first frame update
    void Start()
    {
        white = new Color(1f, 1f, 1f, 0.2f);
        red = new Color(1f, 0f, 0f, 0.2f);
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
        M = GameObject.Find("M").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // first row
        Q.color = Input.GetKey("q")? red : white; W.color = Input.GetKey("w")? red : white; E.color = Input.GetKey("e")? red : white;
        R.color = Input.GetKey("r")? red : white; T.color = Input.GetKey("t")? red : white; Y.color = Input.GetKey("y")? red : white;
        U.color = Input.GetKey("u")? red : white; I.color = Input.GetKey("i")? red : white; O.color = Input.GetKey("o")? red : white;
        P.color = Input.GetKey("p")? red : white;
        // second row
        A.color = Input.GetKey("a")? red : white; S.color = Input.GetKey("s")? red : white; D.color = Input.GetKey("d")? red : white;
        F.color = Input.GetKey("f")? red : white; G.color = Input.GetKey("g")? red : white; H.color = Input.GetKey("h")? red : white;
        J.color = Input.GetKey("j")? red : white; K.color = Input.GetKey("k")? red : white; L.color = Input.GetKey("l")? red : white;
        // third row
        Z.color = Input.GetKey("z")? red : white; X.color = Input.GetKey("x")? red : white; C.color = Input.GetKey("c")? red : white;
        V.color = Input.GetKey("v")? red : white; B.color = Input.GetKey("b")? red : white; N.color = Input.GetKey("n")? red : white;
        M.color = Input.GetKey("m")? red : white;
    }
}