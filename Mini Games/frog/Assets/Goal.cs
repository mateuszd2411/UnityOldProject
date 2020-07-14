using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

    private void OnTriggerEnter2D()
    {
        Debug.Log("You won!");
        Score.CurrentScore += 100;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
