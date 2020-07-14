using UnityEngine;
using System.Collections;

/**
 * Klasa odpowiedzialna za symulacje AI.
 * Animacja wroga chodzącego po terenie z wykorzystaniem Rigidbody.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class DobreAI : MonoBehaviour {

	//Predkosc obrotu przeciwnika.
	public float predkoscObrotu = 6.0f;

	//Gładki obrót przeciwnika
	public bool gladkiObrot = true;

	//Prędkość poruszania się przeciwnika.
	public float predkoscRuchu = 5.0f; 
	//Odległość na jaką widzi przeciwnik.
	public float zasiegWzroku = 30f;
	//Odstęp w jakim zatrzyma się obiekt wroga od gracza.
	public float odstepOdGracza = 2f;

	private Transform mojObiekt; 
	private GameObject graczObiekt;
	private Transform gracz;
	private bool patrzNaGracza = false;
	private Vector3 pozycjaGraczaXYZ; 

	/** Dostarcza informację o tym czy wróg się porusza.*/
	private bool czySiePorusza;

	/** Źródło dzwięki.*/
	public AudioSource zrodloDzwieku;
	/** Dzwięk chodzenia.*/
	public AudioClip chodzenie;
	public float odliczanieDoKroku = 0f;
	public float czasKroku = 0.5f;

	/**Skrypt ataku.*/
	private PrzeciwnikAtak atak;

	// Use this for initialization
	void Start () {
		mojObiekt = transform; 
		//Rigidbody pozwala aby na obiekt oddziaływała fizyka.
		//Wyłaczenie oddziaływanie fizyki na XYZ - 
		// jak obiekt będzie wchodził pod górkę to się przechyli prostopadle do zbocza a fizyka pociągnie go w dół i
		// obiekt się przewróci. POZATYM NIE CHCEMY ABY WRÓG SIĘ TAK DZIWNIE OBRACAŁ ;).
		if (GetComponent<Rigidbody> ()) {
			GetComponent<Rigidbody> ().freezeRotation = true;
		}
		//Pobranie obiektu gracza.
		graczObiekt = GameObject.FindWithTag("Player"); 
		gracz = graczObiekt.transform; 

		atak = gameObject.GetComponent<PrzeciwnikAtak>();
	}

	// Update is called once per frame
	void Update () {
        if (graczObiekt == null) {
            graczObiekt = GameObject.FindWithTag("Player");
            gracz = graczObiekt.transform;
            Debug.Log("-- 1-- "+graczObiekt);
        }
        if (!czyGraczMartwy ()) {//Jężeli gracz ciągle żyje to wykonuj animacje ruchu.
			dzwiekChodzenia();
			ruchWroga();
		}
        
	}

	/**
	 * Metoda odpowiedzialna za wykonanie ruchu wroga.
	 */
	private void ruchWroga(){
		//Pobranie pozycji gracza.
		pozycjaGraczaXYZ = new Vector3 (gracz.position.x, mojObiekt.position.y, gracz.position.z);
		
		//Pobranie dystansu dzielącego wroga od obiektu gracza.
		float dist = Vector3.Distance (mojObiekt.position, gracz.position);
		
		patrzNaGracza = false; //Wróg nie patrz na gracza bo jeszcze nie wiadomo czy jest w zasięgu wzroku.
		
		//Sprawdzenie czy gracz jest w zasięgu wzroku wroga.
		if (dist <= zasiegWzroku && dist > odstepOdGracza && !isDead ()) {
			czySiePorusza = true;
			patrzNaGracza = true;//Gracz w zasiegu wzroku wiec na neigo patrzymy
			
			//Teraz wykonujemy ruch wroga.
			//Vector3.MoveTowards - pozwala na zdefiniowanie nowej pozycji gracza oraz wykonanie animacji.
			//Pierwszy parametr obecna pozycja drógi parametr pozycja do jakiej dążymy (czyli pozycja gracza).
			//Trzeci parametr określa z jaką prędkością animacja/ruch ma zostać wykonany.
			mojObiekt.position = Vector3.MoveTowards (mojObiekt.position, pozycjaGraczaXYZ, predkoscRuchu * Time.deltaTime);
			
			atak.wykonajAtak ();
			
		} else if (dist <= odstepOdGracza && !isDead ()) { //Jeżeli wróg jest tuż przy graczu to niech ciągle na niego patrzy mimo że nie musi się już poruszać.
			patrzNaGracza = true;
			atak.wykonajAtak ();
			czySiePorusza = false;
		}
		
		//Jeżeli obiekt jeszcze ma punkty zdrowia to na nas patrzy, podąża za nami.
		if (!isDead ()) {
			obrotWStroneGracza ();
		} else {//Obiekt nieżyje.
			czySiePorusza = false;
			if (GetComponent<Rigidbody> ()) {
				GetComponent<Rigidbody> ().freezeRotation = false;
			}
		}
	}

	/**
	 * Wróg może nie mieć potrzeby sie pruszać bo jest blisko gracza ale niech się obraca w jego stronę.
	 */
	private void obrotWStroneGracza(){
		if (gladkiObrot && patrzNaGracza == true){

			//Quaternion.LookRotation - zwraca quaternion na podstawie werktora kierunku/pozycji. 
			//Potrzebujemy go aby obrócić wroga w stronę gracza.
			Quaternion rotation = Quaternion.LookRotation(pozycjaGraczaXYZ - mojObiekt.position);
			//Obracamy wroga w stronę gracza.
			mojObiekt.rotation = Quaternion.Slerp(mojObiekt.rotation, rotation, Time.deltaTime * predkoscObrotu);
			obrotGlowy();

		} else if(!gladkiObrot && patrzNaGracza == true){ //Jeżeli nei chcemy gładkiego obracania się wroga tylko błyskawicznego obrotu.		
			//Błyskawiczny obrót wroga.
			transform.LookAt(pozycjaGraczaXYZ);
		}
	}

	/**
	 * Glowa przeciwnika.
	 * Przeciwnik ma głowę po to aby ją obracać, ciało jest wyprostowane ale głowa niech się obraca
	 * w celu śledzenia gracza (weryfikacji czy przeciwnik patrzy na gracza pomimo pionowej postawy).
	 * 
	 * Metoda ustawia/obraca tylko obiekt głowy w strone gracza.
	 */
	private void obrotGlowy() {
		//Pobranie obiektu glowa dolączonego do wroga.
		Transform glowa = transform.Find("Glowa");

		if (glowa != null) {
			//Pobranie pozycji gracza już razem z jego wysokością.
			Vector3 graczXYZ = new Vector3 (gracz.position.x, gracz.position.y, gracz.position.z);
			Quaternion wStroneGracza;
			if (gladkiObrot && patrzNaGracza == true){
				//Quaternion.LookRotation - zwraca quaternion na podstawie werktora kierunku/pozycji. 
				//Potrzebujemy go aby obrócić glowe wroga w stronę gracza.
				wStroneGracza = Quaternion.LookRotation (graczXYZ - mojObiekt.position);
				//Obracamy glowe wroga w stronę gracza.
				glowa.rotation = Quaternion.Slerp (glowa.rotation, wStroneGracza, Time.deltaTime * predkoscObrotu);
			} else {
				glowa.LookAt(graczXYZ);
			}

		}
	}

	/**
	 * Funkcja zwraca informację o tym czy obiekt jeszcze posiada punkty zdrowia.
	 */
	bool isDead(){
		Zdrowie h = gameObject.GetComponent<Zdrowie>();
		if(h != null) {
			return h.czyMartwy();
		}
		return false;
	}

	/**
	 * Funkcja zwraca informację o tym czy gracz ciągle żyje.
	 * Jeżeli gracz żyje to zwraca 'false' w przeciwnym razie 'true;'
	 */
	private bool czyGraczMartwy(){
       
		Zdrowie zdrowie = graczObiekt.GetComponent<ZdrowieGracza> ();
		if (zdrowie != null && zdrowie.czyMartwy ()) {
			return true;
		}
		return false;
	}

	/**Funkcja zwraca zasięg wzroku przeciwnik.*/
	public float getZasiegWzroku(){
		return zasiegWzroku;
	}

	/**
	 * Metoda odpowiedzialna za oddtwarzanie dzwięku chodzenia gracza.
	 */
	private void dzwiekChodzenia(){
		//Zmniejszanie licznika do kolejnego odtworzenia dźwięku.
		if (odliczanieDoKroku > 0) {
				odliczanieDoKroku -= Time.deltaTime;
		}
		//Jeżeli gracz się porusza to odgrywaj dzwięk poruszania.
		if (czySiePorusza && odliczanieDoKroku <= 0) {

			odliczanieDoKroku = czasKroku;
			zrodloDzwieku.PlayOneShot (chodzenie);
		}				
		
	}
}

// All Rights Reserved!
// Wszelkie Prawa Zastrzeżone!
// Tylko do użytku niekomercyjnego.