using System;
using System.Collections.Generic;
using CalamityMod.Cooldowns;
using CalamityMod.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class AugerHoldout : ModProjectile, ILocalizedModType, IModType
{
	public float scaleFx = 1f;

	public int time;

	public float bladeRot;

	public int swingCount;

	public float bladefx;

	public bool makeSound = true;

	public bool makeHitbox = true;

	public float pullFx = 1f;

	public bool pressedRight;

	public new string LocalizationCategory => "Projectiles.Misc";

	public ref float attackTimer => ref base.Projectile.ai[0];

	public Player Owner => Main.player[base.Projectile.owner];

	public int fireRate => Owner.itemAnimationMax;

	public bool longSwing => swingCount >= 2;

	public int cooldown => -Owner.itemAnimationMax * (Owner.Calamity().buffedAuger ? 2 : ((!longSwing) ? 1 : 3));

	public int cooldownGiven => (int)(800f / Owner.GetAttackSpeed<MeleeDamageClass>());

	public override bool? CanDamage()
	{
		return false;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 100);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = TrueMeleeDamageClass.Instance;
		base.Projectile.timeLeft = 5;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public void Positioning(Vector2 toMouse)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = (Owner.itemAnimation = 2);
		Owner.itemRotation = (base.Projectile.velocity * (float)Owner.direction).ToRotation();
		Owner.ChangeDir(Math.Sign(toMouse.X));
		Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, toMouse.ToRotation() + bladeRot * (float)Owner.direction + (float)Math.PI / 2f * (float)(-Owner.direction) + ((Owner.direction == -1) ? ((float)Math.PI) : 0f));
		Vector2 handPos = Owner.GetFrontHandPosition(Player.CompositeArmStretchAmount.None, Owner.compositeFrontArm.rotation) + (Owner.compositeFrontArm.rotation + (float)Math.PI / 2f).ToRotationVector2() * 9f;
		float armRotation = (Owner.Center.DirectionTo(handPos).ToRotation() - (float)Math.PI / 2f) * Owner.gravDir + ((Owner.gravDir == -1f) ? ((float)Math.PI) : 0f);
		Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, armRotation);
		base.Projectile.velocity = toMouse.RotatedBy(bladeRot * (float)Owner.direction);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.Center = handPos;
	}

	public void OnSpawn()
	{
		if (time == 0)
		{
			scaleFx = (Owner.Calamity().buffedAuger ? 1.5f : 1f);
			attackTimer = cooldown;
			if (Owner.Calamity().buffedAuger)
			{
				swingCount = 1;
			}
		}
	}

	public override void AI()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		if (!Owner.CantUseHoldout(needsToHold: false))
		{
			base.Projectile.timeLeft = 5;
		}
		Vector2 toMouse = Owner.Center.DirectionTo(Owner.ClampedMouseWorld());
		Positioning(toMouse);
		if (Owner.Calamity().mouseRight)
		{
			pressedRight = true;
		}
		if (base.Projectile.ai[2] > 0f)
		{
			if (base.Projectile.ai[2] == 5f)
			{
				time = 0;
				attackTimer = 0f;
				swingCount = -1;
				makeHitbox = true;
				base.Projectile.ai[2]--;
				pullFx = 5f;
			}
			if (bladefx > 0f)
			{
				bladefx = 0f;
			}
			bladeRot = 0f;
			pullFx = MathHelper.Lerp(pullFx, 1f, 0.12f);
			if (attackTimer > (float)fireRate && makeHitbox)
			{
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), Owner.ClampedMouseWorld(), Vector2.Zero, ModContent.ProjectileType<AugerPull>(), 0, 0f, base.Projectile.owner);
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/AugerPull");
				style.Volume = 0.9f;
				style.Pitch = 0f;
				SoundEngine.PlaySound(in style, Owner.ClampedMouseWorld());
				Owner.Calamity().buffedAuger = true;
				Owner.Calamity().arsenalCooldown = cooldownGiven;
				Owner.AddCooldown(ArsenalPower.ID, cooldownGiven);
				makeHitbox = false;
			}
			if (attackTimer >= (float)(fireRate * 2))
			{
				base.Projectile.Kill();
				return;
			}
			time++;
			attackTimer++;
			return;
		}
		OnSpawn();
		if (scaleFx > 1f && !Owner.Calamity().buffedAuger)
		{
			scaleFx = MathHelper.Lerp(scaleFx, 1f, 0.15f * Owner.GetAttackSpeed<MeleeDamageClass>());
		}
		if (attackTimer < 0f)
		{
			if ((pressedRight || Owner.Calamity().mouseRight) && !Owner.Calamity().buffedAuger && Owner.Calamity().arsenalCooldown <= 0)
			{
				base.Projectile.ai[2] = 5f;
			}
			else
			{
				pressedRight = false;
			}
			if (longSwing && attackTimer == -1f)
			{
				swingCount = 0;
			}
			attackTimer++;
			time++;
			if (longSwing && attackTimer <= (float)cooldown * 0.35f)
			{
				float lerp = Utils.GetLerpValue(cooldown, -1f, attackTimer, clamped: true);
				bladefx = MathHelper.Lerp(bladefx, 0f, lerp);
			}
			else
			{
				if (!Main.mouseLeft && longSwing && !Owner.Calamity().buffedAuger)
				{
					base.Projectile.Kill();
					return;
				}
				float lerp2 = Utils.GetLerpValue(longSwing ? ((float)cooldown * 0.35f) : ((float)cooldown), -1f, attackTimer, clamped: true);
				bladefx = MathHelper.Lerp(bladefx, 1.5f, lerp2);
			}
			if (attackTimer >= (float)cooldown * 0.3f && makeSound && Owner.Calamity().buffedAuger)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/AugerWindup");
				style.Volume = 0.9f;
				style.Pitch = 0f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				makeSound = false;
			}
			if (attackTimer == 0f)
			{
				pressedRight = false;
				swingCount++;
				makeSound = true;
				makeHitbox = true;
			}
			else
			{
				int swingDir = ((swingCount % 2 != 0) ? 1 : (-1));
				float lerp3 = Utils.GetLerpValue(cooldown, -1f, attackTimer, clamped: true);
				bladeRot = MathHelper.Lerp(-(float)Math.PI * (float)swingDir, (float)Math.PI * -3f / 4f * (float)swingDir, CalamityUtils.EaseInOutExp(lerp3, 2f, 2f));
			}
			return;
		}
		if (attackTimer >= (float)fireRate)
		{
			attackTimer = cooldown * ((!Owner.Calamity().buffedAuger) ? 1 : 3);
			if (Owner.Calamity().buffedAuger)
			{
				Owner.Calamity().buffedAuger = false;
			}
		}
		else
		{
			int swingDir2 = ((swingCount % 2 == 0) ? 1 : (-1));
			float lerp4 = Utils.GetLerpValue(0f, fireRate - 1, attackTimer, clamped: true);
			bladeRot = MathHelper.Lerp((float)Math.PI * -3f / 4f * (float)swingDir2, (float)Math.PI * (float)swingDir2, CalamityUtils.EaseInOutExp(lerp4, 5f, 3f));
			if (lerp4 > 0.5f && makeHitbox)
			{
				SoundStyle obj = (Owner.Calamity().buffedAuger ? new SoundStyle("CalamityMod/Sounds/Item/AugerBigSlash") : ((swingCount % 2 != 0) ? new SoundStyle("CalamityMod/Sounds/Item/AugerSlash1") : new SoundStyle("CalamityMod/Sounds/Item/AugerSlash2")));
				SoundEngine.PlaySound(obj with
				{
					Volume = (Owner.Calamity().buffedAuger ? 1f : 0.7f),
					Pitch = -0.1f,
					MaxInstances = 2
				}, base.Projectile.Center);
				int damage = base.Projectile.damage;
				if (Owner.Calamity().buffedAuger)
				{
					Owner.SetScreenshake(5f);
					damage = (int)((float)damage * 2.5f);
				}
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.Center + toMouse * 20f * (float)Math.Pow(scaleFx, 4.0), toMouse * 25f, ModContent.ProjectileType<AugerSlash>(), damage, 0f, base.Projectile.owner, 0f, swingCount, Owner.Calamity().buffedAuger ? 5 : 0);
				makeHitbox = false;
			}
		}
		time++;
		attackTimer++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		if (time < 3)
		{
			return false;
		}
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/AugerHoldout", (AssetRequestMode)2).Value;
		Texture2D glow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/AugerHoldoutGlow", (AssetRequestMode)2).Value;
		Texture2D blade = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowBlade", (AssetRequestMode)2).Value;
		Texture2D bloom = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		base.Projectile.rotation.ToRotationVector2();
		float randSize = Main.rand.NextFloat(0.8f, 1f);
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = base.Projectile.rotation + ((Owner.direction == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = texture.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)(Owner.direction == -1);
		float lerp = Utils.GetLerpValue(0f, fireRate - 1, attackTimer, clamped: true);
		MathHelper.Lerp(0f, 0.1f, CalamityUtils.EaseInOutExp(lerp, 1f, 1f));
		float bladeRotation = drawRotation + (float)Math.PI / 2f * (float)Owner.direction;
		Vector2 bladePlaceAdjust = (bladeRotation - (float)Math.PI / 2f).ToRotationVector2();
		Color color;
		for (int i = 0; i < 2; i++)
		{
			Vector2 position = drawPosition + bladePlaceAdjust * 20f;
			color = ((i == 0) ? ArsenalEffects.ArsenalGaussColor : Color.White);
			((Color)(ref color)).A = 0;
			Main.EntitySpriteDraw(blade, position, null, color, bladeRotation, new Vector2((float)(blade.Width / 2), (float)blade.Height), new Vector2(((i == 0) ? 0.2f : 0.1f) * randSize, 0.2f * bladefx) * base.Projectile.scale * 0.07f * bladefx * scaleFx, (SpriteEffects)0);
		}
		for (int j = 0; j < 2; j++)
		{
			Vector2 position2 = drawPosition + bladePlaceAdjust * 20f;
			color = ((j == 0) ? ArsenalEffects.ArsenalGaussColor : Color.White);
			((Color)(ref color)).A = 0;
			Main.EntitySpriteDraw(bloom, position2, null, color, bladeRotation, bloom.Size() / 2f, (Vector2)((base.Projectile.ai[2] > 0f) ? (new Vector2(randSize, randSize * ((j == 0) ? 1f : 0.9f)) * 0.1f) : new Vector2(0.15f * bladefx * randSize, (j == 0) ? 0.1f : 0.05f)) * base.Projectile.scale * 0.9f * ((base.Projectile.ai[2] > 0f) ? pullFx : bladefx) * scaleFx, (SpriteEffects)0);
		}
		if (scaleFx > 1f)
		{
			for (int b = -2; b <= 2; b++)
			{
				if (b == 0)
				{
					b++;
				}
				for (int k = 0; k < 2; k++)
				{
					Vector2 position3 = drawPosition + bladePlaceAdjust * 20f;
					color = ((k == 0) ? ArsenalEffects.ArsenalGaussColor : Color.White);
					((Color)(ref color)).A = 0;
					Main.EntitySpriteDraw(blade, position3, null, color, bladeRotation + (float)Math.PI * 11f / 80f * (float)b, new Vector2((float)(blade.Width / 2), (float)blade.Height), new Vector2(0.65f * ((k == 0) ? 0.1f : 0.05f), 0.09f * ((k == 0) ? 1f : 0.9f) * (2f - Math.Abs((float)b * 0.5f))) * randSize * base.Projectile.scale * 0.2f * bladefx * (scaleFx - 1f), (SpriteEffects)0);
				}
			}
		}
		Vector2 placeAdjust = (drawRotation + (float)Math.PI / 2f).ToRotationVector2() * 2.5f;
		Main.EntitySpriteDraw(texture, drawPosition + placeAdjust, null, drawColor, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		Main.EntitySpriteDraw(glow, drawPosition + placeAdjust, null, Color.White, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		return false;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		overPlayers.Add(index);
	}
}
