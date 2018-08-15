using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour 
{
    public float standingThreshold;
    public float distToRaise = 40f;

    private Rigidbody rigidBody;

	// Use this for initialization
	void Start () 
    {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    public void RaiseIfStanding()
    {
        if (IsStanding())
        {
            rigidBody.useGravity = false;
            transform.Translate(new Vector3(0f, distToRaise, 0f), Space.World);
        }
    }

    public void Lower()
    {
        transform.Translate(new Vector3(0f, -distToRaise, 0f), Space.World);
        rigidBody.useGravity = true;
    }

    public bool IsStanding()
    {
        Vector3 rotationInEuler = transform.rotation.eulerAngles;

        float tiltInX = Mathf.Abs(270f - rotationInEuler.x);
        float tiltInZ = Mathf.Abs(rotationInEuler.z);

        if(tiltInX < standingThreshold && tiltInZ < standingThreshold)
        {
            return true;
        }
        else
        {
            return false;    
        }
    }
}
