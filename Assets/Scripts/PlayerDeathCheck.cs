using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathCheck : MonoBehaviour
{

  public GameObject player;

  void OnCollisionEnter(Collision collision)
  {
    if (collision.collider.tag == "Enemy")
    {
      player.GetComponent<PlayerManager>().isAlive = false;
     }
  }

      private void OnTriggerEnter(Collider other)
    {
     
     if ( other.tag == "Enemy") {
      player.GetComponent<PlayerManager>().isAlive = false;
     }
    }


}
