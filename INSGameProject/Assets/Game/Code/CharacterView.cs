using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
	public Image renderer;
	public Character currentCharacter;

	[Space]
	public Color normalColor = Color.white;
	public Color foggingColor = Color.gray;
	public float foggingDuration = 0.3f;

	public void SetCharacter(Character character)
	{
		currentCharacter = character;
		renderer.sprite  = character.view;
	}

	public void SetViewDirection(Direction direction, bool upsideDown = false)
	{
		renderer.transform.localScale = new Vector3(
				direction == Direction.Right ? 1 : -1,
				upsideDown ? -1 : 1,
				1
			);
	}

	public void SetFogging(bool fogging)
	{
		renderer.DOColor(fogging ? foggingColor : normalColor, foggingDuration)
	}
}