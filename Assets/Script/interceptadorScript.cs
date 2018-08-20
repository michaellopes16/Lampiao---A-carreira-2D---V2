using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class interceptadorScript : MonoBehaviour {

	// Use this for initialization
	public PLayerController plc;
	public float startTime;
	public Vector2 initialPosition;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(startTime!=-1){
			if(Time.time - startTime > 1f)
			{
				plc.pular();
				startTime = -1;
			}
		}
	}

	void OnMouseDown()
	{
		if(!EventSystem.current.IsPointerOverGameObject ()){
			startTime = Time.time;
			initialPosition = Input.mousePosition;
		}
	}

	/// <summary>
	/// OnMouseUp is called when the user has released the mouse button.
	/// </summary>
	void OnMouseUp()
	{
		if(startTime!=-1){
				Vector2 finalPosition = Input.mousePosition;
			Vector2 delta = finalPosition - initialPosition;
			if(Vector2.Distance(initialPosition,finalPosition)<50f){
				plc.pular();
				startTime = -1;
				return; 
			}
			if(Time.time - startTime<0.075f)
			{
				plc.pular();
				startTime = -1;
			//}else{
			//	startTime = -1;

			//	float angle =  Mathf.Atan(delta.y / delta.x) * (180.0f / Mathf.PI);
			//	Debug.Log("Angulo swipe:"+angle);
			//	if(angle > 25 && angle < 155)
			//	{
			//		plc.saltarBaixo();
			//	}else{
			//		plc.pular();
			//	}
			}
		}
	}
}
