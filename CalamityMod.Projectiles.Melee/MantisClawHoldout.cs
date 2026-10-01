using System;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class MantisClawHoldout : BaseCustomUseStyleProjectile, ILocalizedModType, IModType
{
	private bool AnimationCooldown;

	private float SlashTimer;

	public float ClawOpenness = MathHelper.ToRadians(80f);

	public float BubbleSize;

	public int m2KillTimer;

	private float JetDamageMultiplier => 6.5f;

	private int SlashSpeed => 6;

	private int BlastChargeUses => 3;

	public override Vector2 SpriteOrigin
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(0f, 20f);
		}
	}

	public override int AssignedItemID => ModContent.ItemType<MantisClaws>();

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/MantisClaws";

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.DamageType = DamageClass.Melee;
	}

	public override void ResetStyle()
	{
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		if (!AnimationCooldown)
		{
			return;
		}
		if (NumberOfAnimations % 5 <= BlastChargeUses)
		{
			ClawOpenness = MathHelper.Lerp(ClawOpenness, MathHelper.ToRadians(80f), 0.15f);
			BubbleSize *= 0.85f;
			if (BubbleSize < 0.05f)
			{
				if (base.Projectile.scale > 0f && m2KillTimer <= 10)
				{
					base.Projectile.scale -= 0.1f;
				}
				if (base.Projectile.scale < 0.1f && m2KillTimer <= 0)
				{
					base.Projectile.active = false;
				}
				else
				{
					Owner.altFunctionUse = 2;
					m2KillTimer--;
				}
			}
		}
		else
		{
			Clamp();
		}
		Offset = Utils.RotatedBy(new Vector2(MathHelper.Lerp(0f, 12f, BubbleSize / 2f), 0f), (double)base.Projectile.rotation, default(Vector2));
	}

	public void Clamp()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		if (BubbleSize != 0f)
		{
			Owner.Calamity().mouseWorldListener = true;
			m2KillTimer = 20;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.Center + Utils.RotatedBy(new Vector2(20f, 0f), (double)base.Projectile.rotation, default(Vector2)), Owner.DirectionTo(Owner.Calamity().mouseWorld) * 30f, ModContent.ProjectileType<MantisClawJet>(), (int)((float)base.Projectile.damage * JetDamageMultiplier), 7f, Owner.whoAmI, 0f, 40f);
			for (int i = 0; i < 9; i++)
			{
				GeneralParticleHandler.SpawnParticle(new WaterFlavoredParticle(Owner.Center, Utils.RotatedBy(new Vector2(Main.rand.NextFloat(15f, 25f), 0f), (double)(base.Projectile.rotation + MathHelper.ToRadians(Main.rand.NextFloat(-45f, 45f))), default(Vector2)), affectedByGravity: true, 40, Main.rand.NextFloat(0.5f, 1.2f), MantisClawJet.WaterColor)
				{
					AffectedByLight = true
				});
			}
			SoundEngine.PlaySound((Main.rand.NextBool(2) ? SoundID.Item85 : SoundID.Item86).WithPitchOffset(-0.5f), Owner.Center);
			SoundEngine.PlaySound(SoundID.NPCDeath14.WithPitchOffset(1f), Owner.Center);
			Owner.SetScreenshake(3f);
			GeneralParticleHandler.SpawnParticle(new MantisPunch(Owner.Center + Utils.RotatedBy(new Vector2(26f, 0f), (double)base.Projectile.rotation, default(Vector2)), base.Projectile.rotation));
		}
		ClawOpenness = MathHelper.ToRadians(80f);
		BubbleSize = 0f;
	}

	public override void UseStyle()
	{
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.altFunctionUse != 2)
		{
			if (AnimationCooldown && !Owner.Calamity().mouseRight)
			{
				if (Owner.itemAnimation > 0)
				{
					base.Projectile.Kill();
				}
				DrawUnconditionally = true;
				AnimationCooldown = true;
				Owner.itemAnimation = 0;
				Owner.itemTime = 0;
			}
			base.Projectile.scale = 1f;
			DrawUnconditionally = false;
			Owner.Calamity().mouseWorldListener = true;
			if (SlashTimer % (float)(int)((float)SlashSpeed / Owner.GetAttackSpeed<MeleeDamageClass>()) == 0f)
			{
				SoundStyle SlashStyle = new SoundStyle("CalamityMod/Sounds/Item/MantisSwipe", 2);
				SlashStyle.PitchVariance = 0.3f;
				SlashStyle.Volume = 0.7f;
				SoundEngine.PlaySound(SlashStyle.WithPitchOffset(0.2f), Owner.Center);
				Owner.Calamity().mouseWorldListener = true;
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), Owner.Center, Owner.DirectionTo(Owner.Calamity().mouseWorld) * 6f, ModContent.ProjectileType<MantisClawSlash>(), base.Projectile.damage, 2f, Owner.whoAmI).rotation = Owner.AngleTo(Owner.Calamity().mouseWorld) + MathHelper.ToRadians(Main.rand.NextFloat(-25f, 25f));
			}
			SlashTimer++;
			Owner.direction = Math.Sign(Owner.Calamity().mouseWorld.X - Owner.Center.X);
			base.Projectile.rotation = Owner.AngleTo(Owner.Calamity().mouseWorld);
			base.Projectile.ai[2] = MathHelper.Lerp(base.Projectile.ai[2], MathHelper.ToRadians(135f * (float)((SlashTimer % (float)(SlashSpeed * 2) < (float)SlashSpeed) ? 1 : (-1))), 0.2f);
			ArmRotationOffset = MathHelper.ToRadians(-90f) - base.Projectile.ai[2] * (float)Owner.direction;
			ArmRotationOffsetBack = MathHelper.ToRadians(-90f) + base.Projectile.ai[2] * (float)Owner.direction;
			return;
		}
		base.Projectile.scale = MathHelper.Lerp(base.Projectile.scale, 1f, 0.15f);
		base.Projectile.scale = MathHelper.Clamp(base.Projectile.scale, 0f, 1f);
		if (Owner.Calamity().mouseRight)
		{
			Owner.itemAnimation = 3;
			float chargeFactor = (float)NumberOfAnimations % 5f / 3f;
			DrawUnconditionally = true;
			ArmRotationOffset = MathHelper.ToRadians(-90f) - ClawOpenness / 5f * (float)Owner.direction;
			ArmRotationOffsetBack = MathHelper.ToRadians(-90f) + ClawOpenness / 5f * (float)Owner.direction;
			if (NumberOfAnimations % 5 > BlastChargeUses)
			{
				Clamp();
			}
			else
			{
				Owner.Calamity().mouseWorldListener = true;
				Owner.direction = Math.Sign(Owner.Calamity().mouseWorld.X - Owner.Center.X);
				base.Projectile.rotation = Owner.AngleTo(Owner.Calamity().mouseWorld);
				base.Projectile.ai[0]++;
				if (AnimationProgress == 1f)
				{
					if (NumberOfAnimations % 5 == 0)
					{
						ClawOpenness = MathHelper.ToRadians(80f);
						BubbleSize = 0f;
					}
					SoundEngine.PlaySound((Main.rand.NextBool(2) ? SoundID.Item85 : SoundID.Item86).WithPitchOffset((float)NumberOfAnimations % 5f / (float)BlastChargeUses), Owner.Center);
					base.Projectile.ai[1] = MathHelper.Lerp(0.5f, 2f, chargeFactor);
					base.Projectile.ai[2] = MathHelper.Lerp(MathHelper.ToRadians(100f), MathHelper.ToRadians(180f), chargeFactor);
				}
				BubbleSize = MathHelper.Lerp(BubbleSize, base.Projectile.ai[1], 0.2f);
				ClawOpenness = MathHelper.Lerp(ClawOpenness, base.Projectile.ai[2], 0.2f);
			}
			Offset = Utils.RotatedBy(new Vector2(MathHelper.Lerp(0f, 12f, BubbleSize / 2f), 0f), (double)base.Projectile.rotation, default(Vector2));
		}
		else
		{
			if (Owner.controlUseItem)
			{
				base.Projectile.Kill();
			}
			DrawUnconditionally = true;
			AnimationCooldown = true;
			Owner.itemAnimation = 0;
			Owner.itemTime = 0;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		FlipAsSword = false;
		float rot = 0f;
		if (Owner.altFunctionUse != 2 && !AnimationCooldown)
		{
			rot = base.Projectile.ai[2];
			if (Owner.direction == -1)
			{
				FlipAsSword = true;
				RotationOffset = MathHelper.ToRadians(-90f);
				Owner.compositeFrontArm.rotation -= RotationOffset;
				Owner.compositeBackArm.rotation -= RotationOffset;
			}
		}
		else
		{
			RotationOffset = 0f;
		}
		Asset<Texture2D> Bubble = ModContent.Request<Texture2D>("CalamityMod/Particles/Bubble", (AssetRequestMode)2);
		Main.EntitySpriteDraw(Bubble.Value, base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY) + Utils.RotatedBy(new Vector2(20f, 0f), (double)(base.Projectile.rotation + RotationOffset), default(Vector2)), Bubble.Frame(), lightColor, 0f, Bubble.Size() / 2f, BubbleSize, (SpriteEffects)0);
		if (Owner.itemAnimation > 0 || DrawUnconditionally)
		{
			Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
			float r = (FlipAsSword ? 0f : MathHelper.ToRadians(90f));
			Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY), tex.Frame(1, FrameCount, 0, Frame), lightColor, base.Projectile.rotation - ClawOpenness + rot + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)tex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects == 0) ? (FlipAsSword ? 1 : 0) : ((int)spriteEffects)));
		}
		if (Owner.itemAnimation > 0 || DrawUnconditionally)
		{
			if (Owner.altFunctionUse == 2 || AnimationCooldown)
			{
				Asset<Texture2D> tex2 = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
				float r2 = (FlipAsSword ? 0f : MathHelper.ToRadians(90f));
				Main.EntitySpriteDraw(tex2.Value, base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY), tex2.Frame(1, FrameCount, 0, Frame), lightColor, base.Projectile.rotation + ClawOpenness + RotationOffset + r2, (Vector2)((!FlipAsSword) ? new Vector2((float)tex2.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects == 0) ? ((!FlipAsSword) ? 1 : 0) : ((int)spriteEffects)));
			}
			else
			{
				Asset<Texture2D> tex3 = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
				float r3 = (FlipAsSword ? 0f : MathHelper.ToRadians(90f));
				Main.EntitySpriteDraw(tex3.Value, base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY), tex3.Frame(1, FrameCount, 0, Frame), lightColor, base.Projectile.rotation - ClawOpenness - rot + RotationOffset + r3, (Vector2)(FlipAsSword ? new Vector2((float)tex3.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects == 0) ? (FlipAsSword ? 1 : 0) : ((int)spriteEffects)));
			}
		}
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void WhenSpawned()
	{
		base.Projectile.scale = 0f;
	}
}
