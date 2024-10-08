using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float camSpeed = 5.0f;
    [SerializeField] float camRotateHorSpeed = 30.0f;
    [SerializeField] float camRotateVerSpeed = 20.0f;
    [SerializeField] float camZoomSpeed = 10.0f;

    [SerializeField] Transform camTransform;

    [SerializeField] float minCamDis = 1.0f;
    [SerializeField] float maxCamDis = 10.0f;
    [SerializeField] float minCamRotate = 45.0f;
    [SerializeField] float maxCamRotate = 90.0f;
    static public float maxCamRadius = 2.0f;

    Vector3 mousePos;

    Vector3 moveCamDir = Vector3.zero;
    float rotateCamHorDir = 0.0f;
    float rotateCamVerDir = 0.0f;
    float zoomCam = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            rotateCamHorDir = Input.GetAxis("Mouse X") * camRotateHorSpeed;
            rotateCamVerDir = -Input.GetAxis("Mouse Y") * camRotateVerSpeed;
        }
        else
        {
            rotateCamHorDir = 0.0f;
            rotateCamVerDir = 0.0f;
        }

        moveCamDir = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveCamDir = camSpeed * moveCamDir.normalized;

        zoomCam = -1 * Input.GetAxis("Mouse ScrollWheel") * camZoomSpeed;
    }

    private void FixedUpdate()
    {
        float newHorRotation = transform.rotation.eulerAngles.y + rotateCamHorDir;
        float newVerRotation = transform.rotation.eulerAngles.x + rotateCamVerDir;
        newVerRotation = Mathf.Clamp(newVerRotation, minCamRotate, maxCamRotate);
        transform.rotation = Quaternion.Euler(newVerRotation, newHorRotation, 0.0f);
        Vector3 newPosition = transform.position + Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f) * moveCamDir * Time.fixedDeltaTime;
        newPosition = Vector3.ClampMagnitude(newPosition, maxCamRadius);
        transform.position = newPosition;
        float newCamDistance = Vector3.Distance(camTransform.position, transform.position) + zoomCam * Time.fixedDeltaTime;
        newCamDistance = Mathf.Clamp(newCamDistance, minCamDis, maxCamDis);
        camTransform.position = transform.position + transform.rotation * Vector3.back * newCamDistance;
    }
}
