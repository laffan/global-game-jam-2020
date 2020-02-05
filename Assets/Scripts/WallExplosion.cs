using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallExplosion : MonoBehaviour
{

  public float radius = 5.0F;
  public float power = 10.0F;

 

  public void Explode()
  {
    int explodedBlocks = 0;
    Debug.Log("Explode!");
    Vector3 explosionPos = transform.position;
    Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
    foreach (Collider hit in colliders)
    {
      if (hit.gameObject.layer == LayerMask.NameToLayer("Buildable"))
      {
        Rigidbody rb = hit.GetComponent<Rigidbody>();

        if (rb != null)
        {
          explodedBlocks++;
          rb.isKinematic = false;
          rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
      }
    }
    Debug.Log(explodedBlocks + " blocks exploded");
  }
}
