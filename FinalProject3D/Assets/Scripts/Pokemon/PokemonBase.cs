using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A ScriptableObject class to define a Pok�mon in the game
[CreateAssetMenu(menuName = "Pokemon/Create a new pokemon")]
public class PokemonBase : ScriptableObject
{
    // The name of the Pok�mon, using 'new' to avoid hiding the inherited 'name' property from UnityEngine.Object
    [SerializeField] new string name;

    // A description of the Pok�mon, displayed as a multiline text field in the Unity Editor
    [TextArea]
    [SerializeField] string description;

    // A GameObject that represents the 3D model of the Pok�mon in the game
    [SerializeField] GameObject model;

    // The primary and secondary types of the Pok�mon (e.g., Fire, Water, Grass)
    [SerializeField] PokemonType type1;
    [SerializeField] PokemonType type2;

    // Base stats for the Pok�mon that influence its performance in battle
    [SerializeField] int maxHp;       // The Pok�mon's maximum HP (Hit Points)
    [SerializeField] int attack;      // The Pok�mon's attack stat for physical moves
    [SerializeField] int defense;     // The Pok�mon's defense stat to reduce physical damage
    [SerializeField] int spAttack;   // The Pok�mon's special attack stat for special moves
    [SerializeField] int spDefense;  // The Pok�mon's special defense stat to reduce special damage
    [SerializeField] int speed;      // The Pok�mon's speed stat, influencing turn order in battle

    // A list of moves that the Pok�mon can learn, along with the level at which they are learned
    [SerializeField] List<LearnableMove> learnableMoves;

    // Property to get the name of the Pok�mon
    public string Name => name;

    // Property to get the description of the Pok�mon
    public string Description => description;

    // Property to get the 3D model of the Pok�mon
    public GameObject Model => model;

    // Property to get the maximum HP of the Pok�mon
    public int MaxHp => maxHp;

    // Property to get the attack stat of the Pok�mon
    public int Attack => attack;

    // Property to get the defense stat of the Pok�mon
    public int Defense => defense;

    // Property to get the special attack stat of the Pok�mon
    public int SpAttack => spAttack;

    // Property to get the special defense stat of the Pok�mon
    public int SpDefense => spDefense;

    // Property to get the speed stat of the Pok�mon
    public int Speed => speed;

    // Property to get the list of learnable moves for the Pok�mon
    public List<LearnableMove> LearnableMoves => learnableMoves;
}

// Enum to define the different types of Pok�mon (e.g., Fire, Water, Electric)
public enum PokemonType
{
    None,      // Used for unassigned types
    Normal,    // Normal type
    Fire,      // Fire type
    Water,     // Water type
    Electric,  // Electric type
    Grass,     // Grass type
    Ice,       // Ice type
    Fighting,  // Fighting type
    Poison,    // Poison type
    Ground,    // Ground type
    Flying,    // Flying type
    Psychic,   // Psychic type
    Bug,       // Bug type
    Rock,      // Rock type
    Ghost,     // Ghost type
    Dragon     // Dragon type
}

// A class to define moves that a Pok�mon can learn, along with the level they are learned at
[System.Serializable]
public class LearnableMove
{
    // Reference to a MoveBase object that defines the move itself
    [SerializeField] MoveBase moveBase;

    // The level at which the Pok�mon learns the move
    [SerializeField] int level;

    // Property to get the MoveBase object, which contains the move's details
    public MoveBase MoveBase => moveBase;

    // Property to get the level at which the Pok�mon learns the move
    public int Level => level;
}
