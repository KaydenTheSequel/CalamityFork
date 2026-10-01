using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class TitaniumShurikenProjectile : ModProjectile, ILocalizedModType, IModType
{
	private static float RotationIncrement = 0.22f;

	private static float ReboundTime = 26f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/TitaniumShuriken";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 34;
		base.Projectile.height = 34;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 3;
		base.Projectile.aiStyle = 2;
		base.Projectile.timeLeft = 600;
		base.AIType = 48;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30;
	}

	public override void AI()
	{
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		if (!base.Projectile.Calamity().stealthStrike)
		{
			return;
		}
		base.DrawOffsetX = -11;
		base.DrawOriginOffsetY = -10;
		base.DrawOriginOffsetX = 0f;
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.ai[1]++;
			if (base.Projectile.ai[1] >= ReboundTime)
			{
				base.Projectile.ai[0] = 1f;
				base.Projectile.ai[1] = 0f;
				base.Projectile.netUpdate = true;
				base.Projectile.ResetLocalNPCHitImmunity();
			}
		}
		else
		{
			base.Projectile.tileCollide = false;
			float acceleration = 3.2f;
			Player owner = Main.player[base.Projectile.owner];
			Vector2 center = owner.Center;
			float xDist = center.X - base.Projectile.Center.X;
			float yDist = center.Y - base.Projectile.Center.Y;
			float dist = (float)Math.Sqrt(xDist * xDist + yDist * yDist);
			if (dist > 3000f)
			{
				base.Projectile.Kill();
			}
			dist = 16f / dist;
			xDist *= dist;
			yDist *= dist;
			if (base.Projectile.velocity.X < xDist)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X + acceleration;
				if (base.Projectile.velocity.X < 0f && xDist > 0f)
				{
					base.Projectile.velocity.X += acceleration;
				}
			}
			else if (base.Projectile.velocity.X > xDist)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X - acceleration;
				if (base.Projectile.velocity.X > 0f && xDist < 0f)
				{
					base.Projectile.velocity.X -= acceleration;
				}
			}
			if (base.Projectile.velocity.Y < yDist)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y + acceleration;
				if (base.Projectile.velocity.Y < 0f && yDist > 0f)
				{
					base.Projectile.velocity.Y += acceleration;
				}
			}
			else if (base.Projectile.velocity.Y > yDist)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y - acceleration;
				if (base.Projectile.velocity.Y > 0f && yDist < 0f)
				{
					base.Projectile.velocity.Y -= acceleration;
				}
			}
			if (Main.myPlayer == base.Projectile.owner)
			{
				Rectangle hitbox = base.Projectile.Hitbox;
				if (((Rectangle)(ref hitbox)).Intersects(owner.Hitbox))
				{
					base.Projectile.Kill();
				}
			}
		}
		base.Projectile.rotation += RotationIncrement;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().stealthStrike)
		{
			if (base.Projectile.velocity.X != oldVelocity.X)
			{
				base.Projectile.velocity.X = 0f - oldVelocity.X;
			}
			if (base.Projectile.velocity.Y != oldVelocity.Y)
			{
				base.Projectile.velocity.Y = 0f - oldVelocity.Y;
			}
			return false;
		}
		return true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		if (base.Projectile.Calamity().stealthStrike)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		}
		else
		{
			Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		SpawnStealthClones();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		SpawnStealthClones();
	}

	private void SpawnStealthClones()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().stealthStrike && base.Projectile.numHits < 3)
		{
			for (int i = 0; i < 4; i++)
			{
				Vector2 randSpeed = Main.rand.NextVector2CircularEdge(6.5f, 6.5f) * Main.rand.NextFloat(0.4f, 1f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, randSpeed, ModContent.ProjectileType<TitaniumClone>(), (int)((float)base.Projectile.damage * 0.8f), base.Projectile.knockBack, base.Projectile.owner);
			}
		}
	}
}
