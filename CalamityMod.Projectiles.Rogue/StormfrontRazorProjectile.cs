using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class StormfrontRazorProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/StormfrontRazor";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.extraUpdates = 1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(10))
		{
			int d = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 226, 0f, 0f, 100, new Color(Main.rand.Next(20, 100), 204, 250));
			Main.dust[d].scale += (float)Main.rand.Next(50) * 0.01f;
			Main.dust[d].noGravity = true;
			Main.dust[d].position = base.Projectile.Center;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= 4)
		{
			base.Projectile.frame = 0;
		}
		base.DrawOriginOffsetX = 50f;
		base.DrawOriginOffsetY = 20;
		base.Projectile.ai[0]++;
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI));
		base.Projectile.rotation += (float)base.Projectile.spriteDirection * MathHelper.ToRadians(45f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<StaticDischarge>(), 45);
		if (Main.rand.NextBool(10))
		{
			int d = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 226, 0f, 0f, 100, new Color(Main.rand.Next(20, 100), 204, 250));
			Main.dust[d].scale += (float)Main.rand.Next(50) * 0.01f;
			Main.dust[d].noGravity = true;
			Main.dust[d].position = base.Projectile.Center;
		}
		int times = 1;
		if (base.Projectile.Calamity().stealthStrike)
		{
			times = 3;
		}
		for (int i = 0; i < times; i++)
		{
			int lightningDamage = (int)((float)base.Projectile.damage * 1.5f);
			Vector2 lightningSpawnPosition = base.Projectile.Center - Vector2.UnitY.RotatedByRandom(0.20000000298023224) * 1000f;
			Vector2 lightningShootVelocity = (target.Center - lightningSpawnPosition + target.velocity * 7.5f).SafeNormalize(Vector2.UnitY) * 15f;
			int lightning = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), lightningSpawnPosition, lightningShootVelocity, ModContent.ProjectileType<StormfrontLightning>(), lightningDamage, 0f, base.Projectile.owner);
			if (Main.projectile.IndexInRange(lightning))
			{
				Main.projectile[lightning].CritChance = base.Projectile.CritChance;
				Main.projectile[lightning].ai[0] = lightningShootVelocity.ToRotation();
				Main.projectile[lightning].ai[1] = Main.rand.Next(100);
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<StaticDischarge>(), 45);
		if (Main.rand.NextBool(10))
		{
			int d = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 226, 0f, 0f, 100, new Color(Main.rand.Next(20, 100), 204, 250));
			Main.dust[d].scale += (float)Main.rand.Next(50) * 0.01f;
			Main.dust[d].noGravity = true;
			Main.dust[d].position = base.Projectile.Center;
		}
		int times = 1;
		if (base.Projectile.Calamity().stealthStrike)
		{
			times = 3;
		}
		for (int i = 0; i < times; i++)
		{
			int lightningDamage = (int)((float)base.Projectile.damage * 1.5f);
			Vector2 lightningSpawnPosition = base.Projectile.Center - Vector2.UnitY.RotatedByRandom(0.20000000298023224) * 1000f;
			Vector2 lightningShootVelocity = (target.Center - lightningSpawnPosition + target.velocity * 7.5f).SafeNormalize(Vector2.UnitY) * 15f;
			int lightning = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), lightningSpawnPosition, lightningShootVelocity, ModContent.ProjectileType<StormfrontLightning>(), lightningDamage, 0f, base.Projectile.owner);
			if (Main.projectile.IndexInRange(lightning))
			{
				Main.projectile[lightning].CritChance = base.Projectile.CritChance;
				Main.projectile[lightning].ai[0] = lightningShootVelocity.ToRotation();
				Main.projectile[lightning].ai[1] = Main.rand.Next(100);
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(10))
		{
			int d = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 226, 0f, 0f, 100, new Color(Main.rand.Next(20, 100), 204, 250));
			Main.dust[d].scale += (float)Main.rand.Next(50) * 0.01f;
			Main.dust[d].noGravity = true;
			Main.dust[d].position = base.Projectile.Center;
		}
		Collision.HitTiles(base.Projectile.position + base.Projectile.velocity, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.position);
		int times = 1;
		if (base.Projectile.Calamity().stealthStrike)
		{
			times = 3;
		}
		for (int i = 0; i < times; i++)
		{
			int lightningDamage = (int)((float)base.Projectile.damage * 1.5f);
			Vector2 lightningSpawnPosition = base.Projectile.Center - Vector2.UnitY.RotatedByRandom(0.20000000298023224) * 1100f;
			Vector2 lightningShootVelocity = (base.Projectile.Center - lightningSpawnPosition + base.Projectile.velocity * 7.5f).SafeNormalize(Vector2.UnitY) * 14f;
			int lightning = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), lightningSpawnPosition, lightningShootVelocity, ModContent.ProjectileType<StormfrontLightning>(), lightningDamage, 0f, base.Projectile.owner);
			if (Main.projectile.IndexInRange(lightning))
			{
				Main.projectile[lightning].CritChance = base.Projectile.CritChance;
				Main.projectile[lightning].ai[0] = lightningShootVelocity.ToRotation();
				Main.projectile[lightning].ai[1] = Main.rand.Next(100);
			}
		}
		return true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
