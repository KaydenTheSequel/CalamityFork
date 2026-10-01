namespace CalamityMod.UI.DialogueDisplay.DialogueEvents;

public abstract class DialogueEvent
{
	internal bool EventOver = true;

	internal int EventCounter;

	public string ID { get; set; }

	public string[] Args { get; set; }

	public bool IsOver => EventOver;

	public virtual void UpdateEvent()
	{
		EventCounter++;
	}
}
