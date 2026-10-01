using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class RadiantStarKnife : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/RadiantStar";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 4;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
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
				NPC npc = enumerator.Current;
				if (!npc.CanBeChasedBy(base.Projectile) || !Collision.CanHit(base.Projectile.Center, 1, 1, npc.Center, 1, 1) || npc.boss)
				{
					continue;
				}
				float npcX = npc.position.X + (float)(npc.width / 2);
				float npcY = npc.position.Y + (float)(npc.height / 2);
				if (Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - npcX) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - npcY) < homeRange)
				{
					if (npc.position.X < projX)
					{
						npc.velocity.X += homingSpeed;
					}
					else
					{
						npc.velocity.X -= homingSpeed;
					}
					if (npc.position.Y < projY)
					{
						npc.velocity.Y += homingSpeed;
					}
					else
					{
						npc.velocity.Y -= homingSpeed;
					}
				}
			}
		}
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] != 25f)
		{
			return;
		}
		int numProj = (base.Projectile.Calamity().stealthStrike ? 7 : 3);
		MathHelper.ToRadians(50f);
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		Vector2 speed = default(Vector2);
		for (int i = 0; i < numProj; i++)
		{
			((Vector2)(ref speed))._002Ector((float)Main.rand.Next(-50, 51), (float)Main.rand.Next(-50, 51));
			while (speed.X == 0f && speed.Y == 0f)
			{
				((Vector2)(ref speed))._002Ector((float)Main.rand.Next(-50, 51), (float)Main.rand.Next(-50, 51));
			}
			((Vector2)(ref speed)).Normalize();
			speed *= (float)Main.rand.Next(30, 61) * 0.1f * 2.5f;
			int stabber2 = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, speed.X, speed.Y, ModContent.ProjectileType<RadiantStar2>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, (base.Projectile.ai[0] == 1f) ? 1f : 0f);
			Main.projectile[stabber2].Calamity().stealthStrike = base.Projectile.Calamity().stealthStrike;
		}
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
		int boomer = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<RadiantExplosion>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		if (base.Projectile.Calamity().stealthStrike && boomer.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[boomer].Calamity().stealthStrike = true;
			Main.projectile[boomer].height = 300;
			Main.projectile[boomer].width = 300;
		}
		Main.projectile[boomer].Center = base.Projectile.Center;
		base.Projectile.active = false;
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
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.position);
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralBlue>(), base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}
}
