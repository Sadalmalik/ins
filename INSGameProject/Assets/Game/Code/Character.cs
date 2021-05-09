using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Interactive Story/Character")]
public class Character : SerializedScriptableObject
{
	public string characterNameKey;
	public string characterName;
	
	[PreviewField(200, ObjectFieldAlignment.Right)]
	public Sprite view;
	
	
    [ContextMenu("Normalize NameKey")]
    private void NormalizeNameKey()
    {
	    characterNameKey = $"char_name_{name.ToLower()}";
    }
}
