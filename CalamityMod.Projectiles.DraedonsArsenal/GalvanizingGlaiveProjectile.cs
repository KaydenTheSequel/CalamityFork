using System;
using System.Collections.Generic;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class GalvanizingGlaiveProjectile : BaseSpearProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<GalvanizingGlaive>();

	public override SpearType SpearAiType => SpearType.GhastlyGlaiveSpear;

	public override float TravelSpeed => 8f;

	public override Action<Projectile> EffectBeforeReelback => delegate
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * (float)base.Projectile.width;
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + val, base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 15f, ModContent.ProjectileType<GaussEnergy>(), (int)((double)base.Projectile.damage * 0.4), 0f, base.Projectile.owner);
	};

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 90;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 100;
		base.Projectile.height = 100;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.hide = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 6;
		base.Projectile.alpha = 255;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		Vector2 drawPosition = base.Projectile.position + new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition;
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(origin: value.Size() * 0.5f, texture: value, position: drawPosition, sourceRectangle: null, color: lightColor, rotation: base.Projectile.rotation, scale: base.Projectile.scale, effects: (SpriteEffects)(base.Projectile.spriteDirection == -1));
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		float angle = base.Projectile.rotation - (float)Math.PI / 4f * (float)(base.Projectile.spriteDirection == -1).ToInt();
		float _ = 0f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + angle.ToRotationVector2() * -105f, base.Projectile.width, ref _);
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		float animationRatio = (float)player.itemAnimation / (float)player.itemAnimationMax;
		DelegateMethods.f_1 = Utils.GetLerpValue(0f, 0.4f, animationRatio, clamped: true) * Utils.GetLerpValue(1f, 0.6f, animationRatio, clamped: true);
		DelegateMethods.c_1 = Color.White;
		List<Vector2> oldPositions = new List<Vector2> { base.Projectile.position };
		Vector2[] oldPos = base.Projectile.oldPos;
		foreach (Vector2 oldPosition in oldPos)
		{
			if (oldPosition != Vector2.Zero)
			{
				oldPositions.Add(oldPosition);
			}
		}
		for (int j = 0; j < oldPositions.Count - 2; j += 2)
		{
			Vector2 offset = base.Projectile.Size * 0.5f + base.Projectile.Size.RotatedBy(base.Projectile.velocity.ToRotation() - (float)Math.PI / 4f) * 0.4f;
			Vector2 start = oldPositions[j] + offset - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY;
			Vector2 end = oldPositions[j + 2] + offset - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY;
			Utils.DrawLaser(Main.spriteBatch, ModContent.Request<Texture2D>("CalamityMod/Projectiles/LightningProj", (AssetRequestMode)2).Value, start, end, new Vector2(0.2f), DelegateMethods.LightningLaserDraw);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(12))
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<GaussFlux>(), hit.Damage, 0f, base.Projectile.owner, 0f, target.whoAmI);
		}
	}
}
