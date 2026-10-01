using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.CalPlayer;
using CalamityMod.NPCs;
using CalamityMod.NPCs.AcidRain;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

[PierceResistException(false)]
public class CryonicShield : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public Player Owner => Main.player[base.Projectile.owner];

	public override string Texture => "CalamityMod/NPCs/Cryogen/CryogenShield";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 222;
		base.Projectile.height = 216;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 90000;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 25;
	}

	public override void AI()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer calamityPlayer = Main.player[base.Projectile.owner].Calamity();
		base.Projectile.friendly = true;
		base.Projectile.hostile = false;
		base.Projectile.rotation += (float)Math.PI / 48f;
		base.Projectile.Center = Owner.Center;
		if (!calamityPlayer.CryoStoneVanity)
		{
			Lighting.AddLight(base.Projectile.Center, base.Projectile.Opacity * 0.2f, base.Projectile.Opacity * 0.45f, base.Projectile.Opacity * 0.5f);
		}
		if (Owner == null || !Owner.active || Owner.dead)
		{
			base.Projectile.Kill();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (!Main.player[base.Projectile.owner].Calamity().CryoStoneVanity)
		{
			target.AddBuff(324, 180);
			target.AddBuff(ModContent.BuffType<GlacialState>(), 30);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		modifiers.HitDirectionOverride = (target.Center.X > base.Projectile.Center.X).ToDirectionInt();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (!Main.player[base.Projectile.owner].Calamity().CryoStoneVanity)
		{
			target.AddBuff(324, 180);
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Vector2 size = base.Projectile.Size;
		return CalamityUtils.CircularHitboxCollision(center, ((Vector2)(ref size)).Length() * 0.5f, targetHitbox);
	}

	public override bool? CanHitNPC(NPC target)
	{
		CalamityPlayer modPlayer = Main.player[base.Projectile.owner].Calamity();
		if ((target.catchItem != 0 && target.type != ModContent.NPCType<Radiator>()) || modPlayer.CryoStoneVanity)
		{
			return false;
		}
		return null;
	}
}
