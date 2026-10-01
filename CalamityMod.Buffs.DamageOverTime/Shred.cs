using System;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using CalamityMod.Projectiles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class Shred : ModBuff
{
	internal const int StackFalloffFrames = 320;

	internal static int BaseDamage = 120;

	internal static int FramesPerDamageTick = 12;

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		CalamityGlobalNPC cgn = npc.Calamity();
		if (cgn.somaShredStacks <= 0)
		{
			cgn.somaShredStacks = 1;
			cgn.somaShredFalloff = 320;
		}
		else
		{
			cgn.somaShredStacks++;
		}
		npc.DelBuff(buffIndex);
		buffIndex--;
	}

	internal static void TickDebuff(NPC target, CalamityGlobalNPC cgn)
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		cgn.somaShredFalloff -= cgn.somaShredStacks;
		if (cgn.somaShredFalloff <= 0)
		{
			cgn.somaShredFalloff += 320;
			cgn.somaShredStacks--;
		}
		if (cgn.somaShredApplicator >= 0 && cgn.somaShredApplicator < 255 && Main.myPlayer == cgn.somaShredApplicator)
		{
			Player applicator = Main.player[cgn.somaShredApplicator];
			if (applicator.miscCounter % FramesPerDamageTick == 0)
			{
				int bleedTickDamage = (int)applicator.GetTotalDamage<RangedDamageClass>().ApplyTo(BaseDamage * cgn.somaShredStacks);
				Projectile projectile = Projectile.NewProjectileDirect(target.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), bleedTickDamage, 0f, applicator.whoAmI, target.whoAmI);
				projectile.DamageType = DamageClass.Ranged;
				CalamityGlobalProjectile calamityGlobalProjectile = projectile.Calamity();
				calamityGlobalProjectile.supercritHits = -1;
				calamityGlobalProjectile.bonusCritDamage++;
			}
		}
	}

	internal static void DrawEffects(Player player)
	{
	}

	internal static void DrawEffects(NPC npc, CalamityGlobalNPC cgn, ref Color drawColor)
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		float roughBloodCount = (float)Math.Sqrt(0.8f * (float)cgn.somaShredStacks);
		int exactBloodCount = (int)roughBloodCount;
		if (Main.rand.NextFloat() < roughBloodCount - (float)exactBloodCount)
		{
			exactBloodCount++;
		}
		float velStackMult = 1f + (float)Math.Log(cgn.somaShredStacks);
		for (int i = 0; i < exactBloodCount; i++)
		{
			int bloodLifetime = Main.rand.Next(22, 36);
			float bloodScale = Main.rand.NextFloat(0.6f, 0.8f);
			Color bloodColor = Color.Lerp(Color.Red, Color.DarkRed, Main.rand.NextFloat());
			bloodColor = ((!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Lerp(bloodColor, new Color(51, 22, 94), Main.rand.NextFloat(0.65f)));
			if (Main.rand.NextBool(20))
			{
				bloodScale *= 2f;
			}
			float randomSpeedMultiplier = Main.rand.NextFloat(1.25f, 2.25f);
			Vector2 bloodVelocity = Main.rand.NextVector2Unit() * velStackMult * randomSpeedMultiplier;
			bloodVelocity.Y -= 5f;
			GeneralParticleHandler.SpawnParticle(new BloodParticle(npc.Center, bloodVelocity, bloodLifetime, bloodScale, bloodColor));
		}
		for (int j = 0; j < exactBloodCount / 3; j++)
		{
			float bloodScale2 = Main.rand.NextFloat(0.2f, 0.33f);
			Color bloodColor2 = ((!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Lerp(Color.Red, Color.DarkRed, Main.rand.NextFloat(0.5f, 1f)));
			Vector2 bloodVelocity2 = Main.rand.NextVector2Unit() * velStackMult * Main.rand.NextFloat(1f, 2f);
			bloodVelocity2.Y -= 2.3f;
			GeneralParticleHandler.SpawnParticle(new BloodParticle2(npc.Center, bloodVelocity2, 20, bloodScale2, bloodColor2));
		}
	}
}
