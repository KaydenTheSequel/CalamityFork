using System;
using System.Runtime.CompilerServices;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class FlakKrakenHoldout : BaseGunHoldoutProjectile
{
	[CompilerGenerated]
	private static Color _003CEffectsColor_003Ek__BackingField;

	public override int AssociatedItemID => ModContent.ItemType<FlakKraken>();

	public override float RecoilResolveSpeed => 0.05f;

	public override float MaxOffsetLengthFromArm => 30f;

	public override float OffsetXUpwards => -18f;

	public override float OffsetXDownwards => 15f;

	public override float BaseOffsetY => -20f;

	public override float OffsetYUpwards => 20f;

	public override float OffsetYDownwards => 10f;

	public ref float ShootingTimer => ref base.Projectile.ai[0];

	public ref float TimerBetweenBursts => ref base.Projectile.ai[1];

	public static int DustEffectsID { get; set; }

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

	public override void HoldoutAI()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		Vector2 ownerToMouse = base.Owner.Calamity().mouseWorld - base.Owner.MountedCenter;
		if (ShootingTimer >= (float)base.HeldItem.useAnimation)
		{
			float adaptiveTimeBetweenShots = MathF.Floor(FlakKraken.TimeBetweenShots * (float)base.HeldItem.useAnimation / (float)FlakKraken.OriginalUseTime);
			if (ShootingTimer % adaptiveTimeBetweenShots == 0f)
			{
				Vector2 direction = base.Projectile.velocity.SafeNormalize(Vector2.Zero);
				base.Owner.PickAmmo(base.Owner.HeldItem, out var _, out var itemShootSpeed, out var itemDamage, out var itemKnockback, out var rocketTypeShot);
				switch (rocketTypeShot)
				{
				case 4447:
					DustEffectsID = 45;
					EffectsColor = Color.RoyalBlue;
					break;
				case 4448:
					DustEffectsID = 6;
					EffectsColor = Color.Red;
					break;
				case 4449:
					DustEffectsID = 152;
					EffectsColor = Color.Yellow;
					break;
				default:
					DustEffectsID = 109;
					EffectsColor = Color.Black;
					break;
				}
				if (Main.myPlayer == base.Projectile.owner)
				{
					float flakDist = MathHelper.Clamp(((Vector2)(ref ownerToMouse)).Length(), 0f, 960f);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, direction * itemShootSpeed, ModContent.ProjectileType<FlakKrakenProjectile>(), itemDamage, itemKnockback, base.Projectile.owner, rocketTypeShot, flakDist);
				}
				Player owner = base.Owner;
				owner.velocity += ownerToMouse.SafeNormalize(Vector2.UnitY) * (0f - FlakKraken.OwnerKnockbackStrength);
				if (!Main.dedServ)
				{
					base.OffsetLengthFromArm = 10f;
					base.Owner.SetScreenshake(3.5f);
					GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(GunTipPosition, Vector2.Zero, Color.Gray * 0.7f, new Vector2(0.5f, 1f), base.Projectile.rotation, 0.1f, 0.4f, 20));
					int smokeAmount = Main.rand.Next(12, 17);
					for (int i = 0; i < smokeAmount; i++)
					{
						GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(GunTipPosition, direction.RotatedByRandom(MathHelper.ToRadians(25f)) * Main.rand.NextFloat(2f, 30f), EffectsColor * 0.1f, Main.rand.Next(45, 61), Main.rand.NextFloat(0.6f, 1.3f), Main.rand.NextFloat(0.2f, 0.35f)));
					}
					int dustAmount = Main.rand.Next(15, 21);
					for (int j = 0; j < dustAmount; j++)
					{
						Vector2 gunTipPosition = GunTipPosition;
						int dustEffectsID = DustEffectsID;
						Vector2? velocity = direction.RotatedByRandom(0.39269909262657166) * Main.rand.NextFloat(2f, 12f);
						float scale = Main.rand.NextFloat(0.8f, 1f);
						Dust dust = Dust.NewDustPerfect(gunTipPosition, dustEffectsID, velocity, 0, default(Color), scale);
						dust.fadeIn = 100f;
						dust.noLight = false;
						dust.noLightEmittence = false;
					}
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/FlakKrakenShoot");
					style.Volume = 0.6f;
					SoundEngine.PlaySound(in style, GunTipPosition);
				}
			}
			if (ShootingTimer >= (float)base.HeldItem.useAnimation + adaptiveTimeBetweenShots * (FlakKraken.ProjectilesPerBurst - 1f))
			{
				ShootingTimer = 0f;
				TimerBetweenBursts = 0f;
			}
		}
		ShootingTimer++;
	}

	public override void OnSpawn(IEntitySource source)
	{
		base.OnSpawn(source);
		base.FrontArmStretch = Player.CompositeArmStretchAmount.ThreeQuarters;
	}
}
