using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WavesConfig waveConfig;
    List<Transform> wayPoints;
    int wayPointIndex = 0;   
    private void Awake() 
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    } 
    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        wayPoints = waveConfig.GetWayPoint();
        transform.position = wayPoints[wayPointIndex].position;
    }

    void Update()
    {
        FollowPath();
    }
    void FollowPath()
    {
        if(wayPointIndex < wayPoints.Count)
        {
            Vector3 targetPosistion = wayPoints[wayPointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosistion, delta);
            if(transform.position == targetPosistion){
                wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
