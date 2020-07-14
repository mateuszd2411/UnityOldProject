using UnityEngine;
using System.Collections;

/**
 * Klasa odpowiedzialna za uruchomienie ataku (oddanie strzalu).
 * Atak z bazooki/ramienia/pistoletu.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class PrzeciwnikAtak : MonoBehaviour {

	/** Taka/żut kamieniem (żut).*/
	private StrzalWrogaKamieniem strzalKamieniem;
	/** Atak/strzał za pomocą rakiety. */
	private StrzalWrogaRakieta strzalRakieta;
	/** Atak/strzał za pomocą broni palnej.*/
	private StrzalWrogaPistolet strzalPistolet;

	/** Typy ataków jakie może wykonać przecisnik.*/
	public TypAtaku typAtaku;

	/** Obiekt gracza.*/
	private GameObject gracz;

	// Use this for initialization
	void Start () {
		//Pobranie obiektu gracza.
		gracz = GameObject.FindWithTag("Player");

		strzalKamieniem = (StrzalWrogaKamieniem)gameObject.GetComponent<StrzalWrogaKamieniem> ();
		strzalRakieta = (StrzalWrogaRakieta)gameObject.GetComponent<StrzalWrogaRakieta> ();
		strzalPistolet = (StrzalWrogaPistolet)gameObject.GetComponent<StrzalWrogaPistolet> ();	
	}


	/**
	 * Wykonuje atak.
	 */
	public void wykonajAtak(){
		Zdrowie zdrowie = (Zdrowie)gracz.GetComponent<Zdrowie>();
		if (!zdrowie.czyMartwy ()) {
			switch (typAtaku) {
			case TypAtaku.Kamien: //Atak pociskiem
				if (strzalKamieniem != null) {
					strzalKamieniem.strzal ();
				}
				break;
			case TypAtaku.Rakieta:
				if (strzalRakieta != null) {
					strzalRakieta.strzal ();
				}
				break;		
			case TypAtaku.Pistolet:
                   
				if (strzalPistolet != null) {
					strzalPistolet.strzal ();
				}
				break;		
			}
		}
	}
}
