using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTest : MonoBehaviour {

    // Use this for initialization

    public GameObject player;

    public float maxTime;
    public float minSwipDistance;

    float startTime;
    float endTime;

    Vector3 startPos;
    Vector3 endPos;

    float swipeDistance;
    float swipeTime;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.touchCount > 0)
        {
            Debug.Log("Toque:"+ Input.touchCount);

            Touch touch = Input.GetTouch(0);
     
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Toque phase:" + touch.phase + " --- " + TouchPhase.Began);
                startTime = Time.time;
                startPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Toque phase:" + touch.phase + " --- " + TouchPhase.Ended);
                endTime = Time.time;
                endPos = touch.position;

                swipeDistance = (endPos - startPos).magnitude;
                swipeTime = endTime - startTime;

                Debug.Log("Start time:" + startTime + " - maxTime " +maxTime +" SwipeDistance:"+swipeDistance +"- MiniSwipeDistance:"+minSwipDistance);

                if (startTime < maxTime && swipeDistance > minSwipDistance)
                {
                    swipe();
                }

            }

        }

	}

    void swipe()

    {

        Debug.Log("Fez o Swipe");

        Vector2 distance = endPos - startPos;

        if ( Mathf.Abs (distance.x) > Mathf.Abs(distance.y) )
        {
            Debug.Log("Horizontal Swipe");
            if (distance.x > 0)
            {
                Debug.Log("Rigth Swipe");
            }
            else
            {
                Debug.Log("Left Swipe");
            }


        }
        else if (Mathf.Abs(distance.x) < Mathf.Abs(distance.y))
        {
            Debug.Log("Vertical Swipe");
            if (distance.y > 0)
            {
                Debug.Log("Up Swipe");
            }
            else
            {
                Debug.Log("Down Swipe");
                player.GetComponent<PLayerController>().Slid();
            }
        }
    }
}
