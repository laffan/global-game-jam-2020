using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinCheck : MonoBehaviour
{

  public GameObject player;

  private void OnTriggerEnter(Collider other)
  {

    if (other.gameObject.name == "WinZone")
    {
      player.GetComponent<PlayerManager>().didWin = true;
    }
  }


}
