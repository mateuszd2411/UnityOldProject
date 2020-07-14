using UnityEngine;
using System.Collections;

/**
 * Klasa odpowiedzialna za poruszanie się wroga.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class DobreAIAnim : MonoBehaviour {

    private Transform trans;
    private Transform rodzicTrans;

    //Predkosc obrotu przeciwnika.
    public float predkoscObrotu = 6.0f;
	
	//Gładki obrót przeciwnika
	public bool gladkiObrot = true;
	
	//Odległość na jaką widzi przeciwnik.
	public float zasiegWzroku = 30f;
	//Odstęp w jakim zatrzyma się obiekt wroga od gracza.
	public float odstepOdGracza = 2f;	

	/** Dostarcza informację o tym czy wróg się porusza.*/
	protected bool czySiePorusza;
	//Pobranie dystansu dzielącego wroga od obiektu gracza.
	protected float dystans;
	
	private Transform mojObiekt; 
	private Transform gracz;
	private bool patrzNaGracza = false;
	private Vector3 pozycjaGraczaXYZ; 
	
	/** Obiekt animatora niezbędny do wywołania odpowiedniej animacji.*/
	private Animator animator;
	//Od jakiego momentu przeciwnik zaczyna iść.
	public float zacznijIsc = 10;
	//Prędkość z jaką chodzi przeciwnika.
	public float predkoscChodzenia = 2.0f;
	//Prędkość z jaką biega przeciwnika.
	public float predkoscBiegania = 5.0f;
	//Czy ma zostać oddany skok.
	public bool skok;
	//Odliczanie do zakończenia animacji skoku.
	private float skokTime = 0;
	//Czy została już odegrana animacja śmierci.
	private bool martwy = false;

    /**Skrypt ataku.*/
    private PrzeciwnikAtak atak;

    // Use this for initialization
    void Start () {
        trans = GetComponent<Transform>();
        rodzicTrans = trans.parent;

        mojObiekt = rodzicTrans; 
		//Rigidbody pozwala aby na obiekt oddziaływała fizyka.
		//Wyłaczenie oddziaływanie fizyki na XYZ - 
		// jak obiekt będzie wchodził pod górkę to się przechyli prostopadle do zbocza a fizyka pociągnie go w dół i
		// obiekt się przewróci. POZATYM NIE CHCEMY ABY WRÓG SIĘ TAK DZIWNIE OBRACAŁ ;).
		if (rodzicTrans.GetComponent<Rigidbody> ()) {
            rodzicTrans.GetComponent<Rigidbody> ().freezeRotation = true;
		}
		
		//Pobranie obiektu gracza.
		gracz = GameObject.FindWithTag("Player").transform; 
		animator = (Animator)rodzicTrans.GetComponent<Animator> ();
        atak = GetComponent<PrzeciwnikAtak>();

    }
	
	// Update is called once per frame
	void Update () {
		//Pobranie pozycji gracza.
		pozycjaGraczaXYZ = new Vector3(gracz.position.x, mojObiekt.position.y, gracz.position.z);
		
		//Pobranie dystansu dzielącego wroga od obiektu gracza.
		dystans = Vector3.Distance (mojObiekt.position, gracz.position);
		
		patrzNaGracza = false; //Wróg nie patrz na gracza bo jeszcze nie wiadomo czy jest w zasięgu wzroku.
		
		animacjaWroga();
		
		//Sprawdzenie czy gracz jest w zasięgu wzroku wroga.
		if(dystans <= zasiegWzroku && dystans > odstepOdGracza && !czyPrzeciwnikMatwy ()) {
			patrzNaGracza = true;//Gracz w zasiegu wzroku wiec na neigo patrzymy
			czySiePorusza = true;
			

			
			//Teraz wykonujemy ruch wroga.
			//Vector3.MoveTowards - pozwala na zdefiniowanie nowej pozycji gracza oraz wykonanie animacji.
			//Pierwszy parametr obecna pozycja drógi parametr pozycja do jakiej dążymy (czyli pozycja gracza).
			//Trzeci parametr określa z jaką prędkością animacja/ruch ma zostać wykonany.
			mojObiekt.position = Vector3.MoveTowards(mojObiekt.position, pozycjaGraczaXYZ, getPredkoscRuchu() * Time.deltaTime);
            atak.wykonajAtak();
        } else if(dystans <= odstepOdGracza && !czyPrzeciwnikMatwy ()) { //Jeżeli wróg jest tuż przy graczu to niech ciągle na niego patrzy mimo że nie musi się już poruszać.
			patrzNaGracza = true;
			czySiePorusza = false;
            atak.wykonajAtak();
        }
		
		//Jeżeli obiekt jeszcze ma punkty zdrowia to na nas patrzy, podąża za nami.
		if (!czyPrzeciwnikMatwy ()) {
			obrotWStroneGracza ();
		} else {//Obiekt nieżyje.
			czySiePorusza = false;
            animator.enabled = false;
            if (rodzicTrans.GetComponent<Rigidbody> ()) {
                rodzicTrans.GetComponent<Rigidbody> ().freezeRotation = false;
			}
		}
		
	}
	
	//Wróg może nie mieć potrzeby sie pruszać bo jest blisko gracza ale niech się obraca w jego stronę.
	void obrotWStroneGracza(){
		if (gladkiObrot && patrzNaGracza == true){
			//Quaternion.LookRotation - zwraca quaternion na podstawie werktora kierunku/pozycji. 
			//Potrzebujemy go aby obrócić wroga w stronę gracza.
			Quaternion rotation = Quaternion.LookRotation(pozycjaGraczaXYZ - mojObiekt.position);
			//Obracamy wroga w stronę gracza.
			mojObiekt.rotation = Quaternion.Slerp(mojObiekt.rotation, rotation, Time.deltaTime * predkoscObrotu);
		} else if(!gladkiObrot && patrzNaGracza == true){ //Jeżeli nei chcemy gładkiego obracania się wroga tylko błyskawicznego obrotu.
			
			//Błyskawiczny obrót wroga.
			transform.LookAt(pozycjaGraczaXYZ);
		}
	}
	
	/**
	 * Funkcja zwraca informację o tym czy obiekt jeszcze posiada punkty zdrowia.
	 */
	bool czyPrzeciwnikMatwy(){
		Zdrowie h = gameObject.GetComponent<Zdrowie>();
		if(h != null) {
			return h.czyMartwy();
		}
		return false;
	}
	
	/**
	 * Funkcja zwraca prędkość poruszania się przeciwnika.
	 */
	protected float getPredkoscRuchu(){		
		/**
		 * Przeciwnik nie idzie bo podszedł do gracza.
		 * Gracz jest po za zasięgiem.
		 * Gracz jest martwy.
		 */
		if (!czySiePorusza || dystans > zasiegWzroku ) {
			return 0;
		}
		
		/**
		 * Weryfikacja czy dystans dzielący przeciwnika od gracza jest mniejszy bądź równy dystansowi 
		 * kiedy przeciwnika ma zacząć iść oraz czy dystans ten jest większy od odstępu od gracza.
		 */
		if (dystans <= zacznijIsc && czySiePorusza) {
			//Warunek spełniony zacznij iść.
			return predkoscChodzenia; 
		}
		
		//Rzaden z warunków nie spełniony przecwinik biegnie.
		return predkoscBiegania;
	}
	
	/**
	 * Metoda odpowiedzialna za wywoływanie animacji poruszania.
	 */
	private void animacjaWroga(){
		if (animator != null) {
			animator.SetFloat ("predkosc", getPredkoscRuchu ());//Uruchom animację przekazując prędkość.
			if (skok && skokTime <= 0) { 
				animator.SetTrigger ("skok");//Uruchom animację skoku.
				skokTime = 1.07f;
			}		
			if (czyPrzeciwnikMatwy () && !martwy) {
				animator.SetTrigger ("martwy");//Uruchom animację śmierci.
				martwy = true;
			}		
			
			if (skokTime > 0) {
				skokTime -= Time.deltaTime;
			}
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "podloga") {
			//Debug.Log("COLL ENTER");
			skok = false;
		}
	}
	void OnCollisionStay(Collision collision) {
		if (collision.gameObject.tag == "podloga") {
			//Debug.Log("COLL STAY");
			skok = false;
		}
	}
	void OnCollisionExit(Collision collision) {
		if (collision.gameObject.tag == "podloga") {
			//Debug.Log("COLL EXIT");
			if(skokTime <= 0 && !skok) {
				skok = true;
				
			}
		}
	}

    /**Funkcja zwraca zasięg wzroku przeciwnik.*/
    public float getZasiegWzroku() {
        return zasiegWzroku;
    }
}

// All Rights Reserved!
// Wszelkie Prawa Zastrzeżone!
// Tylko do użytku niekomercyjnego.
