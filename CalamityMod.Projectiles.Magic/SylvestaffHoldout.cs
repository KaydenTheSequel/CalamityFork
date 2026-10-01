using System;
using System.Linq;
using CalamityMod.Enums;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class SylvestaffHoldout : ModProjectile, IPixelatedPrimitiveRenderer
{
	public RopeHandle? LeftRibbon;

	public RopeHandle? RightRibbon;

	public GeneralDrawLayer LayerToRenderTo => GeneralDrawLayer.BeforeProjectiles | GeneralDrawLayer.AfterPlayers;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Sylvestaff>();

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Time => ref base.Projectile.ai[0];

	public Vector2 RibbonAttachPoint
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center + base.Projectile.velocity * base.Projectile.scale * (float)base.Projectile.width * 0.34f;
		}
	}

	private static float RibbonLength => 70f;

	public override string Texture => ModContent.GetInstance<Sylvestaff>().Texture;

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 94);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.timeLeft = 72000;
	}

	public override void AI()
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		if (!Owner.channel)
		{
			base.Projectile.Kill();
		}
		RopeHandle? leftRibbon = LeftRibbon;
		if (!leftRibbon.HasValue)
		{
			leftRibbon = RightRibbon;
			if (!leftRibbon.HasValue)
			{
				InitializeRibbons();
			}
		}
		AimTowardsMouse();
		HandleHoldoutLogic();
		OrientOwnerArms();
		FireAwesomeMagicRays();
		UpdateRibbon(LeftRibbon, base.Projectile.velocity.RotatedBy(-1.5707963705062866));
		UpdateRibbon(RightRibbon, base.Projectile.velocity.RotatedBy(1.5707963705062866));
		Time++;
	}

	private void InitializeRibbons()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		int ribbonSegmentCount = 12;
		float distancePerSegment = RibbonLength / (float)ribbonSegmentCount;
		RopeSettings ribbonSettings = new RopeSettings
		{
			StartIsFixed = true,
			Mass = 0.72f,
			RespondToEntityMovement = true,
			RespondToWind = true
		};
		LeftRibbon = ModContent.GetInstance<RopeManagerSystem>().RequestNew(RibbonAttachPoint, base.Projectile.Center, ribbonSegmentCount, distancePerSegment, Vector2.Zero, ribbonSettings, 25);
		RightRibbon = ModContent.GetInstance<RopeManagerSystem>().RequestNew(RibbonAttachPoint, base.Projectile.Center, ribbonSegmentCount, distancePerSegment, Vector2.Zero, ribbonSettings, 25);
	}

	private void HandleHoldoutLogic()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = Owner.MountedCenter + Vector2.UnitY * 7f + base.Projectile.velocity * (float)base.Projectile.width * 0.31f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.Center = Owner.RotatedRelativePoint(center) - Vector2.UnitY * Owner.gfxOffY;
		base.Projectile.spriteDirection = (Math.Cos(base.Projectile.rotation) > 0.0).ToDirectionInt();
		Owner.ChangeDir(base.Projectile.spriteDirection);
		Owner.SetDummyItemTime(2);
		Owner.heldProj = base.Projectile.whoAmI;
		base.Projectile.rotation += (float)Math.PI / 4f;
		if (base.Projectile.spriteDirection == -1)
		{
			base.Projectile.rotation += (float)Math.PI / 2f;
		}
	}

	private void OrientOwnerArms()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		float baseRotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		float directionVerticality = MathF.Abs(base.Projectile.velocity.X);
		Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, baseRotation + (float)Owner.direction * directionVerticality * ((float)Math.PI / 4f));
		Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, baseRotation + (float)Owner.direction * directionVerticality * 0.33f);
	}

	private void AimTowardsMouse()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Vector2 idealDirection = base.Projectile.SafeDirectionTo(Main.MouseWorld);
			Vector2 newDirection = Vector2.Lerp(base.Projectile.velocity, idealDirection, Sylvestaff.TurnSpeedInterpolant).SafeNormalize(Vector2.UnitX * (float)Owner.direction);
			if (base.Projectile.velocity != newDirection)
			{
				base.Projectile.velocity = newDirection;
				base.Projectile.netUpdate = true;
				base.Projectile.netSpam = 0;
			}
		}
	}

	private void UpdateRibbon(RopeHandle? ribbon, Vector2 gravityDirection)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if (ribbon.HasValue)
		{
			RopeHandle rope = ribbon.GetValueOrDefault();
			rope.Start = RibbonAttachPoint;
			rope.Gravity = gravityDirection * 0.15f - base.Projectile.velocity * 0.4f;
		}
	}

	private void FireAwesomeMagicRays()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		Item heldItem = Owner.HeldItem;
		if (heldItem != null && Time % (float)heldItem.useAnimation == (float)(heldItem.useAnimation - 1) && Owner.CheckMana(heldItem.mana, pay: true))
		{
			SoundEngine.PlaySound(in Sylvestaff.FireSound, base.Projectile.Center);
			if (Main.myPlayer == base.Projectile.owner)
			{
				int damage = Owner.GetWeaponDamage(heldItem);
				Vector2 shootVelocity = base.Projectile.velocity * heldItem.shootSpeed;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, shootVelocity, ModContent.ProjectileType<SylvRay>(), damage, heldItem.knockBack, base.Projectile.owner);
				Projectile projectile = base.Projectile;
				projectile.velocity -= base.Projectile.velocity.RotatedBy((float)base.Projectile.spriteDirection * ((float)Math.PI / 2f)) * Sylvestaff.StaffRecoilForce;
			}
		}
	}

	public void RenderPixelatedPrimitives(SpriteBatch spriteBatch, GeneralDrawLayer layer)
	{
		bool backLayer = layer == GeneralDrawLayer.BeforeProjectiles;
		RenderRibbon(LeftRibbon, -1, backLayer);
		RenderRibbon(RightRibbon, 1, backLayer);
	}

	private float RibbonWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return base.Projectile.scale * Utils.GetLerpValue(0f, 0.2f, completionRatio, clamped: true) * 3.6f;
	}

	private Color RibbonColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		Color light = Lighting.GetColor(RibbonAttachPoint.ToTileCoordinates());
		return base.Projectile.GetAlpha(light);
	}

	private void RenderRibbon(RopeHandle? ribbon, int direction, bool backLayer)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		if (ribbon.HasValue)
		{
			RopeHandle rope = ribbon.GetValueOrDefault();
			Vector2 forwardDirection = base.Projectile.velocity;
			Vector2 sideDirection = forwardDirection.RotatedBy((float)Math.PI / 2f * (float)direction);
			Vector2 attachmentPoint = RibbonAttachPoint;
			Vector2[] ribbonPositions = rope.Positions.ToArray();
			int positionCount = ribbonPositions.Length;
			for (int i = 0; i < ribbonPositions.Length; i++)
			{
				float completionRatio = (float)i / (float)positionCount;
				float wave = MathF.Cos((float)Math.PI * completionRatio * 1.5f - (float)Math.PI * 2f * Time / 97f) * completionRatio;
				Vector2 backwardsOffset = forwardDirection * (float)i * (0f - RibbonLength) / (float)positionCount;
				Vector2 sideWavyOffset = sideDirection * wave * RibbonLength * 0.5f;
				Vector2 rigidPosition = attachmentPoint + backwardsOffset + sideWavyOffset;
				ribbonPositions[i] = Vector2.Lerp(ribbonPositions[i], rigidPosition, 0.76f);
			}
			Vector2 intersectionPosition = Vector2.Transform((base.Projectile.Center - Main.screenPosition) * 0.5f, Main.GameViewMatrix.TransformationMatrix);
			MiscShaderData ribbonShader = GameShaders.Misc["CalamityMod:SylvestaffRibbon"];
			ribbonShader.UseShaderSpecificData(new Vector4(intersectionPosition.X, intersectionPosition.Y, sideDirection.X, sideDirection.Y));
			ribbonShader.UseSaturation(backLayer ? (-1f) : 1f);
			PrimitiveSettings primitiveSettings = new PrimitiveSettings(RibbonWidthFunction, RibbonColorFunction, null, smoothen: true, pixelate: true, ribbonShader);
			PrimitiveRenderer.RenderTrail(ribbonPositions, primitiveSettings, 33);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection != 1);
		Main.spriteBatch.Draw(texture, drawPosition, (Rectangle?)null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, texture.Size() * 0.5f, base.Projectile.scale, direction, 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		LeftRibbon?.Dispose();
		RightRibbon?.Dispose();
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
