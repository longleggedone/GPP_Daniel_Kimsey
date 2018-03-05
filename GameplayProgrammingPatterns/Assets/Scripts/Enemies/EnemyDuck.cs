using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDuck : Enemy {

    public float speedIncrease = 1;
   

    protected override void Start()
    {
        base.Start();
        EventManager.Instance.Register<EnemyDeathEvent>(IncreaseSpeed);


    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

 

    protected override void Hit()
    {
        base.Hit();
    }

    protected override void Death()
    {
        base.Death();
    }

    void IncreaseSpeed(Event e)
    {
        moveSpeed += speedIncrease;
    }

}
