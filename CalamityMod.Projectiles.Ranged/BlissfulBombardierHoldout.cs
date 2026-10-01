using System;
using System.IO;
using System.Runtime.CompilerServices;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class BlissfulBombardierHoldout : BaseGunHoldoutProjectile
{
	[CompilerGenerated]
	private static Color _003CeffectsColor_003Ek__BackingField;

	[CompilerGenerated]
	private static Color _003CstaticEffectsColor_003Ek__BackingField;

	public bool hasFired;

	public override int AssociatedItemID => ModContent.ItemType<BlissfulBombardier>();

	public override float RecoilResolveSpeed => 0.07f;

	public override float MaxOffsetLengthFromArm => 60f;

	public override float OffsetXUpwards => -18f;

	public override float BaseOffsetY => -5f;

	public override float OffsetYUpwards => 23f;

	public static int dustEffectsID { get; set; }

	public static Color effectsColor
	{
		[CompilerGenerated]
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return _003CeffectsColor_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			_003CeffectsColor_003Ek__BackingField = value;
		}
	}

	public static Color staticEffectsColor
	{
		[CompilerGenerated]
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return _003CstaticEffectsColor_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			_003CstaticEffectsColor_003Ek__BackingField = value;
		}
	}

	public ref float shootingTimer => ref base.Projectile.ai[0];

	public ref float PostFireCooldown => ref base.Projectile.ai[1];

	public override void SendExtraAIHoldout(BinaryWriter writer)
	{
		writer.Write(shootingTimer);
	}

	public override void ReceiveExtraAIHoldout(BinaryReader reader)
	{
		shootingTimer = reader.ReadSingle();
	}

	public override void KillHoldoutLogic()
	{
	}

	public override void ManageHoldout()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		Vector2 armPosition = base.Owner.RotatedRelativePoint(base.Owner.MountedCenter, reverseRotation: true);
		Vector2 ownerToSky = new Vector2(MathHelper.Lerp(base.Owner.ClampedMouseWorld().X, base.Owner.Center.X, 0.55f), base.Owner.Center.Y) + new Vector2(0f, -500f) - base.Owner.Center;
		float holdoutDirection = base.Projectile.velocity.ToRotation();
		float proximityLookingUpwards = Vector2.Dot(ownerToSky.SafeNormalize(Vector2.Zero), -Vector2.UnitY * base.Owner.gravDir);
		int direction = MathF.Sign(ownerToSky.X);
		Vector2 lengthOffset = base.Projectile.rotation.ToRotationVector2() * base.OffsetLengthFromArm;
		Vector2 armOffset = default(Vector2);
		((Vector2)(ref armOffset))._002Ector(Utils.Remap(MathF.Abs(proximityLookingUpwards), 0f, 1f, 0f, (proximityLookingUpwards > 0f) ? OffsetXUpwards : OffsetXDownwards) * (float)direction, BaseOffsetY * base.Owner.gravDir + Utils.Remap(MathF.Abs(proximityLookingUpwards), 0f, 1f, 0f, (proximityLookingUpwards > 0f) ? OffsetYUpwards : OffsetYDownwards) * base.Owner.gravDir);
		base.Projectile.Center = armPosition + lengthOffset + armOffset;
		base.Projectile.velocity = holdoutDirection.AngleTowards(ownerToSky.ToRotation(), 0.2f).ToRotationVector2();
		base.Projectile.rotation = holdoutDirection;
		base.Projectile.spriteDirection = direction;
		base.Owner.ChangeDir(direction);
		base.Owner.heldProj = base.Projectile.whoAmI;
		base.Owner.itemTime = (base.Owner.itemAnimation = 2);
		base.Owner.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
		float armRotation = (base.Projectile.rotation - (float)Math.PI / 2f) * base.Owner.gravDir + ((base.Owner.gravDir == -1f) ? ((float)Math.PI) : 0f);
		base.Owner.SetCompositeArmFront(enabled: true, base.FrontArmStretch, armRotation + base.ExtraFrontArmRotation * (float)direction);
		base.Owner.SetCompositeArmBack(enabled: true, base.BackArmStretch, armRotation + base.ExtraBackArmRotation * (float)direction);
		if (base.KeepRefreshingLifetime)
		{
			base.Projectile.timeLeft = 2;
		}
		if (base.OffsetLengthFromArm != MaxOffsetLengthFromArm)
		{
			base.OffsetLengthFromArm = MathHelper.Lerp(base.OffsetLengthFromArm, MaxOffsetLengthFromArm, RecoilResolveSpeed);
		}
		base.Projectile.ForceNetUpdate();
	}

	public override void HoldoutAI()
	{
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Owner.CantUseHoldout() && PostFireCooldown <= 0f && shootingTimer < (float)(int)((float)base.Owner.itemAnimationMax * 0.8f))
		{
			base.Projectile.Kill();
			return;
		}
		if (!hasFired)
		{
			shootingTimer++;
		}
		if (shootingTimer == (float)base.Owner.itemAnimationMax && base.HeldItem.type == AssociatedItemID && !hasFired)
		{
			ShootRocket();
			for (int i = 0; i <= 25; i++)
			{
				if (i % 2 == 0)
				{
					Dust dust = Dust.NewDustPerfect(GunTipPosition, ModContent.DustType<LightDust>(), (base.Projectile.velocity * 12f).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(0.4f, 1.7f), 0, default(Color), Main.rand.NextFloat(1.8f, 2.3f));
					dust.noGravity = true;
					dust.color = (Main.rand.NextBool(3) ? Color.Orange : effectsColor);
					dust.noLightEmittence = true;
				}
				else
				{
					Dust dust2 = Dust.NewDustPerfect(GunTipPosition, 278, (base.Projectile.velocity * 12f).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(0.4f, 1.7f), 0, default(Color), Main.rand.NextFloat(1.2f, 1.6f));
					dust2.noGravity = false;
					dust2.color = (Main.rand.NextBool(3) ? Color.Orange : staticEffectsColor);
				}
			}
			hasFired = true;
		}
		if (PostFireCooldown > 0f)
		{
			PostFiringCooldown();
		}
	}

	public void ShootRocket()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		Vector2 shootDirection = base.Projectile.velocity.SafeNormalize(Vector2.Zero);
		float velocityMultiplier = 0.9f;
		base.Owner.PickAmmo(base.HeldItem, out var _, out var projSpeed, out var damage, out var knockback, out var rocketType);
		switch (rocketType)
		{
		case 4447:
			dustEffectsID = 45;
			effectsColor = Color.RoyalBlue;
			break;
		case 4448:
			dustEffectsID = 6;
			effectsColor = Color.Red;
			break;
		case 4449:
			dustEffectsID = 152;
			effectsColor = Color.Yellow;
			break;
		default:
			dustEffectsID = 87;
			effectsColor = Color.Goldenrod;
			break;
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootDirection * projSpeed * velocityMultiplier, ModContent.ProjectileType<NukeOfBliss>(), damage, knockback, base.Projectile.owner, rocketType);
			PostFireCooldown = base.Owner.itemAnimationMax;
			base.Owner.SetScreenshake(5f);
		}
		if (!Main.dedServ)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/LauncherHeavyShot");
			style.Volume = 0.9f;
			style.PitchVariance = 0.1f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			for (int k = 0; k < 6; k++)
			{
				float pulseScale = Main.rand.NextFloat(0.2f, 0.4f);
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(GunTipPosition, (shootDirection * 20f).RotatedByRandom(0.25) * Main.rand.NextFloat(0.5f, 1.2f), (Main.rand.NextBool(3) ? effectsColor : staticEffectsColor) * 0.8f, new Vector2(1f, 1f), pulseScale - 0.25f, pulseScale, 0f, 20));
			}
			base.OffsetLengthFromArm -= 25f;
		}
	}

	public void PostFiringCooldown()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		base.Owner.channel = true;
		if (PostFireCooldown > 1f)
		{
			PostFireCooldown--;
			Vector2 smokeVel = new Vector2(0f, -8f) * Main.rand.NextFloat(0.1f, 1.1f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(GunTipPosition, smokeVel, staticEffectsColor, Main.rand.Next(40, 61), Main.rand.NextFloat(0.3f, 0.6f), 0.5f, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextBool(), 0f, required: true));
			Dust dust = Dust.NewDustPerfect(GunTipPosition, 303, smokeVel.RotatedByRandom(0.10000000149011612), 80, default(Color), Main.rand.NextFloat(0.4f, 1.3f));
			dust.noGravity = false;
			dust.color = staticEffectsColor;
		}
		else
		{
			PostFireCooldown--;
			hasFired = false;
			shootingTimer = 0f;
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		base.OnSpawn(source);
		base.FrontArmStretch = Player.CompositeArmStretchAmount.Quarter;
		base.ExtraBackArmRotation = MathHelper.ToRadians(15f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		if (shootingTimer <= 1f)
		{
			return false;
		}
		Texture2D texture = TextureAssets.Projectile[base.Projectile.type].Value;
		Texture2D texture2 = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/BlissfulBombardierGlow", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f) - ((base.Owner.gravDir == -1f) ? ((float)Math.PI * (float)base.Owner.direction) : 0f);
		Vector2 rotationPoint = texture.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		if (hasFired)
		{
			float rumble = Utils.GetLerpValue(0f, base.Owner.itemAnimationMax, PostFireCooldown, clamped: true) * 8f;
			drawPosition += Main.rand.NextVector2Circular(rumble, rumble);
		}
		if (!hasFired)
		{
			float fade = Utils.GetLerpValue(0f, base.Owner.itemAnimationMax, shootingTimer, clamped: true);
			for (int i = 0; i < 10; i++)
			{
				Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 10f).ToRotationVector2() * 5f * fade;
				SpriteBatch spriteBatch = Main.spriteBatch;
				Vector2 val = drawPosition + drawOffset;
				Color val2 = staticEffectsColor;
				((Color)(ref val2)).A = 0;
				spriteBatch.Draw(texture, val, (Rectangle?)null, val2 * fade, drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite, 0f);
			}
		}
		Main.EntitySpriteDraw(texture, drawPosition, null, drawColor, drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		Main.EntitySpriteDraw(texture2, drawPosition, null, Color.White, drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		return false;
	}

	static BlissfulBombardierHoldout()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		staticEffectsColor = Color.Khaki;
	}
}
