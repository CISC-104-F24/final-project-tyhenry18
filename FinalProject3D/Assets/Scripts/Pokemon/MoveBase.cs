using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pokemon/Create a new move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] new string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] PokemonType type;

    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] int maxPP;

    public string Name => name;
    public string Description => description;

    public PokemonType Type => type;

    public int Power => power;
    public int Accuracy => accuracy;
    public int MaxPP => maxPP;
}
