using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public int score = 0;
    private Text myText;

    private void Start()
    {
        myText = GetComponent<Text>();
        Reset();
    }

    public void Score(int points)
    {
        Debug.Log("Score points");
         score += points;
        myText.text = score.ToString();
    }

   public void Reset()
    {
        score = 0;
        myText.text = score.ToString();
    }

}
