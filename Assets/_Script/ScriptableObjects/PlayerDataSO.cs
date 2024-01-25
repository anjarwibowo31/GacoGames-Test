using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataSO : ScriptableObject
{
    // Saveable data
    public int Health { get; set; }
    public int Attack { get; set; }
    public int ExpPoint { get; set; }
    public int Level { get; set; }
    public Vector3 Position { get; set; }
}
