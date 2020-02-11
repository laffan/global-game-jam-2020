
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WallBuilder))]
public class WallEditor : Editor
{

  public override void OnInspectorGUI()
  {

    base.OnInspectorGUI();

    // Cast target as WallBuilder type.
    // so you can access all functions within
    // it.
    WallBuilder builder= (WallBuilder)target;

    if ( GUILayout.Button("Build Wall")){
      builder.buildTheWall();
    }

    if ( GUILayout.Button("Destroy Wall")){
      builder.destroyWall();
    }
  }
}
