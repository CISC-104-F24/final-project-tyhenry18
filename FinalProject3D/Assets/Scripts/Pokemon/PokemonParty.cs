using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonParty : MonoBehaviour
{
    [SerializeField] List<PokemonBase> pokemonBases;  // List of different PokemonBase assets (ScriptableObjects)
    [SerializeField] List<int> levels;  // List of corresponding levels for each Pokémon

    private List<Pokemon> party;  // List to hold your Pokémon objects

    private void Start()
    {
        party = new List<Pokemon>();

        // Assuming you have equal number of PokemonBase and levels
        for (int i = 0; i < pokemonBases.Count; i++)
        {
            Pokemon newPokemon = new Pokemon();  // Create a new Pokemon
            newPokemon.Init(pokemonBases[i], levels[i]);  // Initialize with the corresponding PokemonBase and level
            party.Add(newPokemon);  // Add the initialized Pokemon to the party
        }

        // Now the party is initialized with the provided PokemonBases and levels
    }
}
