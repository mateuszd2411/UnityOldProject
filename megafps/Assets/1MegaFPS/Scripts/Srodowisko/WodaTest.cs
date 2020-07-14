using UnityEngine;
using System.Collections;

public class WodaTest : MonoBehaviour {

    private Transform trans;

    //Nowe ustawienia mgły..
    public Color kolorMgly;//Kolor mgły
    public float gestoscMgly;//Zasięg mgły.
    public FogMode trybMgly;//Tryb mgły.
    public float startMgly;//Początek mgły.
    public float koniecMgly;//Koniec mgły.

    //Pierwotne ustawienia efektów.
    private Color tmpKolorMgly;//Kolor mgły
    private float oldGestoscMgly;//Zasięg mgły.
    private FogMode tmpTrybMgly;//Tryb mgły.
    private float tmpStartMgly;//Początek mgły.
    private float tmpKoniecMgly; //Koniec mgły.

    public float wysokosc = 13f;

    // Use this for initialization
    void Start () {
        trans = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
	    if(trans.position.y < wysokosc) {
            
            //Zapamiętuje domyślne ustawienia mgły.
            tmpKolorMgly = RenderSettings.fogColor;
            oldGestoscMgly = RenderSettings.fogDensity;
            tmpTrybMgly = RenderSettings.fogMode;
            tmpStartMgly = RenderSettings.fogStartDistance;
            tmpKoniecMgly = RenderSettings.fogEndDistance;

            //Nadaję nowe ustawienia mgły.
            RenderSettings.fog = true;
            RenderSettings.fogColor = kolorMgly;
            RenderSettings.fogDensity = gestoscMgly;
            RenderSettings.fogMode = trybMgly;
            RenderSettings.fogStartDistance = startMgly;
            RenderSettings.fogEndDistance = koniecMgly;


        } else {
            //Przywracam domyślne ustawienia mgły.
            RenderSettings.fog = false;
            RenderSettings.fogColor = tmpKolorMgly;
            RenderSettings.fogDensity = oldGestoscMgly;
            RenderSettings.fogMode = tmpTrybMgly;
            RenderSettings.fogStartDistance = tmpStartMgly;
            RenderSettings.fogEndDistance = tmpKoniecMgly;

        }
	}
}
