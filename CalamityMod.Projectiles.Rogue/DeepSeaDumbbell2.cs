using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class DeepSeaDumbbell2 : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/DeepSeaDumbbell";

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 26;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
	}

	public override void AI()
	{
		if (base.Projectile.ai[0] < 60f)
		{
			base.Projectile.ai[0]++;
		}
		else
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 300f, 12f, 20f);
		}
		base.Projectile.rotation += Math.Abs(base.Projectile.velocity.X) * 0.01f * (float)base.Projectile.direction;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.NPCDeath43 with
		{
			Volume = SoundID.NPCDeath43.Volume * 0.35f
		};
		SoundEngine.PlaySound(in style, base.Projectile.position);
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.position.X, base.Projectile.position.Y, base.Projectile.velocity.X, base.Projectile.velocity.Y, ModContent.ProjectileType<DeepSeaDumbbell3>(), (int)((double)base.Projectile.damage * 0.75), base.Projectile.knockBack * 0.75f, Main.myPlayer);
			float randVel = (float)Main.rand.Next(-35, 36) * 0.01f;
			float randVel2 = (float)Main.rand.Next(-35, 36) * 0.01f;
			for (int i = 0; i < 2; i++)
			{
				if (i == 1)
				{
					randVel *= 10f;
					randVel2 *= 10f;
				}
				else
				{
					randVel *= -10f;
					randVel2 *= -10f;
				}
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.position.X, base.Projectile.position.Y, randVel, randVel2, ModContent.ProjectileType<DeepSeaDumbbellWeight>(), (int)((double)base.Projectile.damage * 0.25), base.Projectile.knockBack * 0.25f, Main.myPlayer);
			}
		}
		base.Projectile.Kill();
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<HadopelagicPressure>(), 180);
		SoundStyle style = SoundID.NPCDeath43 with
		{
			Volume = SoundID.NPCDeath43.Volume * 0.35f
		};
		SoundEngine.PlaySound(in style, base.Projectile.position);
		base.Projectile.velocity.X = 0f - base.Projectile.velocity.X;
		base.Projectile.velocity.Y = 0f - base.Projectile.velocity.Y;
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.position.X, base.Projectile.position.Y, base.Projectile.velocity.X, base.Projectile.velocity.Y, ModContent.ProjectileType<DeepSeaDumbbell3>(), (int)((double)base.Projectile.damage * 0.75), base.Projectile.knockBack * 0.75f, Main.myPlayer);
			float randVel = (float)Main.rand.Next(-35, 36) * 0.01f;
			float randVel2 = (float)Main.rand.Next(-35, 36) * 0.01f;
			for (int i = 0; i < 2; i++)
			{
				if (i == 1)
				{
					randVel *= 10f;
					randVel2 *= 10f;
				}
				else
				{
					randVel *= -10f;
					randVel2 *= -10f;
				}
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.position.X, base.Projectile.position.Y, randVel, randVel2, ModContent.ProjectileType<DeepSeaDumbbellWeight>(), (int)((double)base.Projectile.damage * 0.25), base.Projectile.knockBack * 0.25f, Main.myPlayer);
			}
		}
		base.Projectile.Kill();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<HadopelagicPressure>(), 180);
		SoundStyle style = SoundID.NPCDeath43 with
		{
			Volume = SoundID.NPCDeath43.Volume * 0.35f
		};
		SoundEngine.PlaySound(in style, base.Projectile.position);
		base.Projectile.velocity.X = 0f - base.Projectile.velocity.X;
		base.Projectile.velocity.Y = 0f - base.Projectile.velocity.Y;
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.position.X, base.Projectile.position.Y, base.Projectile.velocity.X, base.Projectile.velocity.Y, ModContent.ProjectileType<DeepSeaDumbbell3>(), (int)((double)base.Projectile.damage * 0.75), base.Projectile.knockBack * 0.75f, Main.myPlayer);
			float randVel = (float)Main.rand.Next(-35, 36) * 0.01f;
			float randVel2 = (float)Main.rand.Next(-35, 36) * 0.01f;
			for (int i = 0; i < 2; i++)
			{
				if (i == 1)
				{
					randVel *= 10f;
					randVel2 *= 10f;
				}
				else
				{
					randVel *= -10f;
					randVel2 *= -10f;
				}
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.position.X, base.Projectile.position.Y, randVel, randVel2, ModContent.ProjectileType<DeepSeaDumbbellWeight>(), (int)((double)base.Projectile.damage * 0.25), base.Projectile.knockBack * 0.25f, Main.myPlayer);
			}
		}
		base.Projectile.Kill();
	}
}
