using System;
using Pathfinding;
using UnityEngine;


public  class PathfinderScan : MonoBehaviour
{
  public  void Initialise()
    {
        var pathfinder = GetComponentInParent<AstarPath>();
        pathfinder.Scan();
    }
}
