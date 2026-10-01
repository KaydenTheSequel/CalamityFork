using System;
using System.IO;
using CalamityMod.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class TheAtomSplitterProjectile : ModProjectile, ILocalizedModType, IModType
{
	public static float NormalSplitMultiplier = 0.7f;

	public static float StealthSplitMultiplier = 0.3f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public ref float HitTargetIndex => ref base.Projectile.ai[0];

	public ref float Time => ref base.Projectile.ai[1];

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/TheAtomSplitter";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 124);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.timeLeft);
		writer.Write((byte)base.Projectile.alpha);
		writer.Write(base.Projectile.scale);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.timeLeft = reader.ReadInt32();
		base.Projectile.alpha = reader.ReadByte();
		base.Projectile.scale = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 20f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.02f;
		}
		if (base.Projectile.alpha < 10)
		{
			EmitDustFromTip();
		}
		if (Main.npc.IndexInRange((int)HitTargetIndex) && Main.npc[(int)HitTargetIndex].active)
		{
			if (base.Projectile.alpha < 255)
			{
				base.Projectile.alpha = Utils.Clamp(base.Projectile.alpha + 30, 0, 255);
			}
			bool stealth = base.Projectile.Calamity().stealthStrike;
			int shootRate = (stealth ? 2 : 4);
			if (base.Projectile.timeLeft % shootRate == shootRate - 1)
			{
				NPC target = Main.npc[(int)HitTargetIndex];
				FireDuplicateAtTarget(target, stealth ? 305f : 200f, stealth);
				if (stealth)
				{
					FireExtraDuplicatesAtTarget(target);
				}
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		Time++;
	}

	public void EmitDustFromTip()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			float dustVelocityArcOffset = 1.5f + (float)Math.Sin((float)Math.PI * 2f * (float)base.Projectile.timeLeft / 35f) * 0.25f;
			Color dustColor = CalamityUtils.MulticolorLerp((float)Math.Cos((float)Math.PI * 2f * (float)base.Projectile.timeLeft / 75f) * 0.5f + 0.5f, CalamityUtils.ExoPalette);
			Vector2 currentDirection = base.Projectile.velocity.SafeNormalize(-Vector2.UnitY);
			Vector2 tipPosition = base.Projectile.Center + currentDirection * ((float)base.Projectile.height * 0.67f - 3f);
			tipPosition += Main.rand.NextVector2CircularEdge(0.35f, 0.35f);
			for (float direction = -1f; direction <= 1f; direction += 2f)
			{
				Dust dust = Dust.NewDustPerfect(tipPosition, 267);
				dust.velocity = currentDirection.RotatedBy(direction * dustVelocityArcOffset) * -7f + base.Projectile.velocity;
				dust.scale = 1.2f;
				dust.color = dustColor;
				dust.noGravity = true;
				DustExtensions.BetterCloneDust(dust).scale *= 0.6f;
			}
		}
	}

	public void FireDuplicateAtTarget(NPC target, float baseOutwardness, bool stealth)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Vector2 spawnPosition = target.Center + Main.rand.NextVector2CircularEdge(baseOutwardness, baseOutwardness) * Main.rand.NextFloat(0.9f, 1.15f);
			Vector2 shootVelocity = (target.Center - spawnPosition).SafeNormalize(Vector2.UnitY) * Main.rand.NextFloat(12f, 14f);
			int damage = (int)((float)base.Projectile.damage * (stealth ? StealthSplitMultiplier : NormalSplitMultiplier));
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPosition, shootVelocity, ModContent.ProjectileType<TheAtomSplitterDuplicate>(), damage, base.Projectile.knockBack, base.Projectile.owner, 0f, baseOutwardness / 9f);
		}
	}

	public void FireExtraDuplicatesAtTarget(NPC target)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Vector2 spawnPosition = target.Center + Vector2.UnitY * (float)Main.rand.NextBool().ToDirectionInt() * Main.rand.NextFloat(200f, 270f);
			spawnPosition.X += Main.rand.NextFloatDirection() * (float)target.width * 0.45f;
			int damage = (int)((float)base.Projectile.damage * StealthSplitMultiplier);
			Vector2 shootVelocity = Vector2.UnitY * (float)(target.Center.Y - spawnPosition.Y > 0f).ToDirectionInt() * Main.rand.NextFloat(14f, 16.5f);
			int extra = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPosition, shootVelocity, ModContent.ProjectileType<TheAtomSplitterDuplicate>(), damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 24f);
			Main.projectile[extra].extraUpdates = 2;
		}
	}

	public void ReleaseHitDust(Vector2 spawnPosition)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		int dustCount = (base.Projectile.Calamity().stealthStrike ? 60 : 40);
		Vector2 baseDustVelocity = -Vector2.UnitY.RotatedByRandom(3.1415927410125732) * 1.4f;
		Vector2 outwardFireSpeedFactor = default(Vector2);
		((Vector2)(ref outwardFireSpeedFactor))._002Ector(2.1f, 2f);
		for (float i = 0f; i < (float)dustCount; i++)
		{
			Color dustColor = CalamityUtils.MulticolorLerp(Main.rand.NextFloat(), CalamityUtils.ExoPalette);
			Dust explosionDust = Dust.NewDustDirect(spawnPosition, 0, 0, 267, 0f, 0f, 0, dustColor);
			explosionDust.position = spawnPosition;
			explosionDust.velocity = baseDustVelocity.RotatedBy((float)Math.PI * 2f * i / (float)dustCount) * outwardFireSpeedFactor * Main.rand.NextFloat(0.8f, 1.2f);
			Dust dust = explosionDust;
			dust.velocity += base.Projectile.velocity * Main.rand.NextFloat(0.6f, 0.85f);
			if (base.Projectile.Calamity().stealthStrike)
			{
				Dust dust2 = explosionDust;
				dust2.velocity *= 1.6f;
			}
			explosionDust.noGravity = true;
			explosionDust.scale = 1.1f;
			explosionDust.fadeIn = Main.rand.NextFloat(1.4f, 2.4f);
			Dust dust3 = DustExtensions.BetterCloneDust(explosionDust);
			dust3.velocity *= Main.rand.NextFloat(0.8f);
			explosionDust = DustExtensions.BetterCloneDust(explosionDust);
			explosionDust.scale /= 2f;
			explosionDust.fadeIn /= 2f;
			explosionDust.color = new Color(255, 255, 255, 255);
		}
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.alpha >= 80)
		{
			return false;
		}
		return null;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (HitTargetIndex >= 0f && (float)target.whoAmI != HitTargetIndex)
		{
			return false;
		}
		return base.CanHitNPC(target);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		ReleaseHitDust(target.Center - base.Projectile.velocity * 3f);
		if (!Main.npc.IndexInRange((int)HitTargetIndex))
		{
			HitTargetIndex = target.whoAmI;
			base.Projectile.timeLeft = 60;
			base.Projectile.netUpdate = true;
		}
	}
}
