using System;
using System.Runtime.CompilerServices;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SlagfireDouserHoldout : BaseGunHoldoutProjectile
{
	private static readonly Vector2 CustomHoldoutOffset;

	public static Asset<Texture2D> pistilTexture;

	private float pistilJigglePhysicsTimer;

	private float frontArmRotation = 0.14f;

	private const int BurstProjectiles = 4;

	private const int DelayBetweenShotsInBurst = 4;

	[CompilerGenerated]
	private static Color _003CEffectsColor_003Ek__BackingField;

	[CompilerGenerated]
	private static Color _003CStaticEffectsColor_003Ek__BackingField;

	public override int AssociatedItemID => ModContent.ItemType<SlagfireDouser>();

	public override string Texture => "CalamityMod/Projectiles/Ranged/SlagfireDouserHoldout";

	public static string TexturePathPistil => "CalamityMod/Projectiles/Ranged/SlagfireDouserPistil";

	public override Vector2 GunTipPosition
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			Vector2 baseTip = base.GunTipPosition;
			baseTip.X += CustomHoldoutOffset.X * (float)base.Owner.direction;
			baseTip.Y += CustomHoldoutOffset.Y;
			return baseTip - Vector2.UnitX.RotatedBy(base.Projectile.rotation) * 24f * base.Owner.gravDir;
		}
	}

	public override float MaxOffsetLengthFromArm => 12f;

	public override float OffsetXUpwards => -25f;

	public override float OffsetXDownwards => -25f;

	public override float OffsetYUpwards => -24f;

	public override float OffsetYDownwards => 24f;

	public override float BaseOffsetY => -2f;

	public override float RecoilResolveSpeed => 0.12f;

	public ref float ShootingTimer => ref base.Projectile.ai[0];

	public static Color EffectsColor
	{
		[CompilerGenerated]
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return _003CEffectsColor_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			_003CEffectsColor_003Ek__BackingField = value;
		}
	}

	public static Color StaticEffectsColor
	{
		[CompilerGenerated]
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return _003CStaticEffectsColor_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			_003CStaticEffectsColor_003Ek__BackingField = value;
		}
	}

	public override void HoldoutAI()
	{
		if (!base.Owner.channel)
		{
			base.Projectile.Kill();
			return;
		}
		if (ShootingTimer % (float)(base.HeldItem.useAnimation + 16) == 0f && ShootingTimer > 0f)
		{
			ShootingTimer = 0f;
		}
		int currentShotInBurst = (int)((ShootingTimer % (float)(base.HeldItem.useAnimation + 16) - (float)base.HeldItem.useAnimation) / 4f);
		if (ShootingTimer >= (float)base.HeldItem.useAnimation && currentShotInBurst >= 0 && currentShotInBurst < 4 && (ShootingTimer - (float)base.HeldItem.useAnimation) % 4f == 0f)
		{
			Shoot(base.HeldItem);
		}
		ShootingTimer++;
		pistilJigglePhysicsTimer = MathHelper.Clamp(pistilJigglePhysicsTimer - 0.04f, 0f, 1f);
		base.ExtraFrontArmRotation = frontArmRotation;
	}

	public void Shoot(Item item)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		Vector2 projectileDirection = base.Projectile.velocity.SafeNormalize(Vector2.Zero);
		Vector2 finalProjectileVelocity = projectileDirection.RotatedByRandom(MathHelper.ToRadians(11f)) * item.shootSpeed;
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, finalProjectileVelocity, ModContent.ProjectileType<Slagfire>(), base.Owner.GetWeaponDamage(item), base.Owner.GetWeaponKnockback(item, item.knockBack), base.Projectile.owner);
		if (!Main.dedServ)
		{
			SoundStyle style = SoundID.Item61 with
			{
				Volume = 0.9f,
				Pitch = 0.125f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.OffsetLengthFromArm--;
			pistilJigglePhysicsTimer++;
			int dustAmount = Main.rand.Next(3, 6);
			for (int i = 0; i < dustAmount; i++)
			{
				Dust.NewDustPerfect(GunTipPosition, ModContent.DustType<LightDust>(), projectileDirection.RotatedByRandom(0.9424778819084167) * Main.rand.NextFloat(2.5f, 9f), 0, Color.Red).noGravity = false;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		if (pistilTexture == null)
		{
			pistilTexture = ModContent.Request<Texture2D>(TexturePathPistil, (AssetRequestMode)2);
		}
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float pistilPow = MathF.Pow(pistilJigglePhysicsTimer, 2f);
		Vector2 pistilJiggleScale = default(Vector2);
		((Vector2)(ref pistilJiggleScale))._002Ector(1f - 0.25f * pistilPow, 1f + 0.5f * pistilPow);
		drawPosition.X += 25 * base.Owner.direction;
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		float pistilJiggleRotOffset = (MathF.Sin(MathF.Pow(pistilJigglePhysicsTimer * 3.4f, 2f)) * 0.2f + MathF.Sin((float)Main.LocalPlayer.miscCounter * (float)Math.PI) * 0.1f) * (float)base.Projectile.spriteDirection;
		Vector2 rotationPoint = value.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		Main.EntitySpriteDraw(value, drawPosition, null, drawColor, drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		Main.EntitySpriteDraw(pistilTexture.Value, drawPosition, null, drawColor, drawRotation + pistilJiggleRotOffset, pistilTexture.Size() * 0.5f, base.Projectile.scale * base.Owner.gravDir * pistilJiggleScale, flipSprite);
		return false;
	}

	static SlagfireDouserHoldout()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		CustomHoldoutOffset = new Vector2(25f, -5f);
		EffectsColor = Color.MediumVioletRed * 1.3f;
		StaticEffectsColor = Color.MediumVioletRed * 1.3f;
	}
}
