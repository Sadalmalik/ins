using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Localization
{
	public SystemLanguage language;

	public Dictionary<string, string> terms = new Dictionary<string, string>();

	public Localization(SystemLanguage lang)
	{
		language = lang;
	}

	public void Init(string csv)
	{
		var list = CSVReader.FromStrint(csv);

		foreach (var line in list)
			terms.Add(line[0], line[1]);
	}
}

public class LocalizationManager : BaseManager<LocalizationManager>
{
	private SystemLanguage _currentLanguage;
	public SystemLanguage CurrentLanguage => _currentLanguage;

	public SystemLanguage fallbackLanguage = SystemLanguage.English;

	private readonly Dictionary<SystemLanguage, Localization> _localizations =
		new Dictionary<SystemLanguage, Localization>();

	public event Action OnLocalize;

	public override void Init()
	{
		_currentLanguage = fallbackLanguage;
	}

	public void SetLanguage(SystemLanguage language)
	{
		_currentLanguage = language;
		OnLocalize?.Invoke();
	}

	public void AddLocalizations(SystemLanguage lang, string raw_csv)
	{
		if (!_localizations.TryGetValue(lang, out var localization))
			_localizations.Add(lang, localization = new Localization(lang));
		localization.Init(raw_csv);
	}

	public string Localize(string term)
	{
		if (_localizations.TryGetValue(CurrentLanguage, out var local))
			if (local.terms.TryGetValue(term, out var text))
				return text;

		if (_localizations.TryGetValue(fallbackLanguage, out local))
			if (local.terms.TryGetValue(term, out var text))
				return text;

		return term;
	}

	public List<string> GetTerms()
	{
		if (_localizations.TryGetValue(CurrentLanguage, out var local))
			return local.terms.Keys.ToList();

		if (_localizations.TryGetValue(fallbackLanguage, out local))
			return local.terms.Keys.ToList();

		return null;
	}
}