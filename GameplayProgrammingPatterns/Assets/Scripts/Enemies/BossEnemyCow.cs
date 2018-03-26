using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase
{
    ScalePhase,
    SpawnPhase,
    ShootPhase,
    MovePhase
}

public class BossEnemyCow : Enemy {

    public Projectile projectile;
    public GameObject spawnedEnemyType;
    private readonly TaskManager _tm = new TaskManager();
    private int halfHP;
    private int lowHP;
    public Phase currentPhase;

    Vector3 startingScale;
    Vector3 normalScale;

    public float fireInterval = 1;
    float shotTimer;
  

	protected override void Awake()
	{
        base.Awake();
        currentPhase = Phase.ScalePhase;
        normalScale = Vector3.one;
        startingScale = normalScale / 100;
        moveSpeed = 1f;
        hp = 100;
        halfHP = hp/2;
        lowHP = hp/5;
        shotTimer = 0;

	}

	protected override void Start()
    {
        base.Start();
        Vector3 startPos = new Vector3(0, 2, 0);
        _tm.Do(new SetPos(this.gameObject, startPos))
           .Then(new Scale(this.gameObject, startingScale, normalScale, 1f))
           .Then(new ChangePhase(this, Phase.SpawnPhase));

    }

    protected override void FixedUpdate()
    {

        //base.FixedUpdate();
    }

	private void Update()
	{
        _tm.Update();
        PhaseAction();
        if(hp <= halfHP && hp > lowHP && currentPhase != Phase.ShootPhase)
        {
            currentPhase = Phase.ShootPhase;
        }
        if (hp <= lowHP && currentPhase != Phase.MovePhase)
        {
            currentPhase = Phase.MovePhase;
        }
	}


    void PhaseAction()
    {
        switch(currentPhase)
        {
            case Phase.ScalePhase:
                break;
            case Phase.SpawnPhase:
                break;
            case Phase.ShootPhase:
                HandleFiring();
                break;
            case Phase.MovePhase:
                base.MovementBehaviour();
                break;
                
        }
    }

    //void SpawnEnemy()
    //{
    //    Vector3 spawnPos = new Vector3(this.transform.position.x + UnityEngine.Random.Range(-1, 1),
    //                                   this.transform.position.y, 
    //                                   this.transform.position.z + UnityEngine.Random.Range(-1, 1));
    //    GameObject enemy = Instantiate(spawnedEnemyType, spawnPos, Quaternion.identity);
    //    spawns.Add(enemy.GetComponent<Enemy>());

    //}

    void HandleFiring()
    {
        shotTimer += Time.deltaTime;
        if (shotTimer >= fireInterval)
        {
            shotTimer = 0;
            Shoot();
        }
    }





    protected override void Shoot()
    {
        Debug.Log("Shooting!");
        Quaternion randomRotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
        Instantiate(projectile, this.gameObject.transform.position, randomRotation);
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
