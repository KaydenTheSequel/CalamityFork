using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class NeptunesBountySplitProjectile : ModProjectile, ILocalizedModType, IModType
{
	public int Time;

	public int randTimer;

	public int dustType1;

	public int dustType2;

	public int spreadDust;

	public Color WaterColor;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 110;
		base.Projectile.ignoreWater = true;
		base.Projectile.extraUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		float playerDist = Vector2.Distance(Owner.Center, base.Projectile.Center);
		Time++;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.988f;
		if (base.Projectile.timeLeft % 2 == 0 && (float)Time > 3f && playerDist < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * Main.rand.NextFloat(0.1f, 0.5f), "CalamityMod/Particles/WaterFoam", affectedByGravity: false, Main.rand.Next(4, 7), Main.rand.NextFloat(0.15f, 0.2f), Color.DodgerBlue * 0.75f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, Main.rand.NextFloat(-10f, 10f)));
		}
		if (Main.rand.NextBool())
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(3 + spreadDust, 3 + spreadDust), (!Main.rand.NextBool(5)) ? dustType1 : dustType2, -base.Projectile.velocity * Main.rand.NextFloat(0.05f, 0.35f), 0, default(Color), Main.rand.NextFloat(0.4f, 0.6f));
			dust.noGravity = true;
			dust.color = ((!Main.rand.NextBool(5)) ? WaterColor : Color.Aqua);
			if (dust.type == dustType1)
			{
				dust.scale *= 0.7f;
			}
		}
		if (base.Projectile.timeLeft == 20)
		{
			GeneralParticleHandler.SpawnParticle(new WaterFlavoredParticle(base.Projectile.Center, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 25, 0.85f + (float)Time * 0.013f, WaterColor * 0.15f));
		}
		else if (base.Projectile.timeLeft > 20)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity * 0.05f, "CalamityMod/Particles/WaterFlavored", affectedByGravity: false, 2, 0.85f + (float)Time * 0.013f, WaterColor * (1f - (float)Time * 0.01f), new Vector2(0.2f + (float)Time * 0.01f, 1f)));
		}
		if (base.Projectile.timeLeft < 20)
		{
			Time -= 5;
			spreadDust += 2;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HadopelagicPressure>(), 240);
	}

	public NeptunesBountySplitProjectile()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		dustType1 = 278;
		dustType2 = 267;
		WaterColor = (Main.rand.NextBool() ? Color.DodgerBlue : Color.DeepSkyBlue);
		base._002Ector();
	}
}
