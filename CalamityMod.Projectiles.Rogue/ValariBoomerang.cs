using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ValariBoomerang : ModProjectile, ILocalizedModType, IModType
{
	public int TileCollideDelay;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/FrostcrushValari";

	public ref float State => ref base.Projectile.ai[0];

	public ref float Timer => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 240;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30;
		base.Projectile.coldDamage = true;
	}

	public override void AI()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		Timer++;
		base.Projectile.rotation += 0.2f;
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 67, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = 15;
			SoundEngine.PlaySound(in SoundID.Item7, base.Projectile.position);
		}
		if (Timer == 3f)
		{
			base.Projectile.tileCollide = true;
		}
		if (base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.tileCollide = true;
			if (TileCollideDelay > 0)
			{
				TileCollideDelay--;
			}
			Vector2 mousePos = Main.player[base.Projectile.owner].ClampedMouseWorld();
			if (Vector2.Distance(base.Projectile.Center, mousePos) > 115f && TileCollideDelay == 0)
			{
				float accelerationFactor = 9.5f;
				Projectile projectile = base.Projectile;
				projectile.velocity += (mousePos - base.Projectile.Center).SafeNormalize(Vector2.UnitX) * FrostcrushValari.Speed / accelerationFactor;
				if (((Vector2)(ref base.Projectile.velocity)).Length() > FrostcrushValari.Speed)
				{
					((Vector2)(ref base.Projectile.velocity)).Normalize();
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= FrostcrushValari.Speed;
				}
			}
			else
			{
				Projectile projectile3 = base.Projectile;
				projectile3.velocity *= 1.1f;
				if (((Vector2)(ref base.Projectile.velocity)).Length() > FrostcrushValari.Speed)
				{
					((Vector2)(ref base.Projectile.velocity)).Normalize();
					Projectile projectile4 = base.Projectile;
					projectile4.velocity *= FrostcrushValari.Speed;
				}
			}
			return;
		}
		if (State == 0f)
		{
			if (Timer >= 50f)
			{
				State = 1f;
				base.Projectile.netUpdate = true;
			}
			return;
		}
		base.Projectile.tileCollide = false;
		float num = FrostcrushValari.Speed * 1.5f;
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
			base.Projectile.velocity.X += acceleration;
			if (base.Projectile.velocity.X < 0f && xDist > 0f)
			{
				base.Projectile.velocity.X += acceleration;
			}
		}
		else if (base.Projectile.velocity.X > xDist)
		{
			base.Projectile.velocity.X -= acceleration;
			if (base.Projectile.velocity.X > 0f && xDist < 0f)
			{
				base.Projectile.velocity.X -= acceleration;
			}
		}
		if (base.Projectile.velocity.Y < yDist)
		{
			base.Projectile.velocity.Y += acceleration;
			if (base.Projectile.velocity.Y < 0f && yDist > 0f)
			{
				base.Projectile.velocity.Y += acceleration;
			}
		}
		else if (base.Projectile.velocity.Y > yDist)
		{
			base.Projectile.velocity.Y -= acceleration;
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

	private void OnHitEffects(Entity target)
	{
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		State = 1f;
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int i = 0; i < 3; i++)
			{
				Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<Valaricicle>(), base.Projectile.damage / 4, 0f, base.Projectile.owner, 0f, 0f, Main.rand.NextBool() ? 1f : 0f);
			}
		}
		if (base.Projectile.Calamity().stealthStrike)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/CryogenPhaseTransitionCrack");
			style.Volume = 0.4f;
			style.Pitch = 1f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			float randOffset = Main.rand.NextFloat((float)Math.PI * 2f);
			for (int j = 0; j < 6; j++)
			{
				Vector2 particleVel = Vector2.UnitX.RotatedBy((float)Math.PI / 3f * (float)j + randOffset) * 7.5f;
				GeneralParticleHandler.SpawnParticle(new WaterFlavoredParticle(target.Center, particleVel, affectedByGravity: false, 10, 0.7f, Color.AliceBlue));
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		OnHitEffects(target);
		target.AddBuff(324, 120);
		if (base.Projectile.Calamity().stealthStrike)
		{
			target.AddBuff(ModContent.BuffType<GlacialState>(), 45);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		OnHitEffects(target);
		target.AddBuff(324, 120);
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
		SoundEngine.PlaySound(in SoundID.Item50, base.Projectile.position);
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		State = 1f;
		if (TileCollideDelay == 0)
		{
			TileCollideDelay = 10;
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().stealthStrike)
		{
			SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.Center);
			Vector2 splinterVel = base.Projectile.velocity.RotatedByRandom(0.2617993950843811);
			for (int i = 1; i <= 5; i++)
			{
				Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.Center, splinterVel, base.Mod.Find<ModGore>($"FrostcrushValariGore{i}").Type);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().stealthStrike)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, 2, lightColor);
			return false;
		}
		return true;
	}
}
