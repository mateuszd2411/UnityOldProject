using UnityEngine;
using System.Collections;

/**
* Klasa odpowiedzialna za inicjalne schowanie płótna danego komponentu.
* Niektóre płótna powinny zostać inicjalnie(przy starcie) wyłączone ten skrypt to zapewnia.
*
* @author Hubert Paluch.
* MViRe - na potrzeby kursu UNITY3D v5.
* mvire.com 
*/
public class UstawieniaDomyslnePlutna : MonoBehaviour {

    private Transform trans;
    /** Płutno ksiązki.*/
    private Canvas canvas;

    // Use this for initialization
    void Start () {
        trans = GetComponent<Transform>();
        canvas = trans.GetComponent<Canvas>();
        canvas.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
