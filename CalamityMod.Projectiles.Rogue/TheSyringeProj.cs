using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class TheSyringeProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/TheSyringe";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.light = 0.5f;
		base.Projectile.extraUpdates = 1;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ignoreWater = true;
		base.Projectile.aiStyle = 1;
		base.AIType = 242;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
	}

	public override void AI()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(8))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, Main.rand.NextBool(2) ? 107 : 89, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
		base.Projectile.damage += base.Projectile.originalDamage / 200;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 240);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 240);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(100);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.Item107, base.Projectile.position);
		for (int k = 0; k < 7; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 89, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y);
		}
		int fireAmt = Main.rand.Next(1, 3);
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int f = 0; f < fireAmt; f++)
			{
				Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<TheSyringeCinder>(), (int)((double)base.Projectile.damage * 0.5), 0f, Main.myPlayer);
			}
			for (int s = 0; s < 2; s++)
			{
				float SpeedX = (0f - base.Projectile.velocity.X) * Main.rand.NextFloat(0.4f, 0.7f) + Main.rand.NextFloat(-8f, 8f);
				float SpeedY = (0f - base.Projectile.velocity.Y) * Main.rand.NextFloat(0.4f, 0.7f) + Main.rand.NextFloat(-8f, 8f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X + SpeedX, base.Projectile.Center.Y + SpeedY, SpeedX, SpeedY, ModContent.ProjectileType<TheSyringeS1>(), (int)((double)base.Projectile.damage * 0.25), 0f, Main.myPlayer, Main.rand.Next(3));
			}
		}
		if (base.Projectile.owner == Main.myPlayer && base.Projectile.ai[1] == 1f)
		{
			for (int b = 0; b < 5; b++)
			{
				float speedX = Main.rand.NextFloat(-0.7f, 0.7f);
				float speedY = Main.rand.NextFloat(-0.7f, 0.7f);
				int bee = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, speedX, speedY, ModContent.ProjectileType<PlaguenadeBee>(), (int)((double)base.Projectile.damage * 0.5), 0f, Main.myPlayer);
				Main.projectile[bee].penetrate = 1;
			}
		}
	}
}
