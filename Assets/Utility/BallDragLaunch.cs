using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class BallDragLaunch : Singleton<BallDragLaunch> {

	public Canvas uiCanvas;


	private Vector3 dragStartWorldPos;
    private Vector3 dragEndWorldPos;
    private Vector2 currentDrag;
	private LineRenderer lineRenderer;
	private bool isDragging = false;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        HandleLineRendererDraw();
	}

	private void HandleLineRendererDraw(){
        if (isDragging){
			lineRenderer.enabled = true;
            currentDrag  = dragStartWorldPos - GetMousePosWorldCoordinates();
			lineRenderer.SetPosition(0, BallController.Instance.BallStartPos);
			lineRenderer.SetPosition(1, BallController.Instance.BallStartPos + currentDrag);
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
        Vector2 finalVector = (dragStartWorldPos - dragEndWorldPos).normalized;
        GameManager.Instance.DragFinished(finalVector);
	}

	private Vector3 GetMousePosWorldCoordinates(){
		return (Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, Camera.main.transform.position.z));
	}

	public void SetMouseEnabled(bool isEnabled){
		uiCanvas.GetComponent<GraphicRaycaster>().enabled = isEnabled;
	}

}
