using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class EnemySheep2 : Enemy
{
    private Tree<EnemySheep2> _tree;
    private TaskManager charging;
    private bool hitByPlayer = false;
    private bool chargingUp = false;
    private float attackDistance = 2.0f;
    private int pulses = 0;
    private int requiredPulses = 5;
    private float timeOfHit = 0;
    private float fleeTime = 3.0f;

    protected override void Start()
    {
        base.Start();

        charging = new TaskManager();

        Charging();
        //StartCoroutine(RotateTowardForSeconds(waitTime));
        _tree = new Tree<EnemySheep2>(
            new Selector<EnemySheep2>(
                new Sequence<EnemySheep2>(
                    new WasHit(),
                    new Flee()
                ),
                new Sequence<EnemySheep2>(
                    new Charged(),
                    new Attack()
                ),
                new Sequence<EnemySheep2>(
                    new ChargingStarted(),
                    new ChargeUp()
                ),
                new Sequence<EnemySheep2>(
                    new InRangeOfPlayer(),
                    new StartCharge()
                ),
                new Chase()
            )
        );
    }

	private void Update()
	{
        _tree.Update(this);
	}

	protected override void FixedUpdate()
    {
        
    }

    protected override void Hit()
    {
        hitByPlayer = true;
        pulses = 0;
        timeOfHit = Time.time;
        base.Hit();
    }

    protected override void Death()
    {
        base.Death();
    }

    private void IncreasePulseCount()
    {
        pulses++;
    }

    void Charging()
    {
        var startScale = Vector3.one;
        var endScale = startScale * 1.2f;

        charging.Do(new Scale(gameObject, startScale, endScale, 0.25f))
                .Then(new Scale(gameObject, endScale, startScale, 0.25f))
                .Then(new ActionTask(IncreasePulseCount))
                .Then(new ActionTask(Charging));
        
    }
   
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // NODES
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    ////////////////////
    // Conditions
    ////////////////////
    private class WasHit : Node<EnemySheep2>
    {
        public override bool Update(EnemySheep2 enemy)
        {
            return enemy.hitByPlayer;
        }
    }

    private class InRangeOfPlayer : Node<EnemySheep2>
    {
        public override bool Update(EnemySheep2 enemy)
        {
            var playerPos = enemy.target.transform.position;
            var enemyPos = enemy.transform.position;
            return Vector3.Distance(playerPos, enemyPos) < enemy.attackDistance;
        }
    }

    private class Charged : Node<EnemySheep2>
    {
        public override bool Update(EnemySheep2 enemy)
        {
            return enemy.pulses > enemy.requiredPulses;
        }
    }

    private class ChargingStarted : Node<EnemySheep2>
    {
        public override bool Update(EnemySheep2 enemy)
        {
            return enemy.chargingUp;
        }
    }

    private class StartCharge : Node<EnemySheep2>
    {
        public override bool Update(EnemySheep2 enemy)
        {
            enemy.chargingUp = true;
            return true;
        }
    }

    ///////////////////
    /// Actions
    ///////////////////
    private class Flee : Node<EnemySheep2>
    {
        public override bool Update(EnemySheep2 enemy)
        {
            enemy.pulses = 0;
            enemy.chargingUp = false;
            enemy.LookAwayFrom(enemy.target.position);
            enemy.MoveForward(enemy.moveSpeed);

            if(Time.time - enemy.timeOfHit > enemy.fleeTime)
            {
                enemy.hitByPlayer = false;
            }

            //enemy.SetColor(Color.yellow);
            //enemy.MoveAwayFromPlayer();
            return true;
        }
    }

    private class Attack : Node<EnemySheep2>
    {
        public override bool Update(EnemySheep2 enemy)
        {
            enemy.LookAt(enemy.target.position);
            enemy.MoveForward(enemy.moveSpeed * 1.5f);
            //enemy.SetColor(Color.red);
            //enemy.MoveTowardsPlayer();
            return true;
        }
    }

    private class Chase : Node<EnemySheep2>
    {
        public override bool Update(EnemySheep2 enemy)
        {
            enemy.LookAt(enemy.target.position);
            enemy.MoveForward(enemy.moveSpeed);

            enemy.pulses = 0;
            //enemy.SetColor(Color.red);
            //enemy.MoveTowardsPlayer();
            return true;
        }
    }

    private class ChargeUp : Node<EnemySheep2>
    {
        public override bool Update(EnemySheep2 enemy)
        {
            enemy.charging.Update();
            //enemy.SetColor(Color.blue);
            return true;
        }
    }
}



