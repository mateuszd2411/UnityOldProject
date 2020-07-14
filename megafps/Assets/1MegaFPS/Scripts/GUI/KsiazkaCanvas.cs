using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
* Klasa odpowiedzialna za zamknięcie płutna wyświetlającego treść książki.
*
* @author Hubert Paluch.
* MViRe - na potrzeby kursu UNITY3D v5.
* mvire.com 
*/
public class KsiazkaCanvas : MonoBehaviour {

	private Transform trans;
	/** Płutno ksiązki.*/
	private Canvas canvas;

	// Use this for initialization
	void Start () {
		trans = GetComponent<Transform>();
		canvas = trans.GetComponent<Canvas>();
        canvas.enabled = false;
    }

	/**
    * Metoda odpowiedzialna za zamknięcie płutna z treścią książki.
    */
	public void zamknij() {
		canvas.enabled = false; //Chowamy płutno z treścią książki.
		Cursor.lockState = CursorLockMode.Locked; //Zablokowanie kursora myszy.
		Cursor.visible = false;//Ukrycie kursora.
		Time.timeScale = 1;//Włączenie czasu.

	}
}
