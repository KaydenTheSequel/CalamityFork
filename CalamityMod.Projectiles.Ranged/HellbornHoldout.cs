using System;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

[PierceResistException(false)]
public class HellbornHoldout : BaseGunHoldoutProjectile
{
	public float cooldownTimer;

	public SlotId SoundSlot;

	public bool failedShot;

	public float drawRot;

	public bool hasPlayedSound;

	public bool hasPlayedReloadSound;

	public float fade;

	public int hitTimer;

	public override int AssociatedItemID => ModContent.ItemType<Hellborn>();

	public override float MaxOffsetLengthFromArm => 24f;

	public override float OffsetXUpwards => -5f;

	public override float BaseOffsetY => -5f;

	public override float OffsetYDownwards => 5f;

	public override float RecoilResolveSpeed => 0.5f;

	public ref float time => ref base.Projectile.ai[0];

	public bool onCooldown => cooldownTimer > 0f;

	public bool Spinning => base.Projectile.ai[2] == 0f;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 12;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 62;
		base.Projectile.height = 34;
		base.Projectile.penetrate = -1;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 4;
		base.Projectile.tileCollide = false;
		base.Projectile.ArmorPenetration = 25;
	}

	public override void HoldoutAI()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0739: Unknown result type (might be due to invalid IL or missing references)
		//IL_0744: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0764: Unknown result type (might be due to invalid IL or missing references)
		//IL_0769: Unknown result type (might be due to invalid IL or missing references)
		//IL_0772: Unknown result type (might be due to invalid IL or missing references)
		//IL_0777: Unknown result type (might be due to invalid IL or missing references)
		//IL_077e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0783: Unknown result type (might be due to invalid IL or missing references)
		//IL_0788: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_07af: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_0819: Unknown result type (might be due to invalid IL or missing references)
		//IL_081f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0820: Unknown result type (might be due to invalid IL or missing references)
		//IL_0825: Unknown result type (might be due to invalid IL or missing references)
		//IL_082f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0868: Unknown result type (might be due to invalid IL or missing references)
		//IL_0872: Unknown result type (might be due to invalid IL or missing references)
		//IL_087d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0896: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0905: Unknown result type (might be due to invalid IL or missing references)
		//IL_0912: Unknown result type (might be due to invalid IL or missing references)
		//IL_0918: Unknown result type (might be due to invalid IL or missing references)
		//IL_0945: Unknown result type (might be due to invalid IL or missing references)
		//IL_094a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0963: Unknown result type (might be due to invalid IL or missing references)
		//IL_0968: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0635: Unknown result type (might be due to invalid IL or missing references)
		//IL_0662: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_0680: Unknown result type (might be due to invalid IL or missing references)
		//IL_0685: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_0985: Unknown result type (might be due to invalid IL or missing references)
		//IL_098a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0995: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b3: Unknown result type (might be due to invalid IL or missing references)
		if (hitTimer > 0)
		{
			hitTimer--;
		}
		fade = Utils.GetLerpValue(0f, 40f, time, clamped: true);
		if (SoundEngine.TryGetActiveSound(SoundSlot, out ActiveSound sSound) && sSound.IsPlaying)
		{
			sSound.Volume = fade * 100f * 0.5f;
			sSound.Position = base.Projectile.Center;
		}
		if (onCooldown)
		{
			PostFiringCooldown();
			if (!Spinning)
			{
				return;
			}
		}
		if (Spinning)
		{
			float armRot = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
			float spinArmRot = ((base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, -3f), (double)(drawRot * 0.6f), default(Vector2)) - base.Owner.Center).SafeNormalize(Vector2.UnitX) * 5f).ToRotation();
			base.Owner.SetCompositeArmBack(enabled: true, base.FrontArmStretch, armRot + 0.6f * (float)base.Projectile.direction);
			base.Owner.SetCompositeArmFront(enabled: true, base.BackArmStretch, spinArmRot + MathHelper.ToRadians(-90f));
		}
		if (time > 3f)
		{
			Color newColor;
			if (Spinning)
			{
				if (base.Owner.CantUseHoldout())
				{
					base.Projectile.Kill();
					sSound?.Stop();
				}
				else
				{
					base.OffsetLengthFromArm = MathHelper.Lerp(base.OffsetLengthFromArm, 9f, 0.5f);
					drawRot += 0.75f * (float)base.Projectile.direction * MathHelper.Clamp(fade, 0.3f, 1f);
					Vector2 center = base.Projectile.Center;
					newColor = Color.Lerp(Color.OrangeRed, Color.White, 0.6f);
					Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * fade * 0.7f);
					if (!hasPlayedSound && Main.myPlayer == base.Projectile.owner)
					{
						SoundStyle spin = new SoundStyle("CalamityMod/Sounds/Item/SpinningWoosh");
						SoundStyle style = spin with
						{
							Volume = 0.01f,
							Pitch = -0.1f,
							IsLooped = true
						};
						SoundSlot = SoundEngine.PlaySound(in style, base.Projectile.Center);
						hasPlayedSound = true;
					}
					GeneralParticleHandler.SpawnParticle(new CircularSmearVFX(base.Projectile.Center, Color.Red * Main.rand.NextFloat(0.45f, 0.6f), Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(1.13f, 1.25f) * fade));
					GeneralParticleHandler.SpawnParticle(new CircularSmearVFX(base.Projectile.Center, Color.OrangeRed * Main.rand.NextFloat(0.45f, 0.6f), Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(0.65f, 0.72f) * fade));
					if (Main.rand.NextBool() && fade > 0.2f)
					{
						for (int i = 0; i < 2; i++)
						{
							Vector2 position = base.Projectile.Center + ((float)i * (float)Math.PI + drawRot * 0.25f + (float)Math.PI / 2f).ToRotationVector2().RotatedByRandom(0.5) * 35f * fade;
							Vector2? velocity = ((float)i * (float)Math.PI + drawRot * (float)base.Projectile.direction * 0.25f * (float)Math.Sign(base.Projectile.velocity.X)).ToRotationVector2().RotatedBy(MathHelper.ToRadians(180f) * (float)((base.Projectile.direction != 1) ? 1 : 0)) * -3f * Main.rand.NextFloat(0.5f, 1f) * fade;
							newColor = default(Color);
							Dust dust = Dust.NewDustPerfect(position, 278, velocity, 0, newColor);
							dust.noGravity = true;
							dust.scale = Main.rand.NextFloat(0.42f, 0.62f);
							dust.color = Color.Lerp(Color.Orange, Color.Red, Main.rand.NextFloat(0f, 1f));
						}
					}
					else if (fade > 0.2f)
					{
						for (int j = 0; j < 3; j++)
						{
							Vector2 position2 = base.Projectile.Center + ((float)j * (float)Math.PI + drawRot * 0.5f + (float)Math.PI / 2f).ToRotationVector2().RotatedByRandom(0.5) * 87f * fade;
							Vector2? velocity2 = ((float)j * (float)Math.PI + drawRot * (float)base.Projectile.direction * 0.5f * (float)Math.Sign(base.Projectile.velocity.X)).ToRotationVector2().RotatedBy(MathHelper.ToRadians(180f) * (float)((base.Projectile.direction != 1) ? 1 : 0)) * -7f * Main.rand.NextFloat(0.5f, 1f) * fade;
							newColor = default(Color);
							Dust dust2 = Dust.NewDustPerfect(position2, 267, velocity2, 0, newColor);
							dust2.noGravity = true;
							dust2.scale = Main.rand.NextFloat(0.62f, 0.82f);
							dust2.color = Color.Lerp(Color.Orange, Color.Red, Main.rand.NextFloat(0f, 1f));
						}
					}
				}
			}
			else if (!onCooldown)
			{
				if (base.Owner.Calamity().hellbornShots > 0)
				{
					base.Owner.SetScreenshake(5f);
					base.OffsetLengthFromArm -= 35f;
					cooldownTimer = base.Owner.itemAnimationMax * 2;
					base.Owner.Calamity().hellbornShots--;
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HellbornShoot");
					style.PitchVariance = 0.15f;
					style.Volume = 0.9f;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					Vector2 shootVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 7f;
					Player owner = base.Owner;
					owner.velocity += shootVelocity * -0.68f;
					if (Main.myPlayer == base.Projectile.owner)
					{
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity, ModContent.ProjectileType<HellbornProj>(), base.Projectile.damage, 0f, base.Projectile.owner);
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, -shootVelocity.RotatedBy(0.75f * (float)base.Projectile.direction) * 1f, ModContent.ProjectileType<HellbornShell>(), 0, 0f, base.Projectile.owner);
					}
					for (int k = 0; k <= 13; k++)
					{
						Vector2 gunTipPosition = GunTipPosition;
						Vector2? velocity3 = shootVelocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.2f, 1.5f);
						newColor = default(Color);
						Dust dust3 = Dust.NewDustPerfect(gunTipPosition, 303, velocity3, 80, newColor, Main.rand.NextFloat(0.4f, 1.3f));
						dust3.noGravity = false;
						dust3.color = Color.White;
						Vector2 gunTipPosition2 = GunTipPosition;
						Vector2? velocity4 = shootVelocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.1f, 1f);
						newColor = default(Color);
						Dust dust4 = Dust.NewDustPerfect(gunTipPosition2, 278, velocity4, 0, newColor);
						dust4.noGravity = true;
						dust4.scale = Main.rand.NextFloat(0.52f, 0.72f);
						dust4.color = Color.Lerp(Color.Orange, Color.Red, Main.rand.NextFloat(0f, 1f));
					}
					for (int l = 0; l < 14; l++)
					{
						GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(GunTipPosition, shootVelocity.RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(0.2f, 1.5f), Color.White, Main.rand.Next(40, 61), Main.rand.NextFloat(0.3f, 0.6f), 0.5f, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextBool(), 0f, required: true));
					}
				}
				else
				{
					base.OffsetLengthFromArm -= 8f;
					failedShot = true;
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DudFire");
					style.PitchVariance = 0.15f;
					style.Volume = 0.75f;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					cooldownTimer = base.Owner.itemAnimationMax;
				}
			}
		}
		time++;
	}

	private void PostFiringCooldown()
	{
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		base.Owner.channel = true;
		cooldownTimer -= (Spinning ? 1f : 2f);
		if (cooldownTimer > 1f)
		{
			if (!base.Owner.Calamity().mouseRight && cooldownTimer < (float)base.Owner.itemAnimationMax * 1.7f && !failedShot && !Main.mouseLeftRelease)
			{
				base.Projectile.ai[2] = 0f;
			}
			if (cooldownTimer <= 18f && !hasPlayedReloadSound)
			{
				if (!failedShot)
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HellbornReload");
					style.Volume = 0.8f;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
				hasPlayedReloadSound = true;
			}
		}
		else if (!Spinning)
		{
			if (SoundEngine.TryGetActiveSound(SoundSlot, out ActiveSound sSound) && sSound.IsPlaying)
			{
				sSound?.Stop();
			}
			base.Projectile.Kill();
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (hitTimer == 0)
		{
			modifiers.SourceDamage *= 0.08f;
			if (base.Owner.Calamity().hellbornShots < 12)
			{
				base.Owner.Calamity().hellbornShots++;
			}
			hitTimer = base.Projectile.localNPCHitCooldown;
		}
		else
		{
			modifiers.SourceDamage *= 0.02f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		if (time < 2f)
		{
			return false;
		}
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/Hellborn", (AssetRequestMode)2).Value;
		_ = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/Hellborn", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f) - ((base.Owner.gravDir == -1f) ? ((float)Math.PI * (float)base.Owner.direction) : 0f);
		Main.EntitySpriteDraw(origin: value.Size() * 0.5f, effects: (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f), texture: value, position: drawPosition + (Spinning ? Utils.RotatedBy(new Vector2(0f, -8f), (double)(drawRot * 0.6f), default(Vector2)) : Vector2.Zero), sourceRectangle: null, color: base.Projectile.GetAlpha(lightColor), rotation: drawRotation + drawRot, scale: base.Projectile.scale);
		return false;
	}

	public override bool? CanDamage()
	{
		if (!Spinning || !(time > 5f))
		{
			return false;
		}
		return null;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 100f, targetHitbox);
	}

	public override void KillHoldoutLogic()
	{
	}
}
