using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class LocalizeText : MonoBehaviour
{
    public string term;
    public TMP_Text text;
    
    [ContextMenu("Link text element")]
    private void NormalizeNameKey()
    {
        if (text == null)
            text = GetComponent<TMP_Text>();
    }
    
    private void Awake()
    {
        if (text == null)
            text = GetComponent<TMP_Text>();
        if (string.IsNullOrEmpty(term))
            term = text.text;
        LocalizationManager.instance.OnLocalize += HandleLocalize;
    }
    
    private void HandleLocalize()
    {
        text.text = LocalizationManager.instance.Localize(term);
    }
    
    public void SetTerm(string newTerm)
    {
        if (string.IsNullOrEmpty(newTerm))
            Debug.LogWarning($"Empty term in {name}!");
        term = newTerm;
        text.text = LocalizationManager.instance.Localize(term);
    }
}
