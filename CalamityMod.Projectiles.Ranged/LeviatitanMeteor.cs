using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class LeviatitanMeteor : ModProjectile, ILocalizedModType, IModType
{
	public static int Lifetime = 600;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 38;
		base.Projectile.height = 38;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.DamageType = DamageClass.Ranged;
	}

	public override void AI()
	{
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.Y < 25f)
		{
			base.Projectile.velocity.Y += 0.12f;
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.05f;
		if (base.Projectile.timeLeft <= Lifetime - 6)
		{
			if (Main.rand.NextBool(3))
			{
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center + Utils.RotatedByRandom(new Vector2(25f, 25f), 100.0) * Main.rand.NextFloat(0.1f, 1.3f) * base.Projectile.scale, (-base.Projectile.velocity * 0.2f).RotatedByRandom(0.20000000298023224) + Utils.RotatedByRandom(new Vector2(3f, 3f), 100.0), Color.Peru, Color.PeachPuff, Main.rand.NextFloat(0.4f, 1.3f), 120f, Main.rand.NextFloat(0.03f, -0.03f)));
			}
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 85, 0f, 0f, 100, default(Color), Main.rand.NextFloat(0.75f, 1.2f));
			Dust obj = Main.dust[dust];
			obj.velocity *= 0f;
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		Vector2 smokeVel = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 5f;
		int smokeAmount = Main.rand.Next(8, 13);
		for (int i = 0; i < smokeAmount; i++)
		{
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, smokeVel.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(1.2f, 2f), Color.White, Main.rand.Next(40, 61), Main.rand.NextFloat(0.2f, 0.4f), 0.3f, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextBool(), 0f, required: true));
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MineralMortarExplode");
			style.Volume = 0.9f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<LeviatitanExplosion>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			for (int i = 0; i < 8; i++)
			{
				Vector2 randVel = Utils.RotatedByRandom(new Vector2(12f, 12f), 100.0) * Main.rand.NextFloat(0.2f, 0.7f);
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + randVel, randVel, Color.Peru, Main.rand.Next(25, 36), Main.rand.NextFloat(0.9f, 2.3f), 0.4f));
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, randVel * 0.8f, Color.Peru, Color.PeachPuff, Main.rand.NextFloat(0.4f, 1.3f), 120f, Main.rand.NextFloat(0.03f, -0.03f)));
			}
			int debriAmount = Main.rand.Next(10, 16);
			for (int debriIndex = 0; debriIndex < debriAmount; debriIndex++)
			{
				Vector2 velocity = ((float)Math.PI * 2f / (float)debriAmount * (float)debriIndex).ToRotationVector2() * Main.rand.NextFloat(2f, 8f);
				GeneralParticleHandler.SpawnParticle(new StoneDebrisParticle(base.Projectile.Center, velocity, Color.Lerp(Color.White, Color.LightGray, Main.rand.NextFloat()), Main.rand.NextFloat(0.4f, 0.6f), Main.rand.Next(30, 46), Main.rand.NextFloat((float)Math.PI)));
			}
			int mistAmount = Main.rand.Next(5, 9);
			for (int mistIndex = 0; mistIndex < mistAmount; mistIndex++)
			{
				Vector2 velocity2 = ((float)Math.PI * 2f / (float)mistAmount * (float)mistIndex).ToRotationVector2() * Main.rand.NextFloat(5f, 15f);
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, velocity2, Color.SaddleBrown * 0.8f, Color.Transparent, Main.rand.NextFloat(0.6f, 1.4f), Main.rand.NextFloat(200f, 400f)));
			}
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Red, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), (float)base.Projectile.width / 5000f, (float)base.Projectile.width / 200f, 29, UseAdditiveBlend: true, 0.7f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.SaddleBrown * 0.8f, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), (float)base.Projectile.width / 5000f, (float)base.Projectile.width / 300f, 29, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
