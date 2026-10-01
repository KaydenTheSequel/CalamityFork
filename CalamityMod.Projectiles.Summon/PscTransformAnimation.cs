using System;
using CalamityMod.Items.Accessories;
using CalamityMod.NPCs.Providence;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class PscTransformAnimation : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.NoLiquidDistortion[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 26;
		base.Projectile.ignoreWater = true;
		base.Projectile.aiStyle = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 120;
		base.Projectile.alpha = 255;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.3f, 0.225f, 0f);
		Player owner = Main.player[base.Projectile.owner];
		owner.Calamity().profanedCrystalAnim = base.Projectile.timeLeft;
		base.Projectile.Center = owner.Center;
		Color val;
		if (!owner.Calamity().profanedCrystal)
		{
			owner.Calamity().profanedCrystalAnim = -1;
			base.Projectile.active = false;
		}
		else if (base.Projectile.timeLeft > 1)
		{
			int dustCount = (int)Math.Round(MathHelper.SmoothStep(1f, 3f, (120f - (float)owner.Calamity().profanedCrystalAnim) / 120f));
			float outwardness = MathHelper.SmoothStep(40f, 75f, (120f - (float)owner.Calamity().profanedCrystalAnim) / 120f);
			float dustScale = MathHelper.Lerp(0.45f, 1f, (120f - (float)owner.Calamity().profanedCrystalAnim) / 120f);
			int[] validRockTypes = new int[5] { 1, 3, 4, 5, 6 };
			bool shouldStickAround = owner.ownedProjectileCounts[ModContent.ProjectileType<PscTransformRocks>()] <= 20;
			for (int i = 0; i < dustCount; i++)
			{
				Vector2 spawnPosition = base.Projectile.Center + Main.rand.NextVector2Unit() * outwardness * Main.rand.NextFloat(0.75f, 1.1f);
				Vector2 dustVelocity = (base.Projectile.Center - spawnPosition) * 0.085f + owner.velocity;
				Vector2 position = spawnPosition;
				int dustID = ProvUtils.GetDustID(!Main.dayTime);
				val = default(Color);
				Dust dust = Dust.NewDustPerfect(position, dustID, null, 0, val);
				dust.velocity = dustVelocity;
				dust.scale = dustScale * Main.rand.NextFloat(0.75f, 1.15f);
				dust.noGravity = true;
				dust.noLight = true;
				if (base.Projectile.timeLeft % 3 == 0)
				{
					if (!Main.dedServ)
					{
						Vector2 startVec = Main.rand.NextVector2CircularEdge(250f, 250f);
						float finalDist = Main.rand.NextFloat(50f, 50f);
						Color startColor = (Main.dayTime ? Color.Orange : Color.Aquamarine);
						GeneralParticleHandler.SpawnParticle(new ManaDrainStreak(owner, Main.rand.NextFloat(0.3f, 0.6f), startVec, finalDist, startColor, startColor * 1.5f, Main.rand.Next(20, 31), owner.Center));
					}
					if (owner.whoAmI == Main.myPlayer)
					{
						spawnPosition = owner.Center;
						spawnPosition.X += Main.rand.NextFloat(-500f, 500f);
						spawnPosition.Y += Main.rand.NextFloat(-500f, 500f);
						dustVelocity = (base.Projectile.Center - spawnPosition) * 0.085f + owner.velocity;
						((Vector2)(ref dustVelocity)).Normalize();
						dustVelocity *= 16f;
						int rockType = validRockTypes[Main.rand.Next(0, validRockTypes.Length)];
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPosition, dustVelocity, ModContent.ProjectileType<PscTransformRocks>(), 0, 0f, base.Projectile.owner, shouldStickAround ? 1f : 0f, rockType);
					}
				}
			}
		}
		else
		{
			owner.Calamity().profanedCrystalAnim = -1;
			owner.SetScreenshake(5f);
			ProfanedSoulCrystal.DetermineTransformationEligibility(owner);
			if (!Main.dedServ)
			{
				val = ProfanedSoulCrystal.GetColorForPsc(owner.Calamity().pscState, Main.dayTime);
				((Color)(ref val)).A = byte.MaxValue;
				Color color = val;
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(owner.Center, Vector2.Zero, color, Vector2.One, 0f, 0f, 2.5f, 75));
			}
			OnKill(1);
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in Providence.SpawnSound, base.Projectile.position);
		Player Owner = Main.player[base.Projectile.owner];
		Vector2 dustPos = default(Vector2);
		for (int i = 0; i < 20; i++)
		{
			((Vector2)(ref dustPos))._002Ector(Owner.Center.X + Main.rand.NextFloat(-10f, 10f), Owner.Center.Y + Main.rand.NextFloat(-10f, 10f));
			Vector2 velocity = (Owner.Center - dustPos).SafeNormalize(Vector2.Zero);
			velocity *= (Main.dayTime ? 3f : 6.9f);
			Dust dust = Dust.NewDustPerfect(Owner.Center, ProvUtils.GetDustID(!Main.dayTime), velocity, 0, default(Color), 2f);
			if (!Main.dayTime)
			{
				dust.noGravity = true;
			}
		}
		base.Projectile.active = false;
	}
}
