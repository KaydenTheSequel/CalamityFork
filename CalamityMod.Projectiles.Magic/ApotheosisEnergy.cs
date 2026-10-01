using System;
using CalamityMod.Graphics.Primitives;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

[PierceResistException(false)]
public class ApotheosisEnergy : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 210;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 4;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color cyan = Color.Cyan;
		Lighting.AddLight(center, ((Color)(ref cyan)).ToVector3() * base.Projectile.Opacity);
		if ((float)base.Projectile.timeLeft < 40f)
		{
			base.Projectile.Opacity = Utils.GetLerpValue(0f, 40f, base.Projectile.timeLeft, clamped: true);
		}
		else
		{
			base.Projectile.alpha = Utils.Clamp(base.Projectile.alpha - 30, 0, 255);
		}
		NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(5900f);
		if (potentialTarget != null)
		{
			float squaredTargetDistance = base.Projectile.DistanceSQ(potentialTarget.Center);
			if (squaredTargetDistance > 14400f && squaredTargetDistance < 1000000f)
			{
				base.Projectile.velocity = base.Projectile.velocity.ToRotation().AngleTowards(base.Projectile.AngleTo(potentialTarget.Center), 0.1f).ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length();
			}
		}
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		Color baseColor = Color.Cyan;
		if (completionRatio > 0.66f)
		{
			baseColor = Color.Lerp(baseColor, Color.Fuchsia, (completionRatio - 0.66f) / 0.33f);
		}
		else
		{
			float whiteFade = (float)Math.Sin(Utils.GetLerpValue(0f, 0.2f, completionRatio, clamped: true) * (float)Math.PI + Main.GlobalTimeWrappedHourly * 3f) * 0.45f;
			baseColor = Color.Lerp(baseColor, Color.White, whiteFade);
		}
		Color colorToUse = baseColor;
		if (completionRatio > 0.5f)
		{
			colorToUse = Color.Lerp(baseColor, Color.Transparent, 1f - completionRatio / 0.5f);
		}
		return colorToUse * base.Projectile.Opacity;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		float maxWidthOutwardness = 6f;
		float width = ((!(completionRatio < 0.2f)) ? MathHelper.Lerp(maxWidthOutwardness, 0f, Utils.GetLerpValue(0.2f, 1f, completionRatio, clamped: true)) : ((float)Math.Sin(completionRatio / 0.2f * ((float)Math.PI / 2f)) * maxWidthOutwardness + 0.1f));
		return width * base.Projectile.Opacity;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < base.Projectile.oldPos.Length; i++)
		{
			if (Collision.CheckAABBvAABBCollision(base.Projectile.oldPos[i], projHitbox.Size(), targetHitbox.TopLeft(), targetHitbox.Size()))
			{
				return true;
			}
		}
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}), 85);
		return false;
	}
}
