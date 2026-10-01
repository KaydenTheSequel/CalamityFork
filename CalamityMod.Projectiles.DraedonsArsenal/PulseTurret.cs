using System;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class PulseTurret : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Misc";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 24;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = true;
		base.Projectile.sentry = true;
		base.Projectile.timeLeft = 36000;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (base.Projectile.velocity.Y < 12f)
		{
			base.Projectile.velocity.Y += 0.5f;
		}
		NPC potentialTarget = base.Projectile.Center.MinionHoming(850f, player, ignoreTiles: false);
		if (potentialTarget != null)
		{
			base.Projectile.spriteDirection = (potentialTarget.Center.X - base.Projectile.Center.X > 0f).ToDirectionInt();
			base.Projectile.ai[0]++;
			if (base.Projectile.ai[0] % 40f < 30f)
			{
				float idealAngle = base.Projectile.AngleTo(potentialTarget.Center) + (float)(base.Projectile.spriteDirection == -1).ToInt() * (float)Math.PI;
				if (base.Projectile.ai[1] <= 0f)
				{
					base.Projectile.rotation = base.Projectile.rotation.AngleLerp(idealAngle, (float)Math.PI * 2f / 25f);
				}
				else
				{
					if (base.Projectile.ai[1] > 13f)
					{
						base.Projectile.rotation -= MathHelper.ToRadians(10f) * base.Projectile.localAI[0];
					}
					base.Projectile.ai[1]--;
				}
			}
			if (base.Projectile.ai[0] % 40f != 39f || Main.myPlayer != base.Projectile.owner)
			{
				return;
			}
			Texture2D standTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/PulseTurretStand", (AssetRequestMode)2).Value;
			Vector2 shootPosition = base.Projectile.Center - ((float)(standTexture.Height / 2) + 6f) * Vector2.UnitY;
			shootPosition += (base.Projectile.Size * 0.5f).RotatedBy(base.Projectile.rotation - MathHelper.ToRadians(18f) - (float)(base.Projectile.spriteDirection == -1).ToInt() * (float)Math.PI);
			if (!(Math.Abs(Vector2.Normalize(potentialTarget.Center - shootPosition).ToRotation() - base.Projectile.rotation) < MathHelper.ToRadians(32f) + (float)(base.Projectile.spriteDirection == -1).ToInt() * (float)Math.PI) && !(base.Projectile.Distance(potentialTarget.Center) < 45f))
			{
				return;
			}
			SoundEngine.PlaySound(in PulseRifle.FireSound, shootPosition);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), shootPosition, Vector2.Normalize(potentialTarget.Center - shootPosition) * 12f, ModContent.ProjectileType<PulseTurretShot>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			if (base.Projectile.ai[0] % 120f == 119f)
			{
				for (int i = -1; i <= 1; i += 2)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), shootPosition, (potentialTarget.Center - shootPosition).SafeNormalize(Vector2.UnitY).RotatedBy((float)i * MathHelper.ToRadians(28f)) * 7f, ModContent.ProjectileType<PulseTurretShot>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 1f);
				}
			}
			if (!Main.dedServ)
			{
				for (int j = 0; j < 12; j++)
				{
					Dust.NewDustPerfect(shootPosition, 173).scale = Main.rand.NextFloat(1.4f, 1.8f);
				}
			}
			base.Projectile.ai[1] = 15f;
			base.Projectile.localAI[0] = Math.Sign(base.Projectile.SafeDirectionTo(potentialTarget.Center).X);
		}
		else
		{
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp(0f, (float)Math.PI / 25f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		Texture2D standTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/PulseTurretStand", (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(TextureAssets.Projectile[base.Type].Value, base.Projectile.Center - ((float)(standTexture.Height / 2) + 6f) * Vector2.UnitY - Main.screenPosition, null, lightColor, base.Projectile.rotation, base.Projectile.Size * 0.5f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1));
		Main.EntitySpriteDraw(standTexture, base.Projectile.Center - Main.screenPosition, null, lightColor, 0f, standTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}
}
