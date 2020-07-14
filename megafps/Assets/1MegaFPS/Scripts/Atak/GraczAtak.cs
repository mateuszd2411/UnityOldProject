using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Skrypt odpowiedzialny za atak gracza oraz ustawienie typu ataku. 
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class GraczAtak : MonoBehaviour {

	/** Typy ataków jakie może wykonać gracz.*/
	public TypAtaku typAtaku = TypAtaku.Kamien;

	/** Zawiera obrazek aktualnie wybranej broni.*/
	public Image wybranaBron;
	/** Przechowuje obrazek broni typu kamień.*/
	public Sprite kamien;
	/** Przechowuje obrazek broni typu pistolet.*/
	public Sprite pocisk;
	/** Przechowuje obrazek broni typu rakieta.*/
	public Sprite rakieta;

	/** Skrypt odpowiedzialny za strzelanie przy pomocy broni typu pistolet.*/
	private StrzalPistolet strzalPistolet;
	/** Skrypt odpowiedzialny za strzelanie przy pomocy broni typu kamień.*/
	private StrzalKamieniem strzalKamien;
	/** Skrypt odpowiedzialny za strzelanie przy pomocy broni z rakietą.*/
	private StrzalRakieta strzalRakieta;

    public bool pojedynczyStrzal;
    private bool strzelaj;
    
    /** 
    * Jeżeli 'true' to nie można się poruszać.
    * Opcja ustawiana jeżeli na przykład jakieś płutno (Canvas) z tagiem "Menu"  jest włączone (np. menu główne, opcje itd.) 
    * to poruszanie jest zablokowane.
    */
    private bool ruchZablokowany = false;

    /** Zawiera obrazek aktualnie wybranej broni.*/
    public GameObject ekwipunekWybranaBron;
	

	void Start () {
		/** Pobranie wszystkich skryptów odpowiedzialnych za strzelanie.*/
		strzalPistolet = (StrzalPistolet) GetComponent<StrzalPistolet>();
		strzalKamien = (StrzalKamieniem) GetComponent<StrzalKamieniem>();
		strzalRakieta = (StrzalRakieta) GetComponent<StrzalRakieta>();
	}

	void Update () {
		//Ustawienie broni.
		wybierzBron ();
		odliczanieDoStrzalu();
		if (!isDead () && !ruchZablokowany) {
			if (Input.GetMouseButtonDown (0) ) {//Jeżeli naciśnięto przycisk myszy to wykonaj strzał.
                wykonajStrzal();
            } 
		}
	}

	/**
	 * Wykonuje atak zgodnie z wybranym typem broni.
	 */
	public void wykonajStrzal(){
		switch (typAtaku) {
			case TypAtaku.Kamien: //Atak pociskiem
				if (strzalKamien != null) {
					strzalKamien.strzal ();
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

	/**
	* Metoda odpowiedzialna za wybranie/ustawienie broni i typu ataku zgodnie
	* z naciśniętym klawiszem numerycznym.
	* Uaktualnia obrazek wybranej broni.
	* 
	* Naciśniecie klawisza 
	* 1 - ustawia atak/strzał typu kamień.
	* 2 - ustawia atak/strzał typu pistolet.
	* 3 - ustawia atak/strzał typu rakieta.
	*/
	private void wybierzBron(){

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			typAtaku = TypAtaku.Kamien;//Ustawia typ ataku.
			wybranaBron.overrideSprite  = kamien;//Uaktualnia obrazek wybranej broni na kamień.
		} else if(Input.GetKeyDown(KeyCode.Alpha2)){
			typAtaku = TypAtaku.Pistolet;//Ustawia typ ataku.
			wybranaBron.overrideSprite  = pocisk;//Uaktualnia obrazek wybranej broni na pistolet.
		}else if(Input.GetKeyDown(KeyCode.Alpha3)){
			typAtaku = TypAtaku.Rakieta;//Ustawia typ ataku.
			wybranaBron.overrideSprite  = rakieta;//Uaktualnia obrazek wybranej broni na rakieta.
		}


		//ObszarUpuszczenia ou = ekwipunekWybranaBron.transform.GetComponent<ObszarUpuszczenia> ();
		if (ekwipunekWybranaBron != null) {
			if(ekwipunekWybranaBron.transform.childCount > 0) {
				//Pobieram element umieszczony w slocie.
				Transform eqElem = ekwipunekWybranaBron.transform.GetChild (0);

				//Jeżeli w slocie jest element.
				if(eqElem != null) {
					//Pobieram ikonę broni. Ikona broni jest w elemencie tła.
					Image ikon = eqElem.GetComponent<Image>().transform.GetChild(0).transform.GetComponent<Image>();

                    //Pobieram skrypt/komponent zawierający informacie o typie broni.
                    EqElementInfo elE = eqElem.GetComponent<EqElementInfo>();

					this.typAtaku = elE.typAtaku;//Ustawiam typ ataku na podstawie elementu.
					wybranaBron.overrideSprite = ikon.sprite;//Ustawiam ikonkę aktualnej broni na podstawie ikony ze slotu.
				}
			}
		}
	}

	/**
	 * Metoda zwraca informację o tym czy gracz ciągle żyje.
	 * 
	 * Zwraca 'true' jeżeli gracz jeszcze żyje w przeciwnym razei 'false'.
	 */
	private bool isDead(){
		Zdrowie zdrowie = gameObject.GetComponent<Zdrowie> ();
		if (zdrowie != null && zdrowie.czyMartwy()) {
			return true;
		}
		return false;
	}

	/**
	 * Metoda odliczania do strzalu. 
	 * Aby uniknoąć naliczania odliczania do strzału w momencie gdy naciśnięty jest przycisk myszy.
	 * Licznik odliczania jest wykonywany zawsze.
	 */
	private void odliczanieDoStrzalu(){
		if (strzalKamien != null) {
			strzalKamien.setOdliczanieDoStrzalu ();
		}
		if (strzalPistolet != null) {
			strzalPistolet.setOdliczanieDoStrzalu ();
		}
		if (strzalRakieta != null) {
			strzalRakieta.setOdliczanieDoStrzalu ();
		}
	}	

    public void SetRuchZablokowany(bool val) {
        this.ruchZablokowany = val;
    }
}
// All Rights Reserved!
// Wszelkie Prawa Zastrzeżone!
// Tylko do użytku niekomercyjnego.
