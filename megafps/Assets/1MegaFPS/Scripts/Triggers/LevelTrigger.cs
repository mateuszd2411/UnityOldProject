using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Skrypt odpowiedzialny za wyświetlenie komunikatu i przechodzenie pomiędzy
 * poziomami.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class LevelTrigger : MonoBehaviour {

    /** Nazwa sceny do której będziemy przechodzić.*/
    public string nazwaSceny;
    /** Komunikat wyświetlany po wejściu na obszar odpowiedzialny za przejście.*/
    public string komunikat;
    /** Nazwa punktu startowego do którego mamy zostać przeniesieni.*/
    public string startNr;

    //Zmienna zweira obiekt płutna komunikatu.
    private GameObject canvasKomunikat;
    //Zmienna zawiera płutno komunikatu. Niezbędne do chowania i pokazywania komunikatu.
    private Canvas canvas;
    

    // Use this for initialization
    void Start () {
        //Pobranie obiektu komunikatu.
        canvasKomunikat = GameObject.FindGameObjectWithTag("Komunikat");        
        //Pobranie płutna komunikatu.
        canvas = canvasKomunikat.GetComponent<Canvas>();       
    }	
    
    /**
      * Metoda wywoływana w chwili wejścia gracza na obszar obiektu.
      */
    void OnTriggerEnter(Collider other) {
        Debug.Log("Player Enter");
        if (other.tag == "Player") {
            canvas.enabled = true;
            ustawKomunikat();
        }
    }

    /**
	 * Metoda wywoływana w chwili przebywania gracza na obszarze obiektu.
	 */
    void OnTriggerStay(Collider other) {
        if ( Input.GetKeyDown(KeyCode.E)) { //Jeżeli naciśnięto klaswisz "E" następuje wczytanie sceny/obszaru.
            GraczInstancja.startNr = startNr;
            GraczInstancja.respown = true;
            Application.LoadLevel(nazwaSceny);
        }
    }

    /**
    * Metoda wywoływana chwili wyjścia gracza z obszar obiektu.
    */
    void OnTriggerExit(Collider other) {
        Debug.Log("Player Exit");
        if (other.tag == "Player") {
            canvas.enabled = false;//Ukrycie płutna z informacją o możliwości wczytania obszaru. 
            ustawKomunikat();
        }
    }

    /** Metoda pozwala na ustawienie treści komunikatu wyświetlanego
     * po wejściu gracza na teren obiektu.
     */
    public void ustawKomunikat() {
        canvasKomunikat.GetComponent<Transform>().GetComponentInChildren<Text>().text = komunikat;        
    }
}
// All Rights Reserved!
// Wszelkie Prawa Zastrzeżone!
// Tylko do użytku niekomercyjnego.