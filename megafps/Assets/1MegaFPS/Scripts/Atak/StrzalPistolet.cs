using UnityEngine;
using System.Collections;

/**
 * Klada odpowiedzialna za wykonanie strzału przez gracza.
 * klasa tworzy pocisk na celu.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class StrzalPistolet : MonoBehaviour {

	//Zasiek strzalu
	public float zasieg = 100.0f;
	//Co ile mozna wykonac strzal
	public float czekaj = 2f;
	//Odliczanie do kolejnego strzalu.
	public float odliczanieDoStrzalu = 0f;
	
	//Obiekt pocisku.
	public GameObject pociskPrefab;
	//Obrarzenie jakie zadaje strzal (ile punktow zdrowia zabiera).
	public float obrazenia = 50.0f;

	/** Źródło dzwięki.*/
	public AudioSource zrodloDzwieku;
	/** Dzwięk strzału.*/
	public AudioClip odglosStrzalu;    

    // Use this for initialization
    void Start() {        
    }

    // Update is called once per frame
    public void strzal () {

        //Ustawiamy warstwę do ignorowania.
        //int warstwaDoIgnorowania = ~(1 << 10 | 1 << 9);
       

        //Strzal nastepuje jezeli przycisk myszy jest ciagle wcisniety oraz jezeli odliczanie
        //do kolejnego strzalu jest rowne zero(mozna strzelac).
        if (odliczanieDoStrzalu <= 0){
            //Strzal zostal oddany ustawienie ponownego odliczania.
            odliczanieDoStrzalu = czekaj;

            /*Ray (promien) pozwala na pobranie kierunku w ktorym skierowana jest kamera
		     - Camera.main.transform.position - promien wychodzacy besposrednio/centralnie z celownika (slodka ekranu).
			 - Camera.main.transform.forward - pozycja w kierunku ktorej skierowana jest kamera.
			*/
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
			//Pobranie informacji w co zostal oddany strzal.
			RaycastHit hitInfo;

            //Sprawdzenie czy promien w cos trafil czy obiekt (hitInfo) mieszczacy sie w zakresie (range)
            // w cos trafiły.
            //if (Physics.Raycast(ray, out hitInfo, zasieg, warstwaDoIgnorowania)) {
            if (Physics.Raycast(ray, out hitInfo, zasieg)){
				//Pobranie informacji o trafionym obiekcie.
				Vector3 hitPoint = hitInfo.point;
				//Pobranie trafionego obiektu.
				GameObject go = hitInfo.collider.gameObject;
				
				Debug.Log("Hit Object: "+go.name);//Nazwa trafionego obiektu
				//Debug.Log("Hit Point: "+hitPoint);//Wspolrzedne trafionego obiektu.
				
				//Zadanie obrazenia trafionemu obiektowi.
				hit(go);

				if(pociskPrefab != null){//Czy mamy przypisany obiekt pocisku.
					
					/*Odpowiada za utworzenie obiektu pocisku w momencie trafienia celu.
					 *  Tworzy kulke na obiekcie.
					 * - pociskPrefab - obiekt kulki.
					 * - hitPoint - pozycja trafienia i pozycja na jakiej zostanie utworzony pocisk.
					 * - Quaternion.identity - o ile ma zostac obrucony pocisk (Quaternion.identity = bez zmian)
					 */
					Instantiate(pociskPrefab, hitPoint, Quaternion.identity);

					//Pojedyncze odtwarzanie dzwięku.
					/*
					zrodloDzwieku.clip = odglosStrzalu; 
					zrodloDzwieku.Play();
					*/
					if(zrodloDzwieku != null) {//Na wszelki wypadek jak by nie zostało podpięte źródło dzwięku.
						zrodloDzwieku.PlayOneShot(odglosStrzalu);
					}
				}
				
			}
		}
	}   

    //Funkcja odpowiedzialna za zmniejszenie punktu zdrowia
    void hit( GameObject go){
		//Zadawanie obrarzen obiektowi.
		//Pobranie od trafionego obiektu atrybutu zdrowia.
		Zdrowie zdrowie = go.GetComponentInChildren<Zdrowie>();
		//Sprawdzenie czy trafiony obiekt ma atrybut zdrowia.
		if(zdrowie != null) {
			//Jezeli trafiony obiekt posiada atrybut zdrowia to zadawane jest obrarzenie.
			zdrowie.otrzymaneObrazenia(obrazenia);
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

// All Rights Reserved!
// Wszelkie Prawa Zastrzeżone!
// Tylko do użytku niekomercyjnego.