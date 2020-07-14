using UnityEngine;
using System.Collections;

/**
 * Klasa odpowiedzialna za napęd dla pocisku np. rakiety..
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class PociskNaped : MonoBehaviour {

	/** Predkość pocisku. */
	public float predkosc = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Podobne do update z tym, że wywoływany regularnie w regularnych odstępach czasu.
	// Dobrze jest umieszczać w tej metodzie operacjie związane z fizką aby zawsze wykonywała się
	// taki sam sposób.
	void FixedUpdate () {
		//Wystrzelenie rakiety w kierunku w ktorym zwrocona jest kamera
		//Space.World powoduje odpowiednie odwrocenie rakiety wzgledem kamery
		transform.Translate (transform.forward * predkosc * Time.deltaTime, Space.World);
	}
}
