using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Skrypt odpowiedzialny za otwarcie i zamkniecie drzwi.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class DrzwiTrigger : MonoBehaviour {

	/** Płutno komunikatów.*/
	public Canvas komunikaty;
	/** Komunkat.*/
	public Text otworz;

	/** Obiekt odpowiedzialny za zarządzanie animacjami.*/
	private Animator animator;
	/** Obiekt drzwi.*/
	public GameObject drzwiPivot;

	// Use this for initialization
	void Start () {
		obsluzKomunikat(false);
		animator = (Animator)drzwiPivot.GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
	}
	/**
	 * Metoda wywoływana w momencie wejścia gracza w obszar obiektu.
	 */
	void OnTriggerEnter(Collider other) {
		Debug.Log("Player Enter");
		if (other.tag == "Player") {//Jeżeli w obszaże pojawił się gracz.
			obsluzKomunikat(true); // Pokarz płutno komunikatów.
			Drzwi d = (Drzwi)drzwiPivot.GetComponent<Drzwi> ();//Pobierz komponent drzwi opisujący jego stan.
			ustawKomunikat(d.czyOtwarte());//Ustaw stosowny komunikat na podstawie stanu drzwi.
		}
	}

	/**
	 * Metoda wywoływana w sytuacji przebywania gracza w obszarze obiektu.
	 */
	void OnTriggerStay(Collider other) {
		Debug.Log("Player Stay");
		Drzwi d = (Drzwi)drzwiPivot.GetComponent<Drzwi> ();//Pobierz komponent drzwi opisujący jego stan.

		//Jeżeli płutno jest aktywne oraz gracz nacisnął klawisz interakcji 'E'.
		if (komunikaty.enabled == true && Input.GetKeyDown(KeyCode.E)) {
			Debug.Log("E");

			if (!d.czyOtwarte()) {//Jeżeli drzwi zamknięte.
				animator.SetTrigger ("Otworz");//Uruchom animację otwarcie.
				d.czyOtwarte(true);//Ustaw status drzwi w komponęcie drzwi.
			} else {//Jeżeli drzwi otwarte.
				animator.SetTrigger("Zamknij");//Uruchom animację zamkniecia.
				d.czyOtwarte(false);//Ustaw status drzwi w komponęcie drzwi.
			}
			ustawKomunikat(d.czyOtwarte()); //Wyświetl komunikat stosowny do stanu drzwi.
		}

	}

	/**
	 * Metoda wywoływana w momencie wyjścia gracza z obszar obiektu.
	 */
	void OnTriggerExit(Collider other) {
		Debug.Log("Player Exit");
		if (other.tag == "Player") {
			//Ukrycie płutna komunikatów.
			obsluzKomunikat(false);
		}
	}

	/**
	 * Metoda odpowiedzialna za wyświetlanie płutna z komunikatem.
	 * 
	 * @param val zmienna dla stanu płutna komunikatu.
	 */
	private void obsluzKomunikat(bool val){
		if (komunikaty != null) {
			komunikaty.enabled = val; //Wyłączenie/wyłacza płutno komunikatu.
		} else {
			komunikaty = GameObject.FindGameObjectWithTag("Komunikat").GetComponent<Transform>().GetComponent<Canvas>();
		}
	}

	/**
	 * Metoda wyświetla stosowny komunikat w zalerzności od stanu drzwi.
	 * 
	 * @param val flaga określająca status drzwi.
	 */
	private void ustawKomunikat(bool val ){
		if (otworz != null) {
			if(!val) {//Jeżeli drzwi zamknięte.
				otworz.text = "E aby otworzyć";
			} else {
				otworz.text = "E aby zamknąć";
			}
		}
	}

}
