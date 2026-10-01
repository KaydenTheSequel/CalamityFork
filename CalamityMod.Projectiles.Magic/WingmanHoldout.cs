using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class WingmanHoldout : ModProjectile
{
	public Color StaticEffectsColor;

	public float yOffset;

	private float OffsetLength;

	private float FiringTime;

	public int time;

	public int firingDelay;

	public bool MovingUp;

	private Player Owner;

	private static readonly float MaxOffsetLength = 5f;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Wingman>();

	public override string Texture => "CalamityMod/Items/Weapons/Magic/Wingman";

	private ref float ShootingTimer => ref base.Projectile.ai[0];

	private ref float PostFireCooldown => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 9;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 142);
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.netImportant = true;
		base.Projectile.hide = true;
	}

	public override void AI()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		if (Owner == null)
		{
			Owner = Main.player[base.Projectile.owner];
		}
		if (base.Projectile.ai[2] == 1f)
		{
			StaticEffectsColor = Color.Turquoise;
		}
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref StaticEffectsColor)).ToVector3() * 0.2f);
		if (time == 0)
		{
			MovingUp = base.Projectile.ai[2] == 1f;
		}
		firingDelay--;
		Item heldItem = Owner.HeldItem;
		base.Projectile.damage = ((heldItem != null) ? Owner.GetWeaponDamage(heldItem) : 0);
		if (PostFireCooldown > 0f)
		{
			PostFiringCooldown();
		}
		_ = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(-0.05f * (float)base.Projectile.direction) * 12f;
		if (Owner.CantUseHoldout() || heldItem.type != ModContent.ItemType<Wingman>())
		{
			if (PostFireCooldown <= 0f)
			{
				base.Projectile.Kill();
			}
		}
		else if (PostFireCooldown <= 0f && (Owner.Calamity().mouseRight || (firingDelay <= 0 && base.Projectile.ai[2] == 1f) || base.Projectile.ai[2] == -1f))
		{
			if (Owner.Calamity().mouseRight)
			{
				if (Owner.CheckMana(Owner.HeldItem, (int)((float)heldItem.mana * Owner.manaCost) * 5, pay: true))
				{
					Shoot(isGrenade: true);
					PostFireCooldown = 35f + 55f * Utils.GetLerpValue(10f, 40f, FiringTime, clamped: true);
					ShootingTimer = 0f;
					FiringTime = 40f;
				}
				else
				{
					if (base.Projectile.soundDelay <= 0)
					{
						SoundStyle style = SoundID.MaxMana with
						{
							Pitch = -0.5f
						};
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						base.Projectile.soundDelay = 50;
					}
					ShootingTimer = 0f;
				}
			}
			else if (ShootingTimer >= FiringTime)
			{
				if (Owner.CheckMana(Owner.HeldItem, (int)((float)heldItem.mana * Owner.manaCost), pay: true))
				{
					Shoot(isGrenade: false);
					ShootingTimer = 0f;
					if (FiringTime > 10f)
					{
						FiringTime -= 5f;
					}
					else
					{
						FiringTime = 10f;
					}
				}
				else
				{
					if (base.Projectile.soundDelay <= 0)
					{
						SoundStyle style = SoundID.MaxMana with
						{
							Pitch = -0.5f
						};
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						base.Projectile.soundDelay = 50;
					}
					ShootingTimer = 0f;
				}
			}
		}
		Vector2 ownerPosition = Owner.MountedCenter;
		Vector2 ownerToMouse = Owner.Calamity().mouseWorld - ownerPosition;
		ManageHoldout(ownerToMouse);
		if (OffsetLength != MaxOffsetLength)
		{
			OffsetLength = MathHelper.Lerp(OffsetLength, MaxOffsetLength, 0.1f);
		}
		ShootingTimer++;
		time++;
		base.Projectile.soundDelay--;
		base.Projectile.ForceNetUpdate();
	}

	private void ManageHoldout(Vector2 ownerToMouse)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = base.Projectile.rotation.ToRotationVector2();
		float velocityRotation = base.Projectile.velocity.ToRotation();
		int direction = MathF.Sign(ownerToMouse.X);
		Vector2 lengthOffset = val * OffsetLength;
		if (time % 40 == 0)
		{
			MovingUp = !MovingUp;
		}
		yOffset = MathHelper.Lerp(yOffset, (float)(150 * ((!MovingUp) ? 1 : (-1))), 0.085f - FiringTime * 0.0012f);
		Vector2 placementOffset = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * yOffset;
		Vector2 location = Owner.MountedCenter + placementOffset;
		base.Projectile.Center = lengthOffset + location;
		base.Projectile.velocity = velocityRotation.AngleTowards(ownerToMouse.ToRotation(), 0.2f).ToRotationVector2();
		base.Projectile.rotation = (Owner.Calamity().mouseWorld - base.Projectile.Center).SafeNormalize(Vector2.UnitX).ToRotation();
		base.Projectile.timeLeft = 2;
		base.Projectile.spriteDirection = (base.Projectile.direction = direction);
		Owner.ChangeDir(direction);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = (Owner.itemAnimation = 2);
		Owner.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
		float armRotation = base.Projectile.rotation - (float)Math.PI / 2f;
		if (base.Projectile.ai[2] == 1f)
		{
			Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, armRotation);
		}
		else
		{
			Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, armRotation);
		}
	}

	private void Shoot(bool isGrenade)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		Vector2 shootDirection = (Owner.Calamity().mouseWorld - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
		Vector2 tipPosition = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(-0.05f * (float)base.Projectile.direction) * 12f;
		Vector2 firingVelocity = shootDirection * 10f;
		if (isGrenade)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DeadSunExplosion");
			style.Volume = 0.35f;
			style.Pitch = -0.4f;
			style.PitchVariance = 0.2f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), tipPosition, firingVelocity * 1.3f * Utils.GetLerpValue(60f, 10f, FiringTime, clamped: true), ModContent.ProjectileType<WingmanGrenade>(), base.Projectile.damage * 6, base.Projectile.knockBack * 5f, base.Projectile.owner, 0f, (base.Projectile.ai[2] == 1f) ? (-1) : 0);
		}
		else
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MagnaCannonShot");
			style.Volume = 0.25f;
			style.Pitch = 1f;
			style.PitchVariance = 0.35f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), tipPosition, firingVelocity * Utils.GetLerpValue(80f, 10f, FiringTime, clamped: true), ModContent.ProjectileType<WingmanShot>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, (base.Projectile.ai[2] == 1f) ? (-1) : 0);
		}
		if (!Main.dedServ)
		{
			for (int k = 0; k < 6; k++)
			{
				Vector2 shootVel = (shootDirection * 10f).RotatedByRandom(0.5) * Main.rand.NextFloat(0.1f, 1.8f);
				Dust dust = Dust.NewDustPerfect(tipPosition, Main.rand.NextBool(4) ? 264 : 66, shootVel);
				dust.scale = Main.rand.NextFloat(1.15f, 1.45f);
				dust.noGravity = true;
				dust.color = (Main.rand.NextBool() ? Color.Lerp(StaticEffectsColor, Color.White, 0.5f) : StaticEffectsColor);
			}
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(tipPosition - shootDirection * 14f, shootDirection * 20f, affectedByGravity: false, Main.rand.Next(7, 12), 0.035f, StaticEffectsColor, new Vector2(1.5f, 0.9f), quickShrink: true));
			if (isGrenade)
			{
				OffsetLength -= 27f;
			}
			else
			{
				OffsetLength -= 5f;
			}
		}
	}

	private void PostFiringCooldown()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		Owner.channel = true;
		Vector2 tipPosition = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(-0.05f * (float)base.Projectile.direction) * 12f;
		if (PostFireCooldown > 0f && Main.rand.NextBool())
		{
			Vector2 smokeVel = new Vector2(0f, -8f) * Main.rand.NextFloat(0.1f, 1.1f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(tipPosition, smokeVel, StaticEffectsColor, Main.rand.Next(30, 51), Main.rand.NextFloat(0.1f, 0.4f), 0.5f, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextBool(), 0f, required: true));
			Dust dust = Dust.NewDustPerfect(tipPosition, 303, smokeVel.RotatedByRandom(0.10000000149011612), 80, default(Color), Main.rand.NextFloat(0.2f, 0.8f));
			dust.noGravity = false;
			dust.color = StaticEffectsColor;
		}
		ShootingTimer = 0f;
		firingDelay = 45;
		PostFireCooldown--;
	}

	public override void OnSpawn(IEntitySource source)
	{
		OffsetLength = MaxOffsetLength;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		if (time <= 0 && !Main.dedServ)
		{
			return false;
		}
		Texture2D texture = ((base.Projectile.ai[2] != 1f) ? ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/WingmanAlt", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/Wingman", (AssetRequestMode)2).Value);
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = texture.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		if ((base.Projectile.spriteDirection == -1) ? MovingUp : (!MovingUp))
		{
			flipSprite = (SpriteEffects)(flipSprite | 2);
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], Color.Lerp(StaticEffectsColor, Color.White, 0.5f) * 0.2f, 1, texture);
		Main.EntitySpriteDraw(texture, drawPosition, null, drawColor, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		return false;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindNPCs.Add(index);
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.rotation);
		writer.Write(base.Projectile.spriteDirection);
		writer.Write(OffsetLength);
		writer.Write(FiringTime);
		writer.Write(time);
		writer.Write(firingDelay);
		writer.Write(MovingUp);
		writer.Write(yOffset);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.rotation = reader.ReadSingle();
		base.Projectile.spriteDirection = reader.ReadInt32();
		OffsetLength = reader.ReadSingle();
		FiringTime = reader.ReadSingle();
		time = reader.ReadInt32();
		firingDelay = reader.ReadInt32();
		MovingUp = reader.ReadBoolean();
		yOffset = reader.ReadSingle();
	}

	public WingmanHoldout()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		StaticEffectsColor = Color.Orchid;
		FiringTime = 40f;
		firingDelay = 45;
		MovingUp = true;
		base._002Ector();
	}
}
