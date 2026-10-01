using System;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ScorchedEarthHoldout : BaseGunHoldoutProjectile
{
	public override int AssociatedItemID => ModContent.ItemType<ScorchedEarth>();

	public override Vector2 GunTipPosition
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			return base.GunTipPosition - Vector2.UnitY.RotatedBy(base.Projectile.rotation) * 5f * (float)base.Projectile.spriteDirection * base.Owner.gravDir;
		}
	}

	public override float RecoilResolveSpeed => 0.1f;

	public override float MaxOffsetLengthFromArm => 15f;

	public override float OffsetXUpwards => -12f;

	public override float OffsetXDownwards => 2f;

	public override float BaseOffsetY => -10f;

	public override float OffsetYDownwards => 10f;

	public ref float ShootingTimer => ref base.Projectile.ai[0];

	public ref float TimerBetweenBursts => ref base.Projectile.ai[1];

	public override void HoldoutAI()
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		if (ShootingTimer >= (float)base.HeldItem.useAnimation)
		{
			int adaptedTimeBetweenBursts = ScorchedEarth.TimeBetweenBursts * base.HeldItem.useAnimation / ScorchedEarth.OriginalUseTime;
			if (ShootingTimer % (float)adaptedTimeBetweenBursts == 0f)
			{
				ShootRocket(base.HeldItem, isRMB: false);
			}
			if (ShootingTimer >= (float)(base.HeldItem.useAnimation + adaptedTimeBetweenBursts * (ScorchedEarth.ProjectilesPerBurst - 1)))
			{
				ShootingTimer = 0f;
				TimerBetweenBursts = 0f;
			}
			if (Main.dedServ)
			{
				return;
			}
			Vector2 smokeVel = new Vector2(0f, -8f) * Main.rand.NextFloat(0.1f, 1.1f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(GunTipPosition, smokeVel, Main.rand.NextBool() ? Color.DarkSlateGray : Color.DarkGray, Main.rand.Next(40, 61), Main.rand.NextFloat(0.3f, 0.6f), 0.5f, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextBool(), 0f, required: true));
		}
		ShootingTimer++;
	}

	public void ShootRocket(Item item, bool isRMB)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity.SafeNormalize(Vector2.Zero);
		base.Owner.PickAmmo(item, out var _, out var _, out var damage, out var knockback, out var rocketType);
		Vector2 shootVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 12f;
		if (Main.myPlayer == base.Projectile.owner)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity.RotatedByRandom(MathHelper.ToRadians(10f)), ModContent.ProjectileType<ScorchedEarthRocket>(), damage, knockback, base.Projectile.owner, rocketType);
		}
		if (!Main.dedServ)
		{
			SoundStyle style = ScorchedEarth.RocketShoot with
			{
				Pitch = 0.045f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.OffsetLengthFromArm -= 7f;
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(GunTipPosition, Vector2.Zero, Color.Gray * 0.7f, new Vector2(0.5f, 1f), base.Projectile.rotation, 0.1f, 0.4f, 25));
			for (int i = 0; i <= 8; i++)
			{
				Dust dust = Dust.NewDustPerfect(GunTipPosition, Main.rand.NextBool(3) ? 303 : 244, (shootVelocity * Main.rand.NextFloat(0.2f, 1.1f)).RotatedByRandom(0.4000000059604645));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.8f, 1.4f);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(position: base.Projectile.Center - Main.screenPosition, color: base.Projectile.GetAlpha(lightColor), rotation: base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f), origin: value.Size() * 0.5f, effects: (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f), texture: value, sourceRectangle: null, scale: base.Projectile.scale * base.Owner.gravDir);
		return false;
	}
}
