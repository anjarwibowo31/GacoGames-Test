using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; set; }

    public int Level { get; set; }
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float ExpPoint { get; set; }
    public float ExpRequirement { get; set; }
    public float Damage { get; set; }

    // test
    [SerializeField] private int level = 1;
    [SerializeField] private float health = 200f;
    [SerializeField] private float expPoint = 200f;
    [SerializeField] private float damage = 40f;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        // test
        Level = level;
        Health = health;
        ExpPoint = expPoint;
        ExpRequirement = 400;
        Damage = damage;
    }
}