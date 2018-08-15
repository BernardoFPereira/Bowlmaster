﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    public int lastStandingCount = -1;
    public Text standingDisplay;
    public float distanceToRaise = 40f;

    private Ball ball;
    private float lastChangeTime;
    private bool ballEnteredBox = false;

	// Use this for initialization
	void Start ()
    {
        ball = GameObject.FindObjectOfType<Ball>();
        print(ballEnteredBox);
	}
	
	// Update is called once per frame
	void Update ()
    {
        standingDisplay.text = CountStanding().ToString();

        if(ballEnteredBox)
        {
            CheckStanding();
        }
	}

    public void RaisePins()
    {
        // raise standing pins only by distanceToRaise
        Debug.Log("Raising pins");
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                pin.transform.Translate(new Vector3(0f, distanceToRaise, 0f));
            }
        }
    }

    public void LowerPins()
    {

    }

    public void RenewPins()
    {
        Debug.Log("Pins renewed");
    }

    void CheckStanding()
    {
        int currentStanding = CountStanding();
        // Update the lastStandingCount
        // Call PinsHaveSettled() when they have
        if(currentStanding != lastStandingCount)
        {
            lastChangeTime = Time.time;
            lastStandingCount = currentStanding;
            return;
        }

        float settleTime = 3f; // How long to consider pins settled
        if((Time.time - lastChangeTime) > settleTime) // If last change > 3s ago
        {
            PinsHaveSettled();
        }
    }

    void PinsHaveSettled()
    {
        ball.Reset();
        lastStandingCount = -1; // Indicate pins have settled, and ball not back in box
        ballEnteredBox = false;
        standingDisplay.color = Color.green;
    }

    int CountStanding()
    {
        int standing = 0;

        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if(pin.IsStanding())
            {
                standing++;
            }
        }
        return standing;
    }

    public void OnTriggerEnter(Collider collider)
    {
        GameObject thingHit = collider.gameObject;

        if(thingHit.GetComponent<Ball>())
        {
            ballEnteredBox = true;
            standingDisplay.color = Color.red;
            print(ballEnteredBox);
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        GameObject thingLeft = collider.gameObject;

        if (thingLeft.GetComponent<Pin>())
        {
            Destroy(thingLeft);
        }
    }
}
