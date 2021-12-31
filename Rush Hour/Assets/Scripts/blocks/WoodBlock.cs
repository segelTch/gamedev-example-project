using UnityEngine;

public class WoodBlock : Block
{
  private void Awake()
  {
    // Set random rotation
    Transform blockTransform = GetComponentInChildren<Transform>();
    if (blockTransform != null && Random.Range(0f, 1f) > 0.5f)
    {
      blockTransform.Rotate(new Vector3(0, 0, 90));
    }
  }
}