using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

[PierceResistException(false)]
public class TyphonsGreedStaff : ModProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<TyphonsGreed>();

	public override void SetDefaults()
	{
		base.Projectile.width = 110;
		base.Projectile.height = 110;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.hide = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 6;
	}

	public override void AI()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		float spinTimer = 50f;
		float rotationFactor = 2f;
		float scaleFactor = 20f;
		Player player = Main.player[base.Projectile.owner];
		float rotationSpeed = -(float)Math.PI / 4f;
		Vector2 actualPosition = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		Vector2 rotationPoint = Vector2.Zero;
		if (player.dead)
		{
			base.Projectile.Kill();
			return;
		}
		Lighting.AddLight(player.Center, 0f, 0.2f, 1.45f);
		int rotationVel = Math.Sign(base.Projectile.velocity.X);
		base.Projectile.velocity = new Vector2((float)rotationVel, 0f);
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.rotation = Utils.ToRotation(new Vector2((float)rotationVel, 0f - player.gravDir)) + rotationSpeed + (float)Math.PI;
			if (base.Projectile.velocity.X < 0f)
			{
				base.Projectile.rotation -= (float)Math.PI / 2f;
			}
		}
		base.Projectile.alpha -= 128;
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		_ = base.Projectile.ai[0] / spinTimer;
		base.Projectile.ai[0]++;
		base.Projectile.rotation += (float)Math.PI * 2f * rotationFactor / spinTimer * (float)rotationVel;
		bool isUsing = base.Projectile.ai[0] == (float)(int)(spinTimer / 2f);
		if (base.Projectile.ai[0] >= spinTimer || (isUsing && !player.controlUseItem))
		{
			base.Projectile.Kill();
			player.reuseDelay = 2;
		}
		else if (isUsing)
		{
			int expectedDirection = (player.SafeDirectionTo(Main.MouseWorld).X > 0f).ToDirectionInt();
			if ((float)expectedDirection != base.Projectile.velocity.X)
			{
				player.ChangeDir(expectedDirection);
				base.Projectile.velocity = Vector2.UnitX * (float)expectedDirection;
				base.Projectile.rotation -= (float)Math.PI;
				base.Projectile.netUpdate = true;
			}
		}
		float rotateDirection = base.Projectile.rotation - (float)Math.PI / 4f * (float)rotationVel;
		rotationPoint = (rotateDirection + ((rotationVel == -1) ? ((float)Math.PI) : 0f)).ToRotationVector2() * (base.Projectile.ai[0] / spinTimer) * scaleFactor;
		Vector2 dustSpawn = base.Projectile.Center + (rotateDirection + ((rotationVel == -1) ? ((float)Math.PI) : 0f)).ToRotationVector2() * 30f;
		Vector2 staffTipDirection = rotateDirection.ToRotationVector2();
		Vector2 tipDustDirection = staffTipDirection.RotatedBy((float)Math.PI / 2f * (float)base.Projectile.spriteDirection);
		if (Main.rand.NextBool())
		{
			Dust staffDust = Dust.NewDustDirect(dustSpawn - new Vector2(5f), 10, 10, 33, player.velocity.X, player.velocity.Y, 150);
			staffDust.velocity = base.Projectile.SafeDirectionTo(staffDust.position) * 0.1f + staffDust.velocity * 0.1f;
		}
		for (int j = 0; j < 4; j++)
		{
			float scaleFactor2 = 1f;
			float scaleFactor3 = 1f;
			switch (j)
			{
			case 1:
				scaleFactor3 = -1f;
				break;
			case 2:
				scaleFactor3 = 1.25f;
				scaleFactor2 = 0.5f;
				break;
			case 3:
				scaleFactor3 = -1.25f;
				scaleFactor2 = 0.5f;
				break;
			}
			if (!Main.rand.NextBool(6))
			{
				Dust staffTipDust = Dust.NewDustDirect(base.Projectile.position, 0, 0, 186, 0f, 0f, 100);
				staffTipDust.position = base.Projectile.Center + staffTipDirection * (60f + Main.rand.NextFloat() * 20f) * scaleFactor3;
				staffTipDust.velocity = tipDustDirection * (4f + 4f * Main.rand.NextFloat()) * scaleFactor3 * scaleFactor2;
				staffTipDust.noGravity = true;
				staffTipDust.noLight = true;
				staffTipDust.scale = 0.5f;
				if (Main.rand.NextBool(4))
				{
					staffTipDust.noGravity = false;
				}
			}
		}
		base.Projectile.position = actualPosition - base.Projectile.Size / 2f;
		Projectile projectile = base.Projectile;
		projectile.position += rotationPoint;
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.timeLeft = 2;
		player.ChangeDir(base.Projectile.direction);
		player.heldProj = base.Projectile.whoAmI;
		player.itemTime = 2;
		player.itemAnimation = 2;
		player.itemRotation = MathHelper.WrapAngle(base.Projectile.rotation);
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] % 12f == 0f && base.Projectile.owner == Main.myPlayer)
		{
			CalamityUtils.ProjectileBarrage(base.Projectile.GetSource_FromThis(), base.Projectile.Center, player.Center, Main.rand.NextBool(), 800f, 800f, 0f, 800f, 10f, ModContent.ProjectileType<TyphonsGreedBubble>(), base.Projectile.damage, base.Projectile.knockBack * 0.5f, base.Projectile.owner, clamped: true).ai[1] = Main.rand.NextFloat() + 0.5f;
		}
	}

	public override void CutTiles()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		float cutRadius = 60f;
		float f = base.Projectile.rotation - (float)Math.PI / 4f * (float)Math.Sign(base.Projectile.velocity.X);
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Utils.PlotTileLine(base.Projectile.Center + f.ToRotationVector2() * (0f - cutRadius), base.Projectile.Center + f.ToRotationVector2() * cutRadius, (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CutTiles);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		if (((Rectangle)(ref projHitbox)).Intersects(targetHitbox))
		{
			return true;
		}
		float f = base.Projectile.rotation - (float)Math.PI / 4f * (float)Math.Sign(base.Projectile.velocity.X);
		float rotationFactor = 0f;
		float collisionRadius = 110f;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center + f.ToRotationVector2() * (0f - collisionRadius), base.Projectile.Center + f.ToRotationVector2() * collisionRadius, 23f * base.Projectile.scale, ref rotationFactor))
		{
			return true;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 300);
	}
}
