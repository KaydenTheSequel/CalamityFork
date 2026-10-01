using System;
using System.IO;
using System.Linq;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.NPCs;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

[PierceResistException(false)]
public class PhaseslayerProjectile : ModProjectile
{
	public const float StandardSwingSpeed = (float)Math.PI / 60f;

	public const float DamageUpdateResponsiveness = 0.08f;

	public const int SwordBeamCooldown = 15;

	public const float SwordBeamDamageMultiplier = 0.2f;

	private const float MaximumMouseRange = 360f;

	private const float ProjCenterOffset = 36f;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Phaseslayer>();

	public override string Texture => "CalamityMod/Items/Weapons/DraedonsArsenal/Phaseslayer";

	public bool IsSmall => false;

	public float AngularDamageFactor
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public float FadeoutTime
	{
		get
		{
			return base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = value;
		}
	}

	public int BladeFrameX
	{
		get
		{
			if (!IsSmall)
			{
				return base.Projectile.frame / 7;
			}
			return 1;
		}
	}

	public int BladeFrameY
	{
		get
		{
			if (!IsSmall)
			{
				return base.Projectile.frame % 7;
			}
			return base.Projectile.frame % 3;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 13;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 46;
		base.Projectile.height = 46;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.rotation);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.rotation = reader.ReadSingle();
	}

	public override void AI()
	{
		Player player = Main.player[base.Projectile.owner];
		CalamityGlobalItem modItem = player.HeldItem.Calamity();
		float num = MathHelper.WrapAngle(base.Projectile.rotation) + (float)Math.PI;
		float oldRotationAdjusted = MathHelper.WrapAngle(base.Projectile.oldRot[1]) + (float)Math.PI;
		float deltaAngle = Math.Abs(MathHelper.WrapAngle(num - oldRotationAdjusted));
		if (base.Projectile.localAI[1] == 0f)
		{
			base.Projectile.localAI[1] = 1f;
			base.Projectile.soundDelay = 15;
		}
		ManipulatePlayer(player, modItem);
		bool wasBig = !IsSmall;
		if (IsSmall & wasBig)
		{
			OnShrinkEffects();
		}
		AdjustCurrentDamage(player, deltaAngle);
		ManipulateFrames();
		HandleSwordBeams(player, modItem, deltaAngle);
		HandleFadeout();
	}

