using System.Runtime.CompilerServices;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class FlakToxicannonHoldout : BaseGunHoldoutProjectile
{
	[CompilerGenerated]
	private static Color _003CEffectsColor_003Ek__BackingField;

	public override int AssociatedItemID => ModContent.ItemType<FlakToxicannon>();

	public override float MaxOffsetLengthFromArm => 38f;

	public override float RecoilResolveSpeed => 0.1f;

	public override float OffsetXUpwards => -10f;

	public override float OffsetXDownwards => 2f;

	public override float BaseOffsetY => -10f;

	public override float OffsetYUpwards => 10f;

	public override float OffsetYDownwards => 5f;

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
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		Vector2 mountedCenter = base.Owner.MountedCenter;
		Vector2 ownerToMouse = base.Owner.Calamity().mouseWorld - mountedCenter;
		if (ShootingTimer >= (float)base.HeldItem.useAnimation)
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
				DustEffectsID = 298;
				EffectsColor = Color.GreenYellow;
				break;
			}
			if (Main.myPlayer == base.Projectile.owner)
			{
				float flakDist = MathHelper.Clamp(((Vector2)(ref ownerToMouse)).Length(), 0f, 960f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, direction * itemShootSpeed, ModContent.ProjectileType<FlakToxicannonProjectile>(), itemDamage, itemKnockback, base.Projectile.owner, rocketTypeShot, flakDist);
			}
			Player owner = base.Owner;
			owner.velocity += ownerToMouse.SafeNormalize(Vector2.UnitY) * (0f - FlakToxicannon.OwnerKnockbackStrength);
			if (!Main.dedServ)
			{
				base.OffsetLengthFromArm = 25f;
				base.Owner.SetScreenshake(2f);
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(GunTipPosition, Vector2.Zero, Color.Gray * 0.7f, new Vector2(0.5f, 1f), base.Projectile.rotation, 0.1f, 0.4f, 20));
				int smokeAmount = Main.rand.Next(8, 13);
				for (int i = 0; i < smokeAmount; i++)
				{
					GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(GunTipPosition, direction.RotatedByRandom(MathHelper.ToRadians(25f)) * Main.rand.NextFloat(2f, 30f), EffectsColor * 0.4f, Main.rand.Next(45, 61), Main.rand.NextFloat(0.6f, 1.3f), Main.rand.NextFloat(0.2f, 0.35f), 0f, glowing: true));
				}
				int dustAmount = Main.rand.Next(10, 16);
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
				style.Pitch = 0.65f;
				style.Volume = 0.5f;
				SoundEngine.PlaySound(in style, GunTipPosition);
			}
			ShootingTimer = 0f;
		}
		ShootingTimer++;
	}
}
