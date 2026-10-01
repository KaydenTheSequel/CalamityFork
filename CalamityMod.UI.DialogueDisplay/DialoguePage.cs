using System.Collections.Generic;
using CalamityMod.UI.DialogueDisplay.DialogueEvents;

namespace CalamityMod.UI.DialogueDisplay;

public class DialoguePage
{
	public string[] Lines { get; set; }

	public string BaseColor { get; set; }

	public string BaseBorderColor { get; set; }

	public float BorderDarkening { get; set; } = 0.25f;

	public string Speaker { get; set; }

	public int TextScale { get; set; } = -1;

	public Alignment AlignType { get; set; } = Alignment.None;

	public int TextDelay { get; set; } = -1;

	public int InPunctuationDelay { get; set; } = -1;

	public PunctuationData BasePunctuationDelay { get; set; }

	public int PunctuationDelayCap { get; set; } = -1;

	public Dictionary<string, PunctuationData> PunctuationDelays { get; set; }

	public DialogueEvent Event { get; set; }
}
