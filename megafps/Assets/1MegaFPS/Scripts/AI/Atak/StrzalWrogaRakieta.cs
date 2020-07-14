using UnityEngine;
using System.Collections;

/**
 * Klasa odpowiedzialna za oddanie strzału przez wroga.
 * Strzał z bazooki/rakietnicy.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class StrzalWrogaRakieta : StrzalWroga {

	//Co ile mozna wykonac strzal
	public float czekaj = 1f;
	//Odliczanie do kolejnego strzalu.
	private float odliczanieDoStrzalu = 0f;

	//Obiekt pocisku.
	public GameObject rakietaPrefab;

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
		//do kolejnego strzalu jest rowne zero(mozna strzelac).
		if ( odliczanieDoStrzalu <= 0 && namierzanie() && celowanie(getZasiegWzroku())) {
			//Ustawienie licznika na nowo.
			odliczanieDoStrzalu = czekaj;

			//Wystrzelenie rakiety z pozycji przeciwnika oraz jego zwrocie/obrocie
			//Poniewaz pocisk jest durzy to aby uniknac kolizji z samym soba jest dodany dodatkowa odleglosc (gameObject.transform.forward)
			Instantiate(rakietaPrefab, transform.position+transform.forward, getRotacjaPocisku());
			zrodloDzwieku.PlayOneShot(odglosStrzalu);
		}
	}

}
