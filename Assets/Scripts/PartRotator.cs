using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartRotator : MonoBehaviour
{
    Vector3 origPos;

    public float rotSpeed;
    public bool isDown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (isDown)
            {
                isDown = false;
            }
        }

        if (isDown)
        {
            transform.Rotate(Vector3.forward * (origPos.y - Input.mousePosition.y) * Time.deltaTime * rotSpeed);
            origPos = Input.mousePosition;
        }
    }
    private void OnMouseDown()
    {
        origPos = Input.mousePosition;
        print("down");
        isDown = true;
    }

   
}
