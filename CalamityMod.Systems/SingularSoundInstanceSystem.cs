using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class SingularSoundInstanceSystem : ModSystem
{
	public static SlotId SoundSlot;

	internal static int maxPlaytime;

	internal static int remainingPlaytime;

	internal static int timeUntilReset;

	internal static Entity AttachedEntity;

	public static bool currentlyPlaying;

	public override void UpdateUI(GameTime gameTime)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		if (remainingPlaytime > 0)
		{
			remainingPlaytime--;
		}
		if (timeUntilReset > 0)
		{
			timeUntilReset--;
		}
		if (!currentlyPlaying)
		{
			return;
		}
		ActiveSound activeSound2;
		if (timeUntilReset <= 0)
		{
			currentlyPlaying = false;
			maxPlaytime = 0;
			AttachedEntity = null;
			if (SoundEngine.TryGetActiveSound(SoundSlot, out ActiveSound activeSound))
			{
				activeSound.Stop();
			}
			SoundSlot = SlotId.Invalid;
		}
		else if (!SoundEngine.TryGetActiveSound(SoundSlot, out activeSound2) || AttachedEntity == null || !AttachedEntity.active)
		{
			currentlyPlaying = false;
			maxPlaytime = 0;
			AttachedEntity = null;
			activeSound2?.Stop();
			SoundSlot = SlotId.Invalid;
		}
		else
		{
			if (AttachedEntity != null && AttachedEntity.active)
			{
				activeSound2.Position = AttachedEntity.Center;
			}
			float newVolume = MathHelper.Clamp((float)remainingPlaytime / (float)maxPlaytime, 0f, 1f);
			activeSound2.Volume = newVolume;
			activeSound2.Update();
		}
	}

	public static void PlaySingleInstance(SoundStyle Sound, int Playtime, int ResetTime, Entity Attachment = null)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if (!currentlyPlaying)
		{
			SoundSlot = SoundEngine.PlaySound(in Sound);
		}
		currentlyPlaying = true;
		remainingPlaytime = Playtime;
		maxPlaytime = Playtime;
		timeUntilReset = ResetTime;
		if (Attachment != null)
		{
			AttachedEntity = Attachment;
		}
	}
}
