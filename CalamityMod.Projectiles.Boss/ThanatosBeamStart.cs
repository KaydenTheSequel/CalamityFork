using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Events;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Sounds;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class ThanatosBeamStart : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public int OwnerIndex
	{
		get
		{
			return (int)base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = value;
		}
	}

	public bool OwnerIsValid
	{
		get
		{
			if (Main.npc[OwnerIndex].active)
			{
				return Main.npc[OwnerIndex].type == ModContent.NPCType<ThanatosHead>();
			}
			return false;
		}
	}

	public override float MaxScale => 1f;

	public override float MaxLaserLength => 3600f;

	public override float Lifetime => 180f;

	public override Color LaserOverlayColor
	{
		get
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			return new Color(250, 250, 250, 100);
		}
	}

	public override Color LightCastColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.White;
		}
	}

	public override Texture2D LaserBeginTexture => TextureAssets.Projectile[base.Type].Value;

	public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/ThanatosBeamMiddle", (AssetRequestMode)1).Value;

	public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/ThanatosBeamEnd", (AssetRequestMode)1).Value;

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.hostile = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.CooldownSlot = 1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public override void AttachToSomething()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		if (OwnerIsValid)
		{
			Vector2 fireFrom = Main.npc[OwnerIndex].Center + Vector2.UnitY * Main.npc[OwnerIndex].gfxOffY;
			fireFrom += base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * base.Projectile.scale * 174f;
			base.Projectile.Center = fireFrom;
			if (Main.npc[OwnerIndex].Calamity().newAI[0] != 2f)
			{
				base.Projectile.Kill();
			}
		}
		else if (Main.netMode != 1)
		{
			base.Projectile.Kill();
		}
	}

	public override void UpdateLaserMotion()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = Main.npc[OwnerIndex].velocity.SafeNormalize(Vector2.UnitY);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
	}

	public override void PostAI()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		if (!OwnerIsValid)
		{
			return;
		}
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		int dustType = 235;
		Vector2 dustCreationPosition = base.Projectile.Center + base.Projectile.velocity * (base.LaserLength - 14f);
		for (int i = 0; i < 2; i++)
		{
			Vector2 dustVelocity = (base.Projectile.velocity.ToRotation() + (float)Main.rand.NextBool().ToDirectionInt() * ((float)Math.PI / 2f)).ToRotationVector2() * Main.rand.NextFloat(2f, 4f);
			Dust dust = Dust.NewDustDirect(dustCreationPosition, 0, 0, dustType, dustVelocity.X, dustVelocity.Y);
			dust.noGravity = true;
			dust.scale = 1.7f;
		}
		if (Main.rand.NextBool(5))
		{
			Vector2 dustSpawnOffset = base.Projectile.velocity.RotatedBy(1.5707963705062866) * Main.rand.NextFloatDirection() * (float)base.Projectile.width * 0.5f;
			Dust redFlame = Dust.NewDustDirect(dustCreationPosition + dustSpawnOffset - Vector2.One * 4f, 8, 8, dustType, 0f, 0f, 100, default(Color), 1.5f);
			redFlame.velocity *= 0.5f;
			redFlame.velocity.Y = 0f - Math.Abs(redFlame.velocity.Y);
		}
		float laserFireRate = (Main.getGoodWorld ? 60f : (expertMode ? 80f : 160f));
		if (Main.npc[OwnerIndex].Calamity().newAI[2] % laserFireRate == 0f)
		{
			SoundEngine.PlaySound(in CommonCalamitySounds.LaserCannonSound, Main.npc[OwnerIndex].Center);
			if (base.Projectile.owner == Main.myPlayer)
			{
				Vector2 beamDirection = base.Projectile.velocity.SafeNormalize(Vector2.UnitY);
				float distanceBetweenProjectiles = (death ? 256f : (revenge ? 288f : 320f));
				Vector2 laserFirePosition = Main.npc[OwnerIndex].Center + beamDirection * distanceBetweenProjectiles;
				int laserCount = (int)(base.LaserLength / distanceBetweenProjectiles);
				int type = ModContent.ProjectileType<THanosSideLaser>();
				for (int j = 0; j < laserCount; j++)
				{
					int totalProjectiles = 2;
					float radians = (float)Math.PI * 2f / (float)totalProjectiles;
					for (int k = 0; k < totalProjectiles; k++)
					{
						Vector2 projVelocity = (Vector2)(Main.getGoodWorld ? new Vector2((float)Main.rand.Next(-12, 12), (float)Main.rand.Next(-12, 12)) : (base.Projectile.velocity.RotatedBy(radians * (float)k + (float)Math.PI / 2f) * 12f));
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), laserFirePosition, projVelocity, type, ThanatosHead.LaserDamage, 0f, Main.myPlayer, 0f, -1f);
					}
					laserFirePosition += beamDirection * distanceBetweenProjectiles;
				}
			}
		}
		base.Projectile.frameCounter++;
		if ((float)base.Projectile.frameCounter % 5f == 0f)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		if (!OwnerIsValid)
		{
			return false;
		}
		if (base.Projectile.velocity == Vector2.Zero || base.Projectile.localAI[0] < 2f)
		{
			return false;
		}
		Color beamColor = LaserOverlayColor;
		Rectangle startFrameArea = LaserBeginTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Rectangle middleFrameArea = LaserMiddleTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Rectangle endFrameArea = LaserEndTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(LaserBeginTexture, base.Projectile.Center - Main.screenPosition, startFrameArea, beamColor, base.Projectile.rotation, LaserBeginTexture.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		float laserBodyLength = base.LaserLength + (float)middleFrameArea.Height;
		Vector2 centerOnLaser = base.Projectile.Center + base.Projectile.velocity * -5f;
		if (laserBodyLength > 0f)
		{
			float laserOffset = (float)middleFrameArea.Height * base.Projectile.scale;
			float incrementalBodyLength = 0f;
			while (incrementalBodyLength + 1f < laserBodyLength)
			{
				Main.EntitySpriteDraw(LaserMiddleTexture, centerOnLaser - Main.screenPosition, middleFrameArea, beamColor, base.Projectile.rotation, LaserMiddleTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
				incrementalBodyLength += laserOffset;
				centerOnLaser += base.Projectile.velocity * laserOffset;
				middleFrameArea.Y += LaserMiddleTexture.Height / Main.projFrames[base.Type];
				if (middleFrameArea.Y + middleFrameArea.Height > LaserMiddleTexture.Height)
				{
					middleFrameArea.Y = 0;
				}
			}
		}
		Vector2 laserEndCenter = centerOnLaser - Main.screenPosition;
		Main.EntitySpriteDraw(LaserEndTexture, laserEndCenter, endFrameArea, beamColor, base.Projectile.rotation, LaserEndTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
		}
	}

	public override bool CanHitPlayer(Player target)
	{
		if (OwnerIsValid)
		{
			return base.Projectile.scale >= 0.5f;
		}
		return false;
	}
}
