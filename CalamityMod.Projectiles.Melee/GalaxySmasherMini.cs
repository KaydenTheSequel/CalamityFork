using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Graphics.Metaballs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class GalaxySmasherMini : ModProjectile, ILocalizedModType, IModType
{
	public float radius = 50f;

	public int time;

	public bool homing;

	public NPC targeted;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 90;
		base.Projectile.height = 90;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 500;
		base.Projectile.extraUpdates = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		List<Color> eColors = new List<Color>
		{
			Color.Aqua,
			Color.Magenta
		};
		float rate = Main.GlobalTimeWrappedHourly * 20f;
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color val = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		Color usedColor = Color.Lerp(val, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		if (time % 2 == 0)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity * 0.05f, "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 6, 0.2f, usedColor, new Vector2(Utils.Remap(((Vector2)(ref base.Projectile.velocity)).Length(), 0f, 5f, 1f, 0.6f), 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Utils.Remap(((Vector2)(ref base.Projectile.velocity)).Length(), 0f, 5f, 0f, 0.9f)));
		}
		GalaxyMetaball.SpawnParticle(base.Projectile.Center, -base.Projectile.velocity.RotatedByRandom(0.25) * Main.rand.NextFloat(0.2f, 0.7f), 30f * Main.rand.NextFloat(0.9f, 1f));
		if (time > 45 || homing)
		{
			homing = true;
			if (base.Projectile.ai[2] != -5f)
			{
				targeted = Main.npc[(int)base.Projectile.ai[2]];
			}
			if (targeted == null || !targeted.CanBeChasedBy(base.Projectile) || !targeted.active)
			{
				targeted = base.Projectile.Center.ClosestNPCAt(1000f);
				base.Projectile.ai[2] = -5f;
			}
			if (targeted != null)
			{
				CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targeted, ignoreTiles: true, 0.18f, 25f, 0.99f, 0.95f, accelerate: true);
				base.Projectile.extraUpdates = 3;
			}
		}
		else
		{
			Projectile projectile = base.Projectile;
			projectile.velocity += base.Projectile.velocity.RotatedBy(0.24f * base.Projectile.ai[1]) * 0.04f;
			base.Projectile.extraUpdates = 2;
		}
		time += ((!homing) ? 1 : (-1));
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 60);
		for (int i = 0; i < 4; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), base.Projectile.velocity * 3f * Main.rand.NextFloat(0.3f, 1f), 0, default(Color), Main.rand.NextFloat(0.65f, 1f));
			dust.noGravity = true;
			dust.color = (Main.rand.NextBool() ? Color.Magenta : Color.Aqua);
		}
		if (target == targeted)
		{
			base.Projectile.Kill();
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		return homing && CalamityUtils.CircularHitboxCollision(base.Projectile.Center, radius, targetHitbox);
	}
}
