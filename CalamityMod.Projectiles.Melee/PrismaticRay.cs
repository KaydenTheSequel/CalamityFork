using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Graphics.Primitives;
using CalamityMod.NPCs;
using CalamityMod.Projectiles.BaseProjectiles;
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

[PierceResistException(false)]
public class PrismaticRay : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/Item/ExobladeDashImpact")
	{
		Volume = 0.8f
	};

	public int HitSoundCooldown;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/PrismaticRayStart";

	public Player Owner => Main.player[base.Projectile.owner];

	public override Texture2D LaserBeginTexture => TextureAssets.Projectile[base.Type].Value;

	public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/PrismaticRayMid", (AssetRequestMode)1).Value;

	public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/PrismaticRayEnd", (AssetRequestMode)1).Value;

	public override float MaxScale => 5f;

	public override float MaxLaserLength => 2400f;

	public override float Lifetime => 360f;

	public override Color LaserOverlayColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Main.DiscoColor;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 5000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 36);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = MeleeRangedHybridDamageClass.Instance;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 360;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 9;
		base.Projectile.hide = true;
	}

	public override void AttachToSomething()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.CantUseHoldout() && base.Projectile.timeLeft > 30)
		{
			base.Projectile.timeLeft = 30;
		}
		if (Owner.active && !Owner.dead)
		{
			base.Projectile.Center = Owner.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 20f;
		}
	}

	public override void UpdateLaserMotion()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		Vector2 aimVector = (Main.MouseWorld - Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true)).SafeNormalize(Vector2.UnitY);
		aimVector = Vector2.Normalize(Vector2.Lerp(aimVector, Vector2.Normalize(base.Projectile.velocity), 0.94f));
		if (aimVector != base.Projectile.velocity)
		{
			base.Projectile.netUpdate = true;
		}
		base.Projectile.velocity = aimVector;
	}

	public override void DetermineScale()
	{
		if (base.Time < 30f)
		{
			base.Projectile.scale = MathHelper.Lerp(0f, 1f, base.Time / 30f) * MaxScale;
		}
		else
		{
			base.Projectile.scale = Utils.GetLerpValue(0f, 30f, base.Projectile.timeLeft, clamped: true) * MaxScale;
		}
	}

	public override void ExtraBehavior()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		Owner.SetScreenshake(3f);
		if (HitSoundCooldown > 0)
		{
			HitSoundCooldown--;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 300);
		if (HitSoundCooldown == 0)
		{
			SoundEngine.PlaySound(in HitSound, target.Center);
			HitSoundCooldown = 9;
		}
	}

	public float LaserWidthFunction(float _, Vector2 vertexPos)
	{
		return base.Projectile.scale * (float)base.Projectile.width;
	}

	public Color LaserColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Main.DiscoColor;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		Main.spriteBatch.EnterShaderRegion();
		Vector2 laserEnd = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * base.LaserLength;
		Vector2[] drawPoints = (Vector2[])(object)new Vector2[10];
		for (int i = 0; i < drawPoints.Length; i++)
		{
			drawPoints[i] = Vector2.Lerp(base.Projectile.Center, laserEnd, (float)i / ((float)drawPoints.Length - 1f));
		}
		GameShaders.Misc["CalamityMod:ArtemisLaser"].UseColor(Main.DiscoColor);
		GameShaders.Misc["CalamityMod:ArtemisLaser"].UseImage1(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/MeltyNoise", (AssetRequestMode)2));
		GameShaders.Misc["CalamityMod:ArtemisLaser"].UseImage2(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/LeviathanBomb", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(drawPoints, new PrimitiveSettings(LaserWidthFunction, LaserColorFunction, null, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ArtemisLaser"]), 60);
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindProjectiles.Add(index);
	}
}
