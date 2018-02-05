using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDuck : Enemy {

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Movement()
    {
        StartCoroutine(RotateTowardForSeconds(waitTime));

        //StartCoroutine(MoveForwardForSeconds(runTime));
    }

   

    protected override void Hit()
    {

    }

    protected override void Death()
    {

    }

}
