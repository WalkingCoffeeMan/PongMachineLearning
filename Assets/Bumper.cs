using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public bool isBump1;
    public float speed;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();        
    }

    // Update is called once per frame
    void Update()
    {

       // rb.AddForce(0f, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0f);
        transform.Translate(0f, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0f);

        /*
        if(isBump1)
        {
            transform.Translate(0f, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0f);
           
        }
        else
        {
            transform.Translate(0f, Input.GetAxis("Vertical2") * speed * Time.deltaTime, 0f);
        }
        */
    }

    private void FixedUpdate()
    {
        //  rb.AddForce(Input.GetAxis("Vertical")*Vector3.up*speed );
        //rb.MovePosition(Input.GetAxis("Vertical") * Vector3.up *speed*Time.deltaTime + transform.position );
    }
}
