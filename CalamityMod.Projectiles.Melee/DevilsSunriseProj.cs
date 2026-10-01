using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

[PierceResistException(false)]
public class DevilsSunriseProj : ModProjectile
{
	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/Item/MantisSwipe", 2)
	{
		Pitch = 0.8f
	};

	public static readonly SoundStyle FullChargeSound = new SoundStyle("CalamityMod/Sounds/Item/HellbornImpact");

	public static readonly SoundStyle ThrowSound = new SoundStyle("CalamityMod/Sounds/Item/SwingMid")
	{
		Pitch = 1.2f
	};

	private int red;

	private const int greenAndBlue = 100;

	private const float TimerMax = 450f;

	private bool reachedMaxCharge;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<DevilsSunrise>();

	public ref float Timer => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 28;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 148;
		base.Projectile.height = 68;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		Timer++;
		Vector2 center;
		if (Timer > 450f)
		{
			Timer = 450f;
			if (!reachedMaxCharge)
			{
				for (int i = 0; i < 35; i++)
				{
					Vector2 unitX = Vector2.UnitX;
					double radians = (float)i * ((float)Math.PI * 2f) / 35f;
					center = default(Vector2);
					Vector2 dustVel = unitX.RotatedBy(radians, center) * Main.rand.NextFloat(10f, 12.5f);
					Dust.NewDustPerfect(Owner.Center, ModContent.DustType<BrimstoneFlame>(), dustVel, 0, default(Color), 2.25f).noGravity = true;
				}
				SoundEngine.PlaySound(in FullChargeSound, Owner.Center);
				reachedMaxCharge = true;
			}
		}
		red = 30 + (int)(Timer * 0.5f);
		if (red > 255)
		{
			red = 255;
		}
		if (++base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.soundDelay--;
		if (base.Projectile.soundDelay <= 0)
		{
			SoundEngine.PlaySound(in SoundID.Item15, base.Projectile.Center);
			base.Projectile.soundDelay = 24;
		}
		Vector2 dustSpawn = base.Projectile.Center + base.Projectile.velocity * 3f;
		Lighting.AddLight(dustSpawn, (float)red * 0.001f, 0.1f, 0.1f);
		if (Main.rand.NextBool((Timer >= 450f) ? 2 : 4))
		{
			Dust dust = Dust.NewDustDirect(dustSpawn - base.Projectile.Size * 0.5f, base.Projectile.width, base.Projectile.height, 66, base.Projectile.velocity.X, base.Projectile.velocity.Y, 100, new Color(red, 100, 100));
			dust.noGravity = true;
			dust.position -= base.Projectile.velocity;
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			if (Owner.CantUseHoldout())
			{
				base.Projectile.Kill();
			}
			else
			{
				float velocityScale = 1f;
				if (Owner.HeldItem.shoot == base.Projectile.type)
				{
					velocityScale = Owner.HeldItem.shootSpeed * base.Projectile.scale;
				}
				Vector2 slashDirection = Main.MouseWorld - Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
				slashDirection = slashDirection.SafeNormalize(Vector2.UnitX * (float)Owner.direction) * velocityScale;
				if (slashDirection.X != base.Projectile.velocity.X || slashDirection.Y != base.Projectile.velocity.Y)
				{
					base.Projectile.netUpdate = true;
				}
				base.Projectile.velocity = slashDirection;
				if (Owner.Calamity().mouseRight && Timer >= 450f)
				{
					Vector2 velocity = base.Projectile.velocity;
					center = Owner.Calamity().mouseWorld - Owner.Center;
					Vector2 cycloneVel = velocity * (((Vector2)(ref center)).Length() * 0.00175f);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, cycloneVel, ModContent.ProjectileType<DevilsSunriseCyclone>(), base.Projectile.damage, 0f, base.Projectile.owner);
					SoundEngine.PlaySound(in ThrowSound, Owner.Center);
					base.Projectile.Kill();
				}
			}
		}
		base.Projectile.Center = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (base.Projectile.spriteDirection == -1)
		{
			base.Projectile.rotation += (float)Math.PI;
		}
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.timeLeft = 2;
		Owner.ChangeDir(base.Projectile.direction);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Owner.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		return new Color(red, 100, 100, base.Projectile.alpha);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		float _ = 0f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Main.player[base.Projectile.owner].Center - base.Projectile.velocity, base.Projectile.Center + base.Projectile.velocity * 5.5f, (float)base.Projectile.height * base.Projectile.scale * 1.8f, ref _);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in HitSound, target.Center);
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 180);
		Timer += 15f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		Texture2D value = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition;
		Vector2 mouseDir = Vector2.Normalize(Owner.Calamity().mouseWorld - Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true));
		SpriteEffects sp = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Rectangle frame1 = value.Frame(1, 28, 0, base.Projectile.frame);
		Rectangle frame2 = value.Frame(1, 28, 0, (base.Projectile.frame + 9) % 28);
		Rectangle frame3 = value.Frame(1, 28, 0, (base.Projectile.frame + 18) % 28);
		float randRotOffset = Main.rand.NextFloat(-0.25f, 0.25f);
		Main.EntitySpriteDraw(value, drawPos, frame1, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation + randRotOffset, frame1.Size() / 2f, base.Projectile.scale * 1.15f, sp);
		randRotOffset = Main.rand.NextFloat(-0.25f, 0.25f);
		Main.EntitySpriteDraw(value, drawPos + mouseDir.RotatedBy(randRotOffset) * 15f, frame2, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation + randRotOffset, frame2.Size() / 2f, base.Projectile.scale * 1.75f, sp);
		randRotOffset = Main.rand.NextFloat(-0.25f, 0.25f);
		Main.EntitySpriteDraw(value, drawPos + mouseDir.RotatedBy(randRotOffset) * 22.5f, frame3, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation + randRotOffset, frame3.Size() / 2f, new Vector2(base.Projectile.scale * 2f, base.Projectile.scale * 2.7f), sp);
		return false;
	}
}
