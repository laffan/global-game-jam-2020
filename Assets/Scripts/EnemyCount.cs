using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
  public GameObject enemyManager;
  private List<Collider> colliders = new List<Collider>();
  public List<Collider> GetColliders() { return colliders; }

  void Update()
  {
    enemyManager.GetComponent<EnemyManager>().enemiesAtTheWall = colliders.Count;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (!colliders.Contains(other))
    {
      if (other.tag == "Enemy")
      {
        colliders.Add(other);
      }
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.tag == "Enemy")
    {
      colliders.Remove(other);
    }
  }
}
