using UnityEngine;

public class Grid
{
  public static float size = 3f;

  public static Vector3 positionOnGrid(Vector3 position) =>
      new Vector3(roundToGrid(position.x),
                  roundToGrid(position.y),
                  roundToGrid(position.z));

  private static float roundToGrid(float number) => Mathf.Round(number / size) * size;
}