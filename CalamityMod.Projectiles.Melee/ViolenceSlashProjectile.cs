using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class ViolenceSlashProjectile : ModProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Violence>();

	internal Player Owner => Main.player[base.Projectile.owner];

	internal ref float Time => ref base.Projectile.ai[0];

	internal float SwingSine => (float)Math.Sin((float)Math.PI * 2f * Time / 50f);

	public override string Texture => "CalamityMod/Items/Weapons/Melee/Violence";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 36;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 142);
		base.Projectile.aiStyle = -1;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 90000;
		base.Projectile.ignoreWater = true;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 6;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 15f, Time, clamped: true);
		if (!Owner.channel)
		{
			base.Projectile.Kill();
			return;
		}
		ManipulatePlayerFields();
		DoMovement();
		Vector2 position = base.Projectile.Center + (base.Projectile.rotation + (float)Math.PI / 4f).ToRotationVector2() * (float)base.Projectile.height * 0.45f;
		Color red = Color.Red;
		Lighting.AddLight(position, ((Color)(ref red)).ToVector3() * 0.4f);
		Time++;
	}

	internal void DoMovement()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = Owner.RotatedRelativePoint(Owner.MountedCenter) + (base.Projectile.rotation - (float)Math.PI / 4f).ToRotationVector2() * (float)base.Projectile.height * 0.45f;
		base.Projectile.rotation = SwingSine * MathHelper.ToRadians(87f);
		Vector2 mouse = Owner.ClampedMouseWorld();
		if (Main.myPlayer == base.Projectile.owner && !base.Projectile.WithinRange(mouse, (float)base.Projectile.height + 15f))
		{
			base.Projectile.velocity = base.Projectile.SafeDirectionTo(mouse);
			base.Projectile.ForceNetUpdate();
		}
		base.Projectile.rotation += base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
	}

	internal void ManipulatePlayerFields()
	{
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Owner.ChangeDir((Math.Cos(base.Projectile.rotation - (float)Math.PI / 4f) > 0.0).ToDirectionInt());
	}

	internal Color PrimitiveColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.oldRot.Average((float angle) => MathHelper.WrapAngle(angle) + (float)Math.PI);
		float opacity = base.Projectile.Opacity * Utils.GetLerpValue(0.75f, 0.45f, completionRatio, clamped: true) * 0.5f;
		opacity *= Utils.GetLerpValue(0.125f, 0.15f, Math.Abs(SwingSine), clamped: true);
		MathHelper.WrapAngle(base.Projectile.rotation);
		MathHelper.WrapAngle(base.Projectile.oldRot[1]);
		return Color.Lerp(Color.Red * 1.1f, Color.DarkRed, Utils.GetLerpValue(0f, 0.5f, completionRatio, clamped: true)) * opacity;
	}

	internal float PrimitiveWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return (float)base.Projectile.height * 0.48f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:PhaseslayerRipEffect"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SwordSlashTexture", (AssetRequestMode)2));
		_ = TextureAssets.Projectile[base.Type].Value;
		_ = Main.player[base.Projectile.owner];
		List<Vector2> positions = new List<Vector2>();
		for (int i = 0; i < 16; i++)
		{
			Vector2 position = base.Projectile.position + (base.Projectile.rotation - (float)Math.PI / 4f).ToRotationVector2() * (PrimitiveWidthFunction(0f, Vector2.Zero) - 30f) * base.Projectile.scale * 0.5f;
			float angleOffset = (float)Math.PI / 4f * (float)(-Math.Sign(SwingSine)) * (float)i / 16f;
			position += (base.Projectile.rotation - (float)Math.PI / 4f + (float)Math.PI / 2f).ToRotationVector2().RotatedBy(angleOffset) * (0f - SwingSine) * (float)i * 12f;
			positions.Add(position);
		}
		PrimitiveRenderer.RenderTrail(positions, new PrimitiveSettings(PrimitiveWidthFunction, PrimitiveColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:PhaseslayerRipEffect"]), 50);
		return true;
	}
}
