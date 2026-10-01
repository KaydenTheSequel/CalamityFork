using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class InfernadoRevenge : ModProjectile, ILocalizedModType, IModType
{
	public const int TornadoHeight = 8800;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 320;
		base.Projectile.height = 1020;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 360000;
		base.CooldownSlot = 1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
	}

	public override void AI()
	{
		if (!CalamityPlayer.areThereAnyDamnBosses)
		{
			base.Projectile.active = false;
			base.Projectile.netUpdate = true;
		}
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(Color.Yellow, Color.Yellow, completionRatio);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		float _ = 0f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Bottom, base.Projectile.Bottom - Vector2.UnitY * 8800f, 72f, ref _);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.EnterShaderRegion();
		GameShaders.Misc["CalamityMod:Bordernado"].UseSaturation(-0.2f);
		GameShaders.Misc["CalamityMod:Bordernado"].UseOpacity(1f);
		GameShaders.Misc["CalamityMod:Bordernado"].SetShaderTexture(ModContent.Request<Texture2D>("Terraria/Images/Misc/Perlin", (AssetRequestMode)2));
		Vector2[] drawPoints = (Vector2[])(object)new Vector2[5];
		Vector2 upwardAscent = Vector2.UnitY * 8800f;
		Vector2 downwardOffset = Vector2.UnitY * (float)base.Projectile.height / (float)(drawPoints.Length + 1);
		Vector2 bottom = base.Projectile.Bottom + downwardOffset;
		Vector2 top = bottom - upwardAscent;
		for (int i = 0; i < drawPoints.Length - 1; i++)
		{
			drawPoints[i] = Vector2.Lerp(top, bottom, (float)i / (float)(drawPoints.Length - 1));
		}
		drawPoints[^1] = bottom;
		PrimitiveRenderer.RenderTrail(drawPoints, new PrimitiveSettings((float _, Vector2 _) => (float)base.Projectile.width * 0.5f + 16f, ColorFunction, null, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:Bordernado"]), 85);
		Main.spriteBatch.ExitShaderRegion();
		Main.spriteBatch.EnterShaderRegion();
		Texture2D vortexNoise = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Cracks", (AssetRequestMode)2).Value;
		GameShaders.Misc["CalamityMod:DoGPortal"].UseOpacity(1f);
		GameShaders.Misc["CalamityMod:DoGPortal"].UseColor(Color.Gold);
		GameShaders.Misc["CalamityMod:DoGPortal"].UseSecondaryColor(Color.White);
		GameShaders.Misc["CalamityMod:DoGPortal"].Apply();
		for (int i2 = 0; i2 < 5; i2++)
		{
			float angle = (float)Math.PI * 2f * (float)i2 / 5f + Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f);
			Color drawColor = Color.White;
			((Color)(ref drawColor)).A = 0;
			Vector2 drawPosition = base.Projectile.Bottom - Main.screenPosition + angle.ToRotationVector2() * 3f;
			Main.EntitySpriteDraw(vortexNoise, drawPosition, null, drawColor, angle + (float)Math.PI / 2f, vortexNoise.Size() * 0.5f, base.Projectile.scale * 1.5f, (SpriteEffects)0);
		}
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Dragonfire>(), 180);
		}
	}
}
