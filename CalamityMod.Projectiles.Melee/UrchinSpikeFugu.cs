using System;
using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class UrchinSpikeFugu : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 6);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 90;
		base.Projectile.noEnchantments = true;
	}

	public override void AI()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.alpha = (int)Utils.Remap(Time, 0f, 12f, 255f, 0f);
		NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(256f);
		if (potentialTarget != null && Time >= 12f)
		{
			Vector2 idealVelocity = base.Projectile.SafeDirectionTo(potentialTarget.Center) * 12f;
			base.Projectile.velocity = (base.Projectile.velocity * 20f + idealVelocity) / 21f;
			base.Projectile.velocity = base.Projectile.velocity.MoveTowards(idealVelocity, 2f);
		}
		else if (Time >= 48f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.9f;
		}
		int dustRate = (int)MathF.Max(Utils.Remap(Time, 0f, 12f, 20f, 4f), Utils.Remap(Time, 60f, 90f, 4f, 20f));
		if (Main.rand.NextBool(dustRate))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 171, Main.rand.NextVector2Circular(0.2f, 0.2f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.6f, 1.2f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(20, 120);
	}

	public override bool? CanDamage()
	{
		if (!(Time < 12f))
		{
			return base.CanDamage();
		}
		return false;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return (1f - completionRatio) * base.Projectile.scale * 4f;
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return new Color(91, 62, 153) * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 8);
		return true;
	}
}
