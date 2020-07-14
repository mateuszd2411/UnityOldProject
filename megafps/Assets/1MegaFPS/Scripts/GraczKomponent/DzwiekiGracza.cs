using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
* Skrypt odpowiedzialny za odtwarzanie dzwięków poruszania gracza.
* 
* @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class DzwiekiGracza : MonoBehaviour {

    private Transform trans;

	//Obiekt odpowiedzialny za ruch gracza.
	public CharacterController characterControler;
    //Component odpowiedzialny za kontrolę staminy.
    private StaminaUI stamina;

	/** Źródło dzwięki.*/
	public AudioSource zrodloDzwieku;
	
    /** Dzwięk chodzenia.*/
	//public AudioClip dzwiekChodzenie;

	/** Dzwięk skoku.*/
	public AudioClip dzwiekSkoku;
	/** Dzwięk lądowania.*/
	public AudioClip dzwiekLadowania;
	/** Licznik do następnego odtwarzania.*/
	public float odliczanieDoKroku = 0f;
    /** Odstęp pomiędzy jednym a drugim krokiem.*/
    public float czasKroku = 0.6f;
	
	/**Zmienna z informacją o tym czy gracz dalej chodzi po ziemi czy podskoczył.*/
	public bool graczNaZiemi;

	/** Component gracza odpowiedzialny za poruszanie.*/
	private PlayerControler playerControler;
    /** Component gracza odpowiedzialny za poruszanie w wodzie.*/
    private GraczWodaKontroler graczWodaKontroler;

    /** Tablica z obiektami zawierającymi powiązania tekstury z dzwiękiem.*/
    public DzwiekDlaTekstury[] dzwieki;
    /** Obiekt zawierający teksturę terenu z dzwiękiem do odtwarzania.*/
    private DzwiekDlaTekstury aktualnyDzwiek;
    /** Zmienna informuje o kolizji z obiektem zawierającym dzwięk.*/
    private bool kolizjaZObiektem = false;

    // Use this for initialization
    void Start () {
        trans = GetComponent<Transform>();
        playerControler = GetComponent<PlayerControler> ();
        graczWodaKontroler = GetComponent<GraczWodaKontroler>();
        stamina = GetComponent<StaminaUI>();
        ustawDomyslnyDzwiekKroku();
    }
	
	// Update is called once per frame
	void Update () {
		if (zrodloDzwieku != null ) {//Jeżeli źródło dzwięku nie zostało podpięte to i tak nie ma co odgrywać.
            pobierzDzwiek();
            dzwiekChodzenia ();
		}
	}

	/**
	 * Metoda odpowiedzialna za oddtwarzanie dzwięku chodzenia gracza.
	 */
	private void dzwiekChodzenia(){
		//Zmniejszanie licznika do kolejnego odtworzenia dźwięku.
		if (odliczanieDoKroku > 0) {
			//Sprawdzenie, jeżeli gracz biegnie to dźwięk kroku będzie odtwarzany szybciej.
			if (czyGraczBiegnie() && !czyBrakStaminy()) {
				odliczanieDoKroku -= Time.deltaTime * 1.3f;
			} else {
				odliczanieDoKroku -= Time.deltaTime;
			}
		}

		//Jeżeli gracz się porusza to odgrywaj dzwięk poruszania.
		if (czyGraczChodzi() && characterControler.isGrounded && odliczanieDoKroku <= 0) {            
			odliczanieDoKroku = czasKroku;//Czas trwania dzwięku.
			zrodloDzwieku.PlayOneShot (aktualnyDzwiek.dzwiek);
		}
		//Jeżeli gracz podskoczył i znajduje się na ziemi to odegraj dzwięk skoku.
		if (Input.GetButton ("Jump") && characterControler.isGrounded) {
			zrodloDzwieku.PlayOneShot (dzwiekSkoku);
		}
		//Jeżeli gracz ostatnio był w powietrzu a teraz na ziemi to znaczy, że wylondował na ziemi
		//zatem odegraj dzwięk londowania.
		if(!graczNaZiemi && characterControler.isGrounded) {				
			zrodloDzwieku.PlayOneShot (dzwiekLadowania);				
		}
		//Na zakończenie sprawdzamy czy gracz ciągle chodzi po ziemi.
		graczNaZiemi = characterControler.isGrounded;
	}

	private bool czyBrakStaminy(){
		if (stamina != null) {
			return stamina.brakStaminy();
		}
		return false;
	}

    /**
     * Metoda pobiera nazwę tekstury a następnie obiekt DzwiekDlaTekstury zawierający nazwę pobranej tekstury.
     * Porany obiekt zwiera dzwięk do odtworzenia.
     */
    private void pobierzDzwiek() {
        if (!kolizjaZObiektem) {//Nie ma kolizji z obiektem więc zostanie pobrany dzwięk dla tekstury.
            bool pobrany = false;//Informuje o pobraniu dzwięku dla tekstury.
            foreach (DzwiekDlaTekstury dzwiek in dzwieki) {
                if (dzwiek.tekstura != null && dzwiek.tekstura.name.Equals(PowierzchniaTerenu.NazwaTeksturyWPozycji(trans.position))) {
                    aktualnyDzwiek = dzwiek;
                    pobrany = true;
                    break;
                }
            }
            if (!pobrany) {//Dzwięk nie został pobrany, tekstura nie ma przypisanego dzwięku.
                aktualnyDzwiek = dzwieki[0];//Ustawiam dzwięk domyślny.
            }
        }
    }

    /**
     * Metoda dostarcza dzwięk pobrany z obiektu, z którym koliduje obiekt gracza.
     */
    void OnControllerColliderHit(ControllerColliderHit hit) {        
        Transform trans = hit.collider.gameObject.GetComponent<Transform>();
                
        if (trans.tag == "Teren") { //Czy gracz porusza się po terenie.
            kolizjaZObiektem = false; //Nie ma kolizji z obiektem nastąpi pobranie dzwięku tekstury.
        } else {
            DzwiekiDlaObiektu ddo = trans.GetComponent<DzwiekiDlaObiektu>();//Pobieram komponent z dzwiękiem.
            if (ddo != null && ddo.dzwiekDlaTekstury.dzwiek != null) {
                aktualnyDzwiek = ddo.dzwiekDlaTekstury; //Ustawiam dzwięk kroku pobrany z obiektu.
            } else {
                //Obiekt ma podpięty skrypt dzwięku ale dzwięk nie został ustawiony więc pobieram dźwięk domyślny.
                aktualnyDzwiek = dzwieki[0];//Ustawiam dzwięk domyślny.
            }
            kolizjaZObiektem = true; //Kolizja z obiektem.
        }        
    }

    private void ustawDomyslnyDzwiekKroku() {
        if(dzwieki.Length > 0 && dzwieki[0].dzwiek != null) {
            aktualnyDzwiek = dzwieki[0];
        }
    }

    private bool czyGraczChodzi() {
        if(playerControler.enabled && playerControler.czyGraczChodzi()) {
            return true;
        }
        if (graczWodaKontroler.enabled && graczWodaKontroler.czyGraczChodzi()) {
            return true;
        }
        return false;
    }

    private bool czyGraczBiegnie() {
        if (playerControler.enabled && playerControler.czyGraczBiegnie()) {
            return true;
        }
        if (graczWodaKontroler.enabled && graczWodaKontroler.czyGraczBiegnie()) {
            return true;
        }
        return false;
    }

}

// All Rights Reserved!
// Wszelkie Prawa Zastrzeżone!
// Tylko do użytku niekomercyjnego.