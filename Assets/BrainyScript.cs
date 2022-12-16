using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class BrainyScript : Agent
{
    private Rigidbody rb;
    public Transform ballPos;
    public GameObject ballGo;

    public Rigidbody ballBody;
    private Ball ball;
    public float difference;
   
 

    public float speed;

    private float distanceToBall;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distanceToBall = Vector3.Distance(this.transform.position, ballPos.position);
        ball = (Ball)ballGo.GetComponent(typeof(Ball));
      

        ball.eastEnter += rewardAI;
        ball.westEnter += punishAI;

    }

    // Update is called once per frame

    public override void OnEpisodeBegin()
    {
       
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(ballPos.position);
        sensor.AddObservation(ballBody.velocity);
        sensor.AddObservation(distanceToBall);


        sensor.AddObservation(rb.velocity.y);
        sensor.AddObservation(transform.position.y-ball.transform.position.y);


    }
    public override void OnActionReceived(float[] vectorAction)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.y = Mathf.Clamp(vectorAction[0],-1,1) *20;
        distanceToBall = Vector3.Distance(this.transform.position, ballPos.position);
        Debug.Log(controlSignal.y);
       // rb.AddForce(controlSignal * speed);
        rb.velocity = controlSignal * speed;

         difference = Mathf.Abs(ball.transform.position.y - transform.position.y);

        difference = Mathf.Clamp(difference, 0, 11);



        difference = 3.5f - difference;

        if(difference > 0)
        {
            difference *= 0.3f;
        } 
        else
        {
            difference *= 0.3f;
        }
       
        AddReward(difference);
        
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("ball"))
        {
            AddReward(2.0f);
            EndEpisode();
        }
    }
    public void rewardAI()
    {
        AddReward(1.0f);
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        this.transform.localPosition = new Vector3(-9f, 0, 0);
        EndEpisode();
    }
    public void punishAI()
    {
        AddReward(-1.0f);
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        this.transform.localPosition = new Vector3(-9f, 0, 0);
        EndEpisode();
    }

}
