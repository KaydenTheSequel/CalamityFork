using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Packets;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class Slagfire : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override bool CanHitPlayer(Player target)
	{
		return false;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.hostile = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.extraUpdates = 1;
		base.Projectile.tileCollide = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[1]++;
		if (base.Projectile.localAI[1] >= 4f)
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.scale -= 0.001f;
		if (base.Projectile.scale <= 0f)
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.localAI[0] <= 3f)
		{
			base.Projectile.localAI[0]++;
			return;
		}
		Color color = default(Color);
		((Color)(ref color))._002Ector(254, 63, 63);
		for (int i = 0; i < 3; i++)
		{
			Vector2 relativePosition = base.Projectile.Center - base.Projectile.velocity / 3f * (float)i;
			Vector2 sparkVelocity = -base.Projectile.velocity * 0.01f * Main.rand.NextFloat(0.5f, 1.5f);
			GeneralParticleHandler.SpawnParticle(new CustomSpark(relativePosition, sparkVelocity, "CalamityMod/Particles/BloomLineFade", affectedByGravity: false, 6, 0.04f, color * 0.85f, new Vector2(0.45f, 0.9f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.4f));
		}
		if (Main.rand.NextBool(8))
		{
			Vector2 relativePosition2 = base.Projectile.Center + Main.rand.NextVector2Circular(15f, 15f);
			Vector2 sparkVelocity2 = -base.Projectile.velocity * Main.rand.NextFloat(0.01f, 0.045f);
			GeneralParticleHandler.SpawnParticle(new CustomSpark(relativePosition2, sparkVelocity2, "CalamityMod/Particles/BloomLineFade", affectedByGravity: false, 8, 0.02f, color * 0.5f, new Vector2(0.5f, 0.8f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.3f));
		}
		if (Main.rand.NextBool(50))
		{
			Vector2 spawnHere = base.Projectile.position;
			spawnHere.Y = base.Projectile.Bottom.Y + 3f;
			Vector2 particleVelocity = default(Vector2);
			((Vector2)(ref particleVelocity))._002Ector((float)Main.rand.Next(-1, 1), 3f);
			GeneralParticleHandler.SpawnParticle(new WaterFlavoredParticle(spawnHere, particleVelocity, affectedByGravity: true, Main.rand.Next(30, 50), Main.rand.NextFloat(0.4f, 0.65f), new Color(220, 138, 138)));
		}
		if (base.Projectile.localAI[1] >= 10f)
		{
			base.Projectile.velocity.Y += 0.2f;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (target.type == 22)
		{
			modifiers.FinalDamage *= 25f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		Color innerColor = default(Color);
		((Color)(ref innerColor))._002Ector(230, 198, 198);
		Color outterColor = default(Color);
		((Color)(ref outterColor))._002Ector(248, 63, 63);
		Vector2 baseParticleDirection = Utils.SafeNormalize(new Vector2((float)base.Projectile.direction, 0f), Vector2.UnitX * (float)base.Projectile.direction);
		for (int i = 0; i < 4; i++)
		{
			Vector2 particleVelocity = baseParticleDirection.RotatedByRandom(MathHelper.ToRadians(50f)) * Main.rand.NextFloat(10f, 13f);
			bool affectedByGravity = true;
			int lifetime = Main.rand.Next(20, 45);
			float scale = Main.rand.NextFloat(0.3f, 0.7f);
			GeneralParticleHandler.SpawnParticle(new WaterFlavoredParticle(base.Projectile.Center, particleVelocity, affectedByGravity, lifetime, scale * 1.15f, outterColor));
			GeneralParticleHandler.SpawnParticle(new WaterFlavoredParticle(base.Projectile.Center, particleVelocity, affectedByGravity, lifetime, scale * 0.75f, innerColor));
		}
		base.Projectile.Kill();
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damage)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<SearingLava>(), 600);
		Color innerColor = default(Color);
		((Color)(ref innerColor))._002Ector(230, 198, 198);
		Color outterColor = default(Color);
		((Color)(ref outterColor))._002Ector(248, 63, 63);
		Vector2 baseParticleDirection = Utils.SafeNormalize(new Vector2((float)base.Projectile.direction, 0f), Vector2.UnitX * (float)base.Projectile.direction);
		Vector2 initialSpawnPosition = target.Center + new Vector2((float)target.width / 2f * (float)base.Projectile.direction + 4f * (float)base.Projectile.direction, Main.rand.NextFloat((float)(-target.height) / 2f, (float)target.height / 2f));
		for (int i = 0; i < 4; i++)
		{
			Vector2 relativePosition = initialSpawnPosition + Main.rand.NextVector2Circular(1f, 1f);
			Vector2 particleVelocity = baseParticleDirection.RotatedByRandom(MathHelper.ToRadians(50f)) * Main.rand.NextFloat(10f, 13f);
			bool affectedByGravity = true;
			int lifetime = Main.rand.Next(20, 45);
			float scale = Main.rand.NextFloat(0.3f, 0.7f);
			GeneralParticleHandler.SpawnParticle(new WaterFlavoredParticle(relativePosition, particleVelocity, affectedByGravity, lifetime, scale * 1.15f, outterColor));
			GeneralParticleHandler.SpawnParticle(new WaterFlavoredParticle(relativePosition, particleVelocity, affectedByGravity, lifetime, scale * 0.75f, innerColor));
		}
		if (target.type == 22 && target.life <= 0 && base.Projectile.owner == Main.myPlayer)
		{
			if (Main.netMode == 1)
			{
				SpawnBossOnPositionPacket.Send((int)Main.player[base.Projectile.owner].Center.X, (int)Main.player[base.Projectile.owner].Center.Y, 113, Main.player[base.Projectile.owner]);
			}
			else
			{
				NPC.SpawnWOF(Main.player[base.Projectile.owner].Center);
			}
		}
	}
}
