using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [Header("Attributes")]

    [SerializeField] SelectorType type;

    [SerializeField] Vector3 rotateAxis;
    [SerializeField] Vector3 minRotClamp;
    [SerializeField] Vector3 maxRotClamp;

   [SerializeField] private float _sensitivity = 0.2f;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    [SerializeField] bool isYOnYAxis;

    bool isMouseDown;

    private Vector3 baseRot;
   [SerializeField] private Rigidbody rbTarg;



    // Start is called before the first frame update
    void Start()
    {
       
        _rotation = Vector3.zero;
        baseRot = transform.parent.localEulerAngles;
        
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < 90 || angle > 270)
        {       // if angle in the critic region...
            if (angle > 180) angle -= 360;  // convert all angles to -180..+180
            if (max > 180) max -= 360;
            if (min > 180) min -= 360;
        }
        angle = Mathf.Clamp(angle, min, max);
        if (angle < 0) angle += 360;  // if angle negative, convert to 0..360
        return angle;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Euler(0, -0, 0);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1 << LayerMask.NameToLayer("Selector")))
            {
                if (hit.collider == GetComponent<BoxCollider>())
                {
                    GetComponent<SpriteRenderer>().color = Color.green;
                    _mouseReference = Input.mousePosition;
                    isMouseDown = true;
                    if (type == SelectorType.Mover)
                    {
                        rbTarg = transform.parent.GetComponent<Rigidbody>();
                        rbTarg.isKinematic = true;
                    }
                }
            }
        }

        if(Input.GetMouseButton(0))
        {
            if (isMouseDown)
            {
                _mouseOffset = (Input.mousePosition - _mouseReference);

                if (type == SelectorType.Rotator)
                {
                    // offset

                    // apply rotation
                    if (!isYOnYAxis)
                    {
                        _rotation.y = -(_mouseOffset.x) * _sensitivity;
                    }
                    else if (isYOnYAxis)
                    {
                        _rotation.y = -(_mouseOffset.y) * _sensitivity;

                    }
                    _rotation.x = -(_mouseOffset.y) * _sensitivity;
                    _rotation.z = -(_mouseOffset.y) * _sensitivity;
                    transform.parent.Rotate(new Vector3(_rotation.x * rotateAxis.x, _rotation.y * rotateAxis.y, _rotation.z * rotateAxis.z), Space.World);
                    transform.parent.localRotation = Quaternion.Euler(ClampAngle(transform.parent.localEulerAngles.x, baseRot.x + minRotClamp.x, baseRot.x + maxRotClamp.x), ClampAngle(transform.parent.localEulerAngles.y, baseRot.y + minRotClamp.y, baseRot.y + maxRotClamp.y), ClampAngle(transform.parent.localEulerAngles.z, baseRot.z + minRotClamp.z, baseRot.z + maxRotClamp.z));

                }

                if(type == SelectorType.Mover)
                {
                    transform.parent.Translate(new Vector3( -_mouseOffset.x * rotateAxis.x,  _mouseOffset.y * rotateAxis.y, -_mouseOffset.x * rotateAxis.z) * Time.deltaTime * _sensitivity, Space.World);
                    
                }
                _mouseReference = Input.mousePosition;

            }
        }

        if(Input.GetMouseButtonUp(0))
        {

        }
    }

    private void OnMouseDown()
    {
        /*GetComponent<SpriteRenderer>().color = Color.green;
        _mouseReference = Input.mousePosition;
        isMouseDown = true;
        if(type== SelectorType.Mover)
        {
            rbTarg = transform.parent.GetComponent<Rigidbody>();
            rbTarg.isKinematic = true;
        }*/
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        isMouseDown = false;
        if (rbTarg != null)
        {
            rbTarg.isKinematic = false;
            rbTarg = null;
        }

    }
}
