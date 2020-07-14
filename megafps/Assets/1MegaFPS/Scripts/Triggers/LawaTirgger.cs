using UnityEngine;
using System.Collections;

/**
 * Skrypt odpowiedzialny za zadawanie obrażeń przez lawę.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class LawaTirgger : MonoBehaviour {

	/** Informachja o tym czy obiekt znalazl siw w lawie.*/
	public bool obiektWLawie;
	/** Co ile będą zadawane obrażenia.*/
	public float czasObrazenia = 0.5f;
	/** Odliczanie do kolejnego obrażenia.*/
	public float liczObrazenia = 0;

	// Use this for initialization
	void Start () {
		liczObrazenia = czasObrazenia;
	}

	// Update is called once per frame
	void Update () {
		if (obiektWLawie && liczObrazenia > 0) {
			liczObrazenia -=Time.deltaTime;
		}
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("Player Enter");

		GameObject go = other.transform.gameObject;
		Zdrowie zdrwie = (Zdrowie)go.GetComponent<Zdrowie> ();
		if (zdrwie != null) {
			obiektWLawie = true;

		}

	}

	void OnTriggerStay(Collider other) {
		Debug.Log("Player Stay");		
		GameObject go = other.transform.gameObject;
		Zdrowie zdrwie = (Zdrowie)go.GetComponent<Zdrowie> ();
		if (zdrwie != null) {

			if(liczObrazenia <= 0) {
				zdrwie.otrzymaneObrazenia(5f);
				liczObrazenia = czasObrazenia;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		Debug.Log("Player Exit");
		obiektWLawie = false;
	}

}
