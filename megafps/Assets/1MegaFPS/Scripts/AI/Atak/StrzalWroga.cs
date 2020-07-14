using UnityEngine;
using System.Collections;

/**
 * Klasa odpowiedzialna za namierzenie gracza przez wroga w celu przygotowania się do strzału.
 * Zanim wróg odda strzał musi się odwróciś do gracza dopiero po uzyskaniu zadanego konta (pola widzenia)
 * zostanie oddany strzał.
 * 
 * Po prostu aby uniknąć sytuacji strzela kiedy wróg jest odwrócony plecami do gracza.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class StrzalWroga : MonoBehaviour {

    private Transform trans;

	/**Obiekt gracza.*/
	protected Transform gracz;
	/**Aktualna pozycja gracza.*/
	private Vector3 pozycjaGraczaXYZ; 
	/**Zawiera obrót rakiety.*/
	private Quaternion rotacjaPocisku;

	/**Obiekt gracza.*/
	protected GameObject graczObiekt;

	/** 
	 * Kąt określający pole widzenia, po przekroczeniu tej wartości uznawane jest, że
	 * gracz jest widoczny i można oddać strzał.
	 */
	public float katWidzenia = 160f;

	/*Pobranie pozycji obiektu, na który patrzy przeciwnik.*/
	protected Vector3 hitPoint;

	// Use this for initialization
	void Start () {
        trans = GetComponent<Transform>();
        graczObiekt = GameObject.FindWithTag("Player");
		//Pobranie obiektu gracza.
		gracz = graczObiekt.transform; 
	}

	/**
	 * Funkcja odpowiedzialna za namierzanie gracza.
	 * Jeżeli gracz znajduje się w polu widzenia to zwraca 'true' w przeciwnym razie 'false'.
	 * Jeżeli gracz znajduje się w polu widzenia to można oddać strzał.
	 */
	protected bool namierzanie(){
		if (gracz != null) {
			//Pobranie aktualnego konta obrotu wroga w stosunku do gracza.
			float angle = Quaternion.Angle (gracz.rotation, transform.rotation);
			//Debug.Log (angle);
			//Sprawdzenie czy gracz jest już widoczny podczas obrotu wroga.
			if (angle >= katWidzenia) {
				return true;
			}
		}
		return false;
	}

	/**
	 * Metoda na podstawie pozycji gracza zwraca Quaternion pozwalającego na ustalenie/ukierunkowanie pozycji
	 * do której ma zmierzać.
	 */
	public Quaternion getRotacjaPocisku(){
		//Pobranie pozycji gracza.
		pozycjaGraczaXYZ = new Vector3(gracz.position.x, gracz.position.y, gracz.position.z);
		
		//Quaternion.LookRotation - zwraca quaternion na podstawie werktora kierunku/pozycji. 
		//Potrzebujemy go aby obrócić wroga w stronę gracza.
		rotacjaPocisku = Quaternion.LookRotation(pozycjaGraczaXYZ - transform.position);
		return rotacjaPocisku;
	}

	/**
	 * Metoda zwraca informację o tym czy przeciwnik obrócił głowę w naszą stronę.
	 * Przeciwnik wyposażony został w obiekt, który działa jak głowa.
	 * Zadaniem głowy jest obracanie jej w stronę gracza również po to, aby śledziła wysokość, na jakiej znajduje się gracza.
	 * 
	 * Dzięki temu, że głowa się obraca można za pomocą promienia sprawdzić, na co patrzy przeciwnik.
	 */
	protected bool celowanie(float zasieg){

		//Pobranie obiektu glowa dolączonego do wroga.
		//Transform glowa = transform.FindChild("Glowa");

		/*Ray (promien) pozwala na pobranie kierunku w ktorym skierowany jest obiekt glowy (na co teoretycznie patrzyłby wróg).
	     - glowa.position - promien wychodzacy besposrednio/centralnie z celownika (slodka ekranu).
		 - glowa.forward - pozycja w kierunku ktorej skierowana jest kamera.
		*/
		Ray ray = new Ray(trans.position, trans.forward);
		//Pobranie informacji w co skierowany jest obiekt glowy.
		RaycastHit hitInfo;
		
		//Sprawdzenie czy promien w cos trafil czy obiekt (hitInfo) mieszczacy sie w zakresie (range)
		// w cos trafiły.		
		if(Physics.Raycast(ray, out hitInfo, zasieg)){
			//Pobranie informacji o trafionym obiekcie.
			//Vector3 hitPoint = hitInfo.point;
			//Pobranie trafionego obiektu.
			GameObject go = hitInfo.collider.gameObject;

			Debug.Log("Hit Object: "+go.name);//Nazwa trafionego obiektu

			//Pozycja obiektu na który patrzy się przeciwnik.
			hitPoint = hitInfo.point;

			if(go.name.Equals(graczObiekt.name)) {
				return true;
			}
		}
		return false;
	}

	/**Funkcja zwraca zasięg wzroku przeciwnik.*/
	public float getZasiegWzroku(){
		DobreAIAnim ai = (DobreAIAnim)gameObject.GetComponent<DobreAIAnim> ();
		return ai.getZasiegWzroku ();
	}
}

// All Rights Reserved!
// Wszelkie Prawa Zastrzeżone!
// Tylko do użytku niekomercyjnego.