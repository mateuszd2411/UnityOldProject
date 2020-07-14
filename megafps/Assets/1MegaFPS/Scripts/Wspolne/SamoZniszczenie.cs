using UnityEngine;
using System.Collections;

/**
 * Skrypt powodujący usunięcie obiektu po upływie zadanego czasu.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class SamoZniszczenie : MonoBehaviour {

	//Czas życia obiektu posiadającego ten skrypt.
	public float czasZycia = 1f;

	// Update is called once per frame
	void Update () {
		czasZycia -=Time.deltaTime;

		//Czy upłyną czas życia.
		if(czasZycia <=0){
			//Czas zycia upłyną usuń obiekt.
			Destroy(gameObject);
		}
	}

}
