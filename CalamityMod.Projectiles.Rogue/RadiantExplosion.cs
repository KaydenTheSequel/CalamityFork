using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using CalamityMod.Dusts;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class RadiantExplosion : ModProjectile, ILocalizedModType, IModType
{
	private bool updatedTime;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 150;
		base.Projectile.height = 150;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 10;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 35;
	}

	public override void AI()
	{
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 1f || (base.Projectile.Calamity().stealthStrike && !updatedTime))
		{
			base.Projectile.timeLeft = 100;
			base.Projectile.ai[0] = 0f;
			updatedTime = true;
		}
		if (base.Projectile.timeLeft >= (updatedTime ? 80 : 6))
		{
			for (int i = 0; i < 5; i++)
			{
				int dusty = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralBlue>(), 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[dusty].noGravity = true;
				Dust obj = Main.dust[dusty];
				obj.velocity *= 0f;
			}
			for (int j = 0; j < 5; j++)
			{
				int dusty2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[dusty2].noGravity = true;
				Dust obj2 = Main.dust[dusty2];
				obj2.velocity *= 0f;
			}
		}
		if (!base.Projectile.Calamity().stealthStrike)
		{
			return;
		}
		float projX = base.Projectile.Center.X;
		float projY = base.Projectile.Center.Y;
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
			if (Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - npcCenterX) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - npcCenterY) < 600f)
			{
				if (n.position.X < projX)
				{
					n.velocity.X += 0.25f;
				}
				else
				{
					n.velocity.X -= 0.25f;
				}
				if (n.position.Y < projY)
				{
					n.velocity.Y += 0.25f;
				}
				else
				{
					n.velocity.Y -= 0.25f;
				}
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 120);
		OnHitEffect(target.Center);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 120);
		OnHitEffect(target.Center);
	}

	private void OnHitEffect(Vector2 targetPos)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		IEntitySource source = base.Projectile.GetSource_FromThis();
		for (int n = 0; n < 3; n++)
		{
			int projType = Utils.SelectRandom<int>(Main.rand, ModContent.ProjectileType<AstralStar>(), 726, 955);
			Projectile star = CalamityUtils.ProjectileRain(source, targetPos, 400f, 100f, 500f, 800f, 25f, projType, (int)((double)base.Projectile.damage * 0.75), base.Projectile.knockBack * 0.75f, base.Projectile.owner);
			if (star.whoAmI.WithinBounds(Main.maxProjectiles))
			{
				star.DamageType = RogueDamageClass.Instance;
				star.ai[0] = 2f;
			}
		}
	}
}
