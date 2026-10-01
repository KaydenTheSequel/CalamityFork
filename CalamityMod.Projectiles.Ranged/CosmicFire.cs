using System;
using CalamityMod.Dusts;
using CalamityMod.Enums;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class CosmicFire : ModProjectile, ILocalizedModType, IModType
{
	public Color InnerColor;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref int HitCount => ref Main.player[base.Projectile.owner].Calamity().deadSunCounter;

	public ref float Time => ref base.Projectile.ai[0];

	public ref float BounceHits => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.MaxUpdates = 13;
		base.Projectile.timeLeft = 45 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref InnerColor)).ToVector3() * 0.2f);
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (Main.rand.NextBool(5) && Time > 12f && targetDist < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new GenericBloom(base.Projectile.Center + Main.rand.NextVector2CircularEdge(5f, 5f), base.Projectile.velocity * Main.rand.NextFloat(0.05f, 0.5f), Color.Black, Main.rand.NextFloat(0.2f, 0.4f), Main.rand.Next(9, 12), produceLight: true, AddativeBlend: false));
			int dustStyle = ModContent.DustType<VoidDustInverted>();
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(10f, 10f), dustStyle);
			dust.scale = Main.rand.NextFloat(0.6f, 1.2f);
			dust.velocity = new Vector2(0f, Main.rand.NextFloat(0.1f, 5f));
			dust.noGravity = false;
			dust.color = Color.LightGreen;
		}
		if (base.Projectile.timeLeft % 2 == 0 && Time > 12f && targetDist < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity * 0.05f, "CalamityMod/Particles/GlowSpark2", affectedByGravity: false, 17, 0.052f, Color.Black, new Vector2(0.6f, 1.3f), useAddativeBlend: false));
			CustomSpark customSpark = new CustomSpark(base.Projectile.Center, -base.Projectile.velocity * 0.05f, "CalamityMod/Particles/GlowSpark", affectedByGravity: false, 17, 0.027f, Color.LightGreen, new Vector2(0.6f, 1.3f));
			GeneralParticleHandler.SpawnParticle(customSpark);
			customSpark.DrawLayer = GeneralDrawLayer.AfterEverything;
		}
		if (Time == 9f)
		{
			for (int i = 0; i <= 10; i++)
			{
				float variance = Main.rand.NextFloat(-0.7f, 0.7f);
				int dustStyle2 = ModContent.DustType<VoidDustInverted>();
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, dustStyle2);
				dust2.scale = Main.rand.NextFloat(1.7f, 1.9f) - Math.Abs(variance);
				dust2.velocity = (base.Projectile.velocity * 2f).RotatedBy(variance) * Main.rand.NextFloat(0.35f, 1f) * (1f - Math.Abs(variance));
				dust2.noGravity = true;
				dust2.color = InnerColor;
			}
		}
		if (BounceHits > 0f)
		{
			Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(6) ? 278 : 263, -base.Projectile.velocity);
			dust3.scale = ((dust3.type == 278) ? Main.rand.NextFloat(0.3f, 0.6f) : Main.rand.NextFloat(0.6f, 1.4f));
			dust3.velocity = -base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.3f, 1.7f);
			dust3.noGravity = true;
			dust3.color = InnerColor;
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: false, 600f, 7f, 20f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = DeadSunsWind.Explosion with
		{
			Pitch = (float)HitCount * 0.05f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<DeadSunExplosion>(), (int)((float)base.Projectile.damage * 1.5f), 4f, base.Projectile.owner, HitCount * 10, (BounceHits > 0f) ? 5 : 0);
		if (HitCount >= 15)
		{
			HitCount = 6;
		}
		else
		{
			HitCount++;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.timeLeft = 45 * base.Projectile.MaxUpdates;
		for (int i = 0; i < 2; i++)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Black, "CalamityMod/Particles/LargeBloom", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.35f, 0.4f, 38, UseAdditiveBlend: false, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		for (int j = 0; j < 3; j++)
		{
			CustomPulse customPulse = new CustomPulse(base.Projectile.Center, Vector2.Zero, InnerColor, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.48f, 0.52f, 38, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0);
			GeneralParticleHandler.SpawnParticle(customPulse);
			customPulse.DrawLayer = GeneralDrawLayer.AfterEverything;
		}
		float numberOfDusts = 10f;
		for (int k = 0; (float)k < numberOfDusts; k++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<VoidDustInverted>());
			dust.noGravity = true;
			dust.velocity = Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.3f, 1f);
			dust.scale = Main.rand.NextFloat(1.6f, 2.2f);
			dust.color = InnerColor;
		}
		if (BounceHits == 0f)
		{
			SoundEngine.PlaySound(in DeadSunsWind.Ricochet, base.Projectile.Center);
		}
		BounceHits++;
		if (BounceHits >= 4f)
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		return false;
	}

	public CosmicFire()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		InnerColor = Color.LightGreen;
		base._002Ector();
	}
}
