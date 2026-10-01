using System;
using System.IO;
using System.Runtime.CompilerServices;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class SylvRay : ModProjectile, ILocalizedModType, IModType
{
	[CompilerGenerated]
	private Vector2 _003CGlowCenter_003Ek__BackingField;

	public Vector2 GlowCenter
	{
		[CompilerGenerated]
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _003CGlowCenter_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_003CGlowCenter_003Ek__BackingField = value;
		}
	}

	public int Time
	{
		get
		{
			return (int)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 54;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 70;
		base.Projectile.extraUpdates = 3;
		base.Projectile.timeLeft = 60 * base.Projectile.extraUpdates;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		writer.WriteVector2(GlowCenter);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		GlowCenter = reader.ReadVector2();
	}

	public override void AI()
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.FinalExtraUpdate())
		{
			Time++;
		}
		base.Projectile.Opacity = Utils.GetLerpValue(0f, (float)base.Projectile.MaxUpdates * 10f, base.Projectile.timeLeft, clamped: true);
		base.Projectile.scale = Utils.GetLerpValue(1f, 9.5f, Time, clamped: true) * base.Projectile.Opacity;
		if (GlowCenter == Vector2.Zero)
		{
			GlowCenter = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 74f;
		}
		HandleBoltFiring();
		CreateGlowyDust();
		Lighting.AddLight(base.Projectile.Center, Vector3.One * base.Projectile.Opacity * 0.7f);
	}

	private void HandleBoltFiring()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		int shootRate = Sylvestaff.RayBoltShootRate;
		if (Time % shootRate == 0 && base.Projectile.FinalExtraUpdate())
		{
			int trailSearchPositions = 13;
			for (int i = 0; i < trailSearchPositions; i += 3)
			{
				TryToFireBolt(base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f, (float)i / (float)trailSearchPositions);
			}
		}
	}

	private void TryToFireBolt(Vector2 searchPosition, float hue)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		NPC potentialTarget = searchPosition.ClosestNPCAt(Sylvestaff.RayBoltTargetingRange, ignoreTiles: false);
		if (potentialTarget != null)
		{
			SoundStyle style = Sylvestaff.BounceSound with
			{
				MaxInstances = 12,
				Volume = 0.4f,
				Pitch = 0.4f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			float shootSpeed = ((Vector2)(ref base.Projectile.velocity)).Length();
			Vector2 shootVelocity = (potentialTarget.Center - searchPosition).SafeNormalize(Vector2.UnitY) * shootSpeed;
			if (base.Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), searchPosition, shootVelocity, ModContent.ProjectileType<SylvBolt>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, hue);
			}
			float burstSpeed = Main.rand.NextFloat(1f, 3f);
			for (int i = 0; i < 4; i++)
			{
				Dust dust = Dust.NewDustPerfect(searchPosition, 264);
				dust.velocity = base.Projectile.velocity.RotatedBy((float)Math.PI * 2f * (float)i / 4f + (float)Math.PI / 4f).SafeNormalize(Vector2.Zero) * burstSpeed;
				dust.color = Color.White;
				dust.noLight = true;
				dust.noGravity = true;
			}
		}
	}

	private void CreateGlowyDust()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		if (Time <= 9 && Main.rand.NextBool())
		{
			Color colorAccent = (Main.rand.NextBool() ? Color.HotPink : Color.Aqua);
			Dust dust = Dust.NewDustPerfect(GlowCenter, 264);
			dust.velocity = Main.rand.NextVector2Circular(4.6f, 4.6f) + base.Projectile.velocity * 0.25f;
			dust.color = Color.Lerp(Color.White, colorAccent, Main.rand.NextFloat(0.23f));
			dust.scale *= 1.1f;
			dust.fadeIn = 0.75f;
			dust.noGravity = true;
		}
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		float opacity = MathF.Pow(Utils.GetLerpValue(1f, 0.64f, completionRatio, clamped: true), 3f) * base.Projectile.Opacity;
		float increment = MathF.Cos((float)Math.PI * completionRatio - Main.GlobalTimeWrappedHourly * 7.2f) * 0.5f + 0.5f;
		Color pink = default(Color);
		((Color)(ref pink))._002Ector(255, 147, 255);
		Color blue = default(Color);
		((Color)(ref blue))._002Ector(109, 224, 255);
		return CalamityUtils.MulticolorLerp(increment, pink, Color.White, blue, Color.White) * opacity;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		float expansionCompletion = 1f - MathF.Pow(1f - Utils.GetLerpValue(0f, 0.3f, completionRatio, clamped: true), 2f);
		float maxWidth = MathF.Cos((float)Math.PI * completionRatio * 5f - Main.GlobalTimeWrappedHourly * 23f) * 2.4f + 32f;
		return MathHelper.Lerp(0f, base.Projectile.scale * maxWidth, expansionCompletion);
	}

	internal Vector2 OffsetFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return base.Projectile.Size * 0.5f;
	}

	private void RenderFrontGlow()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		float glowBump = CalamityUtils.Convert01To010(Utils.GetLerpValue(0f, 9.5f, Time, clamped: true));
		float glowRotation = base.Projectile.velocity.ToRotation();
		Vector2 glowScale = new Vector2(1f + glowBump * 0.8f, 1f) * glowBump;
		Vector2 startingPosition = GlowCenter - Main.screenPosition;
		Texture2D lightTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/BloomCirclePinpoint", (AssetRequestMode)2).Value;
		SpriteBatch spriteBatch = Main.spriteBatch;
		Color val = ColorFunction(0f, Vector2.Zero);
		((Color)(ref val)).A = 0;
		spriteBatch.Draw(lightTexture, startingPosition, (Rectangle?)null, val, glowRotation, lightTexture.Size() * 0.5f, glowScale, (SpriteEffects)0, 0f);
		SpriteBatch spriteBatch2 = Main.spriteBatch;
		val = ColorFunction(0f, Vector2.Zero);
		((Color)(ref val)).A = 0;
		spriteBatch2.Draw(lightTexture, startingPosition, (Rectangle?)null, val, glowRotation, lightTexture.Size() * 0.5f, glowScale * 0.4f, (SpriteEffects)0, 0f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		RenderFrontGlow();
		MiscShaderData rayShader = GameShaders.Misc["CalamityMod:SylvestaffProjectile"];
		rayShader.SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, OffsetFunction, smoothen: true, pixelate: false, rayShader), 32);
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.penetrate--;
		if (base.Projectile.penetrate <= 0)
		{
			base.Projectile.Kill();
		}
		else
		{
			SoundEngine.PlaySound(in Sylvestaff.BounceSound, base.Projectile.Center);
			if (base.Projectile.velocity.X != oldVelocity.X)
			{
				base.Projectile.velocity.X = 0f - oldVelocity.X;
			}
			if (base.Projectile.velocity.Y != oldVelocity.Y)
			{
				base.Projectile.velocity.Y = 0f - oldVelocity.Y;
			}
		}
		return false;
	}
}
