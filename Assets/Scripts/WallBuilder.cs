using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]


public class WallBuilder : MonoBehaviour
{

  public GameObject brick;
  public GameObject container;
  public GameObject blocker;
  public int width;
  public int height;
  public float roughness;
  private Mesh mesh;

  public void destroyWall()
  {

    foreach (Transform child in container.transform)
    {
      GameObject.DestroyImmediate(child.gameObject);
    }

  }

  public void buildTheWall()
  {
    destroyWall();
    blocker.active = true;



#if UNITY_EDITOR
    //Only do this in the editor
    MeshFilter mf = brick.GetComponent<MeshFilter>();   //a better way of getting the meshfilter using Generics
    Mesh meshCopy = Mesh.Instantiate(mf.sharedMesh) as Mesh;  //make a deep copy
    mesh = mf.mesh = meshCopy;                    //Assign the copy to the meshes
#else
     //do this in play mode
     mesh = GetComponent<MeshFilter>().mesh;
#endif

    Vector3 brickSize = Vector3.Scale(transform.localScale, mesh.bounds.size);

    for (int h = 0; h < height; h++)
    {
      for (int w = 0; w < width; w++)
      {
        float brickOffset = Random.Range(-roughness, roughness);
        Vector3 brickPosition = new Vector3(container.transform.position.x + (brickSize.x * w), container.transform.position.y + brickSize.y * h, container.transform.position.z + brickOffset);
        Vector3 checkPosition = new Vector3(container.transform.position.x + (brickSize.x * w) + (brickSize.x / 2), container.transform.position.y + (brickSize.y * h) + (brickSize.y / 2), container.transform.position.z + brickOffset);

        // Check to see if there's a collider in the way;
        Collider[] hitColliders = Physics.OverlapSphere(checkPosition, 0.1f);
        // If there's nothing in the way, build;
        if (hitColliders.Length == 0)
        {
          GameObject newBrick = Instantiate(brick, brickPosition, brick.transform.rotation);
          newBrick.active = true;
          newBrick.transform.SetParent(container.transform);
        }

      }
    }
    blocker.active = false;

  }
}
