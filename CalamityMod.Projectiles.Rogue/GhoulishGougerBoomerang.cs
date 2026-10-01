using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class GhoulishGougerBoomerang : ModProjectile, ILocalizedModType, IModType
{
	private const int FramesBeforeReturning = 50;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/GhoulishGouger";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 7;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 58;
		base.Projectile.height = 58;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 36;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.7f, 0f, 0.15f);
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.spriteDirection = base.Projectile.direction;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] == 50f)
		{
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.ai[0] >= 50f)
		{
			float acceleration = 1.15f;
			Player owner = Main.player[base.Projectile.owner];
			Vector2 delta = owner.Center - base.Projectile.Center;
			float dx = delta.X;
			float dy = delta.Y;
			float dist = ((Vector2)(ref delta)).Length();
			if (dist > 3000f)
			{
				base.Projectile.Kill();
			}
			dist = 16f / dist;
			dx *= dist;
			dy *= dist;
			if (base.Projectile.velocity.X < dx)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X + acceleration;
				if (base.Projectile.velocity.X < 0f && dx > 0f)
				{
					base.Projectile.velocity.X = base.Projectile.velocity.X + acceleration;
				}
			}
			else if (base.Projectile.velocity.X > dx)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X - acceleration;
				if (base.Projectile.velocity.X > 0f && dx < 0f)
				{
					base.Projectile.velocity.X = base.Projectile.velocity.X - acceleration;
				}
			}
			if (base.Projectile.velocity.Y < dy)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y + acceleration;
				if (base.Projectile.velocity.Y < 0f && dy > 0f)
				{
					base.Projectile.velocity.Y = base.Projectile.velocity.Y + acceleration;
				}
			}
			else if (base.Projectile.velocity.Y > dy)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y - acceleration;
				if (base.Projectile.velocity.Y > 0f && dy < 0f)
				{
					base.Projectile.velocity.Y = base.Projectile.velocity.Y - acceleration;
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
		float spin = ((base.Projectile.spriteDirection <= 0) ? (-1f) : 1f);
		base.Projectile.rotation += spin * 0.31f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(37f, 34f);
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Rogue/GhoulishGougerGlow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, origin, 1f, (SpriteEffects)0);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer && base.Projectile.Calamity().stealthStrike && base.Projectile.numHits <= 0)
		{
			int numSouls = 8;
			int projID = ModContent.ProjectileType<PhantasmalSoul>();
			int soulDamage = base.Projectile.damage;
			float soulKB = 0f;
			float speed = 6f;
			Vector2 velocity = Main.rand.NextVector2CircularEdge(speed, speed);
			for (int i = 0; i < numSouls; i++)
			{
				float ai1 = Main.rand.NextFloat() + 0.5f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, projID, soulDamage, soulKB, base.Projectile.owner, 0f, ai1);
				velocity = velocity.RotatedBy((float)Math.PI * 2f / (float)numSouls);
			}
		}
	}
}
