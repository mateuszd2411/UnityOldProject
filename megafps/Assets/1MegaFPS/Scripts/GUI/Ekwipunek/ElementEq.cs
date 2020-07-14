using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/**
 * Skrypt odpowiedzialny za obsługę przeciągania elementu ekwipunku. 
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class ElementEq : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	/** Obiekt transform rodzica.*/
	private Transform rodzicEq;
	/** Obiekt transform bieżącego elementu.*/
	private Transform trans;

    private Transform canvas;

	/** Typ elementu.*/
	public EqElementType elementTyp = EqElementType.BRON; 
    
	// Use this for initialization
	void Start () {
        if(GameObject.FindGameObjectWithTag("Ekwipunek") != null) { 
            canvas = GameObject.FindGameObjectWithTag("Ekwipunek").GetComponent<Transform>();
        }
        trans = GetComponent<Transform> ();
        
		rodzicEq = trans.parent;
	}

	/**
	 * Metoda wywoływana w chwili rozpoczęcia przeciągania.
	 */
	public void OnBeginDrag(PointerEventData eventData) {
		Debug.Log ("OnBeginDrag");
        //Na czas przeciągania obiektu zmieniam rodzica
        //aby nasz element nie zmieniał pozycji w ekwipunku (rodzicem będzie płutno).
        if (canvas != null) {
            trans.SetParent(canvas);
        } else {
            trans.SetParent(rodzicEq.parent);
        }

		//Włączamy wykrywanie kursora myszy popbrzez wyłaczenie blokady promienia.
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	/**
	 * Metoda wywoływana w czasie przeciągania elementu.
	 */
	public void OnDrag(PointerEventData eventData){
		Debug.Log ("OnDrag");		
		//Aktualizujemy pozycję elementu o aktualną pozycję kursora.
		trans.position = eventData.position;
	}
	
	public void OnEndDrag(PointerEventData eventData){
		Debug.Log ("OnEndDrag");
        //Ponownie przypisujemy rodzica ekwipunku do elementu
        //spowoduje to jego ustawienie/posortowanie w ekwipunku.
        trans.SetParent (rodzicEq);

        //Wyłączamy wykrywanie kursora myszy popbrzez właczenie blokady promienia.
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public Transform getRodzicEq(){
		return rodzicEq;
	}
	public void setRodzic(Transform trans){
		rodzicEq = trans;
	}
}
