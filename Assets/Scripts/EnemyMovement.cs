using UnityEngine;
using UnityEngine.AI;

// Thank you Brackeys
// ENEMY AI - Making an RPG in Unity (E10)
// https://www.youtube.com/watch?v=xppompv1DBg

public class EnemyMovement : MonoBehaviour
{
  public GameObject targetObject = null;
  NavMeshAgent agent;

  void Start()
  {
    agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
  }

  void Update()
  {

    if (targetObject != null)
    {

      float distance = Vector3.Distance(targetObject.transform.position, transform.position);
      agent.SetDestination(targetObject.transform.position);
      if (distance <= agent.stoppingDistance)
      {
        FaceTarget();
      }
    }

  }

  void FaceTarget()
  {
    Vector3 direction = (targetObject.transform.position - transform.position).normalized;
    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
  }
}
