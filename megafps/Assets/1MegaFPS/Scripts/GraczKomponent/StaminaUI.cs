using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
* Skrypt odpowiedzialny za aktualizacje paska staminy.
* 
* @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class StaminaUI : MonoBehaviour {

	/** Komponent odpowiedzialny za poruszanie.*/
	private PlayerControler playerControler;

	/** Obrazek staminy.*/
	public Image stamina;
	/** Czas przez jaki możemy biegać.*/
	public float poziomStaminy = 5;
	/** Inicjalnie pasek staminy pełny bo zaczynamy biegać.*/
	private float czasBiegania;

	// Use this for initialization
	void Start () {
		playerControler = GetComponent<PlayerControler>();
		czasBiegania = poziomStaminy;

        czyPasekStaminyZnaleziony();
    }
	
	// Update is called once per frame
	void Update () {
		aktualizacjaPaskaStaminy();
	}

	/**
	 * Funkcja zwraca informację o tym czy gracz może biegać (gracz ma staminę).
	 * Zwraca 'true' jeżeli może biegać w przeciwnym razie 'false'.
	 */
	private void aktualizacjaPaskaStaminy(){
		if (playerControler.enabled && playerControler.czyGraczBiegnie() && playerControler.czyGraczChodzi() && czasBiegania > -0.5f) {
			czasBiegania -= Time.deltaTime;
		} else {
			if(czasBiegania < poziomStaminy) {
				czasBiegania += Time.deltaTime;
			}
		}
		if (stamina != null) {
			stamina.fillAmount = czasBiegania / poziomStaminy;
		}
		
	}

	/**
	 * Funkcja dostarcza informacje o tym czy gracz posiada jeszcze stamine.
	 * Zwraca 'true' jeżelli gracz posiada staminwe w przeciwnym razie 'false'.
	 */
	public bool brakStaminy(){
		return czasBiegania <= 0;
	}

    /**
    * Metoda szuka komponentu staminy w części HUD.
    * Przydatne podczas testów, gdy dodajemy do sceny prefab interfejsu oraz gracza. 
    * Prefab gracza korzysta ze staminy i jeżeli zapomnimy podpiąć obrazek paska staminy pod zmienną to ta metoda zrobi to za nas.
    */
    private void czyPasekStaminyZnaleziony() {
        if (stamina == null) {
            GameObject hud = GameObject.FindGameObjectWithTag("HUD");
            Transform hudT = hud.GetComponent<Transform>();
            Transform staminaTLo = hudT.Find("StaminaTlo");
            if (staminaTLo != null) {
                Transform staminaTrans = staminaTLo.Find("Stamin");
                if(staminaTrans != null) {
                    stamina = staminaTrans.GetComponent<Image>();
                }

            }
             
        }
    }
}
