using System;
using System.IO;
using CalamityMod.CalPlayer;
using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TaintedBladeSlasher : ModProjectile, ILocalizedModType, IModType
{
	public const float ForearmLength = 80f;

	public const float ArmLength = 108f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public ref float SwordItemID => ref base.Projectile.ai[1];

	public ref float VerticalOffset => ref base.Projectile.localAI[0];

	public ref float Time => ref base.Projectile.localAI[1];

	public float AttackCompletionRatio
	{
		get
		{
			float completionRatio = 1f - (float)Owner.itemAnimation / (float)Owner.itemAnimationMax;
			if (float.IsNaN(completionRatio) || float.IsInfinity(completionRatio))
			{
				completionRatio = 0f;
			}
			return completionRatio;
		}
	}

	public Player Owner => Main.player[base.Projectile.owner];

	public int Variant => (int)base.Projectile.ai[0] % 2;

	public Rectangle BladeFrame
	{
		get
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			Texture2D bladeTexture = TextureAssets.Item[(int)SwordItemID].Value;
			Rectangle bladeFrame = bladeTexture.Frame();
			if (Main.itemAnimations[(int)SwordItemID] != null)
			{
				bladeFrame = Main.itemAnimations[(int)SwordItemID].GetFrame(bladeTexture);
			}
			return bladeFrame;
		}
	}

	public Vector2 BackArmAimPosition
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Unknown result type (might be due to invalid IL or missing references)
			//IL_009a: Unknown result type (might be due to invalid IL or missing references)
			//IL_009b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0067: Unknown result type (might be due to invalid IL or missing references)
			Vector2 baseAimPosition = Owner.Top + new Vector2((float)(-Owner.direction) * (80f + Math.Abs(VerticalOffset) * 0.55f), -27f);
			if (Variant == 1)
			{
				baseAimPosition.Y += 30f;
			}
			if (Owner.itemAnimation == 0)
			{
				return baseAimPosition;
			}
			Vector2 endSwingPosition = Owner.Center + Vector2.UnitX * (float)Owner.direction * 510f;
			return Vector2.SmoothStep(baseAimPosition, endSwingPosition, Utils.GetLerpValue(0f, 0.67f, AttackCompletionRatio, clamped: true) * Utils.GetLerpValue(1f, 0.67f, AttackCompletionRatio, clamped: true));
		}
	}

	public Vector2 IdleMoveOffset
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_0067: Unknown result type (might be due to invalid IL or missing references)
			//IL_0071: Unknown result type (might be due to invalid IL or missing references)
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_007b: Unknown result type (might be due to invalid IL or missing references)
			Vector2 offset = ((Owner.itemAnimation == 0) ? ((Time / 27f).ToRotationVector2() * new Vector2(5f, 4f)) : Vector2.Zero);
			if (Variant == 1)
			{
				offset += Vector2.UnitX * (float)Owner.direction * (1f - AttackCompletionRatio) * 40f;
			}
			return offset;
		}
	}

	public Vector2 FrontArmEnd
	{
		get
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			float backArmRotation = Owner.AngleTo(BackArmAimPosition);
			Vector2 fromArmDrawPosition = Owner.Center + backArmRotation.ToRotationVector2() * 80f;
			Vector2 backOffset = (base.Projectile.Center - fromArmDrawPosition).SafeNormalize(Vector2.Zero) * 108f + IdleMoveOffset;
			return fromArmDrawPosition + backOffset;
		}
	}

	public Vector2 BladeOffsetDirection
	{
		get
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_0086: Unknown result type (might be due to invalid IL or missing references)
			float backArmRotation = Owner.AngleTo(BackArmAimPosition);
			Vector2 fromArmDrawPosition = Owner.Center + backArmRotation.ToRotationVector2() * 80f;
			return -(((base.Projectile.Center - fromArmDrawPosition).SafeNormalize(Vector2.Zero) * 108f + IdleMoveOffset).ToRotation() - (float)Math.PI / 4f + (float)Math.PI + (float)Math.PI / 4f + BladeRotationOffset).ToRotationVector2();
		}
	}

	public Vector2 BladeCenterPosition
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			float offsetFactor = (float)BladeFrame.Height / 4f + 31f;
			return FrontArmEnd + BladeOffsetDirection * offsetFactor;
		}
	}

	public float BladeRotationOffset => MathHelper.Lerp((float)Math.PI / 2f, 0f, Utils.GetLerpValue(0f, 0.17f, 1f - AttackCompletionRatio, clamped: true)) * (float)(-Owner.direction);

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 70;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 4);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = TrueMeleeDamageClass.Instance;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 90000;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.noEnchantmentVisuals = true;
		base.Projectile.ContinuouslyUpdateDamageStats = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(VerticalOffset);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		VerticalOffset = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer.EnchantHeldItemEffects(Owner, Owner.Calamity(), Owner.HeldItem);
		if (!Owner.Calamity().bladeArmEnchant || (float)Owner.HeldItem.type != SwordItemID || Owner.CCed || !Owner.active || Owner.dead)
		{
			base.Projectile.Kill();
			return;
		}
		if (Owner.itemAnimationMax == 0)
		{
			Owner.itemAnimationMax = (int)((float)Owner.HeldItem.useAnimation * Owner.GetAttackSpeed<MeleeDamageClass>());
		}
		float swingOffsetAngle = MathHelper.SmoothStep(-1.87f, 3.79f, AttackCompletionRatio);
		if (Variant == 1)
		{
			swingOffsetAngle -= 0.7f;
		}
		swingOffsetAngle *= (float)Owner.direction;
		swingOffsetAngle = MathHelper.Lerp(0f, swingOffsetAngle, Utils.GetLerpValue(0f, 0.16f, AttackCompletionRatio, clamped: true));
		if (Owner.itemAnimation == 0)
		{
			swingOffsetAngle = 0f;
		}
		Vector2 destination = Owner.Center;
		if (Owner.itemAnimation == 0)
		{
			destination.X += (float)Owner.direction * (VerticalOffset * 0.6f + 180f);
			destination.Y += 330f + VerticalOffset;
			if (Variant == 1)
			{
				destination.Y += 540f;
			}
			base.Projectile.localNPCHitCooldown = 24;
		}
		else
		{
			destination -= Vector2.UnitY.RotatedBy(swingOffsetAngle) * 600f;
			if (Owner.itemAnimation == 21)
			{
				SoundEngine.PlaySound(in SoundID.DD2_FlameburstTowerShot, Owner.Center);
			}
			base.Projectile.localNPCHitCooldown = 3;
		}
		if (AttackCompletionRatio < 0.16f)
		{
			base.Projectile.oldPos = (Vector2[])(object)new Vector2[base.Projectile.oldPos.Length];
		}
		if (Owner.itemAnimation > 0)
		{
			base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, destination, 0.19f);
			base.Projectile.extraUpdates = 1;
		}
		if (base.Projectile.Center != destination)
		{
			Projectile projectile = base.Projectile;
			projectile.Center += (destination - base.Projectile.Center).SafeNormalize(Vector2.Zero) * MathHelper.Min(base.Projectile.Distance(destination), 12f + ((Vector2)(ref Owner.velocity)).Length());
		}
		if (!base.Projectile.WithinRange(destination, 300f))
		{
			base.Projectile.Center = destination;
		}
		Time++;
	}

	internal float PrimitiveWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return (float)BladeFrame.Height * 0.47f;
	}

	internal Color PrimitiveColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		float opacity = Utils.GetLerpValue(0.8f, 0.52f, completionRatio, clamped: true) * Utils.GetLerpValue(1f, 0.81f, AttackCompletionRatio, clamped: true);
		Color val = Color.Lerp(Color.Red, Color.DarkRed, 0.4f);
		Color endingColor = Color.Lerp(Color.DarkRed, Color.Purple, 0.77f);
		return Color.Lerp(val, endingColor, (float)Math.Pow(completionRatio, 0.3700000047683716)) * opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		Texture2D forearmTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/TaintedForearm", (AssetRequestMode)2).Value;
		Texture2D armTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/TaintedArm", (AssetRequestMode)2).Value;
		Texture2D handTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/TaintedHand", (AssetRequestMode)2).Value;
		SpriteEffects handDirection = (SpriteEffects)0;
		if (Variant == (Owner.direction == -1).ToInt())
		{
			handTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/TaintedHand2", (AssetRequestMode)2).Value;
			handDirection = (SpriteEffects)1;
		}
		Texture2D bladeTexture = TextureAssets.Item[(int)SwordItemID].Value;
		float backArmRotation = Owner.AngleTo(BackArmAimPosition);
		Main.EntitySpriteDraw(forearmTexture, Owner.Center - Main.screenPosition, null, Color.White, backArmRotation - (float)Math.PI / 2f, Vector2.UnitX * forearmTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		Vector2 frontArmDrawPosition = Owner.Center + backArmRotation.ToRotationVector2() * 80f + IdleMoveOffset;
		float frontArmRotation = base.Projectile.AngleFrom(frontArmDrawPosition);
		Main.EntitySpriteDraw(armTexture, frontArmDrawPosition - Main.screenPosition, null, Color.White, frontArmRotation - (float)Math.PI / 2f, Vector2.UnitX * armTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		float handRotation = frontArmRotation + (float)Math.PI / 2f + (float)Math.PI;
		float bladeRotation = handRotation - (float)Math.PI / 4f + (float)Math.PI + BladeRotationOffset;
		if (Owner.direction == -1)
		{
			bladeRotation += (float)Math.PI / 2f;
		}
		if (Owner.itemAnimation > 0)
		{
			GameShaders.Misc["CalamityMod:FadingSolidTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/BladeTrailUVMap", (AssetRequestMode)2));
			GameShaders.Misc["CalamityMod:FadingSolidTrail"].Shader.Parameters["shouldFlip"].SetValue((float)(Owner.direction == -1).ToInt());
			Vector2 bottom = BladeCenterPosition - BladeOffsetDirection * (float)BladeFrame.Height * 0.5f - Main.screenPosition;
			Vector2 offsetToBlade = (BladeCenterPosition + BladeOffsetDirection * (float)BladeFrame.Height * 0.5f - Main.screenPosition - bottom).SafeNormalize(Vector2.Zero).RotatedBy(1.5707963705062866) * 5f;
			Vector2[] drawPoints = (Vector2[])(object)new Vector2[base.Projectile.oldPos.Length];
			Vector2 perpendicularDirection = BladeOffsetDirection.SafeNormalize(Vector2.UnitY).RotatedBy(1.5707963705062866);
			for (int i = 1; i < drawPoints.Length; i++)
			{
				if (!(base.Projectile.oldPos[i] == Vector2.Zero))
				{
					drawPoints[i] = base.Projectile.Center + perpendicularDirection.RotatedBy((float)i * -0.014f * (float)Owner.direction) * (float)i * (float)(-Owner.direction) * 6f;
				}
			}
			Vector2 leftVertexPosition = BladeCenterPosition - BladeOffsetDirection * (float)BladeFrame.Height * 0.5f + offsetToBlade - Main.screenPosition;
			Vector2 rightVertexPosition = BladeCenterPosition + BladeOffsetDirection * (float)BladeFrame.Height * 0.5f + offsetToBlade - Main.screenPosition;
			if (Owner.direction == -1)
			{
				Utils.Swap(ref leftVertexPosition, ref rightVertexPosition);
			}
			PrimitiveRenderer.RenderTrail(drawPoints, new PrimitiveSettings(PrimitiveWidthFunction, PrimitiveColorFunction, delegate
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_000c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0011: Unknown result type (might be due to invalid IL or missing references)
				return BladeCenterPosition - base.Projectile.position;
			}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:FadingSolidTrail"], useUnscaledMatrices: false, (leftVertexPosition, rightVertexPosition)), 67);
		}
		Vector2 bladeDrawPosition = BladeCenterPosition - Main.screenPosition;
		SpriteEffects bladeDirection = (SpriteEffects)(Owner.direction == -1);
		Main.EntitySpriteDraw(bladeTexture, bladeDrawPosition, BladeFrame, Color.White, bladeRotation, BladeFrame.Size() * 0.5f, base.Projectile.scale, bladeDirection);
		Main.EntitySpriteDraw(handTexture, FrontArmEnd - Main.screenPosition, null, base.Projectile.GetAlpha(Color.White), handRotation, handTexture.Size() * 0.5f, base.Projectile.scale, handDirection);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		Vector2 start = BladeCenterPosition - BladeOffsetDirection * (float)BladeFrame.Height * 0.5f;
		Vector2 end = BladeCenterPosition + BladeOffsetDirection * (float)BladeFrame.Height * 0.5f;
		float width = 60f;
		float _ = 0f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, width, ref _);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		ItemLoader.OnHitNPC(Owner.HeldItem, Owner, target, in hit, damageDone);
		NPCLoader.OnHitByItem(target, Owner, Owner.HeldItem, in hit, damageDone);
		PlayerLoader.OnHitNPC(Owner, target, in hit, damageDone);
	}
}
