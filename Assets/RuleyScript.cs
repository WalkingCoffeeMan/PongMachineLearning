using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleyScript : MonoBehaviour
{
    public Transform ball;
    public float speed;
    private Vector3 posBefore;


    // Update is called once per frame
    void Update()
    {
        
         Vector3 pos= Vector3.Lerp(this.transform.position, ball.position, Time.deltaTime * speed);
        float rootPos = Mathf.Lerp(this.transform.position.y, 0 , Time.deltaTime * speed);
        //Debug.Log(Time.deltaTime + " deltadelta");
        //wenn der Ball sich zum Ruley bewegt
        if (posBefore.x < ball.transform.position.x)
        {
            this.transform.position = new Vector3(this.transform.position.x, pos.y, this.transform.position.z);
        }
        else if(posBefore.x > ball.transform.position.x)
        {
            this.transform.position = new Vector3(this.transform.position.x, rootPos, this.transform.position.z);
        }
    }

    private void LateUpdate()
    {
        posBefore = ball.transform.position;
    }
}
