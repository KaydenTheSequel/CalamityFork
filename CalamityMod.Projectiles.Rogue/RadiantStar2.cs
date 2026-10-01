using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class RadiantStar2 : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/RadiantStar";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.timeLeft < 270 && target.CanBeChasedBy(base.Projectile);
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
		if (base.Projectile.ai[0] == 1f)
		{
			float projX = base.Projectile.Center.X;
			float projY = base.Projectile.Center.Y;
			float homeRange = (base.Projectile.Calamity().stealthStrike ? 1800f : 600f);
			float homingSpeed = 0.25f;
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC n = enumerator.Current;
				if (!n.CanBeChasedBy(base.Projectile) || !Collision.CanHit(base.Projectile.Center, 1, 1, n.Center, 1, 1) || CalamityPlayer.areThereAnyDamnBosses)
				{
					continue;
				}
				float npcCenterX = n.position.X + (float)(n.width / 2);
				float npcCenterY = n.position.Y + (float)(n.height / 2);
				if (Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - npcCenterX) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - npcCenterY) < homeRange)
				{
					if (n.position.X < projX)
					{
						n.velocity.X += homingSpeed;
					}
					else
					{
						n.velocity.X -= homingSpeed;
					}
					if (n.position.Y < projY)
					{
						n.velocity.Y += homingSpeed;
					}
					else
					{
						n.velocity.Y -= homingSpeed;
					}
				}
			}
		}
		else if (base.Projectile.timeLeft < 270)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, base.Projectile.Calamity().stealthStrike ? 900f : 450f, 12f, 20f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 120);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralBlue>(), base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}
}
