using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

  public bool isAlive = true;
  public bool didWin = false;
  public bool isTouching = false; // Do not set here.
  public bool isTouchingPickup = false;
  public bool isTouchingBuildable = false;
  public bool isHolding = false;
  public bool isPlacing = false;
  public GameObject heldItem = null;
  public RaycastHit rayHit;

  // Update is called once per frame
  void Update()
  {
    // update heldItem 
    updateInteractionState();
    WhatAmIHolding();
    DidIDie();
    DidIWin();
  }

  void updateInteractionState()
  {
    if (isTouching)
    {
      if (rayHit.collider.tag == "Pickupable")
      {
        isTouchingPickup = true;
      }
      if (rayHit.collider.gameObject.layer == LayerMask.NameToLayer("Buildable"))
      {
        isTouchingBuildable = true;
      }
      if (isHolding && isTouchingBuildable)
      {
        isPlacing = true;
      }

    }
    else
    {
      isPlacing = false;
      isTouchingPickup = false;
      isTouchingBuildable = false;
    }
  }

  void WhatAmIHolding()
  {
    // Keep Track of Pickup
    Transform cameraTransform = Camera.main.transform;

    for (int i = 0; i < cameraTransform.childCount; i++)
    {
      if (cameraTransform.GetChild(i).gameObject.tag == "Pickupable")
      {
        heldItem = cameraTransform.GetChild(i).gameObject;
      }
    }
  }

  void DidIDie()
  {
    if (!isAlive)
    {
      Debug.Log(" I have died. ");
      SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1 );

    }
  }

  void DidIWin()
  {
    if (didWin)
    {
      Debug.Log(" I have won! ");
      SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 2 );

    }
  }


}
