using System;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class ChronoIcicleLarge : ModProjectile, ILocalizedModType, IModType
{
	public static int HomingSpeed = 16;

	public static int IdleSpeedMax = 7;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.aiStyle = -1;
		base.Projectile.coldDamage = true;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.coldDamage = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 180;
		base.Projectile.alpha = 255;
	}

	public override void AI()
	{
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (player == null || player.dead)
		{
			base.Projectile.Kill();
		}
		base.Projectile.ai[1]++;
		float num = base.Projectile.ai[0];
		if (num != 0f)
		{
			if (num != 1f)
			{
				if (num == 2f)
				{
					if (base.Projectile.Calamity().defExtraUpdates == -1)
					{
						base.Projectile.Calamity().defExtraUpdates = base.Projectile.extraUpdates;
					}
					Vector2 destination = base.Projectile.Center;
					float maxDistance = 1400f;
					bool locatedTarget = false;
					for (int i = 0; i < Main.maxNPCs; i++)
					{
						float extraDistance = Main.npc[i].width / 2 + Main.npc[i].height / 2;
						if (Main.npc[i].CanBeChasedBy(base.Projectile) && base.Projectile.WithinRange(Main.npc[i].Center, maxDistance + extraDistance))
						{
							destination = Main.npc[i].Center;
							locatedTarget = true;
							break;
						}
					}
					if (locatedTarget)
					{
						base.Projectile.extraUpdates = base.Projectile.Calamity().defExtraUpdates + 1;
						Vector2 homeDirection = (destination - base.Projectile.Center).SafeNormalize(Vector2.UnitY);
						base.Projectile.velocity = (base.Projectile.velocity * 0.2f + homeDirection * (float)HomingSpeed) / 1.2f;
					}
					else
					{
						base.Projectile.extraUpdates = base.Projectile.Calamity().defExtraUpdates;
						if (((Vector2)(ref base.Projectile.velocity)).Length() < (float)IdleSpeedMax)
						{
							Projectile projectile = base.Projectile;
							projectile.velocity *= 3f;
						}
					}
				}
			}
			else
			{
				if (((Vector2)(ref base.Projectile.velocity)).Length() < (float)IdleSpeedMax)
				{
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= 3f;
				}
				if (base.Projectile.ai[1] > 20f)
				{
					base.Projectile.ai[0] = 2f;
				}
			}
		}
		else
		{
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 0.01f)
			{
				Projectile projectile3 = base.Projectile;
				projectile3.velocity *= 0.001f;
			}
			bool isCounter = base.Projectile.ai[2] < 0f;
			float dist = ((Math.Abs(base.Projectile.ai[2]) == 12f) ? 160f : MathHelper.Lerp(210f, 160f, Utils.GetLerpValue(0f, 5f, base.Projectile.ai[1], clamped: true)));
			if (Main.player[base.Projectile.owner].active && !Main.player[base.Projectile.owner].dead)
			{
				float shardNum = Math.Abs(base.Projectile.ai[2]) - 1f;
				float aivar = (isCounter ? (1f - shardNum - 1f) : shardNum);
				base.Projectile.position = Main.player[base.Projectile.owner].Center + ((float)Math.PI / 6f * aivar - (float)Math.PI / 2f).ToRotationVector2() * dist - base.Projectile.Size / 2f;
			}
			if (base.Projectile.ai[1] >= (12f - Math.Abs(base.Projectile.ai[2])) * 5f + 2f)
			{
				SoundEngine.PlaySound(in SoundID.NPCHit5, base.Projectile.Center);
				Vector2 dspeed = default(Vector2);
				for (int j = 0; j < 10; j++)
				{
					int dusttype = (Main.rand.NextBool() ? 68 : 67);
					if (Main.rand.NextBool(4))
					{
						dusttype = 80;
					}
					((Vector2)(ref dspeed))._002Ector(Main.rand.NextFloat(-7f, 7f), Main.rand.NextFloat(-7f, 7f));
					int dust = Dust.NewDust(base.Projectile.Center, 1, 1, dusttype, dspeed.X, dspeed.Y, 50, default(Color), 1.1f);
					Main.dust[dust].noGravity = true;
				}
				base.Projectile.ai[0] = 1f;
				base.Projectile.ai[1] = 0f;
			}
			if (Math.Abs(base.Projectile.ai[2]) >= 11f)
			{
				base.Projectile.alpha -= 150;
			}
			else
			{
				base.Projectile.alpha -= 30;
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.position);
		target.AddBuff(ModContent.BuffType<TimeDistortion>(), 60);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		for (int index1 = 0; index1 < 3; index1++)
		{
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 76);
			Main.dust[index2].noGravity = true;
			Main.dust[index2].noLight = true;
			Main.dust[index2].scale = 0.7f;
		}
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (base.Projectile.ai[0] > 0f)
		{
			return null;
		}
		return false;
	}
}
