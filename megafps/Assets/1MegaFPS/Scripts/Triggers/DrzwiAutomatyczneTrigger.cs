using UnityEngine;
using System.Collections;

/**
 * Skrypt odpowiedzialny za otwarcie i zamkniecie drzwi automatycznych.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class DrzwiAutomatyczneTrigger : MonoBehaviour {

	/** Animator drzwi.*/
	private Animator animator;
	/** Obiekt drzwi.*/
	public GameObject drzwiPivot;

	// Use this for initialization
	void Start () {
		animator = (Animator)drzwiPivot.GetComponent<Animator> ();
	}

	/**
	 * Metoda wywoływana w momencie wejścia gracza w obszar obiektu.
	 */
	void OnTriggerEnter(Collider other) {
		Debug.Log("Player Enter");
		if (other.tag == "Player") {//Jeżeli w obszaże pojawił się gracz.
			Drzwi d = (Drzwi)drzwiPivot.GetComponent<Drzwi> ();//Pobierz komponent drzwi opisujący jego stan.

			if (!d.czyOtwarte()) {//Jeżeli drzwi zamknięte.
				animator.SetTrigger ("OpenAutomatic");//Uruchom animację otwarcie.
				d.czyOtwarte(true);//Ustaw status drzwi w komponęcie drzwi.
			}
		}
	}

	/**
	 * Metoda wywoływana w momencie wyjścia gracza z obszar obiektu.
	 */
	void OnTriggerExit(Collider other) {
		Debug.Log("Player Exit");
		if (other.tag == "Player") {
			Drzwi d = (Drzwi)drzwiPivot.GetComponent<Drzwi> ();//Pobierz komponent drzwi opisujący jego stan.

			if (d.czyOtwarte()) {//Jeżeli drzwi otwarte.
				animator.SetTrigger("CloseAutomatic");//Uruchom animację zamkniecia.
				d.czyOtwarte(false);//Ustaw status drzwi w komponęcie drzwi.
			}

		}
	}

}
