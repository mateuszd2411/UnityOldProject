using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/**
 * Skrypt odpowiedzialny za obsługę odebrania upuszczonego obiektu. 
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class ObszarUpuszczenia : MonoBehaviour, IDropHandler {

	public EqElementType elementTyp = EqElementType.EKWIPUNEK; 

	/** Obiekt ekwipunku.*/
	public GameObject ekwipunekObiekt;

	/** Maksymalna ilość elementów w obiekcie. */
	public int maksElement;

	/** Obiekt transform bieżącego elementu.*/
	private Transform trans;

	// Use this for initialization
	void Start () {
		trans = GetComponent<Transform> ();
	}

	/**
	 * Metoda wykrywająca zdarzenie upuszczenia obiektu.
	 */
	public void OnDrop(PointerEventData eventData) {
		Debug.Log (eventData.pointerDrag.name +" upuszczono w "+gameObject.name);

		ElementEq d = eventData.pointerDrag.GetComponent<ElementEq> ();
		if(d != null) {
			if(elementTyp == d.elementTyp) {                
				//Sprawdzamy czy slot jest pusty.
				if(trans.childCount < maksElement ) {
					d.setRodzic(trans);

				} else {//Slot nie jest pusty i osiągnięto maksymalną ilość elementów.

					//Pobieram obecny element slotu.
					Transform elem = transform.GetChild(0);
					//Obecny element slotu przerzucam do ekwipunku poprzez ustawienie rodzica.
					elem.SetParent(ekwipunekObiekt.transform);
					//Umieszczam nowy element w slocie poprzez ustawienie rodzica.
					d.setRodzic(trans);
				}
			} else if(elementTyp == EqElementType.EKWIPUNEK) {//Jeżeli typy się nie zgadzają to sprawdź 
															  //czy czasem nie ekwipunek, bo tu można dodać wszystko.
				d.setRodzic(trans);
			}

           
		}
       
    }
}
