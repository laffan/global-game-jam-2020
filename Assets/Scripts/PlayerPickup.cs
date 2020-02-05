using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{

  public GameObject player;
  public int holdingHeight;
  public Collider currentHit;
  public Collider previousHit = null;

  bool isHolding;
  bool isPlacing;
  bool isTouching;
  bool isTouchingPickup;

  RaycastHit rayHit;

  // Update is called once per frame
  void Update()
  {
    rayHit = player.GetComponent<PlayerManager>().rayHit;
    isHolding = player.GetComponent<PlayerManager>().isHolding;
    isPlacing = player.GetComponent<PlayerManager>().isPlacing;
    isTouching = player.GetComponent<PlayerManager>().isTouching;
    isTouchingPickup = player.GetComponent<PlayerManager>().isTouchingPickup;

    // On Left Click
    if (Input.GetMouseButtonDown(0))
    {
      if (isHolding && !isPlacing) { dropItem(); }
      else if (!isHolding && isTouchingPickup) { pickupItem(); }
    }

    updateItems();

  }


  void updateItems()
  {
    currentHit = rayHit.collider;

    // If it's a pickup, update the prefab
    if (currentHit && currentHit.tag == "Pickupable")
    {
      currentHit.GetComponent<PickupManager>().skin = "selected";
    }

    // Cleaning up
    // If you've moved in to a new object, deselect previous.
    if (previousHit && ((currentHit != previousHit) || !isTouching ))
    {
      if (previousHit.tag == "Pickupable")
      {
        previousHit.GetComponent<PickupManager>().skin = "original";
      }
      previousHit = null;
    }
    previousHit = currentHit;
  }


  void pickupItem()
  {

    Collider pickupCollider = rayHit.collider;

    // Parent pickup to player
    currentHit.transform.SetParent(Camera.main.transform);
    Transform cameraT = Camera.main.transform;

    // Figure out where to put it.
    Vector3 newPosition = cameraT.position + Camera.main.transform.forward * 2f;
    newPosition = new Vector3(newPosition.x, newPosition.y + holdingHeight, newPosition.z);

    // Move object in to view.
    currentHit.attachedRigidbody.isKinematic = true;
    currentHit.GetComponent<MeshCollider>().isTrigger = true;
    currentHit.transform.position = newPosition;
    currentHit.transform.rotation = player.transform.rotation;
    currentHit.transform.Rotate(3, 3, 3);

    // Set player to holding
    player.GetComponent<PlayerManager>().isHolding = true;

  }

  void dropItem()
  {
    GameObject Pickup = player.GetComponent<PlayerManager>().heldItem;
    Pickup.GetComponent<Collider>().attachedRigidbody.isKinematic = false;
    Pickup.GetComponent<Collider>().attachedRigidbody.AddForce(transform.forward * 300f);
    Pickup.transform.SetParent(null);
    player.GetComponent<PlayerManager>().isHolding = false;
  }

}
