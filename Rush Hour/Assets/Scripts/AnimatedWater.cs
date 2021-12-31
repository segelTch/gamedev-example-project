using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedWater : MonoBehaviour
{
  [SerializeField] private float speedConstant;
  private float currentLocation;

  void Start()
  {
    this.currentLocation = GetComponent<Renderer>().material.mainTextureOffset.y;
  }

  void Update()
  {
    this.currentLocation += Time.deltaTime * River.speed * this.speedConstant;
    GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0, this.currentLocation));
  }
}
