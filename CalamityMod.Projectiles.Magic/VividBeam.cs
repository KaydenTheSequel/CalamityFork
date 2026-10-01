using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class VividBeam : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 100;
		base.Projectile.timeLeft = 240;
	}

	public override void AI()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			SoundEngine.PlaySound(in VividClarity.BeamSound, base.Projectile.Center);
			initialized = true;
			float dustAmt = 16f;
			for (int d = 0; (float)d < dustAmt; d++)
			{
				Vector2 offset = Vector2.UnitX * 0f;
				offset += -Vector2.UnitY.RotatedBy((float)d * ((float)Math.PI * 2f / dustAmt)) * new Vector2(1f, 4f);
				offset = offset.RotatedBy(base.Projectile.velocity.ToRotation());
				int i = Dust.NewDust(base.Projectile.Center, 0, 0, 66, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
				Main.dust[i].scale = 1.5f;
				Main.dust[i].noGravity = true;
				Main.dust[i].position = base.Projectile.Center + offset;
				Main.dust[i].velocity = base.Projectile.velocity * 0f + offset.SafeNormalize(Vector2.UnitY) * 1f;
			}
		}
		float pi = (float)Math.PI;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] == 48f)
		{
			base.Projectile.ai[0] = 0f;
		}
		else
		{
			for (int j = 0; j < 2; j++)
			{
				Vector2 offset2 = Vector2.UnitX * -12f;
				offset2 = -Vector2.UnitY.RotatedBy(base.Projectile.ai[0] * pi / 24f + (float)j * pi) * new Vector2(5f, 10f) - base.Projectile.rotation.ToRotationVector2() * 10f;
				int i2 = Dust.NewDust(base.Projectile.Center, 0, 0, 66, 0f, 0f, 160, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
				Main.dust[i2].scale = 0.75f;
				Main.dust[i2].noGravity = true;
				Main.dust[i2].position = base.Projectile.Center + offset2;
				Main.dust[i2].velocity = base.Projectile.velocity;
			}
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 4f)
		{
			for (int k = 0; k < 2; k++)
			{
				Vector2 source = base.Projectile.position;
				source -= base.Projectile.velocity * ((float)k * 0.25f);
				int i3 = Dust.NewDust(source, 1, 1, 66, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
				Main.dust[i3].noGravity = true;
				Main.dust[i3].position = source;
				Main.dust[i3].scale = Main.rand.NextFloat(0.91f, 1.417f);
				Dust obj = Main.dust[i3];
				obj.velocity *= 0.1f;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (base.Projectile.owner == Main.myPlayer)
		{
			SummonLasers();
		}
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.owner == Main.myPlayer)
		{
			SummonLasers();
		}
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (base.Projectile.owner == Main.myPlayer)
		{
			SummonLasers();
		}
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	private void SummonLasers()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		IEntitySource source = base.Projectile.GetSource_FromThis();
		float num = base.Projectile.ai[1];
		if (num != 0f)
		{
			if (num != 1f)
			{
				if (num == 2f)
				{
					float offset = Main.rand.NextFloat((float)Math.PI * 2f);
					for (int i = 0; i < 8; i++)
					{
						Vector2 velocity = ((float)Math.PI * 2f * (float)i / 8f - offset).ToRotationVector2() * 4f;
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<VividLaser2>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
					}
				}
			}
			else
			{
				Projectile.NewProjectile(source, base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<VividExplosion>(), base.Projectile.damage * 2, base.Projectile.knockBack, base.Projectile.owner);
			}
		}
		else
		{
			CalamityUtils.ProjectileRain(source, base.Projectile.Center, 320f, 100f, 400f, 640f, 6f, ModContent.ProjectileType<VividClarityBeam>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
	}
}
