using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.NPCs.SunkenSea;
using CalamityMod.Particles;
using CalamityMod.Tiles.Abyss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ApoctolithProj : ModProjectile, ILocalizedModType, IModType
{
	public static Color LowBlueColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.Blue;
		}
	}

	public static Color HighBlueColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.DodgerBlue;
		}
	}

	public int ShardDamage => (int)((float)base.Projectile.damage * 0.15f);

	public int ExplosionDamage => (int)((float)base.Projectile.damage * 0.5f);

	public int ExplosionRadius => 150;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/Apoctolith";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, new Vector3(0.2f, 0.2f, 0.5f));
		base.Projectile.ai[0] = (base.Projectile.Calamity().stealthStrike ? 1 : 0);
		if (Main.rand.NextBool(3))
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, Utils.RotatedBy(new Vector2(Main.rand.NextFloat(12f), 0f), (double)(Vector2.Zero.AngleTo(base.Projectile.velocity) + MathHelper.ToRadians(Main.rand.NextFloat(-20f, 20f))), default(Vector2)), affectedByGravity: false, 10, 1f, Main.rand.NextBool(3) ? LowBlueColor : Color.Black, fadeIn: true));
		}
		base.Projectile.ai[1]++;
		base.Projectile.rotation += 0.4f * (float)base.Projectile.direction;
		base.Projectile.velocity.X *= 0.98f;
		base.Projectile.velocity.Y = base.Projectile.velocity.Y + MathHelper.Clamp(base.Projectile.ai[1] / 40f, 0f, 0.6f);
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
		if (Main.rand.NextBool(13))
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 180, base.Projectile.velocity.X * 0.25f, base.Projectile.velocity.Y * 0.25f, 150, default(Color), 0.9f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 240);
		if (hit.Crit)
		{
			target.Calamity().miscDefenseLoss = Math.Min(target.defense, 15);
		}
		if (base.Projectile.Calamity().stealthStrike)
		{
			target.AddBuff(ModContent.BuffType<Eutrophication>(), 120);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 240);
		if (base.Projectile.Calamity().stealthStrike)
		{
			target.AddBuff(ModContent.BuffType<Eutrophication>(), 120);
		}
	}

	public override bool PreDrawExtras()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, 2, Color.Lerp(HighBlueColor, Color.Transparent, 0.8f), 1, ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value);
		return base.PreDrawExtras();
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, LowBlueColor, "CalamityMod/Particles/LargeBloom", Vector2.One, 0f, 1f, 0f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/LargeBloom", Vector2.One, 0f, 0.5f, 0f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		for (int i = 0; i < 5; i++)
		{
			GeneralParticleHandler.SpawnParticle(new BloodParticle2(base.Projectile.Center, Utils.RotatedBy(new Vector2(Main.rand.NextFloat(6f, 12f), 0f), (double)Main.rand.NextFloat((float)Math.PI * 2f), default(Vector2)), 12, Main.rand.NextFloat(0.2f, 0.8f), HighBlueColor));
		}
		GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, Vector2.Zero, affectedByGravity: false, 20, base.Projectile.Calamity().stealthStrike ? 0.06f : 0.03f, HighBlueColor, Vector2.One, quickShrink: true)
		{
			Rotation = Main.rand.NextFloat(-10f, 10f)
		});
		SoundEngine.PlaySound(in AbyssGravel.MineSound, base.Projectile.position);
		SoundEngine.PlaySound(in GiantClam.SlamSound, base.Projectile.position);
		SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode.WithPitchOffset(0.5f), base.Projectile.position);
		if (base.Projectile.Calamity().stealthStrike)
		{
			SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Item/MineralMortarExplode"), base.Projectile.position);
		}
		if (!base.Projectile.Calamity().stealthStrike || Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ApoctolithExplosion>(), ExplosionDamage, 0f, base.Projectile.owner);
		for (int j = 0; j < 5; j++)
		{
			float shardspeedX = (0f - base.Projectile.velocity.X) * Main.rand.NextFloat(0.5f, 0.7f) + Main.rand.NextFloat(-3f, 3f);
			float shardspeedY = (0f - base.Projectile.velocity.Y) * (float)Main.rand.Next(50, 70) * 0.01f + (float)Main.rand.Next(-8, 9) * 0.2f;
			if (shardspeedX < 2f && shardspeedX > -2f)
			{
				shardspeedX += 0f - base.Projectile.velocity.X;
			}
			if (shardspeedY > 2f && shardspeedY < 2f)
			{
				shardspeedY += 0f - base.Projectile.velocity.Y;
			}
			shardspeedX *= 2f;
			shardspeedY *= 2f;
			int shard = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X + shardspeedX, base.Projectile.Center.Y + shardspeedY, shardspeedX, shardspeedY, ModContent.ProjectileType<ApoctolithShard>(), ShardDamage, base.Projectile.knockBack / 2f, base.Projectile.owner);
			Main.projectile[shard].frame = Main.rand.Next(3);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(32f, 33f);
		Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Rogue/ApoctolithGlow", (AssetRequestMode)2).Value;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(32f, 33f);
		Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
	}
}
