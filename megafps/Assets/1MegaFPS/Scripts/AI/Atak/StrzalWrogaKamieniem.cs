using UnityEngine;
using System.Collections;

/**
 * Klasa odpowiedzialna za oddanie strzału przez wroga.
 * Strzał/rzut kamieniem.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class StrzalWrogaKamieniem : StrzalWroga {

	//Co ile mozna wykonac strzal
	public float czekaj = 1f;
	//Odliczanie do kolejnego strzalu.
	private float odliczanieDoStrzalu = 0f;

	//Obiekt kamienia.
	public GameObject kamienPrefab;
	public float predkosc = 50;

	/** Źródło dzwięki.*/
	public AudioSource zrodloDzwieku;
	/** Dzwięk skoku.*/
	public AudioClip odglosStrzalu;

	/**
	 * Metoda wykonująca strzał.
	 */
	public void strzal() {
		//Zmniejszanie licznika do kolejnego strzalu/odliczanie do strzalu.
		odliczanieDoStrzalu -= Time.deltaTime;
		
		//Strzal nastepuje jezeli odliczanie
		//do kolejnego strzalu jest rowne zero(można strzelać).
		if ( odliczanieDoStrzalu <= 0 && namierzanie() && celowanie(getZasiegWzroku())) {
			odliczanieDoStrzalu = czekaj;

			GameObject kamien;
			//Utworzenie instatncji poocisku, pocisk/kamień nie potrzebuje specjalnego zwrotu więc Quaternion.identity.
			kamien = (GameObject) Instantiate(kamienPrefab, transform.position+transform.forward, Quaternion.identity);
			//Wprawienie pocisku w ruch za pomocą oddziaływania na niego siłą (na Rigidbody).
			kamien.GetComponent<Rigidbody>().AddForce(transform.forward * predkosc, ForceMode.Impulse);

			zrodloDzwieku.PlayOneShot(odglosStrzalu);
		}
	
	}
}
