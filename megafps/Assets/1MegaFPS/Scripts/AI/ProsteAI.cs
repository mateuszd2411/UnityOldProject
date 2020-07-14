using UnityEngine;
using System.Collections;

/**
 * Klada odpowiedzialna za symulacje AI.
 * Animacja wroga lataiącego.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class ProsteAI : MonoBehaviour {

	//Obiekt gracza.
	private Transform gracz;
	//Obiekt wroga.
	private Transform mojObiekt;
	
	//Prędkość obracania się przeciwnika..
	public float predkoscObrotu = 4.0f;
	//Prędkość poruszania się przeciwnika.
	public float predkoscRuchu = 5.0f;
	//Zasięg wzroku przeciwnika.
	public float zasieg = 30.0f;

	//Odstęp w jakim zatrzyma się obiekt wroga od gracza.
	public float odstepOdGracza = 2f;

	// Use this for initialization
	void Start () {
		mojObiekt = transform;
		GameObject go = GameObject.FindWithTag ("Player");
		gracz = go.transform;
	}
	
	// Update is called once per frame
	void Update (){
		
		//Pobranie dystansu dzielącego wroga od obiektu gracza.
		float dystans = Vector3.Distance (mojObiekt.position, gracz.position);
		
		//Jeżeli dystans jaki dzieli obiekt wroga od obiektu gracza mieści się w zakresie widzenia wroga to 
		//obiekt wroga zaczyna poruszać się w kierunku gracza.
		//Obiekt wroga zatrzyma się przed graczem w zadanym odstępie.
		if (dystans < zasieg && dystans > odstepOdGracza) {
			
			//Funkcja Quaternion.Slerp (spherical linear interpolation)
			// pozwala obracać obiekt w zadanym kierunku z zadaną prędkością.
			//Quaternion.LookRotation - zwraca quaternion na podstawie werktora kierunku/pozycji.
			mojObiekt.rotation = Quaternion.Slerp (mojObiekt.rotation, Quaternion.LookRotation (gracz.position - mojObiekt.position), predkoscRuchu * Time.deltaTime);
			
			//Ruch wroga 
			mojObiekt.position += mojObiekt.forward * predkoscRuchu * Time.deltaTime;			

		}
				
	}
}

