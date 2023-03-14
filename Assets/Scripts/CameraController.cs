using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController player;
    public GameObject followTarget;
    private Vector3 targetPosition;
    private Vector3 playerPosition;
    public Vector3 cameraPosition;

    public float moveSpeed;
    public float zoomSpeed;
    public bool zoomEnabled;    
    public float cameraHeight;
    public float cameraRestHeight;
    public float cameraResetTimer;
    public float maxCameraHeight;
    public float minCameraHeight;
    public float cameraLeadRight;
    public float cameraLeadLeft;
    public float lookDistance;

    private float inputX;
    private float inputY;
    public float minCameraX = -50;
    public float maxCameraX = 50;
    public float minCameraY = -50;
    public float maxCameraY = 50;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        cameraHeight = cameraRestHeight;
        
    }

    void FixedUpdate()
    {
        if (!player.gameObject.activeSelf)
        {
            Debug.Log("No player object present!");
            return;
        } else if (player.gameObject.activeSelf){
            playerPosition = FindObjectOfType<PlayerController>().transform.position;
            if (player.lastMove.x > .5f)
                targetPosition = new Vector3((followTarget.transform.position.x + cameraLeadRight) + inputX, 
                                              followTarget.transform.position.y + inputY, cameraHeight);
            else if(player.lastMove.x < 0.5f)
                targetPosition = new Vector3((followTarget.transform.position.x - cameraLeadLeft) + inputX, 
                                              followTarget.transform.position.y + inputY, cameraHeight);
            
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        AdjustZoom();
        
    }

    private void Update()
    {
        cameraPosition = transform.position;
        CameraBounds();
    }

    void AdjustZoom()
    {
        if (!zoomEnabled) return;

        //var inputZoomOut = Input.GetAxis("Left Trigger");
        //var inputResetZoom = Input.GetButtonDown("R-Stick Click");
        //var inputZoomIn = Input.GetAxis("Right Trigger");
        var inputPanY = Input.GetAxisRaw("R-Vertical");
        var inputPanX = Input.GetAxisRaw("R-Horizontal");

        //if (inputZoomOut > 0 && cameraHeight <= maxCameraHeight + 1)cameraHeight = cameraHeight - zoomSpeed;
        //if (inputZoomIn > 0 && cameraHeight >= minCameraHeight - 1) cameraHeight = cameraHeight + zoomSpeed;
        //if (inputResetZoom) ResetZoom();

        if (inputPanY > 0 || inputPanY < 0)inputY = inputPanY * (-lookDistance);
        else inputY = 0;    

        if (inputPanX > 0 || inputPanX < 0)inputX = inputPanX * lookDistance;
        else inputX = 0;

        if (cameraHeight > maxCameraHeight) cameraHeight = maxCameraHeight;
        if (cameraHeight < minCameraHeight) cameraHeight = minCameraHeight;
        //if (player.playerMoving) ResetZoom();
    }

    public void ResetZoom()
    {
        cameraHeight = cameraRestHeight;
    }

    void CameraBounds() { 
        var positionX = transform.position.x;
        var positionY = transform.position.y;
        if (transform.position.x > maxCameraX) transform.position = new Vector3(maxCameraX, transform.position.y, cameraHeight);
        if (transform.position.x < minCameraX) transform.position = new Vector3(minCameraX, transform.position.y, cameraHeight);
        if (transform.position.y > maxCameraY) transform.position = new Vector3(transform.position.x, maxCameraY, cameraHeight);
        if (transform.position.y < minCameraY) transform.position = new Vector3(transform.position.x, minCameraY, cameraHeight);
    }
}