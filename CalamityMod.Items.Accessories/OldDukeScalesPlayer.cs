using CalamityMod.Cooldowns;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class OldDukeScalesPlayer : ModPlayer
{
	public bool OldDukeScalesOn;

	public bool IsTired;

	public bool HasBoostedDashFirstFrame;

	public int Fatigue;

	public int RecoverTimer;

	private SoundStyle TiredSound
	{
		get
		{
			SoundStyle result = new SoundStyle("CalamityMod/Sounds/Custom/OldDukeHuff");
			result.PitchVariance = 0.1f;
			result.Volume = 0.8f;
			return result;
		}
	}

	public override void PostUpdateMiscEffects()
	{
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		if (OldDukeScalesOn)
		{
			if (!base.Player.HasCooldown(OldDukeScalesFatigue.ID))
			{
				base.Player.AddCooldown(OldDukeScalesFatigue.ID, OldDukeScales.MaxFatigue);
			}
			if (!IsTired)
			{
				base.Player.endurance += 0.1f;
				base.Player.maxRunSpeed *= 1.1f;
				base.Player.accRunSpeed *= 1.1f;
				if (base.Player.dashDelay == -1)
				{
					if (!HasBoostedDashFirstFrame)
					{
						RecoverTimer = OldDukeScales.RecoverTime;
						Fatigue += OldDukeScales.DashFatigueIncrease;
						base.Player.velocity.X *= 1.25f;
						HasBoostedDashFirstFrame = true;
					}
				}
				else
				{
					HasBoostedDashFirstFrame = false;
				}
				if (Fatigue >= OldDukeScales.MaxFatigue)
				{
					SoundEngine.PlaySound(TiredSound, base.Player.Center);
					IsTired = true;
				}
			}
		}
		if (IsTired)
		{
			base.Player.moveSpeed -= 0.3f;
			if (base.Player.dashDelay == -1)
			{
				if (!HasBoostedDashFirstFrame)
				{
					base.Player.velocity.X *= 0.5f;
					HasBoostedDashFirstFrame = true;
				}
			}
			else
			{
				HasBoostedDashFirstFrame = false;
			}
			if (Fatigue <= 0)
			{
				IsTired = false;
			}
		}
		if (base.Player.Calamity().cooldowns.TryGetValue(OldDukeScalesFatigue.ID, out var cooldown))
		{
			cooldown.timeLeft = Fatigue;
			cooldown.duration = OldDukeScales.MaxFatigue;
		}
		if (RecoverTimer > 0)
		{
			RecoverTimer--;
		}
		bool PressingMoveKeys = base.Player.controlLeft || base.Player.controlRight || base.Player.controlDown || base.Player.controlJump;
		if (Fatigue > 0 && RecoverTimer <= 0)
		{
			Fatigue -= (PressingMoveKeys ? 3 : 5);
		}
		if (Fatigue >= OldDukeScales.MaxFatigue)
		{
			Fatigue = OldDukeScales.MaxFatigue;
		}
	}

	public override void ResetEffects()
	{
		OldDukeScalesOn = false;
	}

	public override void UpdateDead()
	{
		Fatigue = 0;
		RecoverTimer = 0;
	}
}
