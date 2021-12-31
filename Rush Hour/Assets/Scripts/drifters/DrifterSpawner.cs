using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct DrifterStats
{
  [SerializeField]
  public Drifter drifter;

  [SerializeField]
  public int frequency;
}

public class DrifterSpawner : MonoBehaviour
{
  [SerializeField] private List<DrifterStats> options;
  [SerializeField] private Drifter wall;
  [SerializeField] private int boardSize;
  [SerializeField] private int driftersPerRow;
  private int totalFrequency;
  private float currentTime;
  private float timeToCreate;
  private int leftWallSize = 4;
  private int rightWallSize = 4;

  private void Awake()
  {
    this.boardSize += 3;
    this.totalFrequency = 0;
    foreach (DrifterStats stats in this.options)
    {
      this.totalFrequency += stats.frequency;
    }

    this.timeToCreate = Grid.size / River.speed;
    this.currentTime = this.timeToCreate;
  }

  private void Update()
  {
    if (this.currentTime > this.timeToCreate)
    {
      this.leftWallSize = this.changeWallSize(this.leftWallSize);
      this.rightWallSize = this.changeWallSize(this.rightWallSize);
      this.spawnRow();
      this.currentTime = 0;
    }

    currentTime += Time.deltaTime;
  }

  private int changeWallSize(int previous)
  {
    const int max = 9;
    const int min = 4;
    previous += Random.Range(0, previous) == 0 ? 1 : 0;
    previous -= Random.Range(0, max - previous) == 0 ? 1 : 0;
    previous = Mathf.Clamp(previous, min, max);
    return previous;
  }
  private void spawnRow()
  {
    foreach (Vector3 location in this.wallLocations())
    {
      this.spawnAtLocation(this.wall, location);
    }

    HashSet<Vector3> riverLocations = new HashSet<Vector3>();
    int drifterAmount = this.drifterAmount();

    while (riverLocations.Count < drifterAmount)
    {
      riverLocations.Add(this.randomPositionOnRiver());
    }

    foreach (Vector3 location in riverLocations)
    {
      this.spawnAtLocation(this.randomDrifter(), location);
    }
  }

  private int drifterAmount() => Random.Range(0, this.driftersPerRow);

  private List<Vector3> wallLocations()
  {
    List<Vector3> wallLocations = new List<Vector3>();

    for (int index = -this.boardSize; index <= -this.boardSize + this.leftWallSize; index++)
    {
      wallLocations.Add(this.wallLocation(index));
    }

    for (int index = this.boardSize - this.rightWallSize; index <= this.boardSize; index++)
    {
      wallLocations.Add(this.wallLocation(index));
    }

    return wallLocations;
  }

  private Vector3 wallLocation(int distance) =>
    Grid.positionOnGrid(new Vector2(distance * Grid.size,
                                    Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 1.1f, 1f)).y + 2 * Grid.size));

  private Vector3 randomPositionOnRiver() =>
    Grid.positionOnGrid(new Vector2(Random.Range(Grid.size * (this.leftWallSize - this.boardSize + 1), Grid.size * (this.boardSize - this.rightWallSize - 1)),
                                    Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 1.1f, 1f)).y + 2 * Grid.size));

  private void spawnAtLocation(Drifter drifter, Vector3 location) => Instantiate(drifter, location, Quaternion.identity);

  private Drifter randomDrifter() => this.getDrifterByFrequency(Random.Range(0, this.totalFrequency + 1));

  private Drifter getDrifterByFrequency(int place)
  {
    foreach (DrifterStats stats in this.options)
    {
      if (place <= stats.frequency)
      {
        return stats.drifter;
      }

      place -= stats.frequency;
    }

    return this.options[this.options.Count - 1].drifter;
  }
}