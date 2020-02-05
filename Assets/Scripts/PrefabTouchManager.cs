using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTouchManager : MonoBehaviour
{

  public bool isTouched = false;
  public bool isClicked = false;

  void Update()
  {
    if (isTouched)
    {
      // Debug.Log("Being touched.");
    }
  }

  void OnMouseDown()
  {
    if (isTouched)
    {
      // Debug.Log("Being clicked.");
      isClicked = true;
    }
  }
}
