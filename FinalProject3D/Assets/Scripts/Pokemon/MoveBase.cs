using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A ScriptableObject class to define a Move in the game
[CreateAssetMenu(menuName = "Pokemon/Create a new move")]
public class MoveBase : ScriptableObject
{
    // The name of the move, using 'new' to avoid hiding the inherited 'name' property of UnityEngine.Object
    [SerializeField] new string name;

    // A description of what the move does, shown in the editor as a text area
    [TextArea]
    [SerializeField] string description;

    // The type of the move (e.g., Fire, Water, etc.)
    [SerializeField] PokemonType type;

    // The power of the move, determining how much damage it deals
    [SerializeField] int power;

    // The accuracy of the move, which determines how likely the move is to hit
    [SerializeField] int accuracy;

    // The maximum number of times the move can be used before running out of PP (Power Points)
    [SerializeField] int maxPP;

    // Property to get the name of the move
    public string Name => name;

    // Property to get the description of the move
    public string Description => description;

    // Property to get the type of the move (e.g., Fire, Water, etc.)
    public PokemonType Type => type;

    // Property to get the power of the move (damage)
    public int Power => power;

    // Property to get the accuracy of the move (hit chance)
    public int Accuracy => accuracy;

    // Property to get the maximum PP of the move
    public int MaxPP => maxPP;
}
