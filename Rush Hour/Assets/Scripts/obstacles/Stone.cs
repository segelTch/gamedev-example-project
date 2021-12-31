using UnityEngine;

public class Stone : MonoBehaviour
{
  private void Awake()
  {
    // Set random rotation
    transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
  }
}