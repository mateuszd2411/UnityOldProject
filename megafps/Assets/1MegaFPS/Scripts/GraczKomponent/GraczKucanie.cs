using UnityEngine;
using System.Collections;

/*
* Skrypt odpowiedzialny za kucanie gracza.
* 
* @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class GraczKucanie : MonoBehaviour {

	/** Obiekt odpowiedzialny za ruch gracza.*/
	public CharacterController characterControler;
	
	/** Zmienna pamięta wysokość gracza, która jest potrzebna podczas kucania.*/
	private float wysokoscGracza;
	
	/** Jak szybko gracz kuca.*/
	public float predkoscKucania = 5f;

	/** 
	 * Rozmiar sfery do sprawdzenia w celu unikniecia kolizji. Zanim gracz wstanie za pomocę sfery sprawdzimy czy ma miejsce.
	 */
	public float rozmiarSfery = 0.5f;
	
	/** Sfera sprawdzająca kolizję niech będzie troche ponad głowę a nie idealnie wpasowana w ciało gracza.*/
	public float marginesKolizji = 0.01f;
	
	/** Zmienna zawiera nową wysokość gracza.*/
	private float nowaWysokosc;
	
	/** Zmienna zawiera informację czy gracz kuca (trzyma wciśnięty klawisz kucania).*/
	public bool kucanie = false;
	/** Czy gracz ma wystarczająco miejsca nad sobą aby się pomieścić i uniknąc kolizji.*/
	public bool jestMiejsce = true;

    LayerMask lm;
	
	// Use this for initialization
	void Start () {
		//Pobranie komponentu gracza.
		characterControler = GetComponent<CharacterController>();
		//Zapamiętanie inicjalnej wysokości gracza.
		wysokoscGracza = characterControler.height;

        
    }
	
	// Update is called once per frame
	void Update () {
		//Zawsze zakładamy, że grcz nie kuca ewentualnie potem zostanie to zmienione.
		nowaWysokosc = wysokoscGracza;
		kucnji();
	}
	
	/**
	 * Metoda odpowiedzialna za kucanie.
	 */
	private void kucnji(){		
		if (Input.GetKey (KeyCode.C)) {
			//Jeżeli gracz kucną to zmnijejszamy jego wysokośc o połowe.
			nowaWysokosc = wysokoscGracza / 2;			
			jestMiejsce = czyGraczMaMiejsce (); //Sprawdzamy czy gracz będzie mógł wstać.
			kucanie = true;//Gracz kuca.
		} else {
			kucanie = false; //Gracz przestał kucać (chce przestać kucać).
		}
		
		/**
		 * Kucanie musi wystąpić jeżeli:
		 * - gracz kuca,
		 * - gracz przestał kucać ale nie ma wystarczająco miejsca (wysokość jest za mała),
		 * - gracz przestał kucać ale nie ma wystarczająco miejsca na to aby wstał.
		 */
		if(kucanie || !jestMiejsce) {
			jestMiejsce = czyGraczMaMiejsce (); // Sprawdzamy czy jest miejsce.
			nowaWysokosc = wysokoscGracza / 2; //UStawiamy wysokość dla kucania.
		}		

		//Pobranie jego obecnej wysokości.
		float ostatniaWysokosc = characterControler.height;
		
		//Ustawienie nowej wysokości gracza na podstawie zmiennej 'nowaWysokosc'.
		characterControler.height = Mathf.Lerp (characterControler.height, nowaWysokosc, predkoscKucania * Time.deltaTime);
		
		//Obecna pozycja gracza na osi Y. 
		//Aby uniknąć skoków kolizji CharacterControler'a podczas zmiany romziaru należy zmienić położenie gracza na osi Y (trzeba go trochę podnieść).
		//W celach leprzego efektu.
		float polozenieGraczaY = transform.position.y + (characterControler.height - ostatniaWysokosc) / 2;
		
		//Wektor z pozycją gracza.
		Vector3 pozycja = new Vector3 (transform.position.x, polozenieGraczaY, transform.position.z);
		//Przypisanie nowej pozycji.
		transform.position = pozycja;

	}
	
	/**
	 * Funkcja sprawdza czy gracz może wstać czy jest miejsce na CharacterControlera.
	 * Funkcja tworzy w miejscu głowy gracza sferę a następnie sprawdza czy znajduje się w niej jakiś obiekt.
	 * Jeżeli w sferze znajduje się jakiś obiekt to gracz nie może wstać bo nastąpi kolizja.
	 */
	private bool czyGraczMaMiejsce(){
		
		/**
		 * Gracz ma wysokość 2m środek(pozycja) gracza jest na wysokości 1m.
		 * Podczas kucania środek(pozycja) gracza znajduje się na wysokości 0.5m.
		 * Środek sfery powinien znajdować się na wysokości 1.5m aby obiłą całą górną część.
		 * Do sfery dodany jest margines w celu weryfikacji potencjalnej kolizji.
		 */
		float pozycjaSferySprawdzania = transform.position.y + 1+ marginesKolizji;
		
		/** Tworzę wektor z pozycją dla sfery.*/
		Vector3 pozycjaSfery = new Vector3(transform.position.x, pozycjaSferySprawdzania, transform.position.z);
		
		/* Pobranie wszystkich obiektów zanjdujących się w utworzonej sferze.*/
		Collider[] colliders = Physics.OverlapSphere (pozycjaSfery, rozmiarSfery);
		
		//Debug.Log(colliders.Length);		
		//Przeiterowanie obiektów (do usunięcia)		
		/*
		foreach(Collider c in colliders){			
			Debug.Log(c.gameObject.name);			
		}
		*/

		//Jeżeli w tablicy znajdują się jeszcze jakieś obiekty to oznacza, że gracz nie może wstać.
		return colliders.Length > 0 ? false : true;
	}
}

// All Rights Reserved!
// Wszelkie Prawa Zastrzeżone!
// Tylko do użytku niekomercyjnego.