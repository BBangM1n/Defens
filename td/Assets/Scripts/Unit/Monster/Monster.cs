using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Stat stat;

    // Delegate
    // Events

    // Definitions
    // Properties
    // Fields

    // Unity Messages
    void Start()
    {
        init();
    }
    void Update()
    {
        
    }

    private void init()
    {
        stat = new Stat
        {
            Name = "Stage1",
            Type = "Normal",
            Health = 100,
            PhysicalDefense = 0,
            MagicalDefense = 0,
            Attack = 0f,
            AttackSpeed = 0f
        };

    }
    // Methods
    // Functions
    // Event Handlers

    // Unity Coroutine
    // Interface
}
