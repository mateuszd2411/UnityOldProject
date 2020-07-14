using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
* Skrypt odpowiedzialny za kontrole gracza.
* 
* @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class PlayerControler : MonoBehaviour {

    //Obiekt odpowiedzialny za ruch gracza.
    public CharacterController characterControler;
    //Component odpowiedzialny za kontrolę staminy.
    private StaminaUI stamina;

    //Predkosc biegania.
    public float akualnaPredkosc = 0f;
    //Prędkość poruszania się gracza.
    public float predkoscPoruszania = 5.0f;
    //Predkosc biegania.
    public float predkoscBiegania = 10.0f;
    //Wysokość skoku.
    public float wysokoscSkoku = 7.0f;
    //Aktualna wysokosc skoku.
    public float aktualnaWysokoscSkoku = 0f;

    //Czulość myszki (Sensitivity)
    public float czuloscMyszki = 3.0f;
    public float myszGoraDol = 0.0f;
    //Zakres patrzenia w górę i dół.
    public float zakresMyszyGoraDol = 90.0f;

    /** 
    * Jeżeli 'true' to nie można się poruszać.
    * Opcja ustawiana jeżeli na przykład jakieś płutno (Canvas) z tagiem "Menu"  jest włączone (np. menu główne, opcje itd.) 
    * to poruszanie jest zablokowane.
    */
    private bool ruchZablokowany = false;

    /** 
	 * Pobranie prędkości poruszania się przód/tył.
	 * jeżeli wartość dodatnia to poruszamy się do przodu,
	 * jeżeli wartość ujemna to poruszamy się do tyłu.
	 */
    private float rochPrzodTyl;
    /** 
	 * Pobranie prędkości poruszania się lewo/prawo.
	 * jeżeli wartość dodatnia to poruszamy się w prawo,
	 * jeżeli wartość ujemna to poruszamy się w lewo.
	 */
    private float rochLewoPrawo;
    /** Zmienna dostarcza informację o tym czy gracz bienie.*/
    public bool czyBiegnie;

    // Use this for initialization
    void Start() {
        stamina = GetComponent<StaminaUI>();
        characterControler = GetComponent<CharacterController>();
        akualnaPredkosc = predkoscPoruszania;
    }

    // Update is called once per frame
    void Update() {
        if (!czyGraczMartwy() && !ruchZablokowany) {
            klawiatura();
            myszka();

        }
    }

    /**
	 * Metoda odpowiedzialna za poruszanie się na klawiaturze.
	 */
    private void klawiatura() {
        rochPrzodTyl = Input.GetAxis("Vertical") * akualnaPredkosc;
        rochLewoPrawo = Input.GetAxis("Horizontal") * akualnaPredkosc;

        //Skakanie
        // Jeżeli znajdujemy się na ziemi i została naciśnięta spacja (skok)
        if (characterControler.isGrounded && Input.GetButton("Jump")) {
            aktualnaWysokoscSkoku = wysokoscSkoku;
        } else if (!characterControler.isGrounded) {//Jezeli jestesmy w powietrzu(skok)
                                                    //Fizyka odpowiadająca za grawitacje (os Y).
            aktualnaWysokoscSkoku += Physics.gravity.y * Time.deltaTime;
        }

        //Bieganie
        if (Input.GetKeyDown("left shift")) {
            czyBiegnie = true;
        } else if (Input.GetKeyUp("left shift")) {
            czyBiegnie = false;
        }

        if (czyBiegnie && !czyBrakStaminy()) {
            akualnaPredkosc = predkoscBiegania;
        } else {
            akualnaPredkosc = predkoscPoruszania;
        }

        //Tworzymy wektor odpowiedzialny za ruch.
        //rochLewoPrawo - odpowiada za ruch lewo/prawo,
        //aktualnaWysokoscSkoku - odpowiada za ruch góra/dół,
        //rochPrzodTyl - odpowiada za ruch przód/tył.
        Vector3 ruch = new Vector3(rochLewoPrawo, aktualnaWysokoscSkoku, rochPrzodTyl);
        //Aktualny obrót gracza razy kierunek w którym sie poruszamy (poprawka na obrót myszką abyśmy szli w kierunku w którym patrzymy).
        ruch = transform.rotation * ruch;

        characterControler.Move(ruch * Time.deltaTime);

    }

    /**
	 * Metoda odpowiedzialna za ruch myszką.
	 */
    private void myszka() {
        //Pobranie wartości ruchu myszki lewo/prawo.
        // jeżeli wartość dodatnia to poruszamy w prawo,
        // jeżeli wartość ujemna to poruszamy w lewo.
        float myszLewoPrawo = Input.GetAxis("Mouse X") * czuloscMyszki;
        transform.Rotate(0, myszLewoPrawo, 0);

        //Pobranie wartości ruchu myszki góra/dół.
        // jeżeli wartość dodatnia to poruszamy w górę,
        // jeżeli wartość ujemna to poruszamy w dół.
        myszGoraDol -= Input.GetAxis("Mouse Y") * czuloscMyszki;

        //Funkcja nie pozwala aby wartość przekroczyła dane zakresy.
        myszGoraDol = Mathf.Clamp(myszGoraDol, -zakresMyszyGoraDol, zakresMyszyGoraDol);
        //Ponieważ CharacterController nie obraca się góra/dół obracamy tylko kamerę.
        Camera.main.transform.localRotation = Quaternion.Euler(myszGoraDol, 0, 0);
    }

    /**
	 * Metoda zwraca informację o tym czy gracz ciągle żyje.
	 * 
	 * Zwraca 'true' jeżeli gracz jeszcze żyje w przeciwnym razei 'false'.
	 */
    public bool czyGraczMartwy() {
        Zdrowie zdrowie = gameObject.GetComponent<Zdrowie>();
        if (zdrowie != null && zdrowie.czyMartwy()) {
            return true;
        }
        return false;
    }

    public void SetRuchZablokowany(bool val) {
        this.ruchZablokowany = val;
    }

    /**
	 * Funkcja udostępnia informację o tym czy gracz wykonuje ruch (chodzi).
	 * Jeżeli gracz chodzi to zwraca 'true' jeżeli nie to 'false'.
	 */
    public bool czyGraczChodzi() {
        if (rochPrzodTyl != 0 || rochLewoPrawo != 0) {
            return true;
        }
        return false;
    }

    /**
	 * Funkcja dostarcza informacje o tym czy gracz biegnie.
	 * Zwraca 'true' jeżelli gracz biegnie w przeciwnym razie 'false'.
	 */
    public bool czyGraczBiegnie() {
        return czyBiegnie;
    }

    private bool czyBrakStaminy() {
        if (stamina != null) {
            return stamina.brakStaminy();
        }
        return false;
    }
    
}

// All Rights Reserved!
// Wszelkie Prawa Zastrzeżone!
// Tylko do użytku niekomercyjnego.


