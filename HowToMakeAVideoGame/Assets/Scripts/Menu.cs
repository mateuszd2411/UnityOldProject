using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void startgame ()
       {
          SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
      }

    public void Records ()
    {
        SceneManager.LoadScene("Records", LoadSceneMode.Additive);
    }

    public void About()
    {
        SceneManager.LoadScene("About", LoadSceneMode.Additive);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }

    public void LevelsTo()
    {
        SceneManager.LoadScene("Levels", LoadSceneMode.Additive);
    }

    public void Level01()
    {
        SceneManager.LoadScene("Level01", LoadSceneMode.Single);
    }

    public void Level02()
    {
        SceneManager.LoadScene("Level02", LoadSceneMode.Single);
    }

    public void Level03()
    {
        SceneManager.LoadScene("Level03", LoadSceneMode.Single);
    }




}
