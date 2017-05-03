using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDragLaunch : MonoBehaviour {

	private Vector3 dragStartWorldPos;
	private Vector3 dragEndWorldPos;
	private Vector3 currentDrag;
	private Ball ball;
	private LineRenderer lineRenderer;
	private bool isDragging = false;


	// Use this for initialization
	void Start () {
		ball = FindObjectOfType<Ball>();
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetPosition(0, ball.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		if(isDragging){
			currentDrag = dragStartWorldPos - GetMousePosWorldCoordinates();
			lineRenderer.SetPosition(1, ball.transform.position + currentDrag);
		}
	}

	public void DragStart(){
		isDragging = true;
		dragStartWorldPos = GetMousePosWorldCoordinates();
	}

	public void DragEnd(){
		isDragging = false;
		dragEndWorldPos = GetMousePosWorldCoordinates();

		Vector3 launchVector = (dragStartWorldPos - dragEndWorldPos).normalized;
		ball.Launch(new Vector2(launchVector.x, launchVector.y));
	}

	private Vector3 GetMousePosWorldCoordinates(){
		return (Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, Camera.main.transform.position.z));
	}
}
