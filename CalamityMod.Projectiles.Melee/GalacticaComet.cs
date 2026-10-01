using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class GalacticaComet : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public int cometType;

	public Color useColor;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 15;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 102;
		base.Projectile.height = 102;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 2;
		base.Projectile.timeLeft = 600;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		if (time == 0)
		{
			base.Projectile.scale = Main.rand.NextFloat(0.6f, 0.8f);
			cometType = Main.rand.Next(1, 4);
			useColor = (Color)(cometType switch
			{
				1 => Color.Cyan, 
				2 => Color.Gold, 
				_ => Color.HotPink, 
			});
		}
		if (targetDist < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + base.Projectile.velocity * Main.rand.NextFloat(-2f, -1f), -base.Projectile.velocity * 0.3f, affectedByGravity: false, 7, 0.1f, useColor * 0.45f, new Vector2(1f, 0.3f), quickShrink: true, glow: false));
		}
		time++;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 1.01f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.position);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.9f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.numHits != 0)
		{
			return;
		}
		for (int i = 0; i <= 12; i++)
		{
			if (i < 8)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), (Vector2.One * 9f).RotatedByRandom(100.0) * Main.rand.NextFloat(0.3f, 1.8f), 0, default(Color), Main.rand.NextFloat(1.3f, 1.8f));
				dust.noGravity = true;
				dust.color = useColor;
			}
			else
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, 278, (Vector2.One * 9f).RotatedByRandom(100.0) * Main.rand.NextFloat(0.3f, 1.8f), 0, default(Color), Main.rand.NextFloat(0.8f, 1.3f));
				dust2.noGravity = false;
				dust2.color = Color.Lerp(useColor, Color.White, 0.5f);
			}
		}
		SoundStyle style = SoundID.DD2_CrystalCartImpact with
		{
			Volume = 0.7f,
			PitchVariance = 0.3f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		style = SoundID.DD2_BetsyFireballImpact with
		{
			Volume = 1f,
			PitchVariance = 0.3f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Projectile.type].Value;
		if (cometType == 1)
		{
			tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/GalacticaComet", (AssetRequestMode)2).Value;
		}
		if (cometType == 2)
		{
			tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/GalacticaComet2", (AssetRequestMode)2).Value;
		}
		if (cometType == 3)
		{
			tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/GalacticaComet3", (AssetRequestMode)2).Value;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Projectile.type], Color.White, 2, tex);
		return false;
	}

	public GalacticaComet()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		useColor = Color.White;
		base._002Ector();
	}
}
