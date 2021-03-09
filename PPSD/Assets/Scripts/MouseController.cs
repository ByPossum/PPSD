using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : SubjectBase, IInitable
{
    [SerializeField, Range(0.000001f, 1f)] float f_flySpeed;
    [SerializeField] private float f_sensitivity;
    [SerializeField] private float f_rotationSpeed;
    [SerializeField] private float f_movementSpeed;
    [SerializeField] private Camera cam;
    private Vector3 t_selectedPoint;
    private Vector3 t_localPoint;
    private Vector3 v_lastPos;
    private float f_yLook;
    float xRot;
    float yRot;
    public Vector3 LocalPoint { get { return t_localPoint; } }

    public void Init()
    {
        Debug.Log("I'm initted");
        AddObserver(FindObjectOfType<MeshDeformer>());
    }
    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit = new RaycastHit();
            Debug.DrawRay(cam.ScreenPointToRay(Input.mousePosition).origin, cam.ScreenPointToRay(Input.mousePosition).direction, Color.red, 0.5f);
            if(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 10f))
            {
                t_selectedPoint = hit.point;
                t_localPoint = hit.transform.InverseTransformPoint(hit.point);
                Notify(new ClickEvent(t_localPoint, t_selectedPoint));
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
