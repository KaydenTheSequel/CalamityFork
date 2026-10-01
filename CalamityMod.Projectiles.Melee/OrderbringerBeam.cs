using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class OrderbringerBeam : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public Color mainColor;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/OrderbringerBeam";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 12;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 100;
		base.Projectile.height = 100;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 40;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		mainColor = player.Calamity().lightRGB;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (base.Projectile.timeLeft % 12 == 0 && targetDist < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 0.2f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 23, 0.25f, mainColor * 0.75f, new Vector2(1f, 7.35f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.2f, 1f, 0.7f));
		}
		time++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		int mode = ProjectileID.Sets.TrailingMode[base.Type];
		Color lightColor2 = Color.Lerp(mainColor, Color.White, 0.3f);
		((Color)(ref lightColor2)).A = 0;
		CalamityUtils.DrawAfterimagesCentered(projectile, mode, lightColor2, 1, null, drawCentered: true, shrink: true);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 180);
		if (base.Projectile.numHits < 1)
		{
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(target.Center, base.Projectile.velocity.SafeNormalize(Vector2.UnitY), affectedByGravity: false, 12, 0.07f, mainColor, new Vector2(1.5f, 0.8f), quickShrink: true));
		}
	}

	public OrderbringerBeam()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		mainColor = Color.White;
		base._002Ector();
	}
}
