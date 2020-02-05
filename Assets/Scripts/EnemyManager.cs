using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

  public GameObject spawnContainer;
  public GameObject enemyTarget;
  public Transform[] enemies;
  Transform[] spawnpoints;
  public List<Transform> spawnedEnemies = new List<Transform>();
  public int enemyCap = 20;

  public int enemiesAtTheWall = 0;
  public bool allowEnemies = true;

  void Start()
  {

    if (!allowEnemies) { Debug.Log(">>> ENEMIES TURNED OFF <<< "); }

    else
    {
      spawnpoints = spawnContainer.GetComponentsInChildren<Transform>();
      InvokeRepeating("SpawnEnemy", 0f, 3f);
    }
  }

  void SpawnEnemy()
  {

    if ( spawnedEnemies.Count > enemyCap ) {
      allowEnemies = false;
    }

    if ( allowEnemies ) {
      int randomPoint = Random.Range(0, spawnpoints.Length);
      int randomEnemy = Random.Range(0, enemies.Length);
      if (spawnpoints[randomPoint] != gameObject.transform)
      {
        Transform enemy = Instantiate(enemies[randomEnemy], spawnpoints[randomPoint].position, Quaternion.identity);

        enemy.GetComponent<EnemyMovement>().targetObject = enemyTarget;

        spawnedEnemies.Add( enemy );
      }
    }
  }
}
