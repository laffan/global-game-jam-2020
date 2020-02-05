using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

  public float mouseSensitivity = 100f;
  public Transform playerBody;
  public GameObject player;
  public Transform crosshair;
  RaycastHit rayHit;
  public float distanceToTouch;
  public LayerMask rayCastLayermask;

  float xRotation = 0f;

  // Start is called before the first frame update
  void Start()
  {
    // hide/lock our cursor to center of screen
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;
    crosshair.localPosition = new Vector3(0, 0, distanceToTouch);
  }


  // Update is called once per frame
  void FixedUpdate()
  {
    // timeDeltatime to make sure look speed is disconnected from framerate
    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    xRotation -= mouseY;

    // Prevent player from looking past straight up.
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    // Use Quaternion instead of rotate so Math.Clamp can be used.
    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    playerBody.Rotate(Vector3.up * mouseX);

    
    player.GetComponent<PlayerManager>().isTouching = touchWorld();

  }

  bool touchWorld()
  {
    Debug.DrawRay(this.transform.position, this.transform.forward * distanceToTouch, Color.magenta);
    if (Physics.Raycast(this.transform.position, this.transform.forward, out rayHit, distanceToTouch, rayCastLayermask))
    {
      player.GetComponent<PlayerManager>().rayHit = rayHit;
      return true;
    }
    else
    {
      return false;
    }
  }




}
