using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
* Klasa odpowiedzialna za wyświetlenie treści książki.
*
* @author Hubert Paluch.
* MViRe - na potrzeby kursu UNITY3D v5.
* mvire.com 
*/
public class KsiazkaTrigger : MonoBehaviour {

    /** Płutno komunikatów.*/
    public Canvas komunikaty;
    /** Zmienna będąca płutnem z tekstem ksiązki.*/
    public Canvas ksiazkaTresc;        

    // Use this for initialization
    void Start () {
		getKomunikaty().enabled = false; //Ukrycie menu z pytaniem o interakcję.
		getKsiazkaTresc().enabled = false; //Ukrycie menu z treścią ksiażki.
    }
	
    /**
	 * Metoda wywoływana w chwili wejścia gracza na obszar obiektu.
	 */
    void OnTriggerEnter(Collider other) {
        Debug.Log("Player Enter");
        if (other.tag == "Player") {
			getKomunikaty().enabled = true;   
			setKomunikat();
        }
    }

    /**
	 * Metoda wywoływana w chwili przebywania gracza na obszarze obiektu.
	 */
    void OnTriggerStay(Collider other) {       
		if (getKomunikaty().enabled && Input.GetKeyDown(KeyCode.E)) { //Jeżeli naciśnięto klaswisz "E" pokazana zostanie treść książki.
			getKomunikaty().enabled = false;
			getKsiazkaTresc().enabled = true; //Pokazanie płutna z tekstem książki.
            Time.timeScale = 0;//Zatrzymanie czasu.
            Cursor.visible = true;//Pokazanie kursora myszy.  
            Cursor.lockState = CursorLockMode.None;//Odblokowanie kursora myszy.            
        } 
    }

    /**
    * Metoda wywoływana chwili wyjścia gracza z obszar obiektu.
    */
    void OnTriggerExit(Collider other) {
        Debug.Log("Player Exit");
        if (other.tag == "Player") {
			getKomunikaty().enabled = false;//Ukrycie płutna z informacją o możliwości odczytania książki.   

        }
    }

	/**
	 * Funkcja zwraca obiekt płutna książki.
	 */
	private Canvas getKomunikaty(){
		if (komunikaty != null) {
			return komunikaty;
		} else {
			GameObject gm = GameObject.FindGameObjectWithTag("Komunikat");
			komunikaty = gm.GetComponent<Canvas>();
		}
		return komunikaty;
	}

	/**
	 * Funkcja zwraca obiekt płutna książki wyświetlający treść.
	 */
	private Canvas getKsiazkaTresc(){
		if (ksiazkaTresc != null) {
			return ksiazkaTresc;
		} else {
			GameObject gm = GameObject.FindGameObjectWithTag("KsiazkaTresc");
			ksiazkaTresc = gm.GetComponent<Canvas>();
		}
		return ksiazkaTresc;
	}

	private void setKomunikat(){
		Text ob = getKomunikaty().transform.Find("KomunikatTlo").Find("KomunikatTekst").GetComponent<Text>(); ;
		ob.text = "E aby przeczytać";
	}
}
