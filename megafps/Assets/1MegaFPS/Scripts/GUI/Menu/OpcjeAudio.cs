using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

/**
 * Skrypt pozwalający na ustawienie głośności muzyki i efektów dzwiękowych. * 
 */
public class OpcjeAudio : MonoBehaviour {

	/** Obiekt Miksera.*/
	public AudioMixer masterMixer;

	/**
	 * Metoda pozwala ustawić poziom głośności efektów dzwiękowych.
	 * 
	 * @param sfxLvl poziom głośności eektów.
	 */
	public void setGlosnoscEfektow(float sfxLvl){
		/**Ustaw poziom głośności 'efektyVol' w eksportowany parametr.*/
		masterMixer.SetFloat("DzwiekiVol", sfxLvl);
	}

	/**
	 * Metoda pozwala ustawić poziom głośności muzyki.
	 * 
	 * @param musicLvl poziom głośności muzyki.
	 */
	public void setGlosnoscMuzyki(float musicLvl){
		/**Ustaw poziom głośności 'muzykaVol' w eksportowany parametr.*/
		masterMixer.SetFloat("MuzykaVol", musicLvl);
	}
}
