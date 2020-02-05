using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{

  public string pickupType;
  public Material[] materials;
  public string skin;
  public AudioClip placeSound;

  // Update is called once per frame
  void Update()
  {

    switch (skin)
    {
      case "original":
        GetComponent<Renderer>().sharedMaterial = materials[0];
        break;
      case "selected":
        GetComponent<Renderer>().sharedMaterial = materials[2];
        break;
      case "ghost":
        GetComponent<Renderer>().sharedMaterial = materials[1];
        break;
      default:
        GetComponent<Renderer>().sharedMaterial = materials[0];
        break;
    }
  }

  public void PlayPlaceSound()
  {
    GetComponent<AudioSource>().PlayOneShot(placeSound);
  }
}
