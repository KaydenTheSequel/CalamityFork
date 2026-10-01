using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class CelestusProj : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private float speed = 25f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.localNPCHitCooldown = 30;
		base.Projectile.extraUpdates = 3;
		base.Projectile.penetrate = -1;
		base.Projectile.width = (base.Projectile.height = 132);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (!initialized)
		{
			speed = ((Vector2)(ref base.Projectile.velocity)).Length();
			initialized = true;
		}
		Lighting.AddLight(base.Projectile.Center, (float)Main.DiscoR * 0.5f / 255f, (float)Main.DiscoG * 0.5f / 255f, (float)Main.DiscoB * 0.5f / 255f);
		base.Projectile.rotation++;
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = 8;
			SoundEngine.PlaySound(in SoundID.Item7, base.Projectile.position);
		}
		float num = base.Projectile.ai[0];
		if (num != 0f)
		{
			if (num != 1f)
			{
				if (num == 2f)
				{
					CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 250f, speed, 20f);
				}
				return;
			}
			float returnSpeed = 25f;
			float acceleration = 5f;
			Vector2 playerVec = player.Center - base.Projectile.Center;
			if (((Vector2)(ref playerVec)).Length() > 4000f)
			{
				base.Projectile.Kill();
			}
			((Vector2)(ref playerVec)).Normalize();
			playerVec *= returnSpeed;
			if (base.Projectile.velocity.X < playerVec.X)
			{
				base.Projectile.velocity.X += acceleration;
				if (base.Projectile.velocity.X < 0f && playerVec.X > 0f)
				{
					base.Projectile.velocity.X += acceleration;
				}
			}
			else if (base.Projectile.velocity.X > playerVec.X)
			{
				base.Projectile.velocity.X -= acceleration;
				if (base.Projectile.velocity.X > 0f && playerVec.X < 0f)
				{
					base.Projectile.velocity.X -= acceleration;
				}
			}
			if (base.Projectile.velocity.Y < playerVec.Y)
			{
				base.Projectile.velocity.Y += acceleration;
				if (base.Projectile.velocity.Y < 0f && playerVec.Y > 0f)
				{
					base.Projectile.velocity.Y += acceleration;
				}
			}
			else if (base.Projectile.velocity.Y > playerVec.Y)
			{
				base.Projectile.velocity.Y -= acceleration;
				if (base.Projectile.velocity.Y > 0f && playerVec.Y < 0f)
				{
					base.Projectile.velocity.Y -= acceleration;
				}
			}
			if (Main.myPlayer != base.Projectile.owner)
			{
				return;
			}
			Rectangle projHitbox = default(Rectangle);
			((Rectangle)(ref projHitbox))._002Ector((int)base.Projectile.position.X, (int)base.Projectile.position.Y, base.Projectile.width, base.Projectile.height);
			Rectangle playerHitbox = default(Rectangle);
			((Rectangle)(ref playerHitbox))._002Ector((int)player.position.X, (int)player.position.Y, player.width, player.height);
			if (((Rectangle)(ref projHitbox)).Intersects(playerHitbox))
			{
				if (base.Projectile.Calamity().stealthStrike)
				{
					Projectile projectile = base.Projectile;
					projectile.velocity *= -1f;
					base.Projectile.timeLeft = 600;
					base.Projectile.penetrate = 1;
					base.Projectile.localNPCHitCooldown = -1;
					base.Projectile.ai[0] = 2f;
					base.Projectile.netUpdate = true;
				}
				else
				{
					base.Projectile.Kill();
				}
			}
		}
		else
		{
			base.Projectile.ai[1]++;
			if (base.Projectile.ai[1] >= 40f)
			{
				base.Projectile.ai[0] = 1f;
				base.Projectile.ai[1] = 0f;
				base.Projectile.netUpdate = true;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
		OnHitEffects();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
		OnHitEffects();
	}

	private void OnHitEffects()
	{
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int i = 0; i < 8; i++)
			{
				Vector2 velocity = ((float)Math.PI * 2f * (float)i / 8f - (MathHelper.ToRadians(67.5f) - base.Projectile.velocity.ToRotation())).ToRotationVector2() * 2.5f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<CelestusMiniScythe>(), (int)((double)base.Projectile.damage * 0.7), base.Projectile.knockBack, base.Projectile.owner);
			}
		}
		SoundEngine.PlaySound(in SoundID.Item122, base.Projectile.Center);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(0, 0, 132, 132);
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/CelestusProjGlow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, frame, Color.White, base.Projectile.rotation, base.Projectile.Size / 2f, 1f, (SpriteEffects)0);
	}
}
