using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Serializable class to define a Pokemon
[System.Serializable]
public class Pokemon
{
    // Reference to the PokemonBase which holds the data about the Pokemon species
    [SerializeField] PokemonBase _base;

    // The level of the Pokemon
    [SerializeField] int level;

    // Property for the current HP of the Pokemon
    public int Hp { get; set; }

    // A list that holds the moves the Pokemon can learn
    public List<Move> Moves { get; set; }

    // Initializes the Pokemon with a PokemonBase (species) and a level
    public void Init(PokemonBase pokemonBase, int level)
    {
        _base = pokemonBase;  // Initialize the base Pokemon with the given PokemonBase (species)
        this.level = level;    // Set the Pokemon's level
        Hp = MaxHp;            // Set the current HP to the maximum HP at the start

        // Generate the list of moves the Pokemon can learn, based on its level
        Moves = new List<Move>();

        // Sort the LearnableMoves of the Pokemon in descending order by their level
        foreach (var move in _base.LearnableMoves.OrderByDescending(m => m.Level))
        {
            // Add moves to the list if the Pokemon's level is equal to or greater than the move's required level
            if (move.Level <= level)
                Moves.Add(new Move(move.MoveBase));

            // Stop adding moves once the Pokemon has 4 moves
            if (Moves.Count == 4)
                break;
        }
    }

    // Property to calculate and get the Pokemon's Attack stat based on its level
    public int Attack => Mathf.FloorToInt((_base.Attack * level) / 100) + 5;

    // Property to calculate and get the Pokemon's Defense stat based on its level
    public int Defense => Mathf.FloorToInt((_base.Defense * level) / 100) + 5;

    // Property to calculate and get the Pokemon's Special Attack stat based on its level
    public int SpAttack => Mathf.FloorToInt((_base.SpAttack * level) / 100) + 5;

    // Property to calculate and get the Pokemon's Special Defense stat based on its level
    public int SpDefense => Mathf.FloorToInt((_base.SpDefense * level) / 100) + 5;

    // Property to calculate and get the Pokemon's Speed stat based on its level
    public int Speed => Mathf.FloorToInt((_base.Speed * level) / 100) + 5;

    // Property to calculate and get the Pokemon's maximum HP based on its level
    public int MaxHp => Mathf.FloorToInt((_base.MaxHp * level) / 100) + 10;

    // Property to get the PokemonBase (the species data) that this Pokemon is based on
    public PokemonBase Base => _base;

    // Property to get the level of the Pokemon
    public int Level => level;
}
