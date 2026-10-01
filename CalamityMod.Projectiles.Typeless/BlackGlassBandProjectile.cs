using System;
using CalamityMod.Cooldowns;
using CalamityMod.Items.Accessories;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class BlackGlassBandProjectile : DirectStrike, ILocalizedModType, IModType
{
	public int time;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public Player Owner => Main.player[base.Projectile.owner];

	public bool visual => Owner.Calamity().bGlassBandVisual;

	public override void SetDefaults()
	{
		base.Projectile.width = 2;
		base.Projectile.height = 2;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 0;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 2;
		base.Projectile.ArmorPenetration = 25;
	}

	public override void PostAI()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		if (time == 0)
		{
			float visMult = (visual ? 1f : 0.3f);
			SoundStyle sound = new SoundStyle("CalamityMod/Sounds/Item/BlackGlassBandSound");
			if (visual)
			{
				SoundStyle style = sound with
				{
					Volume = 1f,
					MaxInstances = -1
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				float randRot = Main.rand.NextFloat(0f, (float)Math.PI);
				for (int i = -1; i <= 1; i += 2)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + (Vector2.UnitX * 20f * (float)i).RotatedBy(randRot), Vector2.UnitY.RotatedBy(randRot) * 0.001f, "CalamityMod/Particles/GlowSpark", affectedByGravity: false, 15, 0.03f, Color.DarkSlateBlue, new Vector2(7f, 0.2f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.55f));
				}
				for (int j = -1; j <= 1; j += 2)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + (Vector2.UnitY * 20f * (float)j).RotatedBy(randRot), Vector2.UnitX.RotatedBy(randRot) * 0.001f, "CalamityMod/Particles/GlowSpark", affectedByGravity: false, 15, 0.03f, Color.DarkSlateBlue, new Vector2(7f, 0.2f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.55f));
				}
			}
			for (int k = 0; k < 18; k++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(9f, 9f), 100.0) * Main.rand.NextFloat(0.3f, 1f), "CalamityMod/Particles/GlowOrbParticle", affectedByGravity: true, 35, Main.rand.NextFloat(0.6f, 0.8f), (Main.rand.NextBool() ? Color.DarkSlateBlue : Color.MediumPurple) * visMult, new Vector2(0.8f, 1.2f), useAddativeBlend: false));
			}
		}
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SetCrit();
		float critDamage = Math.Min(Owner.GetTotalCritChance(AverageDamageClass.Instance) * 0.01f, 1f);
		modifiers.SourceDamage *= 1f + critDamage;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		bool hasCD = Owner.Calamity().cooldowns.TryGetValue(GenericBandCooldown.ID, out var bandCD);
		if ((damageDone <= 2 || (target.life <= 0 && target.realLife == -1)) && Owner.Calamity().generalBandCooldown > BlackGlassBand.cooldown / 2)
		{
			Owner.Calamity().generalBandCooldown -= BlackGlassBand.cooldown / 2;
			if (hasCD)
			{
				bandCD.timeLeft -= BlackGlassBand.cooldown / 2;
			}
		}
	}

	public override bool? CanDamage()
	{
		return null;
	}
}
