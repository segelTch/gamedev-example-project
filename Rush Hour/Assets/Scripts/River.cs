using UnityEngine;
public class River : MonoBehaviour
{
  public static float speed = 3f;
  public static float distance { get; private set; }

  private void Start()
  {
    distance = 0;
  }

  private void Update()
  {
    if (!GameManager.gameEnded)
    {
      distance += River.speed * Time.deltaTime / 3;
    }
  }

  public static string distanceAsString() => distance.ToString("F2");
}