using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class CloudElementalMinion : ModProjectile, ILocalizedModType, IModType
{
	public int dust = 3;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 58;
		base.Projectile.height = 116;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!modPlayer.cloudElemental && !modPlayer.allElementals && !modPlayer.cloudElementalVanity && !modPlayer.allElementalsVanity)
		{
			base.Projectile.active = false;
			return;
		}
		if (base.Projectile.type == ModContent.ProjectileType<CloudElementalMinion>())
		{
			if (player.dead)
			{
				modPlayer.cloudEleBuff = false;
			}
			if (modPlayer.cloudEleBuff)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		dust--;
		if (dust >= 0)
		{
			int dustAmt = 50;
			for (int d = 0; d < dustAmt; d++)
			{
				int index = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y + 16f), base.Projectile.width, base.Projectile.height - 16, 16);
				Dust obj = Main.dust[index];
				obj.velocity *= 2f;
				Main.dust[index].scale *= 1.15f;
			}
		}
		if (Math.Abs(base.Projectile.velocity.X) > 0.2f)
		{
			base.Projectile.spriteDirection = -base.Projectile.direction;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 7)
		{
			base.Projectile.frame = 0;
		}
		if (!modPlayer.cloudElementalVanity && !modPlayer.allElementalsVanity)
		{
			float lightScalar = (float)Main.rand.Next(90, 111) * 0.01f;
			lightScalar *= Main.essScale;
			Lighting.AddLight(base.Projectile.Center, 0.25f * lightScalar, 0.55f * lightScalar, 0.75f * lightScalar);
			base.Projectile.ChargingMinionAI(500f, 800f, 1200f, 400f, 0, 30f, 8f, 4f, new Vector2(500f, -60f), 40f, 8f, tileVision: false, ignoreTilesWhenCharging: true);
			return;
		}
		base.Projectile.tileCollide = false;
		base.Projectile.ai[0] = 1f;
		Vector2 playerVec = player.Center - base.Projectile.Center + new Vector2(500f, -60f);
		float num = ((Vector2)(ref playerVec)).Length();
		float playerHomeSpeed = 6f;
		if (base.Projectile.ai[0] == 1f)
		{
			playerHomeSpeed = 15f;
		}
		if (num > 200f && playerHomeSpeed < 8f)
		{
			playerHomeSpeed = 8f;
		}
		if (num < 400f && base.Projectile.ai[0] == 1f && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
		{
			base.Projectile.ai[0] = 0f;
			base.Projectile.netUpdate = true;
		}
		if (num > 2000f)
		{
			base.Projectile.position.X = player.Center.X - (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = player.Center.Y - (float)(base.Projectile.height / 2);
			base.Projectile.netUpdate = true;
		}
		if (num > 70f)
		{
			((Vector2)(ref playerVec)).Normalize();
			playerVec *= playerHomeSpeed;
			base.Projectile.velocity = (base.Projectile.velocity * 40f + playerVec) / 41f;
		}
		else if (base.Projectile.velocity.X == 0f && base.Projectile.velocity.Y == 0f)
		{
			base.Projectile.velocity.X = -0.15f;
			base.Projectile.velocity.Y = -0.05f;
		}
	}

	public override bool? CanDamage()
	{
		CalamityPlayer modPlayer = Main.player[base.Projectile.owner].Calamity();
		if (modPlayer.cloudElementalVanity || modPlayer.allElementalsVanity)
		{
			return false;
		}
		return null;
	}
}
