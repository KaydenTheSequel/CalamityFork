using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class TheSyringeCinder : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 120;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.alpha = 100;
	}

	public override void AI()
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] == 1f)
		{
			base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
			base.Projectile.rotation = (0f - base.Projectile.velocity.X) * 0.05f;
		}
		else
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + 4.712389f;
			base.Projectile.spriteDirection = ((!(base.Projectile.velocity.X > 0f)) ? 1 : (-1));
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.velocity.X != base.Projectile.velocity.X)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X * -0.1f;
		}
		if (base.Projectile.velocity.X != base.Projectile.velocity.X)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X * -0.5f;
		}
		if (base.Projectile.velocity.Y != base.Projectile.velocity.Y && base.Projectile.velocity.Y > 1f)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y * -0.5f;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 5f)
		{
			base.Projectile.ai[0] = 5f;
			if (base.Projectile.velocity.Y == 0f && base.Projectile.velocity.X != 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X * 0.97f;
				if ((double)base.Projectile.velocity.X > -0.01 && (double)base.Projectile.velocity.X < 0.01)
				{
					base.Projectile.velocity.X = 0f;
					base.Projectile.netUpdate = true;
				}
			}
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.2f;
		}
		if (Main.rand.NextBool(4))
		{
			int num199 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 89, 0f, 0f, 100);
			Dust obj = Main.dust[num199];
			obj.position.X -= 2f;
			obj.position.Y += 2f;
			obj.scale += (float)Main.rand.Next(50) * 0.01f;
			obj.noGravity = true;
			obj.velocity.Y -= 2f;
		}
		if (Main.rand.NextBool(10))
		{
			int num200 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 89, 0f, 0f, 100);
			Dust obj2 = Main.dust[num200];
			obj2.position.X -= 2f;
			obj2.position.Y += 2f;
			obj2.scale += 0.3f + (float)Main.rand.Next(50) * 0.01f;
			obj2.noGravity = true;
			obj2.velocity *= 0.1f;
		}
		if ((double)base.Projectile.velocity.Y < 0.25 && (double)base.Projectile.velocity.Y > 0.15)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X * 0.8f;
		}
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		base.Projectile.ai[1] = 1f;
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 120);
	}
}
