using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.BaseProjectiles;

public abstract class BaseSpearProjectile : ModProjectile, ILocalizedModType, IModType
{
	public enum SpearType
	{
		TypicalSpear,
		GhastlyGlaiveSpear
	}

	public new string LocalizationCategory => "Projectiles.Melee";

	public virtual float InitialSpeed => 3f;

	public virtual float ReelbackSpeed => 1f;

	public virtual float ForwardSpeed => 0.75f;

	public virtual float TravelSpeed => 22f;

	public virtual Action<Projectile> EffectBeforeReelback => null;

	public virtual SpearType SpearAiType => SpearType.TypicalSpear;

	private float GetSyncedItemAnimation(Player player)
	{
		float itemAnimation = player.itemAnimation;
		if (Main.netMode != 0 && Main.myPlayer == base.Projectile.owner && base.Projectile.ai[1] != itemAnimation)
		{
			base.Projectile.ai[1] = itemAnimation;
			base.Projectile.netUpdate = true;
		}
		if (Main.netMode == 0 || Main.myPlayer == base.Projectile.owner)
		{
			return itemAnimation;
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.localAI[1] = 1f;
		}
		if (base.Projectile.localAI[1] == 1f)
		{
			return base.Projectile.ai[1];
		}
		return Math.Max(1f, player.itemAnimationMax);
	}

	public virtual void Behavior()
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		if (SpearAiType == SpearType.TypicalSpear)
		{
			Player player = Main.player[base.Projectile.owner];
			float itemAnimationMax = Math.Max(1f, player.itemAnimationMax);
			float syncedItemAnimation = GetSyncedItemAnimation(player);
			player.ChangeDir(base.Projectile.direction);
			player.heldProj = base.Projectile.whoAmI;
			player.itemTime = player.itemAnimation;
			base.Projectile.Center = player.RotatedRelativePoint(player.MountedCenter);
			Projectile projectile = base.Projectile;
			projectile.position += base.Projectile.velocity * base.Projectile.ai[0];
			if (base.Projectile.ai[0] == 0f)
			{
				base.Projectile.ai[0] = InitialSpeed;
				base.Projectile.netUpdate = true;
			}
			if (syncedItemAnimation < itemAnimationMax / 3f)
			{
				base.Projectile.ai[0] -= ReelbackSpeed;
				if (base.Projectile.localAI[0] == 0f && EffectBeforeReelback != null && Main.myPlayer == base.Projectile.owner)
				{
					base.Projectile.localAI[0] = 1f;
					EffectBeforeReelback(base.Projectile);
				}
			}
			else
			{
				base.Projectile.ai[0] += ForwardSpeed;
			}
			if (syncedItemAnimation <= 1f)
			{
				base.Projectile.Kill();
			}
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f + (float)Math.PI / 4f;
			if (base.Projectile.spriteDirection == -1)
			{
				base.Projectile.rotation -= (float)Math.PI / 2f;
			}
		}
		else
		{
			if (SpearAiType != SpearType.GhastlyGlaiveSpear)
			{
				return;
			}
			Player player2 = Main.player[base.Projectile.owner];
			float itemAnimationMax2 = Math.Max(1f, player2.itemAnimationMax);
			float syncedItemAnimation2 = GetSyncedItemAnimation(player2);
			Vector2 playerRelativePoint = player2.RotatedRelativePoint(player2.MountedCenter, reverseRotation: true);
			base.Projectile.direction = player2.direction;
			player2.heldProj = base.Projectile.whoAmI;
			base.Projectile.Center = playerRelativePoint;
			if (player2.dead)
			{
				base.Projectile.Kill();
				return;
			}
			if (!player2.frozen)
			{
				if (syncedItemAnimation2 < itemAnimationMax2 / 3f && base.Projectile.localAI[0] == 0f && EffectBeforeReelback != null && Main.myPlayer == base.Projectile.owner)
				{
					base.Projectile.localAI[0] = 1f;
					EffectBeforeReelback(base.Projectile);
				}
				base.Projectile.spriteDirection = (base.Projectile.direction = player2.direction);
				if (base.Projectile.alpha > 0)
				{
					base.Projectile.alpha -= 127;
					if (base.Projectile.alpha < 0)
					{
						base.Projectile.alpha = 0;
					}
				}
				if (base.Projectile.localAI[0] > 0f)
				{
					base.Projectile.localAI[0]--;
				}
				float inverseAnimationCompletion = 1f - syncedItemAnimation2 / itemAnimationMax2;
				float originalVelocityDirection = base.Projectile.velocity.ToRotation();
				float originalVelocitySpeed = ((Vector2)(ref base.Projectile.velocity)).Length();
				Vector2 flatVelocity = Vector2.UnitX.RotatedBy((float)Math.PI + inverseAnimationCompletion * ((float)Math.PI * 2f)) * new Vector2(originalVelocitySpeed, base.Projectile.ai[0]);
				Projectile projectile2 = base.Projectile;
				projectile2.position += flatVelocity.RotatedBy(originalVelocityDirection) + Utils.RotatedBy(new Vector2(originalVelocitySpeed + TravelSpeed, 0f), (double)originalVelocityDirection, default(Vector2));
				Vector2 destination = playerRelativePoint + flatVelocity.RotatedBy(originalVelocityDirection) + originalVelocityDirection.ToRotationVector2() * (originalVelocitySpeed + TravelSpeed + 40f);
				base.Projectile.rotation = player2.AngleTo(destination) + (float)Math.PI / 4f * (float)player2.direction;
				if (base.Projectile.spriteDirection == -1)
				{
					base.Projectile.rotation += (float)Math.PI;
				}
			}
			if (player2.itemAnimation == 2)
			{
				base.Projectile.Kill();
				player2.reuseDelay = 2;
			}
		}
	}

	public virtual void ExtraBehavior()
	{
	}

	public override void AI()
	{
		Behavior();
		if ((SpearAiType == SpearType.GhastlyGlaiveSpear && !Main.player[base.Projectile.owner].frozen) || SpearAiType == SpearType.TypicalSpear)
		{
			ExtraBehavior();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		if (SpearAiType == SpearType.TypicalSpear)
		{
			Main.EntitySpriteDraw(TextureAssets.Projectile[base.Type].Value, base.Projectile.Center - Main.screenPosition, origin: Vector2.Zero, sourceRectangle: null, color: base.Projectile.GetAlpha(lightColor), rotation: base.Projectile.rotation, scale: base.Projectile.scale, effects: (SpriteEffects)0);
			return false;
		}
		return base.PreDraw(ref lightColor);
	}
}
