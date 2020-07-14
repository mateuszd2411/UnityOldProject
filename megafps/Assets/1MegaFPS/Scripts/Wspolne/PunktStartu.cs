using UnityEngine;
using System.Collections;

/**
* Klasa odpowiedzialna za ustawienie gracza na pozycji respownu tuż po załadowaniu sceny/poziomu.
*
* @author Hubert Paluch.
* MViRe - na potrzeby kursu UNITY3D v5.
* mvire.com 
*/
public class PunktStartu : MonoBehaviour {

	private Transform trans;
	/** Zawiera informację o tym czy gracz został ustawiony na pozycji startu.*/
	private bool startUstawiony;
    
	// Use this for initialization
	void Start () {
		/** Pobieramy obiekt transform w celu pobrania jego pozycji na której zostanie usawiony gracz.*/
		trans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!startUstawiony) {//Inicjalnie (na starcie) napewno jest false.

            if (!GraczInstancja.respown) {//Jeżeli false to oznacza, że wczytany został zapisany stan gry.
                //Ponieważ wczytujemy stan gry to punkt respown musi zostać pominięty.
                startUstawiony = true;//Ustawienie w punktu startu jest niepotrzebne.
                return;//Wychodzimy z metody, już nie musimy tu wchodzić.
            }

            //Pobieramy obiekt gracza.
            GameObject player = GameObject.FindGameObjectWithTag ("Player");
            
			if (player != null) {//Jeżeli gracza znaleziono

                GameObject start = null;
                //Sprawdzamy czy w komponencie 'GraczInstancja' została podana nazwa punktu startu.
                Debug.Log("PS: " + GraczInstancja.startNr);
                if (GraczInstancja.startNr != null && !GraczInstancja.startNr.Equals("")) { 
                    //Pobieramy punkt startu o danej nazwie.
                    start = GameObject.FindGameObjectWithTag(GraczInstancja.startNr);
                }

                Vector3 pos = trans.position;
                if(start != null) {//Jeżeli punkt startu został odnaleziony to go pobieramy.
                    pos = start.GetComponent<Transform>().position;
                }

                //Ustawiamy pozycję gracza na taką jak obiekt 'Respown'
                player.GetComponent<Transform>().position = pos;

                // Pozycja została już ustawiona, więc zmieniamy flagę, aby więcej nie wywoływać tego warunku.
                startUstawiony = true;
			}

		}
	}
}
// All Rights Reserved!
// Wszelkie Prawa Zastrzeżone!
// Tylko do użytku niekomercyjnego.