using UnityEngine;
using System.Collections;

/**
 * Skrypt odpowiedzialny za cykl dnia i nocy. 
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class DzienINoc : MonoBehaviour {

    private Transform trans;
    public float predkosc = 5f;

    /** Żródło światla które ściemniamy lub rozjaśniamy.*/
    public Light slonce;

    /** Maksymalna jasność światła podczas dnia.*/
    public float maxIntensity = 2f;
    /** Minimalna jasność światła w nocy.*/
    public float minIntensity = 0f;

    /** Maksymalna jasność tekstur.*/
    public float maxAmbient = 0.7f;
    /** Minimalna jasność tekstur.*/
    public float minAmbient = 0f;

    /** Kolor tekstur za dnia.*/
    public Color kolorWDzien = new Color(80, 80, 80, 80);
    /** Kolor tekstur w nocy.*/
    public Color kolorWNoc = new Color(0, 0, 0, 250);

    private Transform gwiazdy;

    // Use this for initialization
    void Start () {
        trans = GetComponent<Transform>();
       gwiazdy = trans.Find("Gwiazdy");
    }
	
	// Update is called once per frame
	void Update () {
        trans.Rotate(0, 0, predkosc *Time.deltaTime );
        UstawSwiatla();
    }

    private void UstawSwiatla() {

       
        if(trans.rotation.eulerAngles.z > 0 && trans.rotation.eulerAngles.z < 180) {

            slonce.intensity = maxIntensity;
            RenderSettings.ambientIntensity = maxAmbient;
            RenderSettings.ambientLight = kolorWDzien;

            gwiazdy.gameObject.SetActive(false);

        } else {
            slonce.intensity = minIntensity;
            RenderSettings.ambientIntensity = minAmbient;
            RenderSettings.ambientLight = kolorWNoc;

           gwiazdy.gameObject.SetActive(true);
        }

    }
}
