using System;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class NanoblackStrike : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	internal bool InvalidTarget => !((int)base.Projectile.ai[0]).WithinBounds(Main.maxNPCs);

	internal ref float visualXOffset => ref base.Projectile.ai[1];

	internal ref float visualYOffset => ref base.Projectile.ai[2];

	public override void SetDefaults()
	{
		base.Projectile.width = 2;
		base.Projectile.height = 2;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 0;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 2;
	}

	public override bool PreAI()
	{
		base.Projectile.DamageType = DamageClass.Generic;
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		ProduceImpactParticles(target);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		ProduceImpactParticles(target);
	}

	private void ProduceImpactParticles(Entity target)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		Vector2 visualsPos = base.Projectile.Center + new Vector2(base.Projectile.ai[1], base.Projectile.ai[2]);
		float sparkSpeed = 2f;
		float baseRot = (float)base.Projectile.spriteDirection * ((float)Math.PI / 2f);
		float scale = 0.014f;
		Color color = NanoblackReaper.ZeroPointImpactColor;
		Vector2 squashStretch = default(Vector2);
		for (int i = 0; i < 3; i++)
		{
			float rot = baseRot + (float)i * ((float)Math.PI * 2f / 3f);
			Vector2 sparkVel = sparkSpeed * rot.ToRotationVector2();
			((Vector2)(ref squashStretch))._002Ector(1f, 0.3f);
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(visualsPos, sparkVel, affectedByGravity: false, 11, scale, color, squashStretch, quickShrink: true, glow: false, 1.25f));
		}
	}
}
