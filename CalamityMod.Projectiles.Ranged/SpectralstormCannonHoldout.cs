using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SpectralstormCannonHoldout : BaseGunHoldoutProjectile
{
	public static readonly SoundStyle OverheatSound = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/OmegaBlueAbility")
	{
		Pitch = -0.5f
	};

	public SlotId WarningSlot;

	private bool displayOverheat;

	private const int WarningTime = 440;

	public override int AssociatedItemID => ModContent.ItemType<SpectralstormCannon>();

	public override float MaxOffsetLengthFromArm => 16f;

	public override float BaseOffsetY => -5f;

	public override float OffsetYDownwards => 5f;

	public override Vector2 GunTipPosition
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center + Vector2.UnitX.RotatedBy(base.Projectile.rotation) * (float)base.Projectile.width * 0.35f;
		}
	}

	public ref float Timer => ref base.Projectile.ai[0];

	public ref float SoulTimer => ref base.Projectile.ai[1];

	private int BuiltHeat => (base.Owner.HeldItem.ModItem as SpectralstormCannon).BuiltUpHeat;

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
	}

	public override void KillHoldoutLogic()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (base.Owner.CantUseHoldout(needsToHold: false) || base.HeldItem.type != base.Owner.HeldItem.type || (base.Owner.Calamity().mouseRight && !displayOverheat) || (BuiltHeat == 0 && !Main.mouseLeft))
		{
			if (SoundEngine.TryGetActiveSound(WarningSlot, out ActiveSound warn))
			{
				warn.Stop();
			}
			base.Projectile.Kill();
		}
	}

	public override void HoldoutAI()
	{
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		if (base.Owner.Calamity().flareGunOverheat == 0)
		{
			if (Main.mouseLeft)
			{
				Timer++;
				SoulTimer = 0f;
			}
			else
			{
				Timer = 0f;
				SoulTimer++;
			}
		}
		else
		{
			Timer = 0f;
			SoulTimer = 0f;
		}
		if (Timer >= 30f)
		{
			(base.Owner.HeldItem.ModItem as SpectralstormCannon).BuiltUpHeat++;
			if (BuiltHeat >= 540)
			{
				WarningSlot = SoundEngine.PlaySound(in OverheatSound, base.Owner.Center);
				for (int e = 0; e < 7; e++)
				{
					Vector2 dustVel = -base.Projectile.rotation.ToRotationVector2().RotatedByRandom(0.4712389409542084) * Main.rand.NextFloat(3.8f, 5.5f);
					Dust.NewDustPerfect(base.Projectile.Center, 127, dustVel, 0, default(Color), 1.5f).noGravity = true;
				}
				base.Owner.Hurt(PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.FlareGunOverheat").ToNetworkText(base.Owner.name)), 50, 1, pvp: false, quiet: false, -1, dodgeable: false, 0f, 1f, 0f);
				base.Owner.AddBuff(ModContent.BuffType<Nightwither>(), 160);
				base.Owner.Calamity().flareGunOverheat = 160;
				displayOverheat = true;
				(base.Owner.HeldItem.ModItem as SpectralstormCannon).BuiltUpHeat = 1;
				if (Main.myPlayer == base.Projectile.owner)
				{
					for (int i = 0; i < 25; i++)
					{
						Vector2 soulVel = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(4f, 7.5f);
						Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Owner.Center, soulVel, ModContent.ProjectileType<LostSoulFriendly>(), (int)((float)base.Projectile.damage * 1.15f), 0f, base.Projectile.owner);
						projectile.timeLeft = 250;
						projectile.DamageType = DamageClass.Ranged;
						projectile.frame = Main.rand.Next(4);
					}
				}
				return;
			}
			if (BuiltHeat == 440)
			{
				WarningSlot = SoundEngine.PlaySound(in FirestormCannonHoldout.WarningSound, base.Owner.Center);
			}
			float firingLerp = Utils.GetLerpValue(0f, 90f, Timer - 30f, clamped: true);
			int firingFrequency = (int)MathHelper.Lerp((float)base.HeldItem.useTime, (float)(base.HeldItem.useTime / 2), firingLerp);
			if (Timer % (float)firingFrequency == 0f)
			{
				SoundStyle style = SoundID.Item11 with
				{
					Volume = 0.8f
				};
				SoundEngine.PlaySound(in style, base.Owner.Center);
				if (Main.myPlayer == base.Projectile.owner)
				{
					base.Owner.PickAmmo(base.HeldItem, out var _, out var _, out var _, out var _, out var _);
					Vector2 velocity = base.Projectile.velocity.RotatedByRandom((float)Math.PI * 3f / 200f * (1f + firingLerp * 0.25f)) * base.Owner.HeldItem.shootSpeed;
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, velocity, ModContent.ProjectileType<SpectralFlare>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				}
			}
		}
		if (SoulTimer % 8f == 4f)
		{
			if (SoulTimer % 16f == 4f)
			{
				SoundStyle style = SoundID.Item103 with
				{
					Volume = 0.7f
				};
				SoundEngine.PlaySound(in style, base.Owner.Center);
			}
			if (Main.myPlayer == base.Projectile.owner)
			{
				Vector2 velocity2 = base.Projectile.velocity.RotatedByRandom(0.09424778074026108) * base.Owner.HeldItem.shootSpeed * 0.8f;
				Projectile projectile2 = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), GunTipPosition, velocity2, ModContent.ProjectileType<LostSoulFriendly>(), base.Projectile.damage, 0f, base.Projectile.owner, 2f);
				projectile2.DamageType = DamageClass.Ranged;
				projectile2.frame = Main.rand.Next(4);
			}
		}
		if (base.Owner.Calamity().flareGunOverheat == 0)
		{
			displayOverheat = false;
		}
		if (displayOverheat && Main.rand.NextBool(3))
		{
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(GunTipPosition, -Vector2.UnitY * 7.5f, Color.DarkCyan, 25, 0.4f, 0.75f));
		}
		if (SoundEngine.TryGetActiveSound(WarningSlot, out ActiveSound warning) && warning.IsPlaying)
		{
			warning.Position = base.Projectile.Center;
		}
	}

	public override bool? CanDamage()
	{
		return BuiltHeat >= 120 && base.Owner.Calamity().flareGunOverheat == 0;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(323, 300);
		(base.Owner.HeldItem.ModItem as SpectralstormCannon).BuiltUpHeat -= 6;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f) - ((base.Owner.gravDir == -1f) ? ((float)Math.PI * (float)base.Owner.direction) : 0f);
		Vector2 rotationPoint = texture.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		Color tintColor = (displayOverheat ? Color.Black : ((BuiltHeat >= 440) ? Color.Lerp(Color.LightSalmon, Color.White, MathF.Abs(MathF.Sin((float)base.Owner.miscCounter * (float)Math.PI / 30f))) : Color.LightSalmon));
		float opacity = Utils.GetLerpValue(0f, 440f, BuiltHeat, clamped: true);
		for (int i = 0; i < 16; i++)
		{
			Color auraColor = Color.HotPink * opacity * 0.6f;
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 16f).ToRotationVector2() * 5f;
			Main.EntitySpriteDraw(texture, drawPosition + drawOffset, null, auraColor, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		}
		Main.spriteBatch.EnterShaderRegion();
		GameShaders.Misc["CalamityMod:BasicTint"].UseOpacity((displayOverheat ? 1f : opacity) * 0.45f);
		GameShaders.Misc["CalamityMod:BasicTint"].UseColor(tintColor);
		GameShaders.Misc["CalamityMod:BasicTint"].Apply();
		Main.EntitySpriteDraw(texture, drawPosition, null, base.Projectile.GetAlpha(lightColor), drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}
}
