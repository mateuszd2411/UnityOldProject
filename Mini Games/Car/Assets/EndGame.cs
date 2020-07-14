using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndGame : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D colInfo)
    {
        if (colInfo.CompareTag("Collidable"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
