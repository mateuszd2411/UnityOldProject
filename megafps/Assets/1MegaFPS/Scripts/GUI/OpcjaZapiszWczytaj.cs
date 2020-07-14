using UnityEngine;
using System.Collections;

using System; //Serializable.
using System.Runtime.Serialization.Formatters.Binary;//Serializes i deserializes obiektu lub cały wykres z połączonych obiektów w formacie binarnym.
using System.IO;//Do operacji na plikach (czytanie, pisanie do pliku).

/**
 * Skrypt odpowiedzialny za wyświetlenie komunikatu i przechodzenie pomiędzy
 * poziomami.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class OpcjaZapiszWczytaj : MonoBehaviour {

    /** Zmienna przechowuje obiekt transform racza.*/
    private Transform gracz;

    // Use this for initialization
    void Start() {
        gracz = GraczInstancja.instancja.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        //Szybki zapis.
        if (Input.GetKeyDown(KeyCode.F5)) {
            zapisz();
        }

        //Szybkie wczytanie.
        if (Input.GetKeyDown(KeyCode.F10)) {
            wczytaj();
        }
    }

    /**
    * Metoda zapisuje stan gry do pliku.
    */
    public void zapisz() {
        Debug.Log("ZAPISZ");
        
        //Posłuży do przesyłania danych do pliku.
        //Unity ma własną konfigurację w tym wyspecyfikowane miejsce, w którym składuje takie pliki.
        //Pod Windows jest to z reguły miejsce w katalogu użytkownika.
        FileStream plik = File.Create(Application.persistentDataPath + "/graczInfo.data");

        // Obiekt zawierający informacjie o stanie naszego gracza.
        GraczDane dane = new GraczDane();
        dane.nazwaSceny = Application.loadedLevelName; //Pobieramy nazwę poziomu/sceny.
        dane.zdrowie = gracz.GetComponent<ZdrowieGracza>().zdrowie;//Pobieramy stan zdrowia.
        dane.pozycja = new Vector3Serialization(gracz.GetComponent<Transform>().position);//Pobieramy pozycję gracza.
        dane.obrot = new QuaternionSerialization(gracz.GetComponent<Transform>().rotation);//Pobieramy pozycję gracza.

        //Posłuży do zapisywania danych do pliku.
        BinaryFormatter bf = new BinaryFormatter();

        //Serializujemy/zapisujemy dane do pliku
        bf.Serialize(plik, dane);
        plik.Close();//Zamykamy plik (kończymy operacje na pliku).
    }

    /**
    * Metoda odczytuje stan gry z pliku.
    */
    public void wczytaj() {
        Debug.Log("WCZYTAJ");

        //Zanim odczytamy dane upewnijmy się, że jest co czytać.
        //Sprawdzamy czy plik zapisu istnieje.
        if (File.Exists(Application.persistentDataPath + "/graczInfo.data")) {
                        
            //Odczytujemy/pobieramy dane z pliku.
            FileStream plik = File.Open(Application.persistentDataPath + "/graczInfo.data", FileMode.Open);

            //Posłuży do odczytu danych do pliku.
            BinaryFormatter bf = new BinaryFormatter();

            // Deserializujemy dane z pliku i przekształcamy je na obiekt GraczData.
            GraczDane dane = (GraczDane)bf.Deserialize(plik);
            plik.Close(); //Plik odczytany zamykamy plik.          

            //Ustawiamy dane;
            GraczInstancja.respown = false; //Pomijamy punkt respown, ustawimy pozycję gracza no podstawie odczytanych informacji.
            Application.LoadLevel(dane.nazwaSceny);//Wczytujemy scenę.            
            gracz.GetComponent<ZdrowieGracza>().setZdrowie(dane.zdrowie);//Ustawiamy zdrowie.
            gracz.GetComponent<Transform>().position = dane.pozycja.pobierzWektor();//Ustawiamy pozycję gracza.
            gracz.GetComponent<Transform>().rotation = dane.obrot.pobierzQuaternion();//Ustawiamy pozycję gracza.


            //Naciśnięty został przycisk wczytaj więc chowamy menu.
            Menu menu = GameObject.FindGameObjectWithTag("Interface").GetComponent<Transform>().GetComponentInChildren<Menu>();
            menu.PrzyciskStart();
        }
    }

}

/**
* Klasa zawiera informacje o graczu.
*/
[Serializable]
class GraczDane {
    public String nazwaSceny;//Nazwa poziomu/sceny
    public int zdrowie;//Punkty zdrowia.
    public Vector3Serialization pozycja;//Pozycja gracza.
    public QuaternionSerialization obrot;//Obrót gracza.
}

/**
* Klasa pomocnicza pozwalająca na zapisanie pozycji gracza.
* Pozwala zapisać i odczytać obiekt Vector3.
*/
[Serializable]
class Vector3Serialization {

    //Pozycjie na osiach.
    public float x;
    public float y;
    public float z;

    /**
    * Konstruktor
    */
    public Vector3Serialization(Vector3 v) {
        ustawWektor(v);
    }

    /** Metoda przepisuje obiekt Vector3 do obiektu pomcniczego. */
    public void ustawWektor(Vector3 v) {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;        
    }

    /** Metoda zwraca obiekt Vektor3 na podstawie odczytanych danych z pliku.*/
    public Vector3 pobierzWektor() {
        Vector3 vec = new Vector3();
        vec.x = this.x;
        vec.y = this.y;
        vec.z = this.z;
        return vec;
    }

}

/**
* Klasa pomocnicza pozwalająca na zapisanie obrotu gracza.
* Pozwala zapisać i odczytać zwrot gracza.
*/
[Serializable]
class QuaternionSerialization {

    //Pozycjie na osiach.
    public float x;
    public float y;
    public float z;
    public float w;

    /**
    * Konstruktor
    */
    public QuaternionSerialization(Quaternion v) {
        ustawQuaternion(v);
    }

    /** Metoda przepisuje obiekt Vector3 do obiektu pomcniczego. */
    public void ustawQuaternion(Quaternion v) {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
        this.w = v.w;
    }

    /** Metoda zwraca obiekt Vektor3 na podstawie odczytanych danych z pliku.*/
    public Quaternion pobierzQuaternion() {
        Quaternion vec = new Quaternion();
        vec.x = this.x;
        vec.y = this.y;
        vec.z = this.z;
        vec.w = this.w;
        return vec;
    }

}
// All Rights Reserved!
// Wszelkie Prawa Zastrzeżone!
// Tylko do użytku niekomercyjnego.