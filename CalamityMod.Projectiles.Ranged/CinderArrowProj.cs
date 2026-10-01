using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class CinderArrowProj : ModProjectile, ILocalizedModType, IModType
{
	public bool splitShot;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Items/Ammo/CinderArrow";

	public ref float Time => ref base.Projectile.ai[1];

	public ref float isSplit => ref base.Projectile.ai[2];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 15;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.arrow = true;
		base.Projectile.penetrate = 1;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.timeLeft = 600;
		base.Projectile.aiStyle = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		if (isSplit > 0f)
		{
			base.Projectile.netUpdate = true;
			splitShot = true;
		}
		Color newColor;
		if (splitShot)
		{
			base.Projectile.scale = 0.01f;
			Vector2 center = base.Projectile.Center;
			newColor = Color.Red;
			Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.15f);
			if (Main.rand.NextBool(9))
			{
				Vector2 position = base.Projectile.Center + base.Projectile.velocity * 2f;
				int type = ModContent.DustType<LightDust>();
				Vector2? velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 0.55f);
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, newColor);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.35f, 0.85f);
				dust.noLightEmittence = true;
				dust.color = Color.Lerp(Color.Red, Color.Crimson, Main.rand.NextFloat(0.3f, 0.8f));
			}
			return;
		}
		Vector2 center2 = base.Projectile.Center;
		newColor = Color.Red;
		Lighting.AddLight(center2, ((Color)(ref newColor)).ToVector3() * 0.3f);
		if (Time > 4f && Main.rand.NextBool(3))
		{
			float velMulti = Main.rand.NextFloat(0.05f, 0.35f);
			Vector2 position2 = base.Projectile.Center + base.Projectile.velocity * 2f;
			int type2 = ModContent.DustType<LightDust>();
			Vector2? velocity2 = -base.Projectile.velocity.RotatedBy(0.45) * velMulti;
			newColor = default(Color);
			Dust dust2 = Dust.NewDustPerfect(position2, type2, velocity2, 0, newColor);
			dust2.noGravity = true;
			dust2.scale = Main.rand.NextFloat(0.45f, 0.75f);
			dust2.color = Color.Crimson;
			dust2.noLightEmittence = true;
			Vector2 position3 = base.Projectile.Center + base.Projectile.velocity * 2f;
			int type3 = ModContent.DustType<LightDust>();
			Vector2? velocity3 = -base.Projectile.velocity.RotatedBy(-0.45) * velMulti;
			newColor = default(Color);
			Dust dust3 = Dust.NewDustPerfect(position3, type3, velocity3, 0, newColor);
			dust3.noGravity = true;
			dust3.scale = Main.rand.NextFloat(0.45f, 0.75f);
			dust3.noLightEmittence = true;
			dust3.color = Color.Crimson;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		if (splitShot)
		{
			return;
		}
		int Dusts = 9;
		float radians = (float)Math.PI * 2f / (float)Dusts;
		Vector2 spinningPoint = Vector2.Normalize(new Vector2(-1f, -1f));
		for (int i = 0; i < Dusts; i++)
		{
			Vector2 dustVelocity = spinningPoint.RotatedBy(radians * (float)i).RotatedBy(0.5) * 6.5f;
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, dustVelocity * Main.rand.NextFloat(1f, 2.6f), Color.Crimson, 18, Main.rand.NextFloat(0.9f, 1.6f), 0.35f, Main.rand.NextFloat(-0.3f, 0.3f), glowing: true));
		}
		SoundStyle style = SoundID.Item69 with
		{
			Volume = 0.35f,
			Pitch = 1f,
			PitchVariance = 0.15f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		if (Main.myPlayer == base.Projectile.owner)
		{
			for (int b = 0; b < 3; b++)
			{
				Vector2 velocity = Vector2.UnitY.RotatedByRandom(0.800000011920929) * Main.rand.NextFloat(-5.5f, -4.5f);
				Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<CinderArrowProj>(), (int)((float)base.Projectile.damage * 0.06f), 0f, base.Projectile.owner, 0f, 0f, 1f);
				projectile.timeLeft = 300;
				projectile.arrow = false;
				projectile.MaxUpdates = 4;
			}
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.4f);
			base.Projectile.penetrate = -1;
			base.Projectile.ExpandHitboxBy(110);
			base.Projectile.Damage();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), splitShot ? 180 : 90);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		Color crimson;
		if (splitShot)
		{
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowSpark", (AssetRequestMode)2).Value;
			Projectile projectile = base.Projectile;
			int mode = ProjectileID.Sets.TrailingMode[base.Type];
			crimson = Color.Crimson;
			((Color)(ref crimson)).A = 0;
			CalamityUtils.DrawAfterimagesCentered(projectile, mode, crimson * 0.6f, 1, texture, drawCentered: true, shrink: true);
			return false;
		}
		Texture2D texture2 = ModContent.Request<Texture2D>("CalamityMod/Particles/DrainLineBloom", (AssetRequestMode)2).Value;
		if (Time > 6f)
		{
			Projectile projectile2 = base.Projectile;
			int mode2 = ProjectileID.Sets.TrailingMode[base.Type];
			crimson = Color.Crimson;
			((Color)(ref crimson)).A = 0;
			CalamityUtils.DrawAfterimagesCentered(projectile2, mode2, crimson * 0.4f, 1, texture2, drawCentered: true, shrink: true);
		}
		return true;
	}

	public override bool? CanDamage()
	{
		if (!splitShot || !(Time < 20f))
		{
			return null;
		}
		return false;
	}
}
