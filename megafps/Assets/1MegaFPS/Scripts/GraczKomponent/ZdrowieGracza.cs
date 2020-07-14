using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Skrypt zdrowia gracza.
 * Skrypt odpowiedzialny za zarządzenie zdrowiem gracza oraz wyświetlaniem
 * jego stanu w HUD.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class ZdrowieGracza : Zdrowie {

	/** 
	 * Punkty zdrowia wyświetlane na ekranie.
	 * http://docs.unity3d.com/ScriptReference/UI.Text.html
	 */
	public Text zdrowieUI;
	/**Obrazek obrażeń (błysk).*/
	public Image obrazeniaImage;
	/** Czas podczas którego będzie wyświetlany obrazek obrażeń.*/
	private float predkoscBlysku = 5f;

	/** Kolor obrażeń.*/
	private Color kolorObrazenia = new Color (1f, 0f, 0f, 0.1f); 

	/** Flaga z informacia czy zadano obrażenia.*/
	protected bool zadanoObrazenia = false;

	/** Metoda powoduje odjęcie punktów zdrowia zgodnie z zadanymi obrazeniami w zemiennej
	 * przekazanej jako parametr.
	 * 
	 * @param obrazenia - ilość zdrowia jakia ma zostać odięte od zdrowia obiektu.
	 */
	public override void otrzymaneObrazenia(float obrazenia) {
		base.otrzymaneObrazenia(obrazenia);
		//Zostały zadane obrażenia.
		zadanoObrazenia = true;
	}
	
	void Update(){
		if (zadanoObrazenia && obrazeniaImage != null) {
			obrazeniaImage.color = kolorObrazenia;
			zadanoObrazenia = false;
			//Aktualizacja wyświetlanych w HUD punktów zdrowia.
			setZdrwieUI();
		} else if(obrazeniaImage != null){
			obrazeniaImage.color = Color.Lerp(obrazeniaImage.color, Color.clear, predkoscBlysku * Time.deltaTime);
		}
	}

	/**
	 * Metoda aktualizuje tekst HUD'a wyświetlający punkty zdrowia.
	 */
	private void setZdrwieUI(){
		//Przeciwnicy nie zawierają zdrowia HUD więc zdrowie z zostanie z aktualizowane tylko 
		//jeżeli jesteśmy graczem.
		if (zdrowieUI != null) {
			zdrowieUI.text = zdrowie.ToString ();
		}
	}

    public override void setZdrowie(int zdrowie) {
        base.setZdrowie(zdrowie);
        setZdrwieUI();
    }
}
