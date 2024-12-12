using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Pokemon
{
    [SerializeField] PokemonBase _base;
    [SerializeField] int level;

    public int Hp { get; set; }
    public List<Move> Moves { get; set; }

    public void Init(PokemonBase pokemonBase, int level)
    {
        _base = pokemonBase;  // Initialize the base Pokemon
        this.level = level;  // Set the level
        Hp = MaxHp;

        // Generate the moves based on the level
        Moves = new List<Move>();
        foreach (var move in _base.LearnableMoves.OrderByDescending(m => m.Level))
        {
            if (move.Level <= level)
                Moves.Add(new Move(move.MoveBase));

            if (Moves.Count == 4)
                break;
        }
    }

    public int Attack => Mathf.FloorToInt((_base.Attack * level) / 100) + 5;
    public int Defense => Mathf.FloorToInt((_base.Defense * level) / 100) + 5;
    public int SpAttack => Mathf.FloorToInt((_base.SpAttack * level) / 100) + 5;
    public int SpDefense => Mathf.FloorToInt((_base.SpDefense * level) / 100) + 5;
    public int Speed => Mathf.FloorToInt((_base.Speed * level) / 100) + 5;
    public int MaxHp => Mathf.FloorToInt((_base.MaxHp * level) / 100) + 10;

    public PokemonBase Base => _base;
    public int Level => level;
}
