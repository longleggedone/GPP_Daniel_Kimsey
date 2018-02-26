using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySheep : Enemy {

    protected override void Start()
    {
        base.Start();
        StartCoroutine(RotateTowardForSeconds(waitTime));

    }

    protected override void FixedUpdate()
    {

    }



    protected override void Hit()
    {
        base.Hit();
    }

    protected override void Death()
    {
        base.Death();
    }

}


