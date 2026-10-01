using System;
using System.IO;
using System.Linq;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Events;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.Other;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class AresDeathBeamStart : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

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

	public override float MaxScale => 1f;

	public override float MaxLaserLength => 2400f;

	public override float Lifetime => 600f;

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

	public override Texture2D LaserBeginTexture => ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/AresDeathBeamStart", (AssetRequestMode)1).Value;

	public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/AresDeathBeamMiddle", (AssetRequestMode)1).Value;

	public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/AresDeathBeamEnd", (AssetRequestMode)1).Value;

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.hostile = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
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
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		if (Main.npc[OwnerIndex].active && (Main.npc[OwnerIndex].type == ModContent.NPCType<AresBody>() || Main.npc[OwnerIndex].type == ModContent.NPCType<THELORDE>()))
		{
			Vector2 fireFrom = default(Vector2);
			((Vector2)(ref fireFrom))._002Ector(Main.npc[OwnerIndex].Center.X - 1f, Main.npc[OwnerIndex].Center.Y + 23f);
			fireFrom += base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * MathHelper.Lerp(35f, 127f, base.Projectile.scale * base.Projectile.scale);
			base.Projectile.Center = fireFrom;
			if (Main.npc[OwnerIndex].Calamity().newAI[0] != 1f && Main.npc[OwnerIndex].type != ModContent.NPCType<THELORDE>())
			{
				base.Projectile.Kill();
				return;
			}
			bool num = CalamityWorld.death || BossRushEvent.BossRushActive;
			bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
			bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
			float deathrayTelegraphDuration = (num ? 90f : (revenge ? 105f : (expertMode ? 120f : 150f)));
			base.Time = Main.npc[OwnerIndex].Calamity().newAI[2] - deathrayTelegraphDuration;
		}
		else
		{
			base.Projectile.Kill();
		}
	}

	public override void UpdateLaserMotion()
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		bool num = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		float angularSlowdownDivisor = (num ? 320f : (revenge ? 330f : (expertMode ? 340f : 360f)));
		float angularVelocity = (float)Math.PI * 2f * base.Time / Lifetime / angularSlowdownDivisor;
		if (Main.npc[OwnerIndex].ai[3] % 2f == 0f)
		{
			angularVelocity *= -1f;
		}
		base.Projectile.velocity = base.Projectile.velocity.RotatedBy(angularVelocity);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
	}

	public override float DetermineLaserLength()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		float[] sampledLengths = new float[10];
		Collision.LaserScan(base.Projectile.Center, base.Projectile.velocity, (float)base.Projectile.width * base.Projectile.scale, MaxLaserLength, sampledLengths);
		float newLaserLength = sampledLengths.Average();
		if (!Collision.CanHitLine(Main.npc[OwnerIndex].Center, 1, 1, Main.player[Main.npc[OwnerIndex].target].Center, 1, 1))
		{
			newLaserLength = MaxLaserLength;
		}
		return newLaserLength;
	}

	public override void PostAI()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		int dustType = 107;
		Vector2 dustCreationPosition = base.Projectile.Center + base.Projectile.velocity * (base.LaserLength - 14f);
		for (int i = 0; i < 2; i++)
		{
			Vector2 dustVelocity = (base.Projectile.velocity.ToRotation() + (float)Main.rand.NextBool().ToDirectionInt() * ((float)Math.PI / 2f)).ToRotationVector2() * Main.rand.NextFloat(2f, 4f);
			Dust dust = Dust.NewDustDirect(dustCreationPosition, 0, 0, dustType, dustVelocity.X, dustVelocity.Y, 0, new Color(0, 255, 255));
			dust.noGravity = true;
			dust.scale = 1.7f;
		}
		if (Main.rand.NextBool(5))
		{
			Vector2 dustSpawnOffset = base.Projectile.velocity.RotatedBy(1.5707963705062866) * Main.rand.NextFloatDirection() * (float)base.Projectile.width * 0.5f;
			Dust exoEnergy = Dust.NewDustDirect(dustCreationPosition + dustSpawnOffset - Vector2.One * 4f, 8, 8, dustType, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
			exoEnergy.velocity *= 0.5f;
			exoEnergy.velocity.Y = 0f - Math.Abs(exoEnergy.velocity.Y);
		}
		base.Projectile.frameCounter++;
		if ((float)base.Projectile.frameCounter % 5f == 0f)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		if (base.Projectile.scale < 0.001f)
		{
			return false;
		}
		Color beamColor = (CalamityClientConfig.Instance.Photosensitivity ? Color.CornflowerBlue : LaserOverlayColor);
		Rectangle startFrameArea = LaserBeginTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Rectangle middleFrameArea = LaserMiddleTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Rectangle endFrameArea = LaserEndTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(LaserBeginTexture, base.Projectile.Center - Main.screenPosition, startFrameArea, beamColor, base.Projectile.rotation, LaserBeginTexture.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		float laserBodyLength = base.LaserLength + (float)middleFrameArea.Height;
		Vector2 centerOnLaser = base.Projectile.Center;
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
		return base.Projectile.scale >= 0.5f;
	}
}
