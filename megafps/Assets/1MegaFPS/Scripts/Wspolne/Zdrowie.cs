using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Skrypt zdrowia.
 * Jezeli obiekt ma ten skrypt to znaczy, że ma zdrowie ktore mozna mu zabrać.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class Zdrowie : MonoBehaviour {

	/** Punkty zdrowia obiektu zawierającego dany skrypt.*/
	public int zdrowie = 100;

	/** Metoda powoduje odjęcie punktów zdrowia zgodnie z zadanymi obrazeniami w zemiennej
	 * przekazanej jako parametr.
	 * 
	 * @param obrazenia - ilość zdrowia jakia ma zostać odięte od zdrowia obiektu.
	 */
	public virtual void otrzymaneObrazenia(float obrazenia) {
		//Jeżeli zdrowie większe od zera to można zadać obrażenia.
		if (zdrowie > 0) {
			//Odięcie od zdrowia punktów zadanych obrażeń.
			zdrowie -= (int)obrazenia;

			if(zdrowie < 0){//Jeżeli zdrowie mniejsze od zera to nie chcemy aby było wyświetlane zdrowie na minusie.
				zdrowie = 0; //Mniejsze od zera to wyzeruj.
			}
		}

		//Jeżeli zdrowie równe zero to obiekt do usunięcia.
		/*if(zdrowie <=0){
			Die();
		}*/	
	}

	/**
	 * Metoda powoduje usunięcie obiektu.
	 */
	public void Die(){
		Destroy(gameObject);	
	}

	/**
	 * Funkcja zwraca informację o tym czy obiekt posiadający zdrowie ciągle żyje.
	 * Jeżeli obiekt żyje to zwraca 'false' w przeciwnym razie 'true'.
	 */
	public bool czyMartwy(){
		if (zdrowie <= 0) {
			return true;
		}
		return false;
	}

    public virtual void setZdrowie(int zdrowie) {
        this.zdrowie = zdrowie;
    }

}
