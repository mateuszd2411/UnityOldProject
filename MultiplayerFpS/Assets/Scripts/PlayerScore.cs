using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerScore : MonoBehaviour {

    int lastKills = 0;
    int lastDeaths = 0;

    Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        StartCoroutine(SyncScoreLoop());
    }

    private void OnDestroy()
    {
        if (player != null)
            SyncNow();
    }

    IEnumerator SyncScoreLoop ()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            SyncNow();
        }

    }

    void SyncNow ()
    {
        if (UserAccountManager.IsLoggedIn)
        {
            UserAccountManager.instance.GetData(OnDataRecieved);
        }
    }

    void OnDataRecieved(string data)
    {
        if (player.kills <= lastKills && player.deaths <= lastDeaths)
            return;

        int killsSinceLast = player.kills - lastKills;
        int deathsSinceLast = player.deaths - lastDeaths;

        int kills = DataTranslate.DataToKills(data);
        int deaths = DataTranslate.DataToDeaths(data);

        int newKills = killsSinceLast + kills;
        int newDeath = deathsSinceLast + deaths;

        string newData = DataTranslate.ValuesToData(newKills, newDeath);

        Debug.Log("Syncing: " + newData);

        lastKills = player.kills;
        lastDeaths = player.deaths;

        UserAccountManager.instance.SendData(newData);

    }


}
