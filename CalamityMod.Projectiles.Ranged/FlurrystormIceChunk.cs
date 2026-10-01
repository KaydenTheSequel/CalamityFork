using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class FlurrystormIceChunk : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.aiStyle = 1;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.coldDamage = true;
		base.Projectile.penetrate = 1;
	}

	public override void AI()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool())
		{
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 68, base.Projectile.velocity.X, base.Projectile.velocity.Y, 0, default(Color), 1.1f);
			Main.dust[index2].noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(44, 180);
		target.AddBuff(ModContent.BuffType<GlacialState>(), 30);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.position);
		for (int index1 = 0; index1 < 5; index1++)
		{
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 68);
			Main.dust[index2].noGravity = true;
			Dust obj = Main.dust[index2];
			obj.velocity *= 1.5f;
			Main.dust[index2].scale *= 0.9f;
		}
		for (int split = 0; split < 3; split++)
		{
			float shardspeedX = (0f - base.Projectile.velocity.X) * Main.rand.NextFloat(0.5f, 0.7f) + Main.rand.NextFloat(-3f, 3f);
			float shardspeedY = (0f - base.Projectile.velocity.Y) * (float)Main.rand.Next(50, 70) * 0.01f + (float)Main.rand.Next(-8, 9) * 0.2f;
			if (shardspeedX < 2f && shardspeedX > -2f)
			{
				shardspeedX += 0f - base.Projectile.velocity.X;
			}
			if (shardspeedY > 2f && shardspeedY < 2f)
			{
				shardspeedY += 0f - base.Projectile.velocity.Y;
			}
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.position.X + shardspeedX, base.Projectile.position.Y + shardspeedY, shardspeedX, shardspeedY, ModContent.ProjectileType<FlurrystormIceShard>(), (int)((double)base.Projectile.damage * 0.3), 2f, base.Projectile.owner);
		}
	}
}
