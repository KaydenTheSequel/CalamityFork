using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class BeastScythe : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 54;
		base.Projectile.alpha = 255;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.netImportant = true;
		base.Projectile.penetrate = 2;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
		base.Projectile.extraUpdates = 1;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 20;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		base.Projectile.rotation += 0.5f;
		Lighting.AddLight(base.Projectile.Center, 0.35f, 0f, 0.35f);
		if (Main.rand.NextBool(3))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 173, base.Projectile.velocity.X * 0.25f, base.Projectile.velocity.Y * 0.25f);
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] <= 30f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.999f;
		}
		if (Main.myPlayer == base.Projectile.owner && base.Projectile.ai[0] == 30f)
		{
			if (Main.player[base.Projectile.owner].channel)
			{
				float projDelay = 20f;
				Vector2 vector10 = base.Projectile.Center;
				float projXDirection = (float)Main.mouseX + Main.screenPosition.X - vector10.X;
				float projYDirection = (float)Main.mouseY + Main.screenPosition.Y - vector10.Y;
				if (Main.player[base.Projectile.owner].gravDir == -1f)
				{
					projYDirection = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector10.Y;
				}
				float projDistance = (float)Math.Sqrt(projXDirection * projXDirection + projYDirection * projYDirection);
				projDistance = (float)Math.Sqrt(projXDirection * projXDirection + projYDirection * projYDirection);
				if (projDistance > projDelay)
				{
					projDistance = projDelay / projDistance;
					projXDirection *= projDistance;
					projYDirection *= projDistance;
					int num = (int)(projXDirection * 1000f);
					int projXSpeedMagnified = (int)(base.Projectile.velocity.X * 1000f);
					int projYSpeed = (int)(projYDirection * 1000f);
					int projYSpeedMagnified = (int)(base.Projectile.velocity.Y * 1000f);
					if (num != projXSpeedMagnified || projYSpeed != projYSpeedMagnified)
					{
						base.Projectile.netUpdate = true;
					}
					base.Projectile.velocity.X = projXDirection;
					base.Projectile.velocity.Y = projYDirection;
				}
				else
				{
					int num2 = (int)(projXDirection * 1000f);
					int projXSpeedMagnifiedElse = (int)(base.Projectile.velocity.X * 1000f);
					int projYSpeedElse = (int)(projYDirection * 1000f);
					int projYSpeedMagnifiedElse = (int)(base.Projectile.velocity.Y * 1000f);
					if (num2 != projXSpeedMagnifiedElse || projYSpeedElse != projYSpeedMagnifiedElse)
					{
						base.Projectile.netUpdate = true;
					}
					base.Projectile.velocity.X = projXDirection;
					base.Projectile.velocity.Y = projYDirection;
				}
			}
			else
			{
				base.Projectile.netUpdate = true;
				Vector2 projDirection = base.Projectile.Center;
				float projXDir = (float)Main.mouseX + Main.screenPosition.X - projDirection.X;
				float projYDir = (float)Main.mouseY + Main.screenPosition.Y - projDirection.Y;
				if (Main.player[base.Projectile.owner].gravDir == -1f)
				{
					projYDir = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - projDirection.Y;
				}
				float projDistancing = (float)Math.Sqrt(projXDir * projXDir + projYDir * projYDir);
				if (projDistancing == 0f || base.Projectile.ai[0] < 0f)
				{
					((Vector2)(ref projDirection))._002Ector(Main.player[base.Projectile.owner].position.X + (float)(Main.player[base.Projectile.owner].width / 2), Main.player[base.Projectile.owner].position.Y + (float)(Main.player[base.Projectile.owner].height / 2));
					projXDir = base.Projectile.position.X + (float)base.Projectile.width * 0.5f - projDirection.X;
					projYDir = base.Projectile.position.Y + (float)base.Projectile.height * 0.5f - projDirection.Y;
					projDistancing = (float)Math.Sqrt(projXDir * projXDir + projYDir * projYDir);
				}
				projDistancing = 20f / projDistancing;
				projXDir *= projDistancing;
				projYDir *= projDistancing;
				base.Projectile.velocity.X = projXDir;
				base.Projectile.velocity.Y = projYDir;
			}
		}
		if (base.Projectile.ai[0] >= 30f)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 1.001f;
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 200f, 12f, 20f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 100);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.damage /= 2;
		base.Projectile.Damage();
		bool isInTile = WorldGen.SolidTile(Framing.GetTileSafely((int)base.Projectile.position.X / 16, (int)base.Projectile.position.Y / 16));
		for (int m = 0; m < 4; m++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 100, default(Color), 1.5f);
		}
		for (int n = 0; n < 4; n++)
		{
			int beastial = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 0, default(Color), 2.5f);
			Main.dust[beastial].noGravity = true;
			Dust obj = Main.dust[beastial];
			obj.velocity *= 3f;
			if (isInTile)
			{
				Main.dust[beastial].noLight = true;
			}
			beastial = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 100, default(Color), 1.5f);
			Dust obj2 = Main.dust[beastial];
			obj2.velocity *= 2f;
			Main.dust[beastial].noGravity = true;
			if (isInTile)
			{
				Main.dust[beastial].noLight = true;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}
}
