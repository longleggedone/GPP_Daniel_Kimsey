using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour 
{

    public Transform target;

    //protected Rigidbody rb;


    AudioSource audioSource;
    public AudioClip spawnAudio;
    public AudioClip deathAudio;

    public int hp = 1;
    public bool Dead
    {
        get; private set;
    } 
        
    public float moveSpeed = 1;
    public float rotateSpeed = 1; 
    public int runTime = 1;
    public int waitTime = 1;
    public float vision = 0.8f;


    protected virtual void Awake ()
    {
        Dead = false;
        audioSource = GetComponent<AudioSource>();
    }

	protected virtual void Start () 
    {
        audioSource.PlayOneShot(spawnAudio);
        target = GameObject.Find("Player").GetComponent<Transform>();
        //rb = GetComponent<Rigidbody>();
    }

	protected virtual void FixedUpdate () 
    {
        MovementBehaviour();

	}


    /// 
    /// MOVEMENT
    /// 
    protected virtual void MovementBehaviour()
    {
        if (target != null)
        {
            MoveForward();

            LookAt(target.position);
        }
    }


    protected virtual void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        //rb.MovePosition(Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime));
    }

    protected virtual void LookAt(Vector3 lookTarget)
    {
        Vector3 lookPoint = new Vector3(lookTarget.x, transform.position.y, lookTarget.z);
        Vector3 direction = lookPoint - transform.position;
        //Quaternion targetRotation = Quaternion.LookRotation(direction);
        Vector3 targetRotation = Vector3.RotateTowards(transform.forward, direction, rotateSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(targetRotation);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            
    }

    protected IEnumerator MoveForwardForSeconds(float seconds)
    {
        if (target != null)
        {
            float timer = 0;
            while (timer < seconds)
            {
                // Debug.Log("Moving forward");
                MoveForward();
                timer += Time.deltaTime;
                yield return null;
            }
            yield return StartCoroutine(RotateTowardForSeconds(waitTime));
        }
    }

    protected IEnumerator RotateTowardForSeconds(float seconds)
    {
        if (target != null)
        {
            float timer = 0;
            while (timer < seconds)
            {
                // Debug.Log("Rotating towards target"); 
                LookAt(target.position);
                timer += Time.deltaTime;
                yield return null;
            }
            yield return StartCoroutine(MoveForwardForSeconds(runTime));
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "projectile")
        {
            Hit();
        }
    }

    protected virtual void Hit()
    {
        hp -= 1;

        if(hp <= 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        Dead = true;
        EventManager.Instance.FireEvent(new EnemyDeathEvent(this));
        //SoundManager.Instance.GenerateSourceAndPlay(deathAudio, 1f, 1f, transform.position);

        //Destroy(gameObject);
    }

}

