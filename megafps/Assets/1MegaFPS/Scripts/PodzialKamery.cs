using UnityEngine;
using System.Collections;

/*
* Skrypt odpowiedzialny za podzia� ekranu w chwili zanurzania kamery w wodzie oraz uruchomienie efekt�w podwodnych.
* 
* @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class PodzialKamery : MonoBehaviour {

    private Transform trans;

    /** Kamera g��wna.*/
    private Camera kameraGlowna;
    /** Kamera dla wody.*/
    private Camera kameraWWodzie;
    /** Transform dla wody.*/
    private Transform kamWWodzieTrans;
    /** Obiekt wody potrzebny do pobrania pozycji.*/
    private GameObject woda;

    /** Efekty przebywania pod wod� dla kamery podwodnej.*/
    private EfektyPodwodne efektyPodwodne;

    /** Stopien podzia�u obrazu (skala od 0 do 1).*/
    public float poziomPodzialuEkranu;

    void Start() {
        trans = GetComponent<Transform>();
        kameraGlowna = GetComponent<Camera>();

        //Pobieram obiekt Transform kamery podwodnej.
        kamWWodzieTrans = trans.Find("KameraWWodzie");
        //Pobieram komponent kamery z obiektu kamery podwodnej.
        kameraWWodzie = kamWWodzieTrans.GetComponent<Camera>();

        //Domy�lnie kamera pod wod� wy�aczona.
        kamWWodzieTrans.gameObject.SetActive(false);
        //Pobieram komponent z kamery podwodnej odpowiedzialny za efekty podwodne. 
        efektyPodwodne = kamWWodzieTrans.GetComponent<EfektyPodwodne>();
    }

    void Update() {
        //Ustawiam podzia� ekranu.
        ustawPodzial();
        //W�aczam efekty przebywania pod wod�.
        wlaczEfektWody();

        /*Sprawdzam czy gracz znajduje si� w wodzie oraz czy kamera jest cz�ciowo zanurzona.
          Je�eli kamera zanurzona to mo�na dzieli� ekran.*/
        if (getWodaTrans() != null && czyUstawicPodzialuEkranu()) {

            if (poziomPodzialuEkranu > 0 && poziomPodzialuEkranu < 1) {
                ustawPodzialEkranu();
            }
        } else { // Gracz wyszed� z wody. Przywr�cenie ustawie� domy�lnych.
            resetKameryGlownej();
            poziomPodzialuEkranu = 0;
        }
    }

    /**
     * Metoda odpowiedzialna za podzia� ekranu w zale�no�ci od stopnia zanurzenia kamery.
     */
    void ustawPodzialEkranu() {
        Camera zanurzonaKamera = kameraWWodzie;

        float polowaWysokosci = Mathf.Tan(zanurzonaKamera.fieldOfView * Mathf.Deg2Rad * .5f) * zanurzonaKamera.nearClipPlane;
        float gornaCzescGory = polowaWysokosci;
        float dolnaczescGory = (poziomPodzialuEkranu - .5f) * polowaWysokosci * 2f;
        float gornaCzescDolu = dolnaczescGory;
        float dolnaCzescDolu = -polowaWysokosci;

        Matrix4x4 macierzKameryGlownej = kameraGlowna.projectionMatrix;
        macierzKameryGlownej[1, 1] = (2f * zanurzonaKamera.nearClipPlane) / (gornaCzescGory - dolnaczescGory);
        macierzKameryGlownej[1, 2] = (gornaCzescGory + dolnaczescGory) / (gornaCzescGory - dolnaczescGory);

        Matrix4x4 macierzKameryDolnej = zanurzonaKamera.projectionMatrix;
        macierzKameryDolnej[1, 1] = (2f * zanurzonaKamera.nearClipPlane) / (gornaCzescDolu - dolnaCzescDolu);
        macierzKameryDolnej[1, 2] = (gornaCzescDolu + dolnaCzescDolu) / (gornaCzescDolu - dolnaCzescDolu);

        kameraGlowna.projectionMatrix = macierzKameryGlownej;
        zanurzonaKamera.projectionMatrix = macierzKameryDolnej;

        Rect rectDlaKamDolnej = zanurzonaKamera.rect;
        rectDlaKamDolnej.height = poziomPodzialuEkranu;
        zanurzonaKamera.rect = rectDlaKamDolnej;

        Rect rectDlaKamGornej = kameraGlowna.rect;
        rectDlaKamGornej.height = 1f - poziomPodzialuEkranu;
        rectDlaKamGornej.y = poziomPodzialuEkranu;
        kameraGlowna.rect = rectDlaKamGornej;
    }

    /**
     * Metoda wylicza miejsce podzia�u ekranu na zanurzon� i nad wodn�.
     */
    private void ustawPodzial() {

        if (getWodaTrans() != null) { //Gracz w wodzie.

            if (czyUstawicPodzialuEkranu()) { //Kamera cz�ciowo zanurzona.

                //Pobieram rozmiar ekranu na podstawie g�rnej i dolnej kraw�dzi.
                float rozmiarEkranu = Vector3.Distance(getDolnaCzescKamery(), getGornaCzescKamery());

                //Obliczam kierunek dla promienia wykrywaj�cego wod�.
                Vector3 kierunekPromienia = getDolnaCzescKamery() - getGornaCzescKamery();

                //Tworz� promien wykrywaj�cy stopie� zanurzenia w wodzie.
                Ray ray = new Ray(getGornaCzescKamery(), kierunekPromienia);

                //Zwr�ci informacj� o stopniu zanurzenia.
                RaycastHit hitInfo;

                /*Kamera znajduje si� w obiekcie gracza a zatem nie mo�na dopu�ci�, aby promie� wchodzi� w kolizj�
                z graczem. Tworze zmienn� z informacja o ignorowaniu warstwy gracza.*/
                int warstwaDoIgnorowania = ~(1 << 8);

                //Sprawdzam czy promie� natrafil na wod�.
                if (Physics.Raycast(ray, out hitInfo, rozmiarEkranu, warstwaDoIgnorowania)) {

                    //Pobranie informacji o trafionym obiekcie.
                    Vector3 hitPoint = hitInfo.point;
                    Debug.DrawRay(getGornaCzescKamery(), hitPoint - getGornaCzescKamery(), Color.red);

                    /*Odleg�o�� od punktu trafienia promienia w wod� do dolnej kraw�dzi kamery jest 
                      wielko�ci� zanurzenia kamery w wodzie.*/
                    float zanurzenie = Vector3.Distance(getDolnaCzescKamery(), hitPoint);

                    //Debug.Log("hitPoint["+ hitPoint + "], X ["+dis+"], A["+ getDolnaCzescKamery() + "], C["+ getGornaCzescKamery() + "], AC="+ rozmiarEkranu + "");

                    //Proc�towe zanurzenie kamery w wodzie (od 0 do 1).
                    poziomPodzialuEkranu = zanurzenie / rozmiarEkranu;
                }

            } else if (getDolnaCzescKamery().y > getWodaTrans().position.y) { //Gracz dalej w wodzie ale kamera nie zanurzona.
                //Przywracam domy�lne ustawienia obydwu kamer.
                resetKameryGlownej();
                resetKameryWody();
                poziomPodzialuEkranu = 0;
            } else if (getGornaCzescKamery().y < getWodaTrans().position.y) { //Ca�kowite zanurzenie kamery.
                //Przywracam domy�lne ustawienia obydwu kamer.
                resetKameryGlownej();
                resetKameryWody();
                poziomPodzialuEkranu = 1;
            }
        }
    }

    /**
     * Funkcja dostarcza informacj� o tym czy ma nast�pi� ustawianie podzia�u ekranu czy kamera jest cz�ciowo zanurzona.
     */
    private bool czyUstawicPodzialuEkranu() {
        return getDolnaCzescKamery().y < getWodaTrans().position.y && getGornaCzescKamery().y > getWodaTrans().position.y;
    }

    /**
     * Funkcja zwraca polozenie dolnej krawedzi kamery(plan bliski) w �wiecie;
     * Funkcja zwraca dolny lewy r�g.
     */
    private Vector3 getDolnaCzescKamery() {
        Vector3 p = kameraGlowna.ScreenToWorldPoint(new Vector3(0, 0, kameraGlowna.nearClipPlane));
        return p;
    }

    /**
     * Funkcja zwraca polozenie g�rnej krawedzi kamery(plan bliski) w �wiecie;
     * Funkcja zwraca lewy g�rny r�g.
     */
    private Vector3 getGornaCzescKamery() {
        Vector3 p = kameraGlowna.ScreenToWorldPoint(new Vector3(0, Screen.height - 1, kameraGlowna.nearClipPlane));
        return p;
    }

    /**
     * Przywr�cenie domy�lnych/wyj�ciowych ustawie� kamyry g��wnej.
     */
    private void resetKameryGlownej() {
        Rect rect = new Rect(0f, 0f, 1f, 1f);
        Camera.main.rect = rect;
        kameraGlowna.ResetProjectionMatrix();
    }

    /**
     * Przywr�cenie domy�lnych/wyj�ciowych ustawie� kamyry zanurzonej w wodzie.
     */
    private void resetKameryWody() {
        Rect rect = new Rect(0f, 0f, 1f, 1f);
        kameraWWodzie.rect = rect;
        kameraWWodzie.ResetProjectionMatrix();
    }

    private void wlaczEfektWody() {
        if (getWodaTrans() != null) {
            if (getDolnaCzescKamery().y < getWodaTrans().position.y) {
                efektyPodwodne.fog = true;
            } else {
                efektyPodwodne.fog = false;
            }
        }
    }

    

    /**
     * Dostarcza obiekt Transform wody.
     */
    private Transform getWodaTrans() {
        if (woda != null) {
            return woda.GetComponent<Transform>();
        }
        return null;
    }

    /**
     * Pozwala na przekazanie do skryptu obiektu wody, w kt�rej znalaz� si� gracz.
     */
    public void setWoda(GameObject woda) {
        this.woda = woda;
    }
}


// All Rights Reserved!
// Wszelkie Prawa Zastrze�one!
// Tylko do u�ytku niekomercyjnego.