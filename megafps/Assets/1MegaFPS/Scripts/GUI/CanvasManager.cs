using UnityEngine;
using System.Collections;

/**
 * Skrypt odpowiedzialny za dostarczanie informacji o aktywnym płutnie (Canvas). 
 * Jeżeli któreś płutno z tagiem "Menu" jest aktywne to ruch powinien być zablokowany.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class CanvasManager : MonoBehaviour {

    //Komponent odpowiedzialny za poruszanie się gracza.
    private PlayerControler playerControler;
    //Komponent odpowiedzialny za poruszanie się gracza w wodzie.
    private GraczWodaKontroler graczWodaKontroler;

    //Komponent odpowiedzialny za strzelanie.
    private GraczAtak graczAtak;

    // Use this for initialization
    void Start () {
        graczWodaKontroler = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().GetComponent<GraczWodaKontroler>();
        //graczAtak = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().GetComponent<GraczAtak>();
    }
	
	// Update is called once per frame
	void Update () {
        GameObject[] gm = GameObject.FindGameObjectsWithTag("Menu");
		GameObject[] gm2 = GameObject.FindGameObjectsWithTag("KsiazkaTresc");
        
		if (playerControler != null) {
			//Domyślnie róch i strzelanie aktywne/
			playerControler.SetRuchZablokowany (false);
			graczAtak.SetRuchZablokowany (false);

			bool blokuj1 = blokujGracza(gm);
			bool blokuj2 = blokujGracza(gm2);

			playerControler.SetRuchZablokowany (blokuj1 || blokuj2);
			graczAtak.SetRuchZablokowany (blokuj1 || blokuj2);

		} else {
			GameObject gmPlayer = GameObject.FindGameObjectWithTag("Player");
			if(gmPlayer != null) {
				playerControler = gmPlayer.GetComponent<Transform>().GetComponent<PlayerControler>();
				graczAtak = gmPlayer.GetComponent<Transform>().GetComponent<GraczAtak>();
			}
		}

        if (graczWodaKontroler != null) {
            //Domyślnie róch i strzelanie aktywne/
            graczWodaKontroler.SetRuchZablokowany(false);
            graczAtak.SetRuchZablokowany(false);

            bool blokuj1 = blokujGracza(gm);
            bool blokuj2 = blokujGracza(gm2);

            graczWodaKontroler.SetRuchZablokowany(blokuj1 || blokuj2);
            graczAtak.SetRuchZablokowany(blokuj1 || blokuj2);

        }

        //ustawieniaDomyslne();
    }

	private bool blokujGracza(GameObject[] gm){
		foreach (GameObject go in gm) {
			Canvas canv = go.GetComponent<Canvas> ();
			if (canv.enabled) { 
				return true;
			}
		}  
		return false;
	}

    private void ustawieniaDomyslne() {
        GameObject[] gm2 = GameObject.FindGameObjectsWithTag("KsiazkaTresc");
        foreach (GameObject go in gm2) {
            Canvas canv = go.GetComponent<Canvas>();
            canv.enabled = false;
        }
    }
}
