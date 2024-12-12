using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildPokemon : MonoBehaviour
{
    [SerializeField] PokemonBase pokemonBase;  // Reference to PokemonBase asset
    [SerializeField] int level = 5;  // Default level for wild Pokémon

    private Pokemon pokemon;

    private void Start()
    {
        // Create the Pokemon instance dynamically at runtime
        pokemon = new Pokemon();
        pokemon.Init(pokemonBase, level);  // Initialize with the base and level
    }
}
