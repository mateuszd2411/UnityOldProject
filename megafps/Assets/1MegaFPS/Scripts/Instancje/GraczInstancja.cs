using UnityEngine;
using System.Collections;

/**
* Klasa pozwalająca na zachowanie tylko jednej instancji obiektu gracza.
* Klasa pozwala na przenoszenie gracza pomiędzy scenami/poziomami gry przy
* jednoczesnym zachowaniu jego parametrów.
*
* @author Hubert Paluch.
* MViRe - na potrzeby kursu UNITY3D v5.
* mvire.com 
*/
public class GraczInstancja : MonoBehaviour {

	/** Globalna zmienna z informacją o zachowaniu już instancji obiektu gracza.*/
	public static GraczInstancja instancja;
    /** Nazwa taga obiektu w miejscu którego pojawi się gracz na nowej scenie.*/
    public static string startNr;

    /** Zmienna dostarcza informacji o tym czy gracz wczytał stan gry z pliku.*/
    public static bool respown = false;

    /**
	 * Metoda 'Awake' jest wywoływana tylko raz (tak jak Start) tuż po zaczytaniu skryptu, 
	 * ale zanim zostanie wywołana metoda 'Start'.
	 * Wykorzystywana do inicjalizacji zmiennych lub stanu gry.
	 */
    void Awake () {
		//Jeżeli instancja jeszcze nie została zachowana.
		if (!instancja) {
			//Zachowaj instancję obiektu podczas wczytywania sceny.
			DontDestroyOnLoad(this.gameObject) ;
            //Instancja obiektu została zachowana i podstawiamy ją pod zmienna statyczną
            instancja = this;
		} else {

			/** 
			 * Instancja równa się true, czyli już jest gdzieś w świecie obiekt
			 * a zatem usuń instancję tego obiektu.
			 * 
			 * Ta sytuacja może mieć miejsce, kiedy wrócimy do naszej głównej/startowej lokacji
			 * gdzie znajduje się już taki obiekt.
			 */
			Destroy(gameObject) ;
		}

	}


}
