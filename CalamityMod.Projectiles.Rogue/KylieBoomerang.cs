using System;
using CalamityMod.Items.Weapons.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class KylieBoomerang : ModProjectile, ILocalizedModType, IModType
{
	public int TileBounceDelay;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/Kylie";

	public ref float State => ref base.Projectile.ai[0];

	public ref float Timer => ref base.Projectile.ai[1];

	public override void SetDefaults()
	{
		base.Projectile.friendly = true;
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 240;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		base.Projectile.rotation += 0.2f;
		if (Main.rand.NextBool(15))
		{
			int d = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 7, base.Projectile.velocity.X * 0.25f, base.Projectile.velocity.Y * 0.25f, 100, default(Color), 0f);
			Main.dust[d].position = base.Projectile.Center;
		}
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = 15;
			SoundEngine.PlaySound(in SoundID.Item7, base.Projectile.position);
		}
		if (base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.tileCollide = true;
			if (TileBounceDelay > 0)
			{
				TileBounceDelay--;
			}
			Vector2 mousePos = Owner.ClampedMouseWorld();
			if (Vector2.Distance(base.Projectile.Center, mousePos) > 115f && TileBounceDelay == 0)
			{
				float accelerationFactor = 12f;
				Projectile projectile = base.Projectile;
				projectile.velocity += (mousePos - base.Projectile.Center).SafeNormalize(Vector2.UnitX) * Kylie.Speed / accelerationFactor;
				if (((Vector2)(ref base.Projectile.velocity)).Length() > Kylie.Speed)
				{
					((Vector2)(ref base.Projectile.velocity)).Normalize();
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= Kylie.Speed;
				}
			}
			else
			{
				Projectile projectile3 = base.Projectile;
				projectile3.velocity *= 1.1f;
				if (((Vector2)(ref base.Projectile.velocity)).Length() > Kylie.Speed)
				{
					((Vector2)(ref base.Projectile.velocity)).Normalize();
					Projectile projectile4 = base.Projectile;
					projectile4.velocity *= Kylie.Speed;
				}
			}
			return;
		}
		if (State == 0f)
		{
			Timer++;
			if (Timer == 3f)
			{
				base.Projectile.tileCollide = true;
			}
			if (Timer >= 35f)
			{
				State = 1f;
				Timer = 0f;
				base.Projectile.netUpdate = true;
			}
			return;
		}
		base.Projectile.tileCollide = false;
		float num = Kylie.Speed * 1.5f;
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
		dist = num / dist;
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

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		State = 1f;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position + base.Projectile.velocity, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.position);
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		State = 1f;
		if (TileBounceDelay == 0)
		{
			TileBounceDelay = 10;
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().stealthStrike)
		{
			Vector2 splinterVel = base.Projectile.velocity.RotatedByRandom(0.2617993950843811);
			Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.Center, splinterVel, base.Mod.Find<ModGore>("KylieGore1").Type);
			Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.Center, splinterVel, base.Mod.Find<ModGore>("KylieGore2").Type);
		}
	}
}
