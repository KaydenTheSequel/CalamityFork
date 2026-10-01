using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Healing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class MutilatorSwordProj : BaseSwordHoldoutProjectile, ILocalizedModType, IModType
{
	public bool hasGivenBlood;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override int swingWidth => 180;

	public override string Texture => ModContent.GetModItem(BaseItem.type).Texture;

	public override Item BaseItem => ModContent.GetModItem(ModContent.ItemType<TheMutilator>()).Item;

	public override int AfterImageLength => 5;

	public override int OffsetDistance => 60;

	public override bool drawSwordTrail => true;

	public override Color[] trailColors
	{
		get
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			return (Color[])(object)new Color[3]
			{
				Color.Red,
				Color.DarkRed,
				Color.Gold
			};
		}
	}

	public override float trailOffset => 20f;

	public override int trailLength => 5;

	public override int StartupTime { get; set; }

	public override int CooldownTime { get; set; }

	public override SoundStyle? UseSound => SoundID.Item1;

	public override float trailWidth(float completion, Vector2 vertexPos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return base.trailWidth(completion, vertexPos);
	}

	public override void Spawn()
	{
		BaseSwordHoldoutPlayer modPlayer = Main.player[base.Projectile.owner].GetModPlayer<BaseSwordHoldoutPlayer>();
		StartupTime = 15;
		CooldownTime = 5;
		swingTime -= StartupTime + CooldownTime;
		modPlayer.swingNum = 0;
	}

	public override void AdditionalAI()
	{
		if (base.inStartup)
		{
			base.Projectile.scale = baseScale * MathHelper.Lerp(0.5f, 1f, 1f - MathF.Pow(1f - base.StartupCompletion, 2f));
		}
		else if (base.inCooldown)
		{
			base.Projectile.scale = baseScale * MathHelper.Lerp(1f, 0.5f, MathF.Pow(base.CooldownCompletion, 2f));
		}
		else
		{
			base.Projectile.scale = baseScale * Math.Min(MathHelper.SmoothStep(1f, 2f, base.SwingCompletion), MathHelper.SmoothStep(2f, 1f, base.SwingCompletion));
		}
	}

	public override float SwingFunction()
	{
		if (base.inStartup)
		{
			return MathHelper.ToRadians(MathHelper.SmoothStep((float)(-swingWidth) * 0.5f, (float)(-swingWidth) * 0.75f, 1f - MathF.Pow(1f - base.StartupCompletion, 2f)));
		}
		if (base.inCooldown)
		{
			return MathHelper.ToRadians(MathHelper.SmoothStep((float)swingWidth * 0.25f, (float)swingWidth * 0.33f, base.CooldownCompletion));
		}
		return MathHelper.ToRadians(MathHelper.SmoothStep((float)(-swingWidth) * 0.75f, (float)swingWidth * 0.25f, base.SwingCompletion));
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Laceration>(), 60);
		Player player = Main.player[base.Projectile.owner];
		Item item = player.HeldItem;
		if (item.type != ModContent.ItemType<TheMutilator>())
		{
			base.Projectile.Kill();
			return;
		}
		TheMutilator modItem = item.ModItem as TheMutilator;
		if (hasGivenBlood)
		{
			return;
		}
		modItem.Charge++;
		hasGivenBlood = true;
		if (modItem.Charge > TheMutilator.MaximumCharge)
		{
			modItem.Charge = 0;
			int orbAmount = 30;
			if (orbAmount > 0)
			{
				float spreadAmount = MathHelper.ToRadians(360f);
				for (int i = 0; i < orbAmount; i++)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_OnHit(target), target.Center, -base.angle.RotatedByRandom(spreadAmount) * 3.5f * Main.rand.NextFloat(0.75f, 1.25f), ModContent.ProjectileType<BloodstoneHealOrb>(), 20, 0f, player.whoAmI);
				}
				GeneralParticleHandler.SpawnParticle(new CustomPulse(target.Center, Vector2.Zero, (Color)((!ChildSafety.Disabled) ? Color.CornflowerBlue : new Color(255, 32, 32)) * 0.75f, "CalamityMod/Particles/DustyCircleHardEdge", Vector2.One, Main.rand.NextFloat(-15f, 15f), 0.03f, 0.155f, 40, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/BloodPactCrit");
				style.Volume = 0.5f;
				SoundEngine.PlaySound(in style, player.Center);
			}
		}
		modItem.DecayTimer = 180;
	}
}
