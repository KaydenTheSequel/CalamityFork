using ReLogic.Utilities;
using Terraria.Audio;

namespace CalamityMod.UI.DialogueDisplay.DialogueEvents;

public class SoundEvent : DialogueEvent
{
	private SoundStyle style;

	private SlotId soundSlot;

	public override void UpdateEvent()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		ActiveSound result;
		if (EventCounter == 0)
		{
			SoundStyle soundStyle = (style = new SoundStyle(base.Args[0]));
			soundSlot = SoundEngine.PlaySound(in soundStyle);
		}
		else if (!SoundEngine.TryGetActiveSound(soundSlot, out result) || result.Style != style)
		{
			EventOver = true;
		}
	}
}
