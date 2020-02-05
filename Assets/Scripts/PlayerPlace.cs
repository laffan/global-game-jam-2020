using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlace : MonoBehaviour
{

  public GameObject player;
  public GameObject wallContainer;
  Vector3 targetLocation;
  GameObject heldItem;
  public GameObject detachedItem = null;
  Vector3 itemPlace;
  Vector3 previousitemPlace;
  public float snapSensitivity = 0.5f;

  RaycastHit rayHit;
  bool isPlacing;

  GameObject[] placedObjects;

  // Update is called once per frame
  void Update()
  {

    rayHit = player.GetComponent<PlayerManager>().rayHit;
    heldItem = player.GetComponent<PlayerManager>().heldItem;
    isPlacing = player.GetComponent<PlayerManager>().isPlacing;

    if (isPlacing)
    {
      positionItem();

      if (Input.GetMouseButtonDown(0))
      {
        placeItem();
      }

    }
    else
    {
      stopPositioning();
    }

  }

  void positionItem()
  {


    // Instantiate clone outside of camera
    if (detachedItem == null)
    {
      detachedItem = Instantiate(heldItem, itemPlace, rayHit.collider.transform.rotation);
      detachedItem.transform.SetParent(null);
      detachedItem.GetComponent<Collider>().attachedRigidbody.isKinematic = true; // Toggle Physics
      detachedItem.GetComponent<Collider>().isTrigger = true; // Allow long beams to pass through player;

      heldItem.active = false; // Disable original block.
    }
    // Update prefab
    detachedItem.GetComponent<PickupManager>().skin = "ghost";
    // Hide from raycast
    detachedItem.layer = 0;


    // Postion above block

    if ( detachedItem.GetComponent<PickupManager>().pickupType == "cinderblock") {
      Debug.Log("Positioning cinderblock");
      Transform blockYouHit = rayHit.collider.transform;
      itemPlace = new Vector3(blockYouHit.position.x - 0.5f, blockYouHit.position.y + 1, blockYouHit.position.z);
    }
    if ( detachedItem.GetComponent<PickupManager>().pickupType == "plank") {
      Debug.Log("Positioning plank");
      itemPlace = new Vector3(Mathf.Round(rayHit.point.x), Mathf.Round(rayHit.point.y), Mathf.Round(rayHit.point.z));
    }

    detachedItem.transform.position = itemPlace;
    previousitemPlace = itemPlace;


  }

  void stopPositioning()
  {
    if (detachedItem != null)
    {
      heldItem.active = true;
      Destroy(detachedItem);
    }
  }

  void placeItem()
  {
    float roughness = Random.Range(-0.1f, 0.2f);
    Vector3 currentPosition = new Vector3( detachedItem.transform.position.x, detachedItem.transform.position.y, detachedItem.transform.position.z);
    detachedItem.transform.position = new Vector3( currentPosition.x, currentPosition.y, currentPosition.z + roughness);

    detachedItem.GetComponent<Collider>().tag = "Placed";
    detachedItem.GetComponent<PickupManager>().skin = "original"; // Update materials.
    detachedItem.GetComponent<PickupManager>().PlayPlaceSound(); // Play sound
    detachedItem.layer = LayerMask.NameToLayer("Buildable");
    detachedItem.GetComponent<Collider>().attachedRigidbody.isKinematic = false; // Toggle Physics
    detachedItem.GetComponent<Collider>().isTrigger = false;
    detachedItem.transform.SetParent(wallContainer.transform);

    player.GetComponent<PlayerManager>().isHolding = false;
    player.GetComponent<PlayerManager>().isPlacing = false;
    heldItem = null; // Empty variable so the next pickup doesn't delete it.
    detachedItem = null; // Empty variable so the next pickup doesn't delete it.
    StartCoroutine(SetKinematic());

    Destroy(heldItem);
  }

  IEnumerator SetKinematic()
  {
    Debug.Log("Setting Plank Kinematics");
    yield return new WaitForSeconds(2);
    placedObjects = GameObject.FindGameObjectsWithTag("Placed");
    foreach (GameObject placedObject in placedObjects)
    {
      placedObject.GetComponent<Collider>().attachedRigidbody.isKinematic = true; // Toggle Physics
    }
  }
}
