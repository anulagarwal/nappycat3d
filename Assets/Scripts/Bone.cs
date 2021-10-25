using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    public bool isTouchingHuman;
    public GameObject cross;
    // Start is called before the first frame update
    void Start()
    {
        CatBoneManager.Instance.AddBone(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cross != null)
        {
        cross.transform.rotation = Quaternion.Euler(Vector3.zero);
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Thigh")
        {
            isTouchingHuman = true;       
            CatBoneManager.Instance.AddTouchingBone(this);
            cross =Instantiate(GameManager.Instance.cross, collision.contacts[0].point, Quaternion.identity);
            cross.transform.SetParent(transform);
        }

    
    }
    private void OnCollisionStay(Collision collision)
    {
       
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Thigh")
        {
            isTouchingHuman = false;
            Destroy(cross);

            Invoke("CheckTouch", 0.6f);
        }
    }

    void CheckTouch() { 
        if (!isTouchingHuman)
        {
            CatBoneManager.Instance.RemoveTouchingBone(this);
        }
    }
}
