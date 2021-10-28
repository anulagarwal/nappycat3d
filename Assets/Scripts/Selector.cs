using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [Header("Attributes")]
    bool isMouseDown;



    [SerializeField] Vector3 rotateAxis;
    [SerializeField] Vector3 minRotClamp;
    [SerializeField] Vector3 maxRotClamp;


    private float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private Vector3 baseRot;



    // Start is called before the first frame update
    void Start()
    {
        _sensitivity = 0.2f;
        _rotation = Vector3.zero;
        baseRot = transform.parent.localEulerAngles;
        
    }

    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
       // angle = angle - 180f * Mathf.Floor((angle + 180f) / 180f);
       // if (angle < 0f) angle = 360 + angle;

       // if (angle > 360f) angle = 358f;
        // if (angle > 180f)
        //   return Mathf.Max(angle, 360 + from);

        // return Mathf.Max(Mathf.Min(angle, to), from);
        return Mathf.Clamp(angle, from, to);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Euler(0, -0, 0);
        if (Input.GetMouseButtonDown(0))
        {

        }

        if(Input.GetMouseButton(0))
        {
            if (isMouseDown)
            {
                // offset
                _mouseOffset = (Input.mousePosition - _mouseReference);

                // apply rotation
                _rotation.y = -(_mouseOffset.x ) * _sensitivity;
                _rotation.x = -(_mouseOffset.y) * _sensitivity;
                _rotation.z = -(_mouseOffset.y) * _sensitivity;

                Vector3 rotVec = Vector3.zero;
                // rotate
                 transform.parent.Rotate(new Vector3(_rotation.x * rotateAxis.x, _rotation.y * rotateAxis.y, _rotation.z * rotateAxis.z));
                // store mouse
             

                //if((_rotation.z * rotateAxis.z) + transform.parent.localRotation.
                print(transform.parent.localEulerAngles);

                // transform.parent.localRotation = Quaternion.Euler(ClampAngle(transform.parent.localEulerAngles.x, minRotClamp.x, maxRotClamp.x), ClampAngle(transform.parent.localEulerAngles.y, minRotClamp.y, maxRotClamp.y), Mathf.Clamp(transform.parent.localEulerAngles.z, minRotClamp.z, maxRotClamp.z));
                transform.parent.localRotation = Quaternion.Euler(Mathf.Clamp(transform.parent.localEulerAngles.x, baseRot.x+ minRotClamp.x,baseRot.x+ maxRotClamp.x), ClampAngle(transform.parent.localEulerAngles.y, baseRot.y+ minRotClamp.y,baseRot.y+ maxRotClamp.y), ClampAngle(transform.parent.localEulerAngles.z,baseRot.z+ minRotClamp.z,baseRot.z+ maxRotClamp.z));


                //transform.parent.localEulerAngles = new Vector3(Mathf.Clamp(transform.parent.localEulerAngles.x, minRotClamp.x, maxRotClamp.x), ClampAngle(transform.parent.localEulerAngles.y, minRotClamp.y, maxRotClamp.y), ClampAngle(transform.parent.localEulerAngles.z, minRotClamp.z, maxRotClamp.z));
                _mouseReference = Input.mousePosition;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {

        }
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        _mouseReference = Input.mousePosition;
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        isMouseDown = false;

    }
}
