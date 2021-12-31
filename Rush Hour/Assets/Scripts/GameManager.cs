using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static bool gameEnded = false;
  
  private void Start()
  {
    Debug.Log("game started");
  }
  public void gameOver()
  {
    gameEnded = true;
    Debug.Log("Game over. You've reached " + River.distanceAsString() + " meters.");
  }
}