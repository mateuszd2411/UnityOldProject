using UnityEngine;
using System.Collections;

/**
 * Klada odpowiedzialna za wykonanie strzału/rzutu kamieniem/obiektem przez gracza.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class StrzalKamieniem : MonoBehaviour {

	//Co ile mozna wykonac strzal
	public float czekaj = 1f;
	//Odliczanie do kolejnego strzalu.
	public float odliczanieDoStrzalu = 0f;

	//Obiekt kamienia.
	public GameObject kamienPrefab;
	public float predkosc = 50;

	/** Źródło dzwięki.*/
	public AudioSource zrodloDzwieku;
	/** Dzwięk strzału.*/
	public AudioClip odglosStrzalu;

	/**
	 * Metoda odpowiedzialna za wykonanie strzału/rzutu kamieniem.
	 */
	public void strzal () {
		if(odliczanieDoStrzalu <= 0){ //Jezeli nacisniety przycisk fire
			//Strzal zostal oddany ustawienie ponownego odliczania.
			odliczanieDoStrzalu = czekaj;

			GameObject kamien;
			//Utworzenie instatncji poocisku, pocisk/kamień nie potrzebuje specjalnego zwrotu więc Quaternion.identity.
			kamien = (GameObject) Instantiate(kamienPrefab, Camera.main.transform.position+Camera.main.transform.forward, Quaternion.identity);
			//Wprawienie pocisku w ruch za pomocą oddziaływania na niego siłą (na Rigidbody).
			kamien.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * predkosc, ForceMode.Impulse);

			if(zrodloDzwieku != null) {//Na wszelki wypadek jak by nie zostało podpięte źródło dzwięku.
				zrodloDzwieku.PlayOneShot(odglosStrzalu);
			}
		}
	}

	/**
	 * Metoda odliczania do strzału w celu odliczania nawet jeżei nie jest naciśnięty przycisk myszy.
	 */
	public void setOdliczanieDoStrzalu(){
		if (odliczanieDoStrzalu > 0) {
			//Zmniejszanie licznika do kolejnego strzalu/odliczanie do strzalu.
			odliczanieDoStrzalu -= Time.deltaTime;
		}
	}

}
