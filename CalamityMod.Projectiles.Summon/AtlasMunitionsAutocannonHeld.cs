using System;
using System.IO;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class AtlasMunitionsAutocannonHeld : ModProjectile, ILocalizedModType, IModType
{
	public bool HasInitialized;

	public float HeatInterpolant;

	public ThanatosSmokeParticleSet SmokeDrawer = new ThanatosSmokeParticleSet(-1, 3, 0f, 16f, 1.5f);

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public bool BeingHeld
	{
		get
		{
			return base.Projectile.ai[0] == 0f;
		}
		set
		{
			base.Projectile.ai[0] = (value ? 0f : 1f);
		}
	}

	public bool IsFiring
	{
		get
		{
			return base.Projectile.ai[1] == 1f;
		}
		set
		{
			base.Projectile.ai[1] = value.ToInt();
		}
	}

	public bool OwnerCanHold
	{
		get
		{
			if (Owner.noItems || Owner.CCed)
			{
				return false;
			}
			if (Main.myPlayer == base.Projectile.owner && Main.mouseRightRelease && Main.mouseRight && HasInitialized)
			{
				return false;
			}
			if (HeatInterpolant >= 1f)
			{
				return false;
			}
			if (Owner.HeldItem.type != ModContent.ItemType<AtlasMunitionsBeacon>())
			{
				return false;
			}
			return true;
		}
	}

	public ref float CannonDroppedTimer => ref base.Projectile.localAI[0];

	public ref float ShootTimer => ref base.Projectile.localAI[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 10;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 56;
		base.Projectile.height = 56;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.netImportant = true;
		base.Projectile.timeLeft = 36000;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.Opacity = 1f;
		base.Projectile.ContinuouslyUpdateDamageStats = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(HeatInterpolant);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		HeatInterpolant = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		Vector2 armPosition = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
		base.Projectile.tileCollide = !BeingHeld;
		DetermineFrames();
		SmokeDrawer.ParticleSpawnRate = ((HeatInterpolant > 0.7f) ? 3 : int.MaxValue);
		SmokeDrawer.BaseMoveRotation = (float)Math.PI / 2f + (float)base.Projectile.spriteDirection * (base.Projectile.position.X - base.Projectile.oldPosition.X) * 0.04f;
		SmokeDrawer.Update();
		if (!BeingHeld)
		{
			HeatInterpolant = MathHelper.Clamp(HeatInterpolant - 1f / 180f, 0f, 1f);
		}
		if (BeingHeld)
		{
			if (!OwnerCanHold)
			{
				BeingHeld = false;
				base.Projectile.velocity = new Vector2((float)base.Projectile.spriteDirection * 3f, -4f);
				base.Projectile.netUpdate = true;
				if (HeatInterpolant >= 1f)
				{
					CombatText.NewText(Owner.Hitbox, Color.OrangeRed, CalamityUtils.GetTextValue("Misc.AutocannonHot"), dramatic: true);
				}
				return;
			}
			CannonDroppedTimer = 0f;
			base.Projectile.Opacity = 1f;
			base.Projectile.gfxOffY = 0f;
			DetermineFiringStatus();
			if (IsFiring)
			{
				ShootProjectiles();
			}
			else
			{
				ShootTimer = 0f;
			}
			UpdateProjectileHeldVariables(armPosition);
			ManipulatePlayerVariables();
			HasInitialized = true;
			return;
		}
		base.Projectile.rotation = base.Projectile.rotation.AngleTowards(0f, 0.3f);
		base.Projectile.velocity.X *= 0.96f;
		base.Projectile.velocity.Y = MathHelper.Clamp(base.Projectile.velocity.Y + 0.25f, -16f, 15.9f);
		base.Projectile.gfxOffY = 10f;
		IsFiring = false;
		CannonDroppedTimer++;
		if (CannonDroppedTimer >= 564f)
		{
			base.Projectile.Opacity = Utils.GetLerpValue(0f, -156f, CannonDroppedTimer - 720f, clamped: true);
			if (base.Projectile.Opacity <= 0f)
			{
				int podID = ModContent.ProjectileType<AtlasMunitionsDropPod>();
				int podUpperID = ModContent.ProjectileType<AtlasMunitionsDropPodUpper>();
				int cannonID = ModContent.ProjectileType<AtlasMunitionsAutocannon>();
				ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Projectile p = enumerator.Current;
					if ((p.type == podID || p.type == podUpperID || p.type == cannonID) && p.owner == base.Projectile.owner)
					{
						p.Kill();
					}
				}
				base.Projectile.Kill();
				return;
			}
		}
		bool rightClick = Main.mouseRight && Main.mouseRightRelease;
		bool correctItem = Main.LocalPlayer.HeldItem.type == ModContent.ItemType<AtlasMunitionsBeacon>();
		if ((Main.LocalPlayer.WithinRange(base.Projectile.Center, 200f) & rightClick & correctItem) && HeatInterpolant < 0.5f)
		{
			BeingHeld = true;
			base.Projectile.owner = Main.myPlayer;
			base.Projectile.netUpdate = true;
		}
		Projectile parent = Main.projectile[(int)base.Projectile.ai[2]];
		if (parent.type != ModContent.ProjectileType<AtlasMunitionsDropPod>() || !parent.active)
		{
			base.Projectile.Kill();
		}
	}

	public void DetermineFrames()
	{
		int minFrame = 4;
		int maxFrame = 5;
		bool framesShouldLoop = true;
		if (IsFiring && (base.Projectile.frame < 4 || base.Projectile.frame > 5))
		{
			if (base.Projectile.frame > 5)
			{
				base.Projectile.frame = 0;
			}
			minFrame = 0;
			maxFrame = 4;
			framesShouldLoop = false;
		}
		else if (!IsFiring)
		{
			minFrame = 6;
			maxFrame = 9;
			framesShouldLoop = false;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frame < minFrame)
		{
			base.Projectile.frame = minFrame;
		}
		if (base.Projectile.frameCounter % 5 == 4)
		{
			base.Projectile.frame++;
		}
		if (base.Projectile.frame > maxFrame)
		{
			base.Projectile.frame = (framesShouldLoop ? minFrame : maxFrame);
		}
	}

	public void DetermineFiringStatus()
	{
		if (Main.myPlayer == base.Projectile.owner)
		{
			bool isFiring = IsFiring;
			IsFiring = Main.mouseLeft && !Owner.mouseInterface;
			if (isFiring != IsFiring)
			{
				base.Projectile.ForceNetUpdate();
			}
		}
	}

	public void ShootProjectiles()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		ShootTimer++;
		if (!(ShootTimer >= 9f))
		{
			return;
		}
		SoundStyle style = CommonCalamitySounds.LaserCannonSound with
		{
			Volume = 0.4f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		if (Main.myPlayer == base.Projectile.owner)
		{
			int laserCount = 3;
			int laserDamage = (int)((float)base.Projectile.damage * 1.18f);
			int laserID = ModContent.ProjectileType<AtlasMunitionsLaserOverdrive>();
			for (int i = 0; i < laserCount; i++)
			{
				Vector2 laserVelocity = base.Projectile.velocity.RotatedByRandom(0.10000000149011612) * 9.25f;
				Vector2 laserSpawnOffset = base.Projectile.velocity * 66f + ((float)Math.PI * 2f * (float)i / (float)laserCount + (float)Math.PI / 2f / (float)laserCount).ToRotationVector2() * 10f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), base.Projectile.Center + laserSpawnOffset, laserVelocity, laserID, laserDamage, 0f, base.Projectile.owner);
			}
			HeatInterpolant = MathHelper.Clamp(HeatInterpolant + 0.01f, 0f, 1f);
			base.Projectile.netUpdate = true;
		}
		ShootTimer = 0f;
	}

	public void UpdateProjectileHeldVariables(Vector2 armPosition)
	{
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			float interpolant = Utils.GetLerpValue(16f, 56f, base.Projectile.Distance(Main.MouseWorld), clamped: true) * Utils.GetLerpValue(3f, 10f, MathHelper.Distance(Main.MouseWorld.X, Owner.Center.X), clamped: true);
			Vector2 oldVelocity = base.Projectile.velocity;
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.SafeDirectionTo(Main.MouseWorld), interpolant).SafeNormalize(Vector2.UnitX * (float)Owner.direction);
			base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt();
			base.Projectile.spriteDirection = base.Projectile.direction;
			if (base.Projectile.velocity != oldVelocity)
			{
				base.Projectile.ForceNetUpdate();
			}
		}
		Vector2 cannonEndOffset = base.Projectile.velocity * 26f + base.Projectile.velocity.RotatedBy((float)Math.PI / 2f * (float)base.Projectile.spriteDirection) * 2f;
		base.Projectile.position = armPosition - base.Projectile.Size * 0.5f + cannonEndOffset;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (base.Projectile.spriteDirection == -1)
		{
			base.Projectile.rotation += (float)Math.PI;
		}
	}

	public void ManipulatePlayerVariables()
	{
		Owner.ChangeDir(base.Projectile.direction);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, (float)base.Projectile.spriteDirection * -0.4f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		SmokeDrawer.DrawSet(base.Projectile.Center - base.Projectile.velocity * 24f);
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AtlasMunitionsAutocannonHeld", (AssetRequestMode)2).Value;
		Texture2D glowmask = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AtlasMunitionsAutocannonHeldGlow", (AssetRequestMode)2).Value;
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY;
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		for (int i = 0; i < 12; i++)
		{
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 12f + Main.GlobalTimeWrappedHourly * 2.3f).ToRotationVector2() * (float)Math.Pow(HeatInterpolant, 2.3) * 6f;
			Main.EntitySpriteDraw(texture, drawPosition + drawOffset, frame, AtlasMunitionsBeacon.HeatGlowColor * base.Projectile.Opacity * 0.5f, base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, direction);
		}
		Main.EntitySpriteDraw(texture, drawPosition, frame, Color.Lerp(lightColor, AtlasMunitionsBeacon.HeatGlowColor, HeatInterpolant * 0.45f) * base.Projectile.Opacity, base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, direction);
		Main.EntitySpriteDraw(glowmask, drawPosition, frame, Color.White * base.Projectile.Opacity, base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, direction);
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
