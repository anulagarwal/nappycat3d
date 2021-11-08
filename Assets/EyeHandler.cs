using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeHandler : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] Transform targetPos;
    [SerializeField] Vector3 origPos;
    [SerializeField] Vector3 origLocalPos;

    [SerializeField] float radius;
    // Start is called before the first frame update
    void Start()
    {
        origPos = transform.position;
        origLocalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 x = targetPos.position - origPos;
        x.Normalize();
        Vector3 newVec = origLocalPos + (radius * x);
        transform.localPosition = new Vector3(newVec.x, origLocalPos.y, newVec.z);
    }
}
