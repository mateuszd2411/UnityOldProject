using UnityEngine;
using System.Collections;

/**
 * Skrypt odpowiedzialny za zadanie obrażen obiektowi z którym koliduje 
 * obiekt zawierający dany skrypt.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class Obrazenia : MonoBehaviour {

	public float obrazenia = 20f;

	/**
	 * Metoda wywoływana w chwili nastąpienia kolizji z obiketem.
	 */
	void OnCollisionEnter(Collision kontakt ){  
		GameObject go = kontakt.gameObject;
		Zdrowie zdrowie = (Zdrowie) go.GetComponentInChildren<Zdrowie>();
		if (zdrowie != null) {
			zdrowie.otrzymaneObrazenia(obrazenia);
			//Destroy(gameObject);
		}
	}


}
