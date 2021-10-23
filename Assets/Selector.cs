using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [Header("Attributes")]
    bool isMouseDown;

    float oldX;
    float oldY;

    bool xAxis;
    bool yAxis;


    private float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;

    // Start is called before the first frame update
    void Start()
    {
        _sensitivity = 0.2f;
        _rotation = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, -0, 0);
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
                _rotation.z = -(_mouseOffset.y) * _sensitivity;

                // rotate
                transform.parent.Rotate(_rotation);

                // store mouse
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
