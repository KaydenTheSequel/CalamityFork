using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ShatteredDawnKnife : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/ShatteredDawn";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 56;
		base.Projectile.height = 56;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 4;
		base.Projectile.timeLeft = 300;
		base.Projectile.alpha = 255;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] < 5f)
		{
			base.Projectile.alpha -= 50;
		}
		if (base.Projectile.ai[1] == 5f)
		{
			base.Projectile.alpha = 0;
			base.Projectile.tileCollide = false;
		}
		if (base.Projectile.ai[1] == 20f)
		{
			int numProj = 5;
			if (base.Projectile.owner == Main.myPlayer)
			{
				int spread = 6;
				int projID = ModContent.ProjectileType<ShatteredDawnScorchedBlade>();
				int splitDamage = (int)(0.75f * (float)base.Projectile.damage);
				float splitKB = 1f;
				for (int i = 0; i < numProj; i++)
				{
					Vector2 perturbedspeed = Utils.RotatedBy(new Vector2(base.Projectile.velocity.X, base.Projectile.velocity.Y + (float)Main.rand.Next(-3, 4)), (double)MathHelper.ToRadians((float)spread), default(Vector2));
					int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, perturbedspeed * 0.2f, projID, splitDamage, splitKB, base.Projectile.owner);
					Main.projectile[proj].Calamity().stealthStrike = base.Projectile.Calamity().stealthStrike;
					spread -= Main.rand.Next(2, 6);
				}
				SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.position);
				base.Projectile.active = false;
				for (int j = 0; j < 8; j++)
				{
					int dusty = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 127, 0f, 0f, 100, default(Color), 2f);
					Dust obj = Main.dust[dusty];
					obj.velocity *= 3f;
					if (Main.rand.NextBool())
					{
						Main.dust[dusty].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int k = 0; k < 16; k++)
				{
					int dusty2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 127, 0f, 0f, 100, default(Color), 3f);
					Main.dust[dusty2].noGravity = true;
					Dust obj2 = Main.dust[dusty2];
					obj2.velocity *= 5f;
					dusty2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 127, 0f, 0f, 100, default(Color), 2f);
					Dust obj3 = Main.dust[dusty2];
					obj3.velocity *= 2f;
				}
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

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	private void ShatteredExplosion()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		int projID = ModContent.ProjectileType<ShatteredExplosion>();
		int explosionDamage = (int)((float)base.Projectile.damage * 0.45f);
		float explosionKB = 3f;
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, projID, explosionDamage, explosionKB, base.Projectile.owner);
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		ShatteredExplosion();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		ShatteredExplosion();
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		ShatteredExplosion();
		return true;
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
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 246, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}
}
