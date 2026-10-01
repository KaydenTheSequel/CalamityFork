using System;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.MaceFlails;

[PierceResistException(false)]
public class UrchinMaceProj : BaseMaceFlailProjectile
{
	public static float MaxWindup = 45f;

	public override string Texture => "CalamityMod/Items/Weapons/Melee/UrchinMace";

	public override string ChainTexturePath => string.Empty;

	public override int AssociatedItemID => ModContent.ItemType<UrchinMace>();

	public override int SpinIFrames => 15;

	public override float SpinHitboxRadius => 56f;

	public override float SpinVerticalFactor => 1f;

	public override float LaunchSpeed => 22f;

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 60);
		base.SetDefaults();
		base.Projectile.localNPCHitCooldown = SpinIFrames * base.Projectile.MaxUpdates;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ownerHitCheck = true;
	}

	public override void SpinAI(float launchSpeed)
	{
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		float WindupProgress = MathHelper.Clamp(base.StateTimer, 0f, MaxWindup) / MaxWindup;
		if (base.Projectile.owner == Main.myPlayer)
		{
			Vector2 toMouse = base.Owner.MountedCenter.DirectionTo(Main.MouseWorld).SafeNormalize(Vector2.UnitX * (float)base.Owner.direction);
			base.Owner.ChangeDir((toMouse.X > 0f).ToDirectionInt());
			if (!base.Owner.channel && WindupProgress >= 1f)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Owner.MountedCenter, toMouse * launchSpeed, ModContent.ProjectileType<RedtideWhirlpool>(), (int)((float)base.Projectile.damage * LaunchDamage), base.Projectile.knockBack, base.Projectile.owner);
				SoundEngine.PlaySound(in SoundID.Item7, base.Owner.MountedCenter);
				base.Projectile.Kill();
				return;
			}
			if (base.StateTimer == MaxWindup)
			{
				for (int i = 0; i < 25; i++)
				{
					Vector2 position = base.Owner.position + Main.rand.NextVector2FromRectangle(base.Owner.Hitbox);
					Vector2? velocity = Vector2.UnitY * -5f * Main.rand.NextFloat(1f, 2f) + base.Owner.velocity;
					float scale = Main.rand.NextFloat(1f, 2f);
					Dust.NewDustPerfect(position, 176, velocity, 0, default(Color), scale).noGravity = true;
				}
			}
		}
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.rotation += WindupProgress * ((float)Math.PI / 4f) / 1.5f * (float)base.Owner.direction;
		base.Projectile.Center = base.Owner.MountedCenter + base.Projectile.rotation.ToRotationVector2() * 10f - Vector2.UnitX * 4f * (float)base.Owner.direction;
		if (WindupProgress > 0.5f)
		{
			int dustCount = Main.rand.Next(4);
			float offset = Main.rand.NextFloat((float)Math.PI * 2f);
			for (int j = 0; j < dustCount; j++)
			{
				float angle = (float)j / (float)dustCount * ((float)Math.PI * 2f) + offset;
				Vector2 position2 = base.Owner.MountedCenter + angle.ToRotationVector2() * 40f * WindupProgress;
				Vector2? velocity2 = (angle - (float)Math.PI / 2f * (float)base.Owner.direction).ToRotationVector2() * 5f + base.Owner.velocity;
				float scale = Main.rand.NextFloat(1f, 2f);
				Dust.NewDustPerfect(position2, 176, velocity2, 0, default(Color), scale).noGravity = true;
			}
		}
		base.StateTimer++;
	}

	public override bool ExtraBehavior()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.soundDelay <= 0)
		{
			SoundEngine.PlaySound(in SoundID.DD2_GhastlyGlaivePierce, base.Owner.MountedCenter);
			base.Projectile.soundDelay = 28;
		}
		float armPointingDirection = (base.Owner.Calamity().mouseWorld - base.Owner.MountedCenter).ToRotation();
		armPointingDirection = ((armPointingDirection < (float)Math.PI / 2f && armPointingDirection >= -(float)Math.PI / 2f) ? ((float)Math.PI * -3f / 8f + (float)Math.PI * 3f / 4f * Utils.GetLerpValue(0f, (float)Math.PI, armPointingDirection + (float)Math.PI / 2f, clamped: true)) : ((!(armPointingDirection > 0f)) ? (-(float)Math.PI + (float)Math.PI * 3f / 8f * Utils.GetLerpValue(-(float)Math.PI, -(float)Math.PI / 4f, armPointingDirection, clamped: true)) : ((float)Math.PI * 5f / 8f + (float)Math.PI * 3f / 8f * Utils.GetLerpValue(0f, (float)Math.PI / 2f, armPointingDirection - (float)Math.PI / 2f, clamped: true))));
		base.Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, armPointingDirection - (float)Math.PI / 2f);
		base.Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, base.Projectile.rotation - (float)Math.PI / 2f);
		base.Projectile.timeLeft = 2;
		base.Owner.heldProj = base.Projectile.whoAmI;
		base.Owner.SetDummyItemTime(2);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 3; i++)
		{
			float angle = Main.rand.NextFloat((float)Math.PI * 2f);
			GeneralParticleHandler.SpawnParticle(new UrchinSpikeParticle(target.Center + angle.ToRotationVector2() * 15f, angle.ToRotationVector2() * 6f, angle + (float)Math.PI / 2f, Main.rand.NextFloat(1f, 1.3f), 1f, Main.rand.Next(10) + 25));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		Texture2D maceTexture = TextureAssets.Projectile[base.Type].Value;
		Texture2D whirlpoolTexture = TextureAssets.Projectile[ModContent.ProjectileType<RedtideWhirlpool>()].Value;
		float num = MathHelper.Clamp(base.StateTimer, 0f, MaxWindup) / MaxWindup;
		float whirlpoolScale = MathHelper.Clamp(num * 3f - 0.4f, 0f, 1f) * 1.6f;
		float whirlpoolOpacity = num * 0.2f + MathF.Sin(Main.GlobalTimeWrappedHourly * 3f) * 0.1f;
		float whirlpoolRotation = base.StateTimer * 0.34f * (float)base.Owner.direction;
		SpriteEffects flip = (SpriteEffects)(base.Owner.direction >= 0);
		Main.EntitySpriteDraw(whirlpoolTexture, base.Owner.MountedCenter - Main.screenPosition, null, Lighting.GetColor((int)base.Owner.MountedCenter.X / 16, (int)base.Owner.MountedCenter.Y / 16) * whirlpoolOpacity * 0.3f, whirlpoolRotation * 1.2f, whirlpoolTexture.Size() * 0.5f, whirlpoolScale, flip);
		Main.EntitySpriteDraw(whirlpoolTexture, base.Owner.MountedCenter - Main.screenPosition, null, Lighting.GetColor((int)base.Owner.MountedCenter.X / 16, (int)base.Owner.MountedCenter.Y / 16) * whirlpoolOpacity, whirlpoolRotation, whirlpoolTexture.Size() * 0.5f, whirlpoolScale, flip);
		Vector2 handleOrigin = default(Vector2);
		((Vector2)(ref handleOrigin))._002Ector(0f, (float)maceTexture.Height);
		float maceRotation = base.Projectile.rotation + (float)Math.PI / 4f;
		Main.EntitySpriteDraw(maceTexture, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), maceRotation, handleOrigin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
