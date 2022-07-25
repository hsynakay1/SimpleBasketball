using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    public float mouseSpeed = 50;
    public float minX, maxX;

    float xrot, yrot;
    
    
    void LateUpdate()
    {
        xrot -= Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSpeed;
        yrot += Input.GetAxis("Mouse X") * Time.deltaTime * mouseSpeed;
        xrot = Mathf.Clamp(xrot, minX, maxX);
        transform.GetChild(0).localRotation = Quaternion.Euler(xrot, 0, 0);
        transform.localRotation = Quaternion.Euler(0,yrot,0);
        transform.position = Vector3.Lerp(transform.position, Target.position, Time.deltaTime);

    }
    
}
