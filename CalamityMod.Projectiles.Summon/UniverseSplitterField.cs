using System;
using System.IO;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class UniverseSplitterField : ModProjectile, ILocalizedModType, IModType
{
	public float DustRadius;

	public const float DustChargeTime = 30f;

	public const float DustMinRadius = 0f;

	public const float DustMaxRadius = 90f;

	public const int SpiralPrecision = 25;

	public const int SpiralRings = 5;

	public const int TimeLeft = 720;

	public const float SmallBeamAngleMax = (float)Math.PI * 2f / 15f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public float Timer
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 200);
		base.Projectile.alpha = 255;
		base.Projectile.friendly = true;
		base.Projectile.minion = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 720;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(DustRadius);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		DustRadius = reader.ReadSingle();
	}

	public override void AI()
	{
		Timer++;
		if (Timer < 30f)
		{
			DustRadius = MathHelper.Lerp(0f, 90f, Timer / 30f);
		}
		else
		{
			DustRadius = 90f + (float)Math.Sin(Timer / 50f) * 16f;
		}
		if (!Main.dedServ)
		{
			GenerateIdleDust();
		}
		SpawnLasers();
	}

	public void GenerateIdleDust()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 80; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + ((float)i / 80f * ((float)Math.PI * 2f)).ToRotationVector2() * DustRadius, 247);
			dust.velocity = Vector2.Zero;
			dust.scale = 1.2f;
			dust.noGravity = true;
		}
		for (int j = 0; j < 25; j++)
		{
			for (int direction = -1; direction <= 1; direction += 2)
			{
				for (int k = 0; k < 5; k++)
				{
					Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + Vector2.UnitY.RotatedBy(Timer / 25f * (float)direction).RotatedBy((float)k / 5f * ((float)Math.PI * 2f)).RotatedBy((float)j / 25f * ((float)Math.PI * 2f) / 5f * (float)direction) * DustRadius * (float)j / 25f, 261);
					dust2.velocity = Vector2.Zero;
					dust2.scale = 0.7f;
					dust2.noGravity = true;
				}
			}
		}
		bool firingGiantLaserBeam = Timer > 540f;
		for (int l = 0; l < (firingGiantLaserBeam ? 30 : 16); l++)
		{
			Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center, 247);
			dust3.velocity = Main.rand.NextVector2Circular(8f, 8f) * (firingGiantLaserBeam ? 1.6f : 1f);
			dust3.noGravity = true;
			dust3.scale = 1.25f;
		}
		if (Timer > 420f)
		{
			float outwardCircleRadius = MathHelper.Lerp(0f, DustRadius * 1.2f, MathHelper.Clamp((Timer - 420f) / 40f, 0f, 1f));
			for (int m = 0; m < 95; m++)
			{
				Dust dust4 = Dust.NewDustPerfect(base.Projectile.Center + ((float)m / 95f * ((float)Math.PI * 2f)).ToRotationVector2() * outwardCircleRadius, 247);
				dust4.scale = 1.2f;
				dust4.noGravity = true;
				dust4.velocity = (Main.rand.NextBool(7) ? (base.Projectile.DirectionFrom(dust4.position) * 6f) : Vector2.Zero);
			}
		}
	}

	public void SpawnLasers()
	{
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		if (Timer > 120f && Timer < 540f && Timer % 60f == 0f)
		{
			SoundEngine.PlaySound(in CommonCalamitySounds.PlasmaBoltSound, base.Projectile.Center);
			if (Main.myPlayer == base.Projectile.owner)
			{
				Vector2 offset = default(Vector2);
				((Vector2)(ref offset))._002Ector(Main.rand.NextFloat(-800f, 800f), -1460f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + offset, -Vector2.Normalize(offset), ModContent.ProjectileType<UniverseSplitterSmallBeam>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, (-Vector2.Normalize(offset)).ToRotation());
			}
		}
		if (Timer == 540f && Main.myPlayer == base.Projectile.owner)
		{
			SoundEngine.PlaySound(in SoundID.Zombie104, base.Projectile.Center);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + Vector2.UnitY * -3000f / 2f, Vector2.UnitY, ModContent.ProjectileType<UniverseSplitterHugeBeam>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
