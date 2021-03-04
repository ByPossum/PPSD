using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField, Range(0.000001f, 1f)] float f_flySpeed;
    [SerializeField] private float f_sensitivity;
    [SerializeField] private float f_rotationSpeed;
    [SerializeField] private float f_movementSpeed;
    [SerializeField] private Camera cam;
    private Vector3 t_selectedPoint;
    private Vector3 v_lastPos;
    private float f_yLook;
    float xRot;
    float yRot;
    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 1f))
            {
                t_selectedPoint = hit.point;
            }
        }
        if (Input.GetMouseButton(1))
        {
            xRot += (-(mouseY * f_rotationSpeed * Time.deltaTime));
            yRot += (mouseX * f_rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(new Vector3(xRot, yRot, 0.0f));
        }
        if (Input.GetMouseButton(2))
        {

            transform.Translate(new Vector3(-mouseX * f_movementSpeed * Time.deltaTime, -mouseY * f_movementSpeed * Time.deltaTime, 0.0f));
        }
        if(Input.mouseScrollDelta.y != 0)
        {
            transform.position += ((transform.forward.normalized * f_flySpeed) * Input.mouseScrollDelta.y); 
        }
    }
}
