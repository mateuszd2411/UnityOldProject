using UnityEngine;
using UnityEngine.UI;

public class UserAcount_Lobby : MonoBehaviour {

    public Text usernameText;

    void Start ()
    {
        if(UserAccountManager.IsLoggedIn)
        usernameText.text = UserAccountManager.playerUsername;
    }

    public void LogOut()
    {
        if (UserAccountManager.IsLoggedIn)
            UserAccountManager.instance.LogOut();
    }
}
