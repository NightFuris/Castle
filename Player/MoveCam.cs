using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    Vector3 touch = Vector3.zero;
    public bool isAct = true;

    [SerializeField] private float zoomMin = 3, zoomMax = 8;
    public Camera cameraPlayer = null;
    private Transform playerPos;
    private void Start()
    {
        playerPos = this.transform;
    }
    private void Update()
    {
        if (isAct)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touch = cameraPlayer.ScreenToWorldPoint(Input.mousePosition);
            }

            if(Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroLastPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOneLastPos = touchOne.position - touchOne.deltaPosition;

                float distTouch = (touchZeroLastPos - touchOneLastPos).magnitude;
                float currentDistTouch = (touchZero.position - touchOne.position).magnitude;

                float difference = currentDistTouch - distTouch;

                Zoom(difference * 0.01f);
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 direction = touch - cameraPlayer.ScreenToWorldPoint(Input.mousePosition);
                playerPos.position += direction;
            }
            //WINDOWS
            Zoom(Input.GetAxis("Mouse ScrollWheel"));
        }
    }
    private void Zoom(float increment)
    {
        cameraPlayer.orthographicSize = Mathf.Clamp(cameraPlayer.orthographicSize - increment, zoomMin, zoomMax);
    }
}
