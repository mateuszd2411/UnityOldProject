using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Skrypt odpowiedzialny za pokazanie/ukrycie płutna ekwipunku.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class CanvasEkwipunek : MonoBehaviour {

	/** Obiekt (płutno) ekwipunku.*/
	private Canvas ekwipunek;

	// Use this for initialization
	void Start () {
		ekwipunek = (Canvas)GetComponent<Canvas>();//Pobranie menu głównego.
		ekwipunek.enabled = false; //Domyślnie ekwipunek wyłączony.
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.I)) { //Jeżeli naciśnięto klawisz "I"

			ekwipunek.enabled = !ekwipunek.enabled;//Ukrycie/pokazanie ekwipunku.			
			Cursor.visible = !ekwipunek.enabled;//Ukrycie/pokazanie kursora myszy.
			
			if(ekwipunek.enabled) {
				Cursor.lockState = CursorLockMode.None;//Odblokowanie kursora myszy.
				Cursor.visible = true;//Pokazanie kursora.
				Time.timeScale = 0;//Zatrzymanie czasu.
			} else {
				Cursor.lockState = CursorLockMode.Locked; //Zablokowanie kursora myszy.
				Cursor.visible = false;//Ukrycie kursora.
				Time.timeScale = 1;//Włączenie czasu.
			}	
		}
	}


}
