using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;

    [SerializeField] int speed = 2;

    Vector3 movePosition;

    bool arrive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
    }

    void FixedUpdate()
    {
        if (!arrive)
        {
            Move(movePosition);
        }

        if (anim)
        {
            Vector3 runspeed = rb.velocity;
            runspeed.y = 0;
            anim.SetFloat("Run", runspeed.magnitude);
        }
    }


    public void MovePoint(Vector3 point)
    {
        if (arrive)
        {
            movePosition = point;
            arrive = false;
        }
    }
    /// <summary>
    /// 目的地に移動する
    /// </summary>
    /// <param name="movePosition">目的地</param>
    void Move(Vector3 movePosition)
    {
        Vector3 charaPosition = this.transform.position;
        charaPosition.y = 0f;

        Vector3 heading = movePosition - charaPosition;
        //距離
        float dist = heading.magnitude;
        //向き
        Vector3 direction = heading / dist; 
        this.transform.forward = direction;
        //Debug.Log(dist);
        if (dist > 0.1 || dist < -0.1)
        {
            //Debug.Log(this.transform.position);
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
            arrive = true;
            Debug.Log("着いた ");
        }
    }
}
