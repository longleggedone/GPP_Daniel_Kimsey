using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public int waveNumber = 0;

    private BossEnemyCow bossInstance;
    private GameObject boss;
    public GameObject bossCow;
    public GameObject chicken;
    public GameObject duck;
    public GameObject sheep;
    public int chickenCount;
    public int duckCount;
    public int sheepCount;


    private readonly List<Enemy> enemiesInCurrentWave = new List<Enemy>();
    private List<GameObject> openSpawnPoints = new List<GameObject>();
    private List<GameObject> usedSpawnPoints = new List<GameObject>();

    //private List<Enemy> enemiesWaitingForDestruction = new List<Enemy>();

    void Awake()
    {
        ResetSpawnPoints();
        EventManager.Instance.Register<EnemyDeathEvent>(DestroyDeadEnemies);
    }

    void Start()
    {
        SpawnBoss(bossCow);
        SpawnWave();
    }
    void Update()
    {
        
        //DestroyDeadEnemies();

        if(enemiesInCurrentWave.Count == 0)
        {
            if (boss != null)
            {
                if (bossInstance.currentPhase == Phase.SpawnPhase || bossInstance.currentPhase == Phase.MovePhase)
                {
                    SpawnWave();
                }
            }
            if (boss == null)
            {
                ResetSpawnPoints();
                waveNumber++;
                SpawnWave();
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister<EnemyDeathEvent>(DestroyDeadEnemies);
    }

    void ResetSpawnPoints()
    {
        openSpawnPoints.Clear();
        usedSpawnPoints.Clear();
        openSpawnPoints.AddRange(GameObject.FindGameObjectsWithTag("spawn"));
        //Debug.Log(openSpawnPoints.Count);
    }

    GameObject GetSpawnPoint()
    {
        GameObject point = openSpawnPoints[Random.Range(0, openSpawnPoints.Count)];
        usedSpawnPoints.Add(point);
        openSpawnPoints.Remove(point);
        return point;
    }

    void SpawnBoss(GameObject _boss)
    {
        boss = Instantiate(_boss, Vector3.zero, Quaternion.identity);
        bossInstance = boss.GetComponent<BossEnemyCow>();
    }

    void SpawnEnemy(GameObject enemyType)
    {
        Vector3 spawn;
        if (boss != null)
        {
            spawn = new Vector3(boss.transform.position.x + Random.Range(-1, 1),
                                boss.transform.position.y,
                                boss.transform.position.z + Random.Range(-1, 1));
        }
        else
        {
            spawn = GetSpawnPoint().transform.position;
        }
        GameObject enemy = Instantiate(enemyType, spawn, Quaternion.identity);
        enemiesInCurrentWave.Add(enemy.GetComponent<Enemy>());
    }


    void SpawnWave()
    {


        if ((bossInstance == null && openSpawnPoints.Count != 0) || 
            (bossInstance != null && (bossInstance.currentPhase == Phase.SpawnPhase || bossInstance.currentPhase == Phase.MovePhase )))
            {

                for (int i = 0; i < 3; i++)
                {
                    SpawnEnemy(chicken);
                }
                for (int i = 0; i < 2; i++)
                {
                    SpawnEnemy(duck);
                }
                for (int i = 0; i < 1; i++)
                {
                    SpawnEnemy(sheep);
                }
            }
       
    }

    void DestroyDeadEnemies(Event e)
    {
        for (int i = enemiesInCurrentWave.Count - 1; i >= 0; i--)
        {
            if (enemiesInCurrentWave[i].Dead)
            {
                Destroy(enemiesInCurrentWave[i].gameObject);
                enemiesInCurrentWave.Remove(enemiesInCurrentWave[i]);
               // Debug.Log(enemiesInCurrentWave.Count);
            }
        }
    }
}
