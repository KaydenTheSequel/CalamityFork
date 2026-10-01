using System;
using CalamityMod.Cooldowns;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.DesertProwler;

public class DesertProwlerPlayer : ModPlayer
{
	internal SlotId SmokeBombSoundSlot;

	public bool desertProwlerSet;

	public bool stopSmokeBomb;

	public override void ResetEffects()
	{
		desertProwlerSet = false;
	}

	public override void UpdateDead()
	{
		desertProwlerSet = false;
	}

	public override void PostUpdate()
	{
		if (base.Player.Calamity().cooldowns.TryGetValue(SandsmokeBomb.ID, out var cd))
		{
			if (stopSmokeBomb)
			{
				cd.timeLeft = DesertProwlerHat.SmokeCooldown;
				SetBonusBounceEffect();
				stopSmokeBomb = false;
			}
			if (cd.timeLeft == DesertProwlerHat.SmokeCooldown)
			{
				SetBonusEndEffect();
			}
		}
	}

	public void SetBonusStartEffect()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		SmokeBombSoundSlot = SoundEngine.PlaySound(in DesertProwlerHat.SmokeBombSound, base.Player.Center);
	}

	public void SetBonusEndEffect()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(SmokeBombSoundSlot, out ActiveSound sound))
		{
			sound.Stop();
			SmokeBombSoundSlot = SlotId.Invalid;
		}
	}

	public void SetBonusBounceEffect()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in DesertProwlerHat.SmokeBombEndSound, base.Player.Center);
		base.Player.velocity.Y = Math.Min(-6f, base.Player.velocity.Y - 6f);
		base.Player.jump = Player.jumpHeight / 2;
		for (int i = 0; i < 30; i++)
		{
			Vector2 dustDisplace = Main.rand.NextVector2Circular(80f, 30f);
			Vector2 position = base.Player.MountedCenter + Vector2.UnitY * (float)base.Player.height * 0.5f + dustDisplace;
			Vector2 dustSpeed = Main.rand.NextVector2Circular(0.5f, 0.5f) + base.Player.velocity / 8f - Vector2.UnitY.RotatedByRandom(0.7853981852531433) * 0.06f;
			dustSpeed.X += 1.4f * (float)Math.Sin((dustDisplace.X + 80f) / 160f * (float)Math.PI) * (float)((!Main.rand.NextBool()) ? 1 : (-1));
			GeneralParticleHandler.SpawnParticle(new SandyDustParticle(position, dustSpeed, Color.White, Main.rand.NextFloat(0.7f, 1.2f), Main.rand.Next(20, 50), 0.03f, Vector2.UnitY * 0.03f));
		}
		if (Main.dedServ)
		{
			return;
		}
		for (int j = 0; j < 10; j++)
		{
			float dustOrientation = (float)j / 10f * ((float)Math.PI * 2f) + 0.47123894f;
			Vector2 dustDirection = Vector2.UnitX * (float)Math.Sin(dustOrientation) + Vector2.UnitY * (float)Math.Cos(dustOrientation) * 0.2f;
			Vector2 dustPos = base.Player.MountedCenter + Vector2.UnitY * (float)base.Player.height / 2f + dustDirection * 20f;
			Vector2 dustVel = base.Player.velocity * 0.3f + dustDirection * 1.4f;
			int sandstormJump = Gore.NewGore(base.Player.GetSource_Misc("Jump"), dustPos, dustVel, Main.rand.Next(220, 223));
			Main.gore[sandstormJump].velocity = dustVel;
			Main.gore[sandstormJump].alpha = 100;
			for (int k = 0; k < 1; k++)
			{
				Dust.NewDustDirect(dustPos, 32, 32, 124, dustVel.X, dustVel.Y * 0.3f, 150).fadeIn = 1.5f;
			}
		}
	}

	public override void PostUpdateMiscEffects()
	{
		if (!desertProwlerSet && DesertProwlerHat.ShroudedInSmoke(base.Player, out var cd))
		{
			cd.timeLeft = DesertProwlerHat.SmokeCooldown;
		}
	}
}
