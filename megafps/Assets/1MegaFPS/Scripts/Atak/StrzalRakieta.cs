using UnityEngine;
using System.Collections;

/*
* Skrypt odpowiedzialny za oddawanie strzalu z animacją/lotem pocisku.
* 
* @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class StrzalRakieta : MonoBehaviour {

	//Co ile mozna wykonac strzal
	public float czekaj = 1f;
	//Odliczanie do kolejnego strzalu.
	public float odliczanieDoStrzalu = 0f;
	//Obiekt pocisku.
	public GameObject pociskPrefab;

	/** Źródło dzwięki.*/
	public AudioSource zrodloDzwieku;
	/** Dzwięk strzału.*/
	public AudioClip odglosStrzalu;
		
	// Update is called once per frame
	public void strzal() {		
		//Strzal nastepuje jezeli przycisk myszy jest ciagle wcisniety oraz jezeli odliczanie
		//do kolejnego strzalu jest rowne zero(mozna strzelac).
		if(odliczanieDoStrzalu <= 0){
			//Strzal zostal oddany ustawienie ponownego odliczania.
			odliczanieDoStrzalu = czekaj;
			
			//Wystrzelenie rakiety (projectilePrefab) z pozycji kamery oraz jej zwrocie/obrocie (rotation)
			//Poniewaz pocisk jest durzy to aby uniknac kolizji z samym soba jest dodany dodatkowa odleglosc (Camera.main.transform.forward)
			Instantiate(pociskPrefab, Camera.main.transform.position+Camera.main.transform.forward, Camera.main.transform.rotation);

			if(zrodloDzwieku != null) {//Na wszelki wypadek jak by nie zostało podpięte źródło dzwięku.
				zrodloDzwieku.PlayOneShot(odglosStrzalu);
			}
		}
	}

	/**
	 * Metoda odliczania do strzału w celu odliczania nawet jeżei nie jest naciśnięty przycisk myszy.
	 */
	public void setOdliczanieDoStrzalu(){
		if (odliczanieDoStrzalu > 0) {
			//Zmniejszanie licznika do kolejnego strzalu/odliczanie do strzalu.
			odliczanieDoStrzalu -= Time.deltaTime;
		}
	}
}
