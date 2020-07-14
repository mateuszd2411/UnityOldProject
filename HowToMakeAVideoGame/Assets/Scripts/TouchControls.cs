using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour {

    private PlayerMovement thePlayer;

    private void Start()
    {
        thePlayer = FindObjectOfType<PlayerMovement>();
    }
    private void leftArrow()
    {
        //thePlayer.Move(-1)
    }
}
