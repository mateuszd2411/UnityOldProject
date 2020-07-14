using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

/**
 * Skrypt odpowiedzialny za zarządzanie głównym menu gry. 
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class Menu : MonoBehaviour {

	public Canvas quitMenu;
	public Canvas hudMenu;
	public Canvas opcjeMenu;

	public Button btnStart;
	public Button btnExit;
	public Button btnOpcje;
    public Button btnZapisz;
    public Button btnWczytaj;
    public Button btnZamknijOpcje;

	public AudioMixerSnapshot pauza;
	public AudioMixerSnapshot brakPauzy;
    public bool debbug;

	/** Obiekt menu.*/
	private Canvas manuUI;
	
	void Start (){
		manuUI = (Canvas)GetComponent<Canvas>();//Pobranie menu głównego.

		quitMenu = quitMenu.GetComponent<Canvas>(); //Pobranie menu pytania o wyjście z gry.
		hudMenu = hudMenu.GetComponent<Canvas>(); //Pobranei menu HUD gracza.
		opcjeMenu = opcjeMenu.GetComponent<Canvas>(); //Pobranei menu Opcji.


		btnStart = btnStart.GetComponent<Button> ();//Ustawienie przycisku uruchomienia gry.
		btnExit = btnExit.GetComponent<Button> ();//Ustawienie przycisku wyjścia z gry.

		quitMenu.enabled = false; //Ukrycie menu z pytaniem o wyjście z gry.
		hudMenu.enabled = false; //Wyłączenie interfejsu uytkownika.
		opcjeMenu.enabled = false;//Wyłączenie menu opcji.

		Time.timeScale = getTime();//Zatrzymanie czasu.
		Cursor.visible = manuUI.enabled;//Ukrycie pokazanie kursora myszy.
		Cursor.lockState = CursorLockMode.Confined;//Odblokowanie kursora myszy.
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) { //Jeżeli naciśnięto klawisz "Escape"
			manuUI.enabled = !manuUI.enabled;//Ukrycie/pokazanie menu.
			hudMenu.enabled = !hudMenu.enabled;//Ukrycie/pokazanie HUD.

			Cursor.visible = manuUI.enabled;//Ukrycie pokazanie kursora myszy.
			
			if(manuUI.enabled) {
				Cursor.lockState = CursorLockMode.Confined;//Odblokowanie kursora myszy.
				Cursor.visible = true;//Pokazanie kursora.
				Time.timeScale = getTime();//Zatrzymanie czasu.
			} else {
				Cursor.lockState = CursorLockMode.Locked; //Zablokowanie kursora myszy.
				Cursor.visible = false;//Ukrycie kursora.
				Time.timeScale = getTime();//Włączenie czasu.
			}					
		}
		ustawDzwiek();
	}

	/**
	 * Metoda odpowiedzialna za wywołanie Snapshot'a z ustawieniami dzwięku.
	 * Jeżeli jest właczone główne menu to dzwięk jest przyciszony.
	 */
	private void ustawDzwiek(){
		if (pauza != null && brakPauzy != null) {//Wymagam aby były ustawione obydwa Snapshot'y.
			if (Time.timeScale == 0) {//Jeżeli czas zatrzymany.
				pauza.TransitionTo (0.01f); //Wyciszenie dzwięków za pomocą Snapshot.
			} else {
				brakPauzy.TransitionTo (0.01f); //Jeżeli czas płynie przywrócenie normalnego poziomu głośności.
			}
		}
	}

	//Metoda wywoływana po naciśnięciu przycisku "Exit"
	public void PrzyciskWyjscie() {
		quitMenu.enabled = true; //Uaktywnienie meny z pytaniem o wyjście
		btnStart.enabled = false; //Deaktywacja przycsiku 'Start'.
        btnZapisz.enabled = false; //Deaktywacja przycsiku 'Zapsiz'.
        btnWczytaj.enabled = false; //Deaktywacja przycsiku 'Wczytaj'.
        btnExit.enabled = false; //Deaktywacja przycsiku 'Wyjście'.
		btnOpcje.enabled = false; //Deaktywacja przycsiku 'Opcje'.
	}

	//Metoda wywoływana po naciśnięciu przycisku "Exit"
	public void PrzyciskOpcje() {
		opcjeMenu.enabled = true; //Uaktywnienie meny z pytaniem o wyjście
		btnStart.enabled = false; //Deaktywacja przycsiku 'Start'.
        btnZapisz.enabled = false; //Deaktywacja przycsiku 'Zapsiz'.
        btnWczytaj.enabled = false; //Deaktywacja przycsiku 'Wczytaj'.
        btnExit.enabled = false; //Deaktywacja przycsiku 'Wyjście'.
		btnOpcje.enabled = false; //Deaktywacja przycsiku 'Opcje'.
	}

	//Metoda wywoływana podczas udzielenia odpowiedzi przeczącej na pytanie o wyjście z gry.
	public void PrzyciskNieWychodz(){
		quitMenu.enabled = false; //Ukrycie menu z pytaniem o wyjście z gry.
		btnStart.enabled = true; //Uaktywnienie przycisku 'Start'.
        btnZapisz.enabled = true; //Uaktywnienie przycsiku 'Zapsiz'.
        btnWczytaj.enabled = true; //Uaktywnienie przycsiku 'Wczytaj'.
        btnExit.enabled = true; //Uaktywnienie przycisku 'Wyjscie'.
		btnOpcje.enabled = true; //Uaktywnienie przycisku 'Opcje'.
	}

	//Metoda wywoływana podczas udzielenia odpowiedzi przeczącej na pytanie o wyjście z gry.
	public void PrzyciskZamknijOpcje(){
		opcjeMenu.enabled = false; //Ukrycie menu z opcjami.
		btnStart.enabled = true; //Uaktywnienie przycisku 'Start'.
        btnZapisz.enabled = true; //Uaktywnienie przycsiku 'Zapsiz'.
        btnWczytaj.enabled = true; //Uaktywnienie przycsiku 'Wczytaj'.
        btnExit.enabled = true; //Uaktywnienie przycisku 'Wyjscie'.
		btnOpcje.enabled = true; //Uaktywnienie przycisku 'Opcje'.
	}

	//Metoda wywoływana przez przycisk uruchomienia gry 'Play Game'
	public void PrzyciskStart (){
		//Application.LoadLevel (0); //this will load our first level from our build settings. "1" is the second scene in our game	
		manuUI.enabled = false; //Ukrycie głównego menu.
		hudMenu.enabled = true; //Włączenie interfejsu uytkownika.

		Time.timeScale = getTime();//Właczenie czasu.

		Cursor.visible = false;//Ukrycie kursora.
		Cursor.lockState = CursorLockMode.Locked; //Zablokowanie kursora myszy.
	}

	//Metoda wywoływana podczas udzielenia odpowiedzi twierdzącej na pytanie o wyjście z gry.
	public void PrzyciskTakWyjdz () {
		Application.Quit(); //Powoduje wyjście z gry.
		
	}

    private float getTime() {
        if(debbug) {
            
            return 1;
        } else {
            if(manuUI.enabled) {
                return 0;
            } else {
                return 1;
            }
        }
    }

}
