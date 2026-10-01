using System;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class StygianShieldAttack : ModProjectile, ILocalizedModType, IModType
{
	public const float MinChargeTime = 15f;

	public const float MaxChargeTime = 45f;

	public const float MaxChargeDistance = 720f;

	public const float MaxChargeDamageMult = 6f;

	public const float PiercingDamageMult = 0.6f;

	public const float DashDuration = 21f;

	public Vector2 DashDestination;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Charge => ref base.Projectile.ai[0];

	public ref float DashTime => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 2);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.noEnchantmentVisuals = true;
	}

	public override void AI()
	{
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		if (Owner == null || Owner.dead || Owner.HeldItem.type != ModContent.ItemType<StygianShield>())
		{
			base.Projectile.Kill();
		}
		Owner.heldProj = base.Projectile.whoAmI;
		if (Charge == 0f)
		{
			SoundEngine.PlaySound(in StygianShield.DashChargeSound, Owner.Center);
		}
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 14.7f, DashTime, clamped: true);
		if (DashDestination != Vector2.Zero)
		{
			if (Owner.mount != null)
			{
				Owner.mount.Dismount(Owner);
			}
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile proj = enumerator.Current;
				if (proj.owner == Owner.whoAmI && proj.aiStyle == 7 && proj.aiStyle == 7)
				{
					proj.Kill();
				}
			}
			DashTime++;
			base.Projectile.velocity = base.Projectile.SafeDirectionTo(DashDestination) * 720f * Charge / 45f / 21f;
			if (Vector2.Distance(base.Projectile.Center, DashDestination) < 4f || DashTime > 21f || Collision.SolidCollision(base.Projectile.Center, 1, 1, acceptTopSurfaces: false))
			{
				base.Projectile.Kill();
			}
			Owner.Center = base.Projectile.Center;
			Owner.ChangeDir(base.Projectile.direction);
		}
		else
		{
			base.Projectile.Center = Owner.MountedCenter;
			if (Charge < 45f)
			{
				Charge++;
				float streakThickness = Main.rand.NextFloat(0.4f, 0.7f) * Charge / 45f;
				Vector2 spawnPoint = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(80f, 120f);
				GeneralParticleHandler.SpawnParticle(new ManaDrainStreak(Owner, streakThickness, spawnPoint, Main.rand.NextFloat(30f, 44f), Color.Firebrick, Color.OrangeRed, Main.rand.Next(12, 21)));
			}
			if (Owner.CantUseHoldout())
			{
				Attack();
			}
		}
	}

	public void Attack()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		if (Charge >= 15f && !Owner.noItems && !Owner.CCed)
		{
			Vector2 intendedDestination = base.Projectile.Center + base.Projectile.SafeDirectionTo(Owner.Calamity().mouseWorld) * 720f * Charge / 45f;
			if (intendedDestination.X >= 660f && intendedDestination.Y >= 660f && intendedDestination.X <= (float)Main.maxTilesX * 16f - 680f && intendedDestination.Y <= (float)Main.maxTilesY * 16f - 680f)
			{
				SoundEngine.PlaySound(in StygianShield.DashSound, Owner.Center);
				Owner.immune = true;
				Owner.immuneNoBlink = true;
				Owner.immuneTime = 21;
				for (int k = 0; k < Owner.hurtCooldowns.Length; k++)
				{
					Owner.hurtCooldowns[k] = Owner.immuneTime;
				}
				for (int i = 0; i < base.Projectile.oldPos.Length; i++)
				{
					base.Projectile.oldPos[i] = Vector2.Zero;
					base.Projectile.oldRot[i] = 0f;
					base.Projectile.oldSpriteDirection[i] = 0;
				}
				for (int j = 0; j < 3; j++)
				{
					float scale = 1f / (float)Math.Pow(1.6, j);
					float rot = (float)Owner.miscCounter / ((float)Math.PI * 2f) + MathHelper.ToRadians(120f * (float)j);
					GeneralParticleHandler.SpawnParticle(new FlatGlow(base.Projectile.Center, Vector2.Zero, Color.DarkOrange * 0.3f, rot, Vector2.One * scale, Vector2.One * 8f * scale, 12));
				}
				DashDestination = intendedDestination;
				base.Projectile.damage = (int)((float)base.Projectile.damage * 6f * Charge / 45f);
				base.Projectile.ExpandHitboxBy(100);
				base.Projectile.tileCollide = true;
				return;
			}
		}
		base.Projectile.Kill();
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = (height = 32);
		return true;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(Owner.Center, 48f, targetHitbox);
	}

	public override bool? CanDamage()
	{
		if (!(DashTime > 0f))
		{
			return false;
		}
		return base.CanDamage();
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.damage = (int)((float)base.Projectile.damage * 0.6f);
		SoundEngine.PlaySound(in StygianShield.DashHitSound, Owner.Center);
		float rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI;
		GeneralParticleHandler.SpawnParticle(new SlashThrough(Color.Red * 0.9f, Owner.Center, rotation, 15, target));
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		float fadeOpacity = Utils.GetLerpValue(1f, 0.2f, completionRatio, clamped: true) * base.Projectile.Opacity;
		return Color.Lerp(Color.DarkOrange, Color.Red, completionRatio) * fadeOpacity;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return 12f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_064d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Unknown result type (might be due to invalid IL or missing references)
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_0683: Unknown result type (might be due to invalid IL or missing references)
		//IL_068d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0692: Unknown result type (might be due to invalid IL or missing references)
		//IL_0697: Unknown result type (might be due to invalid IL or missing references)
		//IL_0728: Unknown result type (might be due to invalid IL or missing references)
		//IL_0734: Unknown result type (might be due to invalid IL or missing references)
		//IL_073e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0745: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0705: Unknown result type (might be due to invalid IL or missing references)
		//IL_070f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0804: Unknown result type (might be due to invalid IL or missing references)
		//IL_0810: Unknown result type (might be due to invalid IL or missing references)
		//IL_081e: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_082c: Unknown result type (might be due to invalid IL or missing references)
		//IL_086e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0867: Unknown result type (might be due to invalid IL or missing references)
		//IL_0847: Unknown result type (might be due to invalid IL or missing references)
		//IL_0873: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_090a: Unknown result type (might be due to invalid IL or missing references)
		//IL_090c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0911: Unknown result type (might be due to invalid IL or missing references)
		//IL_0920: Unknown result type (might be due to invalid IL or missing references)
		//IL_092e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0938: Unknown result type (might be due to invalid IL or missing references)
		//IL_096f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0985: Unknown result type (might be due to invalid IL or missing references)
		//IL_0994: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_09df: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a44: Unknown result type (might be due to invalid IL or missing references)
		Texture2D mainTex = TextureAssets.Projectile[base.Type].Value;
		Texture2D bloomTex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Texture2D flatTex = ModContent.Request<Texture2D>("CalamityMod/Particles/FlatShape", (AssetRequestMode)2).Value;
		Texture2D shieldTex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/StygianShieldBloom", (AssetRequestMode)2).Value;
		Texture2D ringTex = ModContent.Request<Texture2D>("CalamityMod/Particles/HollowCircleHardEdge", (AssetRequestMode)2).Value;
		float chargeLevel = Charge / 45f;
		Effect ArrowEffect = Filters.Scene["CalamityMod:SpreadTelegraph"].GetShader().Shader;
		ArrowEffect.Parameters["centerOpacity"].SetValue(1f);
		ArrowEffect.Parameters["mainOpacity"].SetValue(1f);
		ArrowEffect.Parameters["edgeBlendLength"].SetValue(0.07f);
		ArrowEffect.Parameters["edgeBlendStrength"].SetValue(8f);
		if (DashTime > 0f && DashTime < 20f && DashDestination != Vector2.Zero && ((Vector2)(ref base.Projectile.velocity)).Length() > 0f)
		{
			float durationRatio = DashTime / 21f;
			float scaleMult = MathHelper.Lerp(1.8f, 1f, durationRatio);
			Vector2 direction = base.Projectile.SafeDirectionTo(DashDestination);
			Vector2 extraOffset = direction * 800f / ((Vector2)(ref base.Projectile.velocity)).Length() - Main.screenPosition;
			float arrowFace = base.Projectile.velocity.ToRotation() - (float)Math.PI;
			float side = MathHelper.ToRadians(135f);
			Color headColor = Color.Lerp(Color.Orange, Color.OrangeRed, durationRatio);
			Color shieldColor = Color.LightSalmon;
			GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
			PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
			{
				//IL_000b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0015: Unknown result type (might be due to invalid IL or missing references)
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0020: Unknown result type (might be due to invalid IL or missing references)
				//IL_0025: Unknown result type (might be due to invalid IL or missing references)
				//IL_002a: Unknown result type (might be due to invalid IL or missing references)
				//IL_0030: Unknown result type (might be due to invalid IL or missing references)
				//IL_003a: Unknown result type (might be due to invalid IL or missing references)
				//IL_003f: Unknown result type (might be due to invalid IL or missing references)
				return base.Projectile.Size * 0.5f + extraOffset + Main.screenPosition - direction * 80f;
			}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 10);
			ArrowEffect.Parameters["halfSpreadAngle"].SetValue(MathHelper.ToRadians(7.5f));
			ArrowEffect.Parameters["edgeColor"].SetValue(((Color)(ref headColor)).ToVector3());
			ArrowEffect.Parameters["centerColor"].SetValue(((Color)(ref headColor)).ToVector3());
			Main.spriteBatch.EnterShaderRegion(BlendState.Additive, ArrowEffect);
			for (float i = 0f - side; i <= side; i += side)
			{
				Main.EntitySpriteDraw(mainTex, base.Projectile.Center + extraOffset + (direction * 72f * scaleMult).RotatedBy(i), null, Color.White, arrowFace + i, mainTex.Size() / 2f, 300f * scaleMult, (SpriteEffects)0);
			}
			Main.spriteBatch.ExitShaderRegion();
			Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
			float shieldRot = arrowFace - (float)Math.PI;
			Main.EntitySpriteDraw(shieldTex, base.Projectile.Center + extraOffset, null, shieldColor, shieldRot, shieldTex.Size() / 2f, 1.25f * scaleMult, (SpriteEffects)0);
			Main.EntitySpriteDraw(bloomTex, base.Projectile.Center + extraOffset, null, shieldColor * 0.75f, 0f, bloomTex.Size() / 2f, 0.5f * scaleMult, (SpriteEffects)0);
			Vector2 ringScale = default(Vector2);
			((Vector2)(ref ringScale))._002Ector(0.033f, 2.25f * scaleMult * (float)shieldTex.Height / (float)ringTex.Height);
			Main.EntitySpriteDraw(ringTex, base.Projectile.Center + extraOffset - direction * 36f * scaleMult, null, shieldColor, shieldRot, ringTex.Size() / 2f, ringScale * 1.2f, (SpriteEffects)0);
			Main.EntitySpriteDraw(ringTex, base.Projectile.Center + extraOffset - direction * 30f * scaleMult, null, shieldColor, shieldRot, ringTex.Size() / 2f, ringScale * 0.8f, (SpriteEffects)0);
			Main.EntitySpriteDraw(ringTex, base.Projectile.Center + extraOffset - direction * 24f * scaleMult, null, shieldColor, shieldRot, ringTex.Size() / 2f, ringScale, (SpriteEffects)0);
			Main.spriteBatch.ExitShaderRegion();
		}
		else if (DashTime <= 0f)
		{
			Vector2 shieldPos = Vector2.UnitX * (float)Owner.direction * (float)Owner.width * 0.4f + Owner.Center - Main.screenPosition;
			if (Owner.bodyFrame.Y == 280)
			{
				shieldPos -= Vector2.UnitY * (float)Owner.height * 0.35f;
			}
			Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
			if (Charge >= 45f)
			{
				float glowScale = 0.5f * CalamityUtils.Convert01To010((float)(Owner.miscCounter % 40) / 40f);
				Main.EntitySpriteDraw(flatTex, shieldPos, null, Color.DarkRed * 0.3f, 0f, flatTex.Size() / 2f, 0.3f + glowScale, (SpriteEffects)0);
			}
			Main.EntitySpriteDraw(bloomTex, shieldPos, null, Color.DarkGoldenrod * 0.4f * chargeLevel, 0f, bloomTex.Size() / 2f, 0.4f * chargeLevel, (SpriteEffects)0);
			Main.spriteBatch.ExitShaderRegion();
			if (Charge >= 15f)
			{
				Vector2 dashLength = base.Projectile.SafeDirectionTo(Owner.Calamity().mouseWorld) * 720f * Charge / 45f;
				Vector2 intendedDestination = base.Projectile.Center + dashLength;
				float direction2 = base.Projectile.SafeDirectionTo(intendedDestination).ToRotation();
				Color telegraphColor = ((intendedDestination.X < 660f || intendedDestination.Y < 660f || intendedDestination.X > (float)Main.maxTilesX * 16f - 680f || intendedDestination.Y > (float)Main.maxTilesY * 16f - 680f) ? Color.Red : Color.White);
				Effect TelegraphEffect = ArrowEffect;
				TelegraphEffect.Parameters["centerOpacity"].SetValue(0.6f);
				TelegraphEffect.Parameters["halfSpreadAngle"].SetValue(MathHelper.ToRadians(64f));
				TelegraphEffect.Parameters["edgeColor"].SetValue(((Color)(ref telegraphColor)).ToVector3());
				TelegraphEffect.Parameters["centerColor"].SetValue(((Color)(ref telegraphColor)).ToVector3());
				Main.spriteBatch.EnterShaderRegion(BlendState.Additive, TelegraphEffect);
				Main.EntitySpriteDraw(mainTex, intendedDestination - Main.screenPosition, null, Color.White, direction2 - (float)Math.PI, mainTex.Size() / 2f, 135f, (SpriteEffects)0);
				Main.spriteBatch.ExitShaderRegion();
				for (int i2 = -1; i2 <= 1; i2 += 2)
				{
					Vector2 val = base.Projectile.Center + base.Projectile.SafeDirectionTo(Owner.Calamity().mouseWorld).RotatedBy(90f * (float)i2) * 60f;
					Vector2 pointEnd = base.Projectile.Center + dashLength * 0.1f;
					Vector2 lineStart = val + dashLength * 0.1f;
					Vector2 lineEnd = val + dashLength;
					Color lineColor = telegraphColor * 0.3f;
					float lineScale = 2f;
					Main.spriteBatch.DrawLineBetter(lineStart, pointEnd, lineColor, lineScale);
					Main.spriteBatch.DrawLineBetter(lineStart, lineEnd, lineColor, lineScale * 2f);
					Main.spriteBatch.DrawLineBetter(lineEnd, intendedDestination, lineColor, lineScale);
				}
			}
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		if (!(DashTime <= 0f))
		{
			for (int i = 0; i < 3; i++)
			{
				float scale = 1f / (float)Math.Pow(1.6, i);
				float rot = (float)Owner.miscCounter / ((float)Math.PI * 2f) + MathHelper.ToRadians(120f * (float)i);
				GeneralParticleHandler.SpawnParticle(new FlatGlow(base.Projectile.Center, Vector2.Zero, Color.DarkGoldenrod * 0.3f, rot, Vector2.One * scale, Vector2.One * 6f * scale, 9));
			}
		}
	}

	public StygianShieldAttack()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		DashDestination = Vector2.Zero;
		base._002Ector();
	}
}
