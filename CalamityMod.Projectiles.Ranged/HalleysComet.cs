using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class HalleysComet : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 5;
		base.Projectile.MaxUpdates = 15;
		base.Projectile.timeLeft = 20 * base.Projectile.MaxUpdates;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		Time++;
		bool isDrawingUpdate = base.Projectile.numUpdates % 3 == 0;
		Color newColor;
		if ((Time > 6f) & isDrawingUpdate)
		{
			Color outerSparkColor = default(Color);
			((Color)(ref outerSparkColor))._002Ector(8, 35, 156);
			float scaleBoost = MathHelper.Clamp(Time * 0.005f, 0f, 2f);
			float outerSparkScale = 3.2f + scaleBoost;
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, base.Projectile.velocity, affectedByGravity: false, 7, outerSparkScale, outerSparkColor));
			Color innerSparkColor = default(Color);
			((Color)(ref innerSparkColor))._002Ector(184, 215, 245);
			float innerSparkScale = 1.6f + scaleBoost;
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, base.Projectile.velocity, affectedByGravity: false, 7, innerSparkScale, innerSparkColor));
		}
		else if (Time == 5f)
		{
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, base.Projectile.velocity * 0.75f, Color.Aqua, new Vector2(1f, 2.5f), base.Projectile.rotation, 0.2f, 0.03f, 20));
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, base.Projectile.velocity * 0.4f, Color.DodgerBlue, new Vector2(1f, 2.5f), base.Projectile.rotation, 0.1f, 0.025f, 35));
			for (int i = 0; i <= 25; i++)
			{
				Vector2 center = base.Projectile.Center;
				int type = (Main.rand.NextBool(3) ? 172 : 206);
				Vector2? velocity = base.Projectile.velocity;
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(center, type, velocity, 0, newColor);
				dust.scale = Main.rand.NextFloat(1.6f, 2.5f);
				dust.velocity = base.Projectile.velocity.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.3f, 1.6f);
				dust.noGravity = true;
				Vector2 center2 = base.Projectile.Center;
				int type2 = (Main.rand.NextBool(3) ? 172 : 206);
				Vector2? velocity2 = base.Projectile.velocity;
				newColor = default(Color);
				Dust dust2 = Dust.NewDustPerfect(center2, type2, velocity2, 0, newColor);
				dust2.scale = Main.rand.NextFloat(1.35f, 2.1f);
				dust2.velocity = base.Projectile.velocity.RotatedByRandom(0.05999999865889549) * Main.rand.NextFloat(0.8f, 3.1f);
				dust2.noGravity = true;
			}
		}
		if (base.Projectile.FinalExtraUpdate())
		{
			Vector2 center3 = base.Projectile.Center;
			newColor = Color.MediumBlue;
			Lighting.AddLight(center3, ((Color)(ref newColor)).ToVector3() * 0.4f);
		}
		if (base.Projectile.timeLeft == 1 && base.Projectile.ai[1] < 1f)
		{
			Main.player[base.Projectile.owner].Calamity().HalleyAccuracyCounter -= HalleysInferno.LostAccuracyPerMiss;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in HalleysInferno.Hit, base.Projectile.Center);
		for (int i = 0; i < 14; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 172 : 206, base.Projectile.velocity);
			dust.scale = Main.rand.NextFloat(1.1f, 1.9f);
			dust.velocity = base.Projectile.velocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.2f, 2.1f);
			dust.noGravity = true;
		}
		base.Projectile.ai[1] = 1f;
		if (base.Projectile.penetrate == 5)
		{
			CalamityPlayer cplay = Main.player[base.Projectile.owner].Calamity();
			cplay.HalleyAccuracyCounter++;
			cplay.HalleyAccuracyCounter = MathF.Min(HalleysInferno.MaxAccuracy, cplay.HalleyAccuracyCounter);
			Main.player[base.Projectile.owner].Calamity().StarburstSpawnFrameCounter += cplay.HalleyAccuracyCounter / HalleysInferno.MaxAccuracy * HalleysInferno.MaxStarburstPerComet;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Nightwither>(), 450);
		SoundEngine.PlaySound(in HalleysInferno.Hit, base.Projectile.Center);
		for (int i = 0; i < 14; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 172 : 206, base.Projectile.velocity);
			dust.scale = Main.rand.NextFloat(1.1f, 1.9f);
			dust.velocity = base.Projectile.velocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.2f, 2.1f);
			dust.noGravity = true;
		}
	}
}