	private void ManipulatePlayer(Player player, CalamityGlobalItem modItem)
	{
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == player.whoAmI)
		{
			Item playerItem = player.HeldItem;
			bool hasCharge = true;
			if ((!player.CantUseHoldout() && playerItem.type == ModContent.ItemType<Phaseslayer>()) & hasCharge)
			{
				float mouseDistance = base.Projectile.Distance(Main.MouseWorld);
				float distRatio = Utils.GetLerpValue(0f, 360f, mouseDistance, clamped: true);
				float aimResponsiveness = 0.035f + 0.3f * MathF.Pow(distRatio, 1f / 3f);
				float newRotation = base.Projectile.rotation.AngleLerp(player.AngleTo(Main.MouseWorld), aimResponsiveness);
				if (base.Projectile.rotation != newRotation)
				{
					base.Projectile.ForceNetUpdate();
				}
				base.Projectile.rotation = newRotation;
			}
			else if (FadeoutTime == 0f)
			{
				if (!player.channel)
				{
					FadeoutTime = 10f;
				}
				else
				{
					base.Projectile.Kill();
				}
			}
		}
		base.Projectile.Center = player.MountedCenter + base.Projectile.rotation.ToRotationVector2() * 36f;
		base.Projectile.direction = (Math.Cos(base.Projectile.rotation) > 0.0).ToDirectionInt();
		player.ChangeDir(base.Projectile.direction);
		player.itemTime = 2;
		player.itemAnimation = 2;
		player.itemRotation = CalamityUtils.WrapAngle90Degrees(base.Projectile.rotation);
	}

	private void OnShrinkEffects()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in Karasawa.FireSound, base.Projectile.Center);
		if (!Main.dedServ)
		{
			for (int i = 0; i < 60; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 235);
				dust.velocity = Main.rand.NextVector2Circular(20f, 20f);
				dust.scale = 2.5f;
				dust.fadeIn = 1.2f;
				dust.noGravity = true;
			}
		}
	}

	private void AdjustCurrentDamage(Player player, float deltaAngle)
	{
		AngularDamageFactor = MathHelper.Lerp(AngularDamageFactor, deltaAngle, 0.08f);
		float speedDamageScalar = MathF.Log(AngularDamageFactor / ((float)Math.PI / 60f) + 3f, 3f);
		int damageWithChargeAndStats = player.GetWeaponDamage(player.HeldItem);
		float sizeDamageScalar = (IsSmall ? 0.9f : 1f);
		base.Projectile.damage = (int)((float)damageWithChargeAndStats * speedDamageScalar * sizeDamageScalar);
	}

	private void ManipulateFrames()
	{
		base.Projectile.frame = 0;
		if (IsSmall)
		{
			if (FadeoutTime > 5f)
			{
				base.Projectile.frame = 1;
			}
			else if (FadeoutTime > 0f)
			{
				base.Projectile.frame = 2;
			}
			return;
		}
		base.Projectile.frameCounter++;
		int adjustFrameCounter = base.Projectile.frameCounter % 120;
		if (adjustFrameCounter >= 50 && adjustFrameCounter <= 78)
		{
			base.Projectile.frame = (int)MathHelper.Lerp(1f, 9f, Utils.GetLerpValue(50f, 75f, adjustFrameCounter, clamped: true));
		}
		if (adjustFrameCounter >= 90 && adjustFrameCounter <= 120)
		{
			base.Projectile.frame = (int)MathHelper.Lerp(10f, 18f, Utils.GetLerpValue(90f, 117f, adjustFrameCounter, clamped: true));
		}
		if (FadeoutTime > 5f)
		{
			base.Projectile.frame = 19;
		}
		else if (FadeoutTime > 0f)
		{
			base.Projectile.frame = 20;
		}
	}

	private void HandleSwordBeams(Player player, CalamityGlobalItem modItem, float deltaAngle)
	{
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.soundDelay <= 0 && deltaAngle >= 0.06806784f)
		{
			if (Main.myPlayer == player.whoAmI)
			{
				Vector2 velocity = base.Projectile.rotation.ToRotationVector2() * 20f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<PhaseslayerBeam>(), (int)((float)base.Projectile.damage * 0.2f), 0f, player.whoAmI);
			}
			base.Projectile.soundDelay = 15;
			SoundEngine.PlaySound(in CommonCalamitySounds.ELRFireSound, base.Projectile.Center);
		}
	}

	private void HandleFadeout()
	{
		if (FadeoutTime > 0f)
		{
			FadeoutTime--;
			if (FadeoutTime <= 0f)
			{
				base.Projectile.Kill();
			}
		}
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		float deltaAngle = Math.Abs(base.Projectile.oldRot.Take(20).Average((float angle) => MathHelper.WrapAngle(angle) + (float)Math.PI) - (MathHelper.WrapAngle(base.Projectile.rotation) + (float)Math.PI));
		float opacity = base.Projectile.Opacity;
		opacity *= Utils.GetLerpValue(0.036651913f, (float)Math.PI / 60f, AngularDamageFactor, clamped: true);
		opacity *= MathF.Pow(Utils.GetLerpValue(1f, 0.45f, completionRatio, clamped: true), 4f);
		opacity *= MathF.Pow(Utils.GetLerpValue(0.9f, 1.1f, deltaAngle, clamped: true), 2f);
		float num = MathHelper.WrapAngle(base.Projectile.rotation) + (float)Math.PI;
		float oldRotationAdjusted = MathHelper.WrapAngle(base.Projectile.oldRot[1]) + (float)Math.PI;
		deltaAngle = Math.Abs(MathHelper.WrapAngle(num - oldRotationAdjusted));
		if (deltaAngle < 0.04f)
		{
			opacity = 0f;
		}
		return Color.Lerp(Color.Red, Color.PaleVioletRed * completionRatio, MathHelper.Clamp(completionRatio * 0.8f, 0f, 1f)) * opacity;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return (IsSmall ? 101f : 127f) * (1f - completionRatio) * 0.8f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		Texture2D bladeTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/PhaseslayerBlade", (AssetRequestMode)2).Value;
		Texture2D hiltTexture = TextureAssets.Projectile[base.Type].Value;
		if (IsSmall)
		{
			bladeTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/PhaseslayerBladeSmall", (AssetRequestMode)2).Value;
		}
		float bladeLength = (IsSmall ? 90f : 132f) * base.Projectile.scale;
		Vector2 bladeOffset = base.Projectile.rotation.ToRotationVector2() * bladeLength;
		Vector2 origin = bladeTexture.Size() * 0.5f;
		origin /= (IsSmall ? new Vector2(1f, 3f) : new Vector2(3f, 7f));
		Rectangle frame = (IsSmall ? bladeTexture.Frame(1, 3, 0, BladeFrameY) : bladeTexture.Frame(3, 7, BladeFrameX, BladeFrameY));
		GameShaders.Misc["CalamityMod:PhaseslayerRipEffect"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SwordSlashTexture", (AssetRequestMode)2));
		_ = Main.player[base.Projectile.owner];
		float swingAngularDirection = Math.Sign(MathHelper.WrapAngle(base.Projectile.rotation - base.Projectile.oldRot[1]));
		Vector2[] drawPoints = (Vector2[])(object)new Vector2[base.Projectile.oldPos.Length];
		Vector2 perpendicularDirection = bladeOffset.SafeNormalize(Vector2.UnitY).RotatedBy(1.5707963705062866);
		for (int i = 0; i < drawPoints.Length; i++)
		{
			if (!(base.Projectile.oldPos[i] == Vector2.Zero))
			{
				float swingFactor = MathHelper.Min(1f, AngularDamageFactor);
				float offsetFactor = (float)i * (0f - swingAngularDirection) * MathHelper.Min(0.8f, swingFactor) * 100f;
				float angularTurn = (float)i * swingAngularDirection * -0.09f;
				drawPoints[i] = base.Projectile.position + perpendicularDirection.RotatedBy(angularTurn) * offsetFactor;
			}
		}
		PrimitiveRenderer.RenderTrail(drawPoints, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f + bladeOffset;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:PhaseslayerRipEffect"]), 50);
		Main.EntitySpriteDraw(bladeTexture, base.Projectile.Center + bladeOffset - Main.screenPosition, frame, Color.White, base.Projectile.rotation + (float)Math.PI / 2f, origin, base.Projectile.scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(hiltTexture, base.Projectile.Center - Main.screenPosition, null, lightColor, base.Projectile.rotation + (float)Math.PI / 2f, hiltTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		float _ = 0f;
		Vector2 start = base.Projectile.Center + base.Projectile.rotation.ToRotationVector2() * 28f;
		Vector2 end = start + base.Projectile.rotation.ToRotationVector2() * (IsSmall ? 202f : 254f) * base.Projectile.scale;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, 60f * base.Projectile.scale, ref _);
	}
}
