using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class BileExplosion : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 150;
		base.Projectile.height = 150;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 25;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		modifiers.HitDirectionOverride = (base.Projectile.Center.X < target.Center.X).ToDirectionInt();
	}

	public override void AI()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		Vector2 randVel = Utils.RotatedByRandom(new Vector2(8f, 8f), 6.2831854820251465) * Main.rand.NextFloat(0.04f, Main.rand.NextBool(3) ? 0.4f : 0.5f);
		GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + randVel, randVel, Color.GreenYellow, Main.rand.Next(25, 36), Main.rand.NextFloat(0.3f, 0.5f), 0.3f));
		if (Main.rand.NextBool(2))
		{
			Color bubbleColor = (Main.rand.NextBool() ? Color.DarkOliveGreen : Color.GreenYellow);
			Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(70f, 70f);
			Vector2 bubbleVelocity = Vector2.UnitY * Main.rand.NextFloat(-3.5f, -5f);
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(position, bubbleVelocity, bubbleColor, new Vector2(0.6f, 0.8f), 0f, 0.1f, 0f, 65));
		}
		if (base.Projectile.Calamity().stealthStrike && Main.rand.NextBool(4))
		{
			Projectile bubble = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center + Main.rand.NextVector2Circular(70f, 70f), Vector2.UnitY * Main.rand.NextFloat(-3.5f, -5f), ModContent.ProjectileType<SulphuricAcidBubbleFriendly>(), (int)((double)base.Projectile.damage * 0.5), base.Projectile.knockBack, base.Projectile.owner);
			if (bubble.whoAmI.WithinBounds(Main.maxProjectiles))
			{
				bubble.Calamity().stealthStrike = base.Projectile.Calamity().stealthStrike;
				bubble.DamageType = RogueDamageClass.Instance;
				bubble.timeLeft = 45;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 60);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 60);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (float)base.Projectile.width * 0.5f * base.Projectile.scale, targetHitbox);
	}
}
