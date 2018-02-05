using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour 
{

    public Transform target;

    protected Rigidbody rb;

    public int moveSpeed;
    public int rotateSpeed;
    public int runTime;
    public int waitTime;
    public float vision = 0.8f;


	protected virtual void Start () 
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

	protected virtual void Update () 
    {
        Movement();

	}



    /// 
    /// MOVEMENT
    /// 


    protected virtual void Movement()
    {
        LookAt(target.position);
        MoveForward();
    }


    protected virtual void MoveForward()
    {
        rb.MovePosition(Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime));
    }

    protected virtual void LookAt(Vector3 lookTarget)
    {
        if (target != null)
        {


            Vector3 lookPoint = new Vector3(lookTarget.x, transform.position.y, lookTarget.z);
            Vector3 relativePos = Vector3.Normalize(lookPoint - transform.position);
            Quaternion targetRotation = Quaternion.LookRotation(relativePos);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);


            //Vector3 myHeightPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z); //corrects so that point is considered to be at the same height as the enemy
            //transform.LookAt(myHeightPoint);

        }
    }

    protected IEnumerator MoveForwardForSeconds(float seconds)
    {
        
        float timer = 0;
        while (timer < seconds)
        {
           // Debug.Log("Moving forward");
            MoveForward();
            timer += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(RotateTowardForSeconds(waitTime));
    }

    protected IEnumerator RotateTowardForSeconds(float seconds)
    {
        float timer = 0;
        while (timer < seconds )
        {
           // Debug.Log("Rotating towards target"); 
            LookAt(target.position);
            timer += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(MoveForwardForSeconds(runTime));
    }



    protected float GetPositionRelativeToRotation()
    {
        Vector3 toTarget = (target.position - transform.position).normalized;
        float dotProduct = (Vector3.Dot(toTarget, transform.forward));

        return dotProduct;
    }


    /// 
    /// DAMAGE AND DEATH
    /// 

    protected virtual void Hit()
    {

    }

    protected virtual void Death()
    {

    }
}

