using System;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Spears;

public class BansheeHookProj : BaseSpearProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<BansheeHook>();

	public override SpearType SpearAiType => SpearType.GhastlyGlaiveSpear;

	public override float TravelSpeed => 22f;

	public override Action<Projectile> EffectBeforeReelback => delegate
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + base.Projectile.velocity * 0.5f, base.Projectile.velocity * 0.8f, ModContent.ProjectileType<BansheeHookScythe>(), (int)((float)base.Projectile.damage * 0.85f), base.Projectile.knockBack * 0.85f, base.Projectile.owner);
	};

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.timeLeft = 90;
		base.Projectile.height = 40;
		base.Projectile.friendly = true;
		base.Projectile.hostile = false;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.hide = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
		base.Projectile.alpha = 255;
	}

	public override void ExtraBehavior()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		Vector2 val = player.RotatedRelativePoint(player.MountedCenter);
		float itemAnimationCompletion = (float)player.itemAnimation / (float)player.itemAnimationMax;
		float completionAsAngle = (1f - itemAnimationCompletion) * ((float)Math.PI * 2f);
		float startingVelocityRotation = base.Projectile.velocity.ToRotation();
		float startingVelocitySpeed = ((Vector2)(ref base.Projectile.velocity)).Length();
		Vector2 flatVelocity = Vector2.UnitX.RotatedBy((float)Math.PI + completionAsAngle) * new Vector2(startingVelocitySpeed, base.Projectile.ai[0]);
		Vector2 destination = val + flatVelocity.RotatedBy(startingVelocityRotation) + Utils.RotatedBy(new Vector2(startingVelocitySpeed + TravelSpeed + 40f, 0f), (double)startingVelocityRotation, default(Vector2));
		Vector2 directionTowardsEnd = player.SafeDirectionTo(destination, Vector2.UnitX * (float)player.direction);
		Vector2 initalVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY);
		float dustCount = 2f;
		for (int i = 0; (float)i < dustCount; i++)
		{
			Dust dust = Dust.NewDustDirect(base.Projectile.Center, 14, 14, 60, 0f, 0f, 110);
			dust.velocity = player.SafeDirectionTo(dust.position) * 2f;
			dust.position = base.Projectile.Center + initalVelocity.RotatedBy(completionAsAngle * 2f + (float)i / dustCount * ((float)Math.PI * 2f)) * 10f;
			dust.scale = 1f + Main.rand.NextFloat(0.6f);
			dust.velocity += initalVelocity * 3f;
			dust.noGravity = true;
		}
		if (Main.rand.NextBool(3))
		{
			Dust dust2 = Dust.NewDustDirect(base.Projectile.Center, 20, 20, 60, 0f, 0f, 110);
			dust2.velocity = player.SafeDirectionTo(dust2.position) * 2f;
			dust2.position = base.Projectile.Center + directionTowardsEnd * -110f;
			dust2.scale = 0.45f + Main.rand.NextFloat(0.4f);
			dust2.fadeIn = 0.7f + Main.rand.NextFloat(0.4f);
			dust2.noGravity = true;
			dust2.noLight = true;
		}
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
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		Vector2 drawPosition = base.Projectile.position + new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition;
		Texture2D alternateHookTexture = ((base.Projectile.spriteDirection == -1) ? ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/Spears/BansheeHookAlt", (AssetRequestMode)2).Value : TextureAssets.Projectile[base.Type].Value);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((base.Projectile.spriteDirection == 1) ? ((float)alternateHookTexture.Width + 8f) : (-8f), -8f);
		Main.EntitySpriteDraw(alternateHookTexture, drawPosition, null, new Color(255, 255, 255, 127), base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void PostDraw(Color lightColor)
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
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		Vector2 drawPosition = base.Projectile.position + new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition;
		Texture2D texture = ((base.Projectile.spriteDirection == -1) ? ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/Spears/BansheeHookAltGlow", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/Spears/BansheeHookGlow", (AssetRequestMode)2).Value);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((base.Projectile.spriteDirection == 1) ? ((float)texture.Width - -8f) : (-8f), -8f);
		Main.EntitySpriteDraw(texture, drawPosition, null, Color.White, base.Projectile.rotation, origin, 1f, (SpriteEffects)0);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		float angle = base.Projectile.rotation - (float)Math.PI / 4f * (float)Math.Sign(base.Projectile.velocity.X) + (float)(base.Projectile.spriteDirection == -1).ToInt() * (float)Math.PI;
		float areaCheck = -95f;
		float reduntantVariable = 0f;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + angle.ToRotationVector2() * areaCheck, (TravelSpeed + 1f) * base.Projectile.scale, ref reduntantVariable))
		{
			return true;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<BansheeHookBoom>(), (int)((double)hit.Damage * 0.25), hit.Knockback * 0.25f, base.Projectile.owner);
	}
}
