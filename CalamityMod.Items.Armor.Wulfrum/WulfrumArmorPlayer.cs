using CalamityMod.Cooldowns;
using CalamityMod.Items.Accessories.Vanity;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Wulfrum;

public class WulfrumArmorPlayer : ModPlayer
{
	public bool wulfrumSet;

	public override void ResetEffects()
	{
		wulfrumSet = false;
	}

	public override void UpdateDead()
	{
		wulfrumSet = false;
	}

	public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
	{
		if (WulfrumHat.PowerModeEngaged(base.Player, out var _) && !Main.dedServ)
		{
			SetBonusEndEffect(violent: true);
			if (!base.Player.GetModPlayer<WulfrumTransformationPlayer>().vanityEquipped)
			{
				base.Player.Transformation().currentTransformation = null;
			}
		}
	}

	public override void PostUpdate()
	{
		if (wulfrumSet && base.Player.Calamity().cooldowns.TryGetValue(WulfrumBastion.ID, out var cd) && cd.timeLeft == WulfrumHat.BastionCooldown)
		{
			SetBonusEndEffect(violent: false);
		}
	}

	public override void PostHurt(Player.HurtInfo info)
	{
		if (!WulfrumHat.PowerModeEngaged(base.Player, out var cd))
		{
			return;
		}
		cd.timeLeft -= WulfrumHat.TimeLostPerHit;
		if (cd.timeLeft < WulfrumHat.BastionCooldown)
		{
			cd.timeLeft = WulfrumHat.BastionCooldown - 1;
			if (!Main.dedServ)
			{
				SetBonusEndEffect(violent: true);
			}
		}
	}

	public void SetBonusEndEffect(bool violent)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle breakSound = WulfrumHat.SetBreakSoundSafe;
		float goreSpeed = 3f;
		int goreCount = 4;
		int goreIncrement = 2;
		if (violent)
		{
			breakSound = WulfrumHat.SetBreakSound;
			goreSpeed = 9f;
			goreCount = 9;
			goreIncrement = 1;
		}
		SoundEngine.PlaySound(in breakSound, base.Player.Center);
		if (base.Player.GetModPlayer<WulfrumTransformationPlayer>().vanityEquipped)
		{
			Vector2 shrapnelVelocity = Main.rand.NextVector2Circular(goreSpeed, goreSpeed);
			Gore.NewGore(base.Player.GetSource_Death(), base.Player.Center, shrapnelVelocity, base.Mod.Find<ModGore>("WulfrumPowerSuit1").Type);
			return;
		}
		int j = 1;
		for (int i = 1; i < goreCount; i++)
		{
			Vector2 shrapnelVelocity2 = Main.rand.NextVector2Circular(goreSpeed, goreSpeed);
			string goreType = "WulfrumPowerSuit" + j;
			Gore.NewGore(base.Player.GetSource_Death(), base.Player.Center, shrapnelVelocity2, base.Mod.Find<ModGore>(goreType).Type);
			j += Main.rand.Next(1, goreIncrement);
		}
	}

	public override void PostUpdateMiscEffects()
	{
		if (Main.mouseItem.ModItem is WulfrumFusionCannon && !WulfrumHat.PowerModeEngaged(base.Player, out var _))
		{
			Main.mouseItem.TurnToAir();
		}
		if (!wulfrumSet && WulfrumHat.PowerModeEngaged(base.Player, out var cd2))
		{
			cd2.timeLeft = WulfrumHat.BastionCooldown;
		}
	}

	public override void FrameEffects()
	{
		if (!base.Player.Male && base.Player.head == EquipLoader.GetEquipSlot(base.Mod, "WulfrumHat", EquipType.Head))
		{
			base.Player.head = EquipLoader.GetEquipSlot(base.Mod, "WulfrumHatFemale", EquipType.Head);
		}
	}
}
