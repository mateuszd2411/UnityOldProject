using UnityEngine;
using System.Collections;

/*
* Skrypt odpowiedzialny za włączenie efektów podwodnych.
* 
* @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class EfektyPodwodne : MonoBehaviour {

    //Nowe ustawienia mgły..
    public bool fog;//Czy mgła właczona.
    public Color kolorMgly;//Kolor mgły
    public float gestoscMgly;//Zasięg mgły.
    public FogMode trybMgly;//Tryb mgły.
    public float startMgly;//Początek mgły.
    public float koniecMgly;//Koniec mgły.

    //Pierwotne ustawienia efektów.
    private bool tmpFog; //Czy mgła właczona.
    private Color tmpKolorMgly;//Kolor mgły
    private float oldGestoscMgly;//Zasięg mgły.
    private FogMode oldTrybMgly;//Tryb mgły.
    private float tmpStartMgly;//Początek mgły.
    private float tmpKoniecMgly; //Koniec mgły.

    void Start() {
    }

    /**
    * Metoda wywoływana zanim kamera zacznie wyświetlać obraz.
    * http://docs.unity3d.com/ScriptReference/Camera-onPreRender.html
    */
    void OnPreRender() {
        //Zapamiętuje domyślne ustawienia mgły.
        tmpFog = RenderSettings.fog;
        tmpKolorMgly = RenderSettings.fogColor;
        oldGestoscMgly = RenderSettings.fogDensity;
        oldTrybMgly = RenderSettings.fogMode;
        tmpStartMgly = RenderSettings.fogStartDistance;
        tmpKoniecMgly = RenderSettings.fogEndDistance;

        //Nadaję nowe ustawienia mgły.
        RenderSettings.fog = fog;
        RenderSettings.fogColor = kolorMgly;
        RenderSettings.fogDensity = gestoscMgly;
        RenderSettings.fogMode = trybMgly;
        RenderSettings.fogStartDistance = startMgly;
        RenderSettings.fogEndDistance = koniecMgly;
    }

    /**
    * Metoda wywoływana po zakończeniu wyświetlania obrazu przez kamerę.
    * http://docs.unity3d.com/ScriptReference/MonoBehaviour.OnPostRender.html
    */
    void OnPostRender() {
        //Przywracam domyślne ustawienia mgły.
        RenderSettings.fog = tmpFog;
        RenderSettings.fogColor = tmpKolorMgly;
        RenderSettings.fogDensity = oldGestoscMgly;
        RenderSettings.fogMode = oldTrybMgly;
        RenderSettings.fogStartDistance = tmpStartMgly;
        RenderSettings.fogEndDistance = tmpKoniecMgly;
    }
    
}