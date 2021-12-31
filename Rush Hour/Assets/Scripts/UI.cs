using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
  public Text scoreText;
  void Update()
  {
    this.scoreText.text = "meters: " + River.distanceAsString();
  }
}
