using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    public bool isTouchingHuman;
    // Start is called before the first frame update
    void Start()
    {
        CatBoneManager.Instance.AddBone(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Human")
        {
            isTouchingHuman = true;       
            CatBoneManager.Instance.AddTouchingBone(this);
        }

        if (collision.gameObject.tag == "Ground")
        {

        }
    }
    private void OnCollisionStay(Collision collision)
    {
       
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Human")
        {
            isTouchingHuman = false;
         
            CatBoneManager.Instance.RemoveTouchingBone(this);

           

        }
    }
}
