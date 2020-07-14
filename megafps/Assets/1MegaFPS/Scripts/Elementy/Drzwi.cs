using UnityEngine;
using System.Collections;

/**
 * Skrypt odpowiedzialny za przechowanie stanu drzwi.
 * 
 * @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class Drzwi : MonoBehaviour {

	private bool otwarte = false;

	// Use this for initialization
	void Start () {
		
	}
	
	public void czyOtwarte(bool otwarte){
		this.otwarte = otwarte;
	}

	public bool czyOtwarte(){
		return otwarte;
	}
}
