using UnityEngine;
using System.Collections;

/*
* Skrypt odpowiedzialny za zwrócenie nazwy tekstury terenu, na której znajduje się gracz.
* 
* @author Hubert Paluch.
 * MViRe - na potrzeby kursu UNITY3D v5.
 * mvire.com 
 */
public class PowierzchniaTerenu : MonoBehaviour {

	/**
	 * Funkcja zwraca tablicę z wartościami określającymi stopień użycia/zmiksowania tekstury w danej komórce.
     * Tablica zwracana na podstawie pozycji gracza na terenie.  
	 */
    private static float[] PobierzMixTekstur (Vector3 pozycjaGracza) {
        		
		//Pobieram główny teren świata.
        Terrain terrain = Terrain.activeTerrain;
		// Pobieramy obiekt TerrainData zawierający wszsytkie informacje o terenie.
        TerrainData terrainData = terrain.terrainData;       

        //Pobieramy pozycję terenu.
        Vector3 terrainPos = terrain.transform.position;

        /* Pobieramy pozycję gracza i konwertujemy ją do pozycji komórki w siatce MixMapy
        * Aby przekonwertować pozycję gracza do pozycji komórki w siatce musimy
        * pobrać pozycję gracza następnie odjąć pozycję terenu, podzielić przez rozmiar terenu a następnie pomnożyć przez rozmiar MixMapy.
        */
        int mapX = (int)(((pozycjaGracza.x - terrainPos.x) / terrainData.size.x) * terrainData.alphamapWidth);
        int mapZ = (int)(((pozycjaGracza.z - terrainPos.z) / terrainData.size.z) * terrainData.alphamapHeight);

        /* Kiedy mamy już koordynaty komórki z siatki MixMap musimy pobrać wartości odpowiedzialne za miksowanie tekstur w danej pozycji.*/
        float[,,] splatmapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1); 
        
        /* Zawiera wartości ze stopniem zmiksowania/użycia tekstury w danej komórce.
         * Tablica jest o rozmiarze ilości tekstur przypiętych do terenu.
         */
        float[] cellMix = new float[splatmapData.GetUpperBound(2) + 1];

        /* Przepisuje stopień użycia tekstury w komórce do tablicy cellMix. */
        for (int n = 0; n < cellMix.Length; ++n) {
            cellMix[n] = splatmapData[0, 0, n];
        }

        return cellMix;
    }

    /**
     * Funkcja zwraca nazwę tekstury, które dominuje na terenie w miejscu, w którym znajduje się gracza.
     */
    public static string NazwaTeksturyWPozycji(Vector3 pozycjaGracza) {        
        float[] mix = PobierzMixTekstur(pozycjaGracza);

        float maxMix = 0; // Największy stopień użycia.
        int maxIndex = 0; // Indeks tekstury o największym stopniu użycia.

        //Iteruję tablice w poszukiwaniu największej wartości.
        //Porównuję kolejne wartości i zapamiętuję największą oraz jej indeks, który odpowiada indeksowi tekstury.
        for (int n = 0; n < mix.Length; ++n) {
            if (mix[n] > maxMix) {
                maxIndex = n;
                maxMix = mix[n];
            }
        }
               
        // Pobieramy obiekt splatPrototypes w celu pobrania wszystkich tekstór przypisanych do terenu.        
        SplatPrototype[] sp = Terrain.activeTerrain.terrainData.splatPrototypes;        
       
        return sp[maxIndex].texture.name;
    }
}
