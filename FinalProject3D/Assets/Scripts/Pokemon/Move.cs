using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Move
{
    [SerializeField] MoveBase moveBase;  // Reference to the MoveBase scriptable object

    // Properties to store move stats like Power, Accuracy, etc.
    public string Name => moveBase.Name;
    public string Description => moveBase.Description;
    public PokemonType Type => moveBase.Type;
    public int Power => moveBase.Power;
    public int Accuracy => moveBase.Accuracy;
    public int MaxPP => moveBase.MaxPP;

    // Constructor
    public Move(MoveBase baseMove)
    {
        moveBase = baseMove;
    }
}
