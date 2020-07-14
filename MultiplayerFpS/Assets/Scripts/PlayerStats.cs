using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    public Text deathCount;
    public Text killCount;

    private void Start()
    {
        if(UserAccountManager.IsLoggedIn)
        UserAccountManager.instance.GetData(OnReceivedData);
    }

    void OnReceivedData (string data)
    {
        if (killCount == null || deathCount == null)
            return;

        killCount.text = DataTranslate.DataToKills(data).ToString() + " KILLS";
        deathCount.text = DataTranslate.DataToDeaths(data).ToString() + " DEATHS";
    }

}
