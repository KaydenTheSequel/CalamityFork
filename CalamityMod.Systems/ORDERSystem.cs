using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class ORDERSystem : ModSystem
{
	private static readonly SoundStyle ORDERTrack = new SoundStyle("CalamityMod/Sounds/Custom/ORDER", SoundType.Music);

	private static SlotId orderSoundSlot;

	internal static float DefaultOrderTime = 100f;

	internal static float DefaultResetTime = 240f;

	internal static float remainingPlaytime = 0f;

	internal static float timeUntilReset = 0f;

	private static bool currentlyPlaying = false;

	public override void UpdateUI(GameTime gameTime)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		if (remainingPlaytime > 0f)
		{
			remainingPlaytime--;
		}
		if (timeUntilReset > 0f)
		{
			timeUntilReset--;
		}
		if (!currentlyPlaying)
		{
			return;
		}
		ActiveSound activeSound2;
		if (timeUntilReset <= 0f)
		{
			currentlyPlaying = false;
			if (SoundEngine.TryGetActiveSound(orderSoundSlot, out ActiveSound activeSound))
			{
				activeSound.Stop();
			}
			orderSoundSlot = SlotId.Invalid;
		}
		else if (!SoundEngine.TryGetActiveSound(orderSoundSlot, out activeSound2))
		{
			currentlyPlaying = false;
		}
		else
		{
			float newVolume = MathHelper.Clamp(remainingPlaytime / DefaultOrderTime, 0f, 1f);
			activeSound2.Volume = newVolume;
			activeSound2.Update();
		}
	}

	public static void JUDGEMENT()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		if (!currentlyPlaying)
		{
			orderSoundSlot = SoundEngine.PlaySound(in ORDERTrack);
		}
		currentlyPlaying = true;
		remainingPlaytime = DefaultOrderTime;
		timeUntilReset = DefaultResetTime;
	}
}
