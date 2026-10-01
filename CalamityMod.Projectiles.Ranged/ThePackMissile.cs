using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ThePackMissile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 9;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 600;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		Vector2 targetCenter = base.Projectile.Center;
		float minTargetDistance = 2500f;
		bool homeIn = false;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (!n.CanBeChasedBy(base.Projectile) || !Collision.CanHit(base.Projectile.Center, 1, 1, n.Center, 1, 1))
			{
				continue;
			}
			float distanceFromTarget = base.Projectile.Center.ManhattanDistance(n.Center);
			if (distanceFromTarget < 200f)
			{
				if (base.Projectile.owner == Main.myPlayer)
				{
					for (int j = 0; j < 5; j++)
					{
						Vector2 velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(6f, 12f);
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<ThePackMinissile>(), (int)((double)base.Projectile.damage * 0.25), base.Projectile.knockBack, base.Projectile.owner);
					}
				}
				SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
				base.Projectile.Kill();
				return;
			}
			if (distanceFromTarget < minTargetDistance)
			{
				minTargetDistance = distanceFromTarget;
				targetCenter = n.Center;
				homeIn = true;
			}
		}
		if (homeIn)
		{
			base.Projectile.velocity = (base.Projectile.velocity * 15f + base.Projectile.SafeDirectionTo(targetCenter) * 30f) / 16f;
			return;
		}
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 4 % Main.projFrames[base.Type];
	}

	public override void OnKill(int timeLeft)
	{
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2);
		base.Projectile.width = 400;
		base.Projectile.height = 400;
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int i = 0; i < 40; i++)
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 255, 0f, 0f, 0, default(Color), 1.5f);
			Dust obj = Main.dust[dust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[dust].scale = 0.5f;
				Main.dust[dust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 60; j++)
		{
			int dust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 255, 0f, 0f, 0, default(Color), 2f);
			Main.dust[dust2].noGravity = true;
			Dust obj2 = Main.dust[dust2];
			obj2.velocity *= 5f;
			dust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 255, 0f, 0f, 0, default(Color), 1.5f);
			Dust obj3 = Main.dust[dust2];
			obj3.velocity *= 2f;
		}
		base.Projectile.Damage();
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
