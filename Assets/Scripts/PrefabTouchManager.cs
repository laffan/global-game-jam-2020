using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTouchManager : MonoBehaviour
{

  public bool isTouched = false;
  public bool isClicked = false;

  void OnMouseDown()
  {
    if (isTouched)
    {
      isClicked = true;
    }
  }
}
