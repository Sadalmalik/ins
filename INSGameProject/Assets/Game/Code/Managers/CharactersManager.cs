using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharactersManager : BaseManager<CharactersManager>
{
	public string pathToCharacters = "Characters";

	public Dictionary<string, Character> characters;

	public override void Init()
	{
		var tempCharacters = Resources.LoadAll<Character>(pathToCharacters);

		characters = tempCharacters.ToDictionary(c => c.name, c => c);
	}
}