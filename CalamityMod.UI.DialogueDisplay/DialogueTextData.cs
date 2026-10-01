using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CalamityMod.UI.DialogueDisplay;

public class DialogueTextData
{
	public DialoguePage[] Pages { get; init; }

	public DialoguePage this[int index]
	{
		get
		{
			return Pages[index];
		}
		set
		{
			Pages[index] = value;
		}
	}

	public int Page { get; set; }

	public int PageCount => Pages.Length;

	public string DefaultColor { get; init; }

	public string DefaultSpeaker { get; init; }

	public int DefaultScale { get; init; }

	public Alignment AlignType { get; init; }

	public int TextDelay { get; init; }

	public int InPunctuationDelay { get; init; }

	public PunctuationData BasePunctuationDelay { get; init; }

	public int PunctuationDelayCap { get; init; }

	public Dictionary<string, PunctuationData> PunctuationDelays { get; init; }

	public int Revision { get; init; }

	[JsonConstructor]
	public DialogueTextData(DialoguePage[] Pages, int Page = 0, string DefaultColor = null, string DefaultSpeaker = null, int DefaultScale = 1, Alignment AlignType = Alignment.Left, int TextDelay = 3, int InPunctuationDelay = -1, PunctuationData BasePunctuationDelay = null, int PunctuationDelayCap = 60, Dictionary<string, PunctuationData> PunctuationDelays = null)
	{
		this.Pages = Pages;
		this.Page = Page;
		this.DefaultColor = DefaultColor;
		this.DefaultSpeaker = DefaultSpeaker;
		this.DefaultScale = DefaultScale;
		this.TextDelay = TextDelay;
		this.InPunctuationDelay = ((InPunctuationDelay == -1) ? TextDelay : InPunctuationDelay);
		this.BasePunctuationDelay = BasePunctuationDelay ?? new PunctuationData();
		this.PunctuationDelayCap = PunctuationDelayCap;
		this.PunctuationDelays = PunctuationDelays ?? new Dictionary<string, PunctuationData>();
		this.AlignType = AlignType;
		foreach (DialoguePage p in Pages)
		{
			DialoguePage dialoguePage = p;
			if (dialoguePage.BaseColor == null)
			{
				string text = (dialoguePage.BaseColor = this.DefaultColor);
			}
			dialoguePage = p;
			if (dialoguePage.Speaker == null)
			{
				string text = (dialoguePage.Speaker = this.DefaultSpeaker);
			}
			if (p.TextScale == -1)
			{
				p.TextScale = this.DefaultScale;
			}
			if (p.TextDelay == -1)
			{
				p.TextDelay = this.TextDelay;
			}
			if (p.InPunctuationDelay == -1)
			{
				p.InPunctuationDelay = this.InPunctuationDelay;
			}
			if (p.AlignType == Alignment.None)
			{
				p.AlignType = this.AlignType;
			}
			dialoguePage = p;
			if (dialoguePage.BasePunctuationDelay == null)
			{
				PunctuationData punctuationData = (dialoguePage.BasePunctuationDelay = this.BasePunctuationDelay);
			}
			if (p.PunctuationDelayCap == -1)
			{
				p.PunctuationDelayCap = this.PunctuationDelayCap;
			}
			dialoguePage = p;
			if (dialoguePage.PunctuationDelays == null)
			{
				Dictionary<string, PunctuationData> dictionary = (dialoguePage.PunctuationDelays = this.PunctuationDelays);
			}
		}
	}
}
