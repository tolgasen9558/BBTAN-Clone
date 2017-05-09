using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDragLaunch : MonoBehaviour {

	private Vector3 dragStartWorldPos;
	private Vector3 dragEndWorldPos;
	private Vector3 currentDrag;
	private Vector3 launchVector;
	private Ball ball;
	private LineRenderer lineRenderer;
	private bool isDragging = false;
	private bool launchBalls = false;
	private int numberOfBallsToLaunch = 3;
	private int numberOfBallsLaunched = 0;


	// Use this for initialization
	void Start () {
		ball = FindObjectOfType<Ball>();
		lineRenderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		HandleLineRendererDraw();
		if(numberOfBallsLaunched < numberOfBallsToLaunch && launchBalls){
			ball.Launch(launchVector);
			numberOfBallsLaunched++;
		}
	}

	private void HandleLineRendererDraw(){
		if(isDragging){
			lineRenderer.enabled = true;
			currentDrag = dragStartWorldPos - GetMousePosWorldCoordinates();
			lineRenderer.SetPosition(0, ball.transform.position);
			lineRenderer.SetPosition(1, ball.transform.position + currentDrag);
		}
		else{
			lineRenderer.enabled = false;
		}
	}

	public void DragStart(){
		isDragging = true;
		dragStartWorldPos = GetMousePosWorldCoordinates();
	}

	public void DragEnd(){
		isDragging = false;
		dragEndWorldPos = GetMousePosWorldCoordinates();

		launchVector = (dragStartWorldPos - dragEndWorldPos).normalized;
		launchBalls = true;
	}

	private Vector3 GetMousePosWorldCoordinates(){
		return (Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, Camera.main.transform.position.z));
	}
}
