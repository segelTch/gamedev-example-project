using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/KeySetScriptableObject", order = 1)]
public class KeySet : ScriptableObject
{
  public KeyCode up;
  public KeyCode down;
  public KeyCode right;
  public KeyCode left;
  public KeyCode pivot;
  public KeyCode use;
}