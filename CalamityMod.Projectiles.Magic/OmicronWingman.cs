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

public class OmicronWingman : ModProjectile
{
	public Color StaticEffectsColor;

	private float FiringTime;

	private float PostFireCooldown;

	public bool MovingUp;

	public float xOffset;

	public float yOffset;

	public int time;

	public int firingDelay;

	public int launchDelay;

	private Player Owner;

	private float MaxOffsetLength;

	public bool recharging;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Wingman>();

	public override string Texture => "CalamityMod/Items/Weapons/Magic/Wingman";

	private ref float ShootingTimer => ref base.Projectile.ai[0];

	private ref float OffsetLength => ref base.Projectile.localAI[0];

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
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		if (Owner == null)
		{
			Owner = Main.player[base.Projectile.owner];
		}
		if (time > 1 && Owner.ownedProjectileCounts[ModContent.ProjectileType<OmicronHoldout>()] < 1 && PostFireCooldown <= 0f)
		{
			base.Projectile.Kill();
		}
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref StaticEffectsColor)).ToVector3() * 0.2f);
		if (time == 0)
		{
			MovingUp = base.Projectile.ai[2] == 1f;
		}
		firingDelay--;
		Item heldItem = Owner.HeldItem;
		base.Projectile.damage = ((heldItem != null) ? Owner.GetWeaponDamage(heldItem) : 0);
		if (((PostFireCooldown == 0f && launchDelay == 0 && Owner.CantUseHoldout()) || heldItem.type != ModContent.ItemType<Omicron>()) && PostFireCooldown <= 0f)
		{
			base.Projectile.Kill();
		}
		if (PostFireCooldown > 0f)
		{
			PostFiringCooldown();
		}
		if (launchDelay > 0 || (PostFireCooldown <= 0f && (Owner.Calamity().mouseRight || (firingDelay <= 0 && base.Projectile.ai[2] == 1f) || base.Projectile.ai[2] == -1f)))
		{
			if (launchDelay > 0 || Owner.Calamity().mouseRight)
			{
				if (launchDelay < 50)
				{
					launchDelay++;
				}
				if (launchDelay >= 50 && Owner.CheckMana(Owner.HeldItem, (int)((float)heldItem.mana * Owner.manaCost) * 2, pay: true))
				{
					Shoot(isGrenade: true);
					PostFireCooldown = 50f;
					ShootingTimer = 0f;
					launchDelay = 0;
				}
			}
			else if (ShootingTimer >= FiringTime)
			{
				if (Owner.CheckMana(Owner.HeldItem))
				{
					Shoot(isGrenade: false);
					ShootingTimer = 0f;
				}
				else if (PostFireCooldown <= 0f)
				{
					base.Projectile.Kill();
				}
			}
		}
		Vector2 ownerPosition = Owner.MountedCenter;
		Vector2 ownerToMouse = Owner.Calamity().mouseWorld - ownerPosition;
		ManagePlayerProjectileMembers(ownerToMouse);
		if (OffsetLength != MaxOffsetLength)
		{
			OffsetLength = MathHelper.Lerp(OffsetLength, MaxOffsetLength, 0.1f);
		}
		ShootingTimer++;
		time++;
		base.Projectile.soundDelay--;
		base.Projectile.ForceNetUpdate();
	}

	private void ManagePlayerProjectileMembers(Vector2 ownerToMouse)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = base.Projectile.rotation.ToRotationVector2();
		float velocityRotation = base.Projectile.velocity.ToRotation();
		int direction = MathF.Sign(ownerToMouse.X);
		Vector2 lengthOffset = val * OffsetLength;
		if (time % 30 == 0)
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
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		Vector2 shootDirection = (Owner.Calamity().mouseWorld - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
		Vector2 tipPosition = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(-0.05f * (float)base.Projectile.direction) * 12f;
		Vector2 firingVelocity = shootDirection * 10f;
		if (isGrenade)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DeadSunExplosion");
			style.Volume = 0.2f;
			style.Pitch = -0.4f;
			style.PitchVariance = 0.2f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), tipPosition, firingVelocity, ModContent.ProjectileType<WingmanGrenade>(), base.Projectile.damage * 14, base.Projectile.knockBack * 5f, base.Projectile.owner, 0f, 2f).timeLeft = 530;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), tipPosition, firingVelocity * 1.2f, ModContent.ProjectileType<WingmanGrenade>(), base.Projectile.damage * 14, base.Projectile.knockBack * 5f, base.Projectile.owner, 0f, 2f);
			}
		}
		else
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MagnaCannonShot");
			style.Volume = 0.25f;
			style.Pitch = 1f;
			style.PitchVariance = 0.35f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), tipPosition, firingVelocity, ModContent.ProjectileType<WingmanShot>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 2f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), tipPosition, firingVelocity.RotatedBy(-0.05) * 0.85f, ModContent.ProjectileType<WingmanShot>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 2f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), tipPosition, firingVelocity.RotatedBy(0.05) * 0.85f, ModContent.ProjectileType<WingmanShot>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 2f);
			}
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
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
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
		firingDelay = 15;
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
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		if (time <= 0)
		{
			return false;
		}
		Texture2D texture = ((base.Projectile.ai[2] != 1f) ? ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/OmicronWingmanAlt", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/OmicronWingman", (AssetRequestMode)2).Value);
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
		writer.Write(time);
		writer.Write(firingDelay);
		writer.Write(launchDelay);
		writer.Write(MovingUp);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.rotation = reader.ReadSingle();
		base.Projectile.spriteDirection = reader.ReadInt32();
		OffsetLength = reader.ReadSingle();
		time = reader.ReadInt32();
		firingDelay = reader.ReadInt32();
		launchDelay = reader.ReadInt32();
		MovingUp = reader.ReadBoolean();
	}

	public OmicronWingman()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		StaticEffectsColor = Color.MediumVioletRed;
		FiringTime = 10f;
		MovingUp = true;
		xOffset = 1f;
		firingDelay = 15;
		MaxOffsetLength = 5f;
		base._002Ector();
	}
}
