using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class BloodfireArrowProj : ModProjectile, ILocalizedModType, IModType
{
	public bool DisableEffects;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Items/Ammo/BloodfireArrow";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.arrow = true;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 9;
		base.Projectile.timeLeft = 1200;
	}

	public override void AI()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		base.Projectile.spriteDirection = base.Projectile.direction;
		if (base.Projectile.localAI[0] == 0f)
		{
			if (DisableEffects)
			{
				base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 9f;
			}
			else
			{
				base.Projectile.damage = (int)((float)base.Projectile.damage * 1.3f);
				player.statLife--;
				if (player.statLife <= 0)
				{
					PlayerDeathReason pdr = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.BloodFireArrow" + Main.rand.Next(1, 3)).ToNetworkText(player.name));
					player.KillMe(pdr, 1000.0, 0);
				}
				base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 9f;
			}
		}
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		Vector2 center = base.Projectile.Center;
		Color newColor = ((!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Red);
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.7f);
		base.Projectile.localAI[0]++;
		if (!(base.Projectile.localAI[0] > 6f) || !(targetDist < 1400f))
		{
			return;
		}
		if (Main.rand.NextBool())
		{
			Vector2 center2 = base.Projectile.Center;
			int type = (Main.rand.NextBool(3) ? 130 : 60);
			Vector2? velocity = -base.Projectile.velocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.2f, 0.6f);
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(center2, type, velocity, 0, newColor);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.3f, 0.7f);
			if (dust.type == 130)
			{
				dust.scale = Main.rand.NextFloat(0.25f, 0.45f);
			}
		}
		if (base.Projectile.localAI[0] % 2f == 0f)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center - base.Projectile.velocity * 2f, -base.Projectile.velocity * 0.01f, "CalamityMod/Particles/BloomLineFade", affectedByGravity: false, 6, 0.025f, (!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Firebrick, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 1.4f, 1f, 0.4f));
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		for (int b = 0; b < 6; b++)
		{
			int dustType = ModContent.DustType<DiamondDust>();
			float velMulti = Main.rand.NextFloat(0.1f, 0.75f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, dustType, (base.Projectile.velocity * 2f).RotatedByRandom(0.3) * velMulti);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.75f, 0.95f);
			dust.color = ((!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Firebrick);
			dust.noLightEmittence = true;
			dust.noLight = true;
			dust.fadeIn = 15f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (!DisableEffects)
		{
			Player player = Main.player[base.Projectile.owner];
			player.lifeRegenTime += 2f;
			float lifeRatio = (float)player.statLife / (float)player.statLifeMax2;
			float num = MathHelper.Lerp(4f, 0.5f, lifeRatio);
			int guaranteedHeal = (int)num;
			float chanceOfOneMoreHP = num - (float)guaranteedHeal;
			bool bonusHeal = Main.rand.NextFloat() < chanceOfOneMoreHP;
			int finalHeal = guaranteedHeal + (bonusHeal ? 1 : 0);
			player.SpawnLifeStealProjectile(target, base.Projectile, 305, finalHeal, 0.5f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], Color.White, 2);
		return false;
	}
}
