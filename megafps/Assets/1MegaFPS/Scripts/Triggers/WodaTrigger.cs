using UnityEngine;
using System.Collections;

/*
* Skrypt trigger wykrywający wejście do wody przez gracza.
* 
* @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class WodaTrigger : MonoBehaviour {

    //Transform triggera.
    private Transform trans;

    private Transform graczTransform;

    //Objekt wody który zostanie przekazany do skryptu podziału obrazu.
    private GameObject woda;
    //Komponent podziału obrazu w kamerze.
    private PodzialKamery podzialKamery;

    //Komponent transform kamery w wodzie.
    private Transform kameraWWodzieTrans;
    //Komponent transform głównej kamery
    private Transform kameraGlownaTrans;

    /** Skrypt odpowiedzialny za kotrolę gracza.*/
    private PlayerControler graczKontroler;

    /** Flaga z informacją czy gracz w wodzie.*/
    private bool graczWWodzie;
    /** Na jakiej głębokości nastąpi aktywacja/przełaczenie skryptów.*/
    public float poziomAktywacjiSkryptu = 0.6f;

    /** Flaga z informacją czy przełączyć skrypt na poruszanie w wodą.*/
    private bool przelaczSkrypt = false;
    /** Flaga z informacją czy skrypt został już przełączony, (aby uniknąć ciągłego przenikania).*/
    private bool przelaczony;

    private AudioSource zrodloDzwieku;
    public AudioClip dzwiekPodWoda;
    public AudioClip dzwiekWody;

    // Use this for initialization
    void Start () {
        trans = GetComponent<Transform>();
        woda = trans.Find("Woda1").gameObject;//Pobieram  obiekt (Mesh) wody.
        //Pobieram transform głównej kamery.
        kameraGlownaTrans = Camera.main.GetComponent<Transform>();
        //Pobieram komponent podziału obrazu w kamerze.
        podzialKamery = kameraGlownaTrans.GetComponent<PodzialKamery>();
        //Pobieram obiekt kamery dla wody.
        kameraWWodzieTrans = kameraGlownaTrans.Find("KameraWWodzie");

        zrodloDzwieku = trans.GetComponent<AudioSource>();
        DzwiekiWody();
    }
	
	// Update is called once per frame
	void Update () {
       if(graczWWodzie) {//Jeżeli gracz w wodzie.

            //Jeżeli gracz jest na odpowiedniej głębokości oraz skrypt nie został jeszcze przełączony.
            if (przelaczSkrypt && !przelaczony) {
                wlaczPoruszanieWWodzie();//Przełaczam skrypt na poruszanie pod wodą.
                przelaczony = true;//Zmieniamy flagę, aby ponownie tu nie wchodzić. 

                DzwiekiPodWoda();

            } else if(!przelaczSkrypt && przelaczony) {
                wlaczPoruszaniePoLadzie();//Przełączam skrypt na poruszanie się w wodzie.
                przelaczony = false;//Zmieniamy flagę, aby ponownie tu nie wchodzić. 

                DzwiekiWody();
            }
        } 
	}

    /**
     * Gracz wchodzi do wody.
     */
    void OnTriggerEnter(Collider other) {
        Debug.Log("OnTriggerEnter");

        Transform obiektTrans = other.GetComponent<Transform>();
        //Pobieram komponent PlayerControler.
        graczKontroler = obiektTrans.GetComponent<PlayerControler>();

        if (graczKontroler != null) { //Jeżeli różne null to do wody na pewno wszedł gracz.
            //Pobieramy obiekt transform gracza.
            graczTransform = other.GetComponent<Transform>();

            //Ustawiam flagę z informacją, że gracz w wodzie.
            graczWWodzie = true;
            //Przekazuję obiekt wody do skryptu podziału ekranu.
            podzialKamery.setWoda(woda);
            //Aktywuję kamerę dla wody.
            kameraWWodzieTrans.gameObject.SetActive(true);
        }

        
    }

    /**
     * Gracz wychodzi z wody.
     */
    void OnTriggerExit(Collider other) {
        Debug.Log("OnTriggerExit");
        //Pobieramy obiekt transform gracza.
        Transform obiektTrans = other.GetComponent<Transform>();
        //Pobieram komponent PlayerControler.
        graczKontroler = obiektTrans.GetComponent<PlayerControler>();
        if (graczKontroler != null) { //Jeżeli różne null to do wody na pewno wszedł gracz.
            //Ustawiam flagę z informacją, że gracz opuścił wodę.
            graczWWodzie = false;
            //Usuwam obiekt wody ze skryptu podziału ekranu.
            podzialKamery.setWoda(null);

            //Wyłączam obiekt kamery w wodzie.
            kameraWWodzieTrans.gameObject.SetActive(false);
        }
        
    }

    void OnTriggerStay(Collider other) {
        Debug.Log("OnTriggerStay");

        //Pobieram obiekt z którym nastąpiła kolizja.
        Transform graczTransform = other.GetComponent<Transform>();

        //Pobieram komponent PlayerControler.
        graczKontroler = graczTransform.GetComponent<PlayerControler>();

        if (graczKontroler != null) {//Jeżeli komponent został odnaleziony to kolizja z graczem.
            //Sprawdzam na jakiej wysokości jest powierzchnia.
            float powierzchnia = trans.position.y + trans.localScale.y;

            //Czy gracz na wysokości przełączenia skryptów..
            if (graczTransform.position.y > powierzchnia - poziomAktywacjiSkryptu) {
                przelaczSkrypt = false; //Gracz za wysoko.
            } else {
                przelaczSkrypt = true;//Gracz na poziomie przełączania skryptów.
            }

            Debug.Log("OnTriggerStay" + powierzchnia);
        }
        
    }

    /**
    * Metoda odpowiedzialna za przełączenie skryptu z poruszania się po lądzie na skrypt
    * poruszania się w wodzie.
    */
    private void wlaczPoruszanieWWodzie() {
        if (graczTransform != null) {
            //Gracz wszedł do wody, zatem wyłączamy podstawowy skrypt odpowiedzialny za poruszanie.
            graczKontroler = graczTransform.GetComponent<PlayerControler>();

            if (graczKontroler != null) { //Jeżeli różne null to do wody na pewno wszedł gracz.
                graczKontroler.enabled = false;

                //Aktywujemy skrypt odpowiedzialny za poruszanie się w wodzie.
                GraczWodaKontroler graczWodaKontroler = graczTransform.GetComponent<GraczWodaKontroler>();
                graczWodaKontroler.enabled = true;
               
                //Ustawiamy aktualny zwrot kamery w skrypcie poruszania się w wodzie.
                graczWodaKontroler.myszGoraDol = graczKontroler.myszGoraDol;
                //Restart potęcjalnego skoku.
                graczWodaKontroler.aktualnaWysokoscSkoku = 0f;
            }
        }
    }

    /**
    * Metoda odpowiedzialna za przełączenie skryptu z poruszania się w wodzie na skrypt
    * poruszania się po lądzie.
    */
    private void wlaczPoruszaniePoLadzie() {
        if (graczTransform != null) {

            //Gracz wyszedł z wody, zatem włączamy podstawowy skrypt odpowiedzialny za poruszanie na lądzie.
            graczKontroler = graczTransform.GetComponent<PlayerControler>();
            if (graczKontroler != null) { //Jeżeli różne null to do wody na pewno wszedł gracz.
                graczKontroler.enabled = true;

                //Deaktywujemy skrypt odpowiedzialny za poruszanie się w wodzie.
                GraczWodaKontroler graczWodaKontroler = graczTransform.GetComponent<GraczWodaKontroler>();
                graczWodaKontroler.enabled = false;

                //Ustawiamy aktualną wysokość w skrypcie poruszania się na lądzie.
                graczKontroler.aktualnaWysokoscSkoku = graczWodaKontroler.aktualnaWysokoscSkoku;
                //Ustawiamy aktualny zwrot kamery w skrypcie poruszania się na lądzie.
                graczKontroler.myszGoraDol = graczWodaKontroler.myszGoraDol;

            }
        }
    }
    
    /**
    * Metoda uruchamia dzwięki pod wodą.
    */
    private void DzwiekiPodWoda() {
        if (zrodloDzwieku != null && dzwiekPodWoda != null) {
            zrodloDzwieku.Stop();
            zrodloDzwieku.clip = dzwiekPodWoda;
            zrodloDzwieku.spatialBlend = 0;
            zrodloDzwieku.Play();
        }
    }

    /**
    * Metoda uruchamia dzwięki pod wodą.
    */
    private void DzwiekiWody() {
        if (zrodloDzwieku != null && dzwiekWody != null) {
            zrodloDzwieku.Stop();
            zrodloDzwieku.spatialBlend = 1;
            zrodloDzwieku.clip = dzwiekWody;
            zrodloDzwieku.Play();
        }
    }
    
}
// All Rights Reserved!
// Wszelkie Prawa Zastrzeżone!
// Tylko do użytku niekomercyjnego.