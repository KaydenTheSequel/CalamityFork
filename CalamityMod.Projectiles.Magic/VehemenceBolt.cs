using System;
using CalamityMod.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class VehemenceBolt : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float Time => ref base.Projectile.ai[0];

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 2;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color darkRed = Color.DarkRed;
		Lighting.AddLight(center, ((Color)(ref darkRed)).ToVector3());
		if (Time == 0f)
		{
			GenerateInitialBurstDust();
		}
		GenerateHelicalDust();
		Time++;
	}

	private void GenerateInitialBurstDust()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = 0; i < 40; i++)
			{
				float angle = (float)Math.PI * 2f * (float)i / 40f;
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + base.Projectile.velocity * 7f, 27);
				dust.velocity = angle.ToRotationVector2() * 15f;
				dust.color = Color.Lerp(Color.Red, Color.MediumPurple, (float)Math.Sin(angle) * 0.5f + 0.5f);
				dust.scale = 1.6f;
				dust.noGravity = true;
			}
		}
	}

	private void GenerateHelicalDust()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = -1; i <= 1; i += 2)
			{
				Vector2 spawnOffset = Utils.RotatedBy(new Vector2((float)Math.Sin(Time / 45f * ((float)Math.PI * 2f)) * (float)i * 8f, 10f), (double)base.Projectile.rotation, default(Vector2));
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + spawnOffset, 235);
				dust.velocity = Vector2.Zero;
				dust.scale = 1.1f;
				dust.noGravity = true;
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item74, base.Projectile.Center);
		if (Main.myPlayer == base.Projectile.owner)
		{
			int skullID = ModContent.ProjectileType<VehemenceSkull>();
			int damage = (int)((float)base.Projectile.damage * Vehemence.SkullRatio);
			for (int i = 0; i < 18; i++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Main.rand.NextVector2Circular(12f, 12f), skullID, damage, base.Projectile.knockBack, base.Projectile.owner);
			}
		}
		if (Main.dedServ)
		{
			return;
		}
		for (int j = 0; j < 20; j++)
		{
			for (int k = 0; k < 4; k++)
			{
				Vector2 shootVelocity = base.Projectile.velocity.RotatedBy(MathHelper.Lerp(-0.35f, 0.35f, (float)k / 4f)) * Main.rand.NextFloat(0.75f, 1.1f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + shootVelocity.SafeNormalize(Vector2.Zero) * 10f, 5);
				dust.velocity = shootVelocity;
				dust.scale = MathHelper.Lerp(1.7f, 0.85f, (float)j / 20f);
			}
		}
		for (int l = 0; l < 60; l++)
		{
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool() ? 235 : 27);
			dust2.velocity = Main.rand.NextVector2Circular(18f, 18f);
			dust2.scale = 1.7f;
			dust2.noGravity = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}
}
