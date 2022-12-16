using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Ball : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 startPos;
    public Rigidbody rb;

    public Text currentRoundText;
    public Text brainyRoundsText;
    public Text ruleyRoundsText;
    public Text sumScoreTextBrainy;
    public Text sumScoreTextRuley;
    private int currentRuleyScore = 0;
    private int currentBrainyScore = 0;
    private int roundScoreRuley = 0;
    private int roundScoreBrainy = 0;
    private int sumScoreBrainy = 0;
    private int sumScoreRuley = 0;

    public Action eastEnter;
    public Action westEnter;


    private float xAxis;
    private float yAxis;
    private float collisionTimer = 0;
    private float yFreezeTimer = 0;

  

    private float oldYPosition;

    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = this.transform.position;
        yFreezeTimer = 0;
        count = 0;
        collisionTimer = 0;
        randomPos();

        
        
    }

    // Update is called once per frame
    void Update()
    {
        collisionTimer += Time.deltaTime;
        yFreezeTimer += Time.deltaTime;

        Debug.Log("Y-Achse: " + this.transform.position.y + " , old: " + oldYPosition);

        if (this.transform.position.y != oldYPosition)
        {
            yFreezeTimer = 0;
        }


        Debug.Log("Timer: " + collisionTimer);
        
        if(collisionTimer > 6 )
        {
            randomPos();
            collisionTimer = 0;
        }

        if(yFreezeTimer > 6)
        {
            randomPos();
            yFreezeTimer = 0;
        }

        
    }

    private void LateUpdate()
    {
        oldYPosition = this.transform.position.y;
    }
    private void OnTriggerEnter(Collider other)
    {
       

        if (other.tag.Equals("wallWest"))
        {
            currentRuleyScore++;
            sumScoreRuley++;
            westEnter();
        }
        else if (other.tag.Equals("wallEast"))
        {
            currentBrainyScore++;
            sumScoreBrainy++;
            eastEnter();
           
        }


        if (currentRuleyScore == 3   || currentBrainyScore == 3)
        {
            if(currentRuleyScore == 3)
            {
                roundScoreRuley++;
           
            }
            else
            {
                roundScoreBrainy++;
            }

            currentRuleyScore = 0;
            currentBrainyScore = 0;
            brainyRoundsText.text = "" + roundScoreBrainy;
            ruleyRoundsText.text = "" + roundScoreRuley;
        }


        
        currentRoundText.text = "Brainy " + currentBrainyScore + ": " +   currentRuleyScore + " Ruley ";
        sumScoreTextBrainy.text = "" + sumScoreBrainy;
        sumScoreTextRuley.text = "" + sumScoreRuley;
        randomPos();


    }


    public void randomPos()
    {
        speed = 5;
        count = 0;

        this.transform.position = startPos;
         float sx = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
         float sy = UnityEngine.Random.Range(-1.0f, 1.0f);
        rb.velocity = new Vector3( sx, sy, 0f).normalized*speed;
    }

    private void OnCollisionEnter(Collision collision)
    {

      
        xAxis = this.transform.position.x;
        yAxis = this.transform.position.y;
        if (collision.transform.tag.Equals("bump"))
         {
            collisionTimer = 0;
            speed = 15;

            if(count == 0)
            {
                rb.velocity = (this.transform.position).normalized * speed;
                count = 1;
            }
            
        }
    }



}
