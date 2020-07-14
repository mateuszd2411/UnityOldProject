using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float standingTreshold = 3f;
    public float distToRaise = 40f;
    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        IsStanding();
	}
	
	// Update is called once per frame
	void Update () {
        ///print(name + IsStanding());
	}

    public bool IsStanding ()
    {
        Vector3 rotationInEuler = transform.rotation.eulerAngles;

        float tilInX = Mathf.Abs(270 -rotationInEuler.x);
        float tilInZ = Mathf.Abs(rotationInEuler.z);

       if (tilInX < standingTreshold && tilInZ < standingTreshold)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void RaiseIfStanding()
    {
        if (IsStanding())
        {
            rigidBody.useGravity = false;
            transform.Translate(new Vector3(0, distToRaise, 0), Space.World);
        }
    }

    public void Lower ()
    {
        transform.Translate(new Vector3(0, -distToRaise, 0), Space.World);
        rigidBody.useGravity = true;
    }

}
