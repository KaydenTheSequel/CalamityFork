using System;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

[PierceResistException(false)]
public class MurasamaSlash : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public bool Slashing;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Murasama>();

	private Player Owner => Main.player[base.Projectile.owner];

	public ref int hitCooldown => ref Main.player[base.Projectile.owner].Calamity().murasamaHitCooldown;

	public bool Slash1 => base.Projectile.frame == 10;

	public bool Slash2 => base.Projectile.frame == 0;

	public bool Slash3 => base.Projectile.frame == 6;

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 14;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 216;
		base.Projectile.height = 216;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 6;
		base.Projectile.frameCounter = 0;
		base.Projectile.alpha = 255;
		base.Projectile.noEnchantmentVisuals = true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.frameCounter <= 1)
		{
			return false;
		}
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(origin: frame.Size() * 0.5f, effects: (SpriteEffects)(base.Projectile.direction != 1), texture: value, position: base.Projectile.Center - Main.screenPosition + base.Projectile.velocity * 0.3f + Utils.RotatedBy(new Vector2(0f, -32f), (double)base.Projectile.rotation, default(Vector2)), sourceRectangle: frame, color: Color.White, rotation: base.Projectile.rotation, scale: base.Projectile.scale);
		return false;
	}

	public override void AI()
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		if (time == 0)
		{
			if (Main.zenithWorld)
			{
				base.Projectile.scale = 2f;
				base.Projectile.damage = base.Projectile.damage * 2;
			}
			base.Projectile.frame = (Main.zenithWorld ? 6 : 10);
			base.Projectile.alpha = 0;
			time++;
		}
		Player player = Main.player[base.Projectile.owner];
		if (Slash2)
		{
			SoundStyle style = Murasama.Swing with
			{
				Pitch = -0.1f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			if (hitCooldown == 0)
			{
				Slashing = true;
			}
			base.Projectile.numHits = 0;
		}
		else if (Slash3)
		{
			SoundStyle style = Murasama.BigSwing with
			{
				Pitch = 0f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			if (hitCooldown == 0)
			{
				Slashing = true;
			}
			base.Projectile.numHits = 0;
		}
		else if (Slash1)
		{
			SoundStyle style = Murasama.Swing with
			{
				Pitch = -0.05f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			if (hitCooldown == 0)
			{
				Slashing = true;
			}
			base.Projectile.numHits = 0;
		}
		else
		{
			Slashing = false;
		}
		if (base.Projectile.frame == 5 && base.Projectile.frameCounter % 3 == 0)
		{
			base.Projectile.damage = base.Projectile.damage * 2;
		}
		if (base.Projectile.frame == 7 && base.Projectile.frameCounter % 3 == 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.5f);
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 3 == 0)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
		}
		Vector2 position = base.Projectile.Center + base.Projectile.velocity * 3f;
		Color red = Color.Red;
		Lighting.AddLight(position, ((Color)(ref red)).ToVector3() * (Slashing ? 3.5f : 2f));
		Vector2 playerRotatedPoint = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		if (Main.myPlayer == base.Projectile.owner)
		{
			if (!player.CantUseHoldout())
			{
				HandleChannelMovement(player, playerRotatedPoint);
			}
			else
			{
				hitCooldown = ((!Main.zenithWorld) ? 8 : 0);
				base.Projectile.Kill();
			}
		}
		if (Slashing || Slash1)
		{
			float velocityAngle = base.Projectile.velocity.ToRotation();
			base.Projectile.rotation = velocityAngle + (float)(base.Projectile.direction == -1).ToInt() * (float)Math.PI;
		}
		float velocityAngle2 = base.Projectile.velocity.ToRotation();
		base.Projectile.direction = (Math.Cos(velocityAngle2) > 0.0).ToDirectionInt();
		float offset = 80f * base.Projectile.scale;
		base.Projectile.Center = playerRotatedPoint + velocityAngle2.ToRotationVector2() * offset;
		player.ChangeDir(base.Projectile.direction);
		base.Projectile.timeLeft = 2;
		player.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
		player.heldProj = base.Projectile.whoAmI;
		player.itemTime = 2;
		player.itemAnimation = 2;
	}

	public void HandleChannelMovement(Player player, Vector2 playerRotatedPoint)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		float speed = 1f;
		if (player.HeldItem.shoot == base.Projectile.type)
		{
			speed = player.HeldItem.shootSpeed * base.Projectile.scale;
		}
		Vector2 newVelocity = (Main.MouseWorld - playerRotatedPoint).SafeNormalize(Vector2.UnitX * (float)player.direction) * speed;
		if (Slashing)
		{
			if (base.Projectile.velocity.X != newVelocity.X || base.Projectile.velocity.Y != newVelocity.Y)
			{
				base.Projectile.netUpdate = true;
			}
			base.Projectile.velocity = newVelocity;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		Vector2 lineEnd = Main.player[base.Projectile.owner].Center + base.Projectile.velocity * (Slash3 ? 11.35f : 8.6f);
		float lineWidth = (((base.Projectile.direction == 1 && ((Rectangle)(ref projHitbox)).Center.X > ((Rectangle)(ref targetHitbox)).Center.X) || (base.Projectile.direction == -1 && ((Rectangle)(ref projHitbox)).Center.X < ((Rectangle)(ref targetHitbox)).Center.X)) ? 320f : 200f);
		float _ = 0f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Main.player[base.Projectile.owner].Center, lineEnd, lineWidth, ref _);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		if (target.Organic())
		{
			SoundStyle style = Murasama.OrganicHit with
			{
				Pitch = (Slash2 ? (-0.1f) : (Slash3 ? 0.1f : (Slash1 ? (-0.15f) : 0f)))
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		else
		{
			SoundStyle style = Murasama.InorganicHit with
			{
				Pitch = (Slash2 ? (-0.1f) : (Slash3 ? 0.1f : (Slash1 ? (-0.15f) : 0f)))
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		for (int i = 0; i < 3; i++)
		{
			Color impactColor = ((!Slash3) ? (Main.rand.NextBool(4) ? Color.LightCoral : Color.Crimson) : (Main.rand.NextBool(3) ? Color.LightCoral : Color.White));
			float impactParticleScale = Main.rand.NextFloat(1f, 1.75f);
			if (Slash3)
			{
				GeneralParticleHandler.SpawnParticle(new SparkleParticle(target.Center + Main.rand.NextVector2Circular((float)target.width * 0.75f, (float)target.height * 0.75f), Vector2.Zero, Color.White, Color.Red, impactParticleScale * 1.2f, 8, 0f, 4.5f));
			}
			GeneralParticleHandler.SpawnParticle(new SparkleParticle(target.Center + Main.rand.NextVector2Circular((float)target.width * 0.75f, (float)target.height * 0.75f), Vector2.Zero, impactColor, Color.Red, impactParticleScale, 8, 0f, 2.5f));
		}
		float sparkCount = MathHelper.Clamp(Slash3 ? (18 - base.Projectile.numHits * 3) : (5 - base.Projectile.numHits * 2), 0, 18);
		for (int j = 0; (float)j < sparkCount; j++)
		{
			Vector2 sparkVelocity2 = base.Projectile.velocity.RotatedBy(Slash2 ? (-0.45f * (float)Owner.direction) : (Slash3 ? 0f : (Slash1 ? (0.45f * (float)Owner.direction) : 0f))).RotatedByRandom(0.3499999940395355) * Main.rand.NextFloat(0.5f, 1.8f);
			int sparkLifetime2 = Main.rand.Next(23, 35);
			float sparkScale2 = Main.rand.NextFloat(0.95f, 1.8f);
			Color sparkColor2 = ((!Slash3) ? (Main.rand.NextBool() ? Color.Red : Color.Firebrick) : (Main.rand.NextBool(3) ? Color.Red : Color.IndianRed));
			if (Main.rand.NextBool())
			{
				GeneralParticleHandler.SpawnParticle(new AltSparkParticle(target.Center + Main.rand.NextVector2Circular((float)target.width * 0.5f, (float)target.height * 0.5f) + base.Projectile.velocity * 1.2f, sparkVelocity2 * (Slash3 ? 1f : 0.65f), affectedByGravity: false, (int)((float)sparkLifetime2 * (Slash3 ? 1.2f : 1f)), sparkScale2 * (Slash3 ? 1.4f : 1f), sparkColor2));
			}
			else
			{
				GeneralParticleHandler.SpawnParticle(new LineParticle(target.Center + Main.rand.NextVector2Circular((float)target.width * 0.5f, (float)target.height * 0.5f) + base.Projectile.velocity * 1.2f, sparkVelocity2 * ((base.Projectile.frame == 7) ? 1f : 0.65f), affectedByGravity: false, (int)((float)sparkLifetime2 * ((base.Projectile.frame == 7) ? 1.2f : 1f)), sparkScale2 * ((base.Projectile.frame == 7) ? 1.4f : 1f), Main.rand.NextBool() ? Color.Red : Color.Firebrick));
			}
		}
		float dustCount = MathHelper.Clamp(Slash3 ? (25 - base.Projectile.numHits * 3) : (12 - base.Projectile.numHits * 2), 0, 25);
		for (int k = 0; (float)k <= dustCount; k++)
		{
			int dustID = (Main.rand.NextBool(3) ? 182 : (Main.rand.NextBool() ? (Slash3 ? 309 : 296) : 90));
			Dust dust = Dust.NewDustPerfect(target.Center + Main.rand.NextVector2Circular((float)target.width * 0.5f, (float)target.height * 0.5f), dustID, base.Projectile.velocity.RotatedBy(Slash2 ? (-0.45f * (float)Owner.direction) : (Slash3 ? 0f : (Slash1 ? (0.45f * (float)Owner.direction) : 0f))).RotatedByRandom(0.550000011920929) * Main.rand.NextFloat(0.3f, 1.1f));
			dust.scale = Main.rand.NextFloat(0.9f, 2.4f);
			dust.noGravity = true;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		return new Color(100, 0, 0, 0);
	}

	public override bool? CanDamage()
	{
		if (Slashing)
		{
			return null;
		}
		return false;
	}
}
