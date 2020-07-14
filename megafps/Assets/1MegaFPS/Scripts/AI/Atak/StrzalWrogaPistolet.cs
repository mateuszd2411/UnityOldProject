using UnityEngine;
using System.Collections;

/**
 * Klasa odpowiedzialna za oddanie strzalu z broni palnej.
 * 
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class StrzalWrogaPistolet : StrzalWroga {

	//Zasiek strzalu
	public float zasieg = 100.0f;
	//Co ile mozna wykonac strzal
	public float czekaj = 0.5f;
	//Odliczanie do kolejnego strzalu.
	public float odliczanieDoStrzalu = 0f;

	//Obrarzenie jakie zadaje strzal (ile punktow zdrowia zabiera).
	public float obrazenia = 20.0f;

	/** Źródło dzwięki.*/
	public AudioSource zrodloDzwieku;
	/** Dzwięk skoku.*/
	public AudioClip odglosStrzalu;


	public void strzal () {
		
		//Zmniejszanie licznika do kolejnego strzalu/odliczanie do strzalu.
		if (odliczanieDoStrzalu < czekaj) {
			odliczanieDoStrzalu += Time.deltaTime;
		}
		
		//Strzal nastepuje jezeli przycisk myszy jest ciagle wcisniety oraz jezeli odliczanie
		//do kolejnego strzalu jest rowne zero(mozna strzelac).
		if (odliczanieDoStrzalu >= czekaj && namierzanie() && celowanie(getZasiegWzroku())) {
			//Strzal zostal oddany ustawienie ponownego odliczania.
			odliczanieDoStrzalu = 0;

			//Pobranie obiektu gracza.
			Zdrowie zdrowie = (Zdrowie) graczObiekt.GetComponent<Zdrowie>();
			if(zdrowie != null && !zdrowie.czyMartwy()) { 
				zdrowie.otrzymaneObrazenia(obrazenia);
				zrodloDzwieku.PlayOneShot(odglosStrzalu);
			}

		}
		
	}
}
