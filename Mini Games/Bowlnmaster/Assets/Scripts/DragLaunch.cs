using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLaunch : MonoBehaviour {
    private Vector3 dragStart, dragEnd;
    private float startTime, endTime;
    private Ball ball;

	// Use this for initialization
	void Start () {
        ball = GetComponent<Ball>();
	}

    public void MoveStart (float amount)
    {
        if (!ball.inPlay)
        {
            ball.transform.Translate(new Vector3(amount, 0, 0));
        }
        
    }

    public void DragStart ()
    {
        dragStart = Input.mousePosition;
        startTime = Time.time;
    
    }

    public void DragEnd ()
    {
        dragEnd = Input.mousePosition;
        endTime = Time.time;

        float dragDuration = endTime - startTime;

        float launchSpeedX = (dragEnd.x - dragStart.x) / dragDuration;
        float launchSpeedZ = (dragEnd.z - dragStart.z) / dragDuration;

        Vector3 launchVelocity = new Vector3(0, 0, 500);
        ball.Launch(launchVelocity);
    }
	
	
}
