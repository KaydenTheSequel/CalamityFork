using System;
using System.IO;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class KelvinCatalystBoomerang : ModProjectile, ILocalizedModType, IModType
{
	public int AIState;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/KelvinCatalyst";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 12;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.coldDamage = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20 * base.Projectile.MaxUpdates;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(AIState);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		AIState = reader.ReadInt32();
	}

	public override void AI()
	{
		VisualAudioEffects();
		BoomerangAI();
	}

	private void BoomerangAI()
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		switch (AIState)
		{
		case 0:
			base.Projectile.localAI[0]++;
			if (base.Projectile.localAI[0] >= 75f)
			{
				ResetStats(base.Projectile.Calamity().stealthStrike);
			}
			break;
		case 1:
			ReturnToPlayer();
			break;
		case 2:
			base.Projectile.ChargingMinionAI(1200f, 1500f, 2200f, 150f, 1, 40f, 12f, 6f, new Vector2(0f, -60f), 40f, 12f, tileVision: true, ignoreTilesWhenCharging: true);
			base.Projectile.localAI[0]++;
			if (base.Projectile.localAI[0] >= 180f)
			{
				ResetStats(chaseEnemies: false);
			}
			break;
		}
	}

	private void ReturnToPlayer()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		float returnSpeed = 20f;
		float acceleration = 2f;
		Vector2 playerVec = player.Center - base.Projectile.Center;
		if (((Vector2)(ref playerVec)).Length() > 3000f)
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
		if (Main.myPlayer == base.Projectile.owner)
		{
			Rectangle hitbox = base.Projectile.Hitbox;
			if (((Rectangle)(ref hitbox)).Intersects(player.Hitbox))
			{
				base.Projectile.Kill();
			}
		}
	}

	private void VisualAudioEffects()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, (float)Main.DiscoR * 0.3f / 255f, (float)Main.DiscoR * 0.4f / 255f, (float)Main.DiscoR * 0.5f / 255f);
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = 8;
			SoundEngine.PlaySound(in SoundID.Item7, base.Projectile.Center);
		}
		int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 67, 0f, 0f, 100);
		Main.dust[dust].noGravity = true;
		Dust obj = Main.dust[dust];
		obj.velocity *= 0f;
		base.Projectile.rotation += 0.25f;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		ResetStats(base.Projectile.Calamity().stealthStrike);
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

	private void ResetStats(bool chaseEnemies)
	{
		AIState = ((!chaseEnemies) ? 1 : 2);
		base.Projectile.localAI[0] = 0f;
		base.Projectile.width = (base.Projectile.height = 60);
		base.Projectile.tileCollide = false;
		base.Projectile.netUpdate = true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(324, 240);
		OnHitEffects();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(324, 240);
		OnHitEffects();
	}

	private void OnHitEffects()
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		int maxSpawns = ((!base.Projectile.Calamity().stealthStrike) ? 1 : 3);
		if (base.Projectile.owner == Main.myPlayer && base.Projectile.numHits < maxSpawns)
		{
			for (int i = 0; i < 5; i++)
			{
				Vector2 velocity = ((float)Math.PI * 2f * (float)i / 5f).ToRotationVector2() * 4f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<KelvinCatalystStar>(), base.Projectile.damage / 2, base.Projectile.knockBack * 0.5f, base.Projectile.owner);
			}
			SoundEngine.PlaySound(in SoundID.Item30, base.Projectile.Center);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}
}
