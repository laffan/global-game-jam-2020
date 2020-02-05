using UnityEngine;

public class WallManager : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject[] placedBricksList;
  public GameObject bomb;
  public GameObject enemyManager;
  public GameObject wall;
  public bool wallExists = true;
  public AudioClip explosionSound;
  public AudioClip fallSound;

  // Update is called once per frame
  void Update()
  {

    placedBricksList = GameObject.FindGameObjectsWithTag("Placed");
    int placedBricks = placedBricksList.Length;
    int enemiesAtWall = enemyManager.GetComponent<EnemyManager>().enemiesAtTheWall;

    // Debug.Log( "Enemies at wall: " + enemiesAtWall + " / Bricks placed: " + placedBricks );

    if (wallExists)
    {
      if ( enemiesAtWall > placedBricks * 3 )
      {
        Debug.Log("The wall has fallen");
        destroyWall();
        wallExists = false;
      }
      // if ((placedBricks.Length == 0) && (enemiesAtWall != 0))
      // {
      //   Debug.Log("The wall has fallen due to laziness.");
      //   destroyWall();
      //   wallExists = false;
      // }
    }
  }

  void destroyWall()
  {
    bomb.GetComponent<WallExplosion>().Explode();
    GetComponent<AudioSource>().PlayOneShot(explosionSound);
    GetComponent<AudioSource>().PlayOneShot(fallSound);

  }
}
