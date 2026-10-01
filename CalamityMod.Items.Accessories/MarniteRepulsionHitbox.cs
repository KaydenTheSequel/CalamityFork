using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class MarniteRepulsionHitbox : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 80;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 700;
		base.Projectile.tileCollide = false;
		base.Projectile.ArmorPenetration = 20;
		base.Projectile.netImportant = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.active && Owner.GetModPlayer<MarniteRepulsionShieldPlayer>().shieldEquipped)
		{
			base.Projectile.Center = Owner.Center + Vector2.UnitX * (float)Owner.direction * -20f;
			if (Owner.mount.Active)
			{
				Projectile projectile = base.Projectile;
				projectile.Center += -Vector2.UnitY * (float)Owner.mount.PlayerOffset;
			}
			if (Main.rand.NextBool(6))
			{
				Vector2 dustOrigin = Owner.MountedCenter;
				Vector2 dustDirection = (Vector2.UnitX * -1f * (float)Owner.direction).RotatedByRandom(1.4608405828475952);
				dustOrigin += dustDirection * 14f;
				float spikeSpeed = Main.rand.NextFloat(1f, 3f);
				Vector2 dustVelocity = dustDirection * spikeSpeed + Owner.velocity;
				Vector2 dustOriginOffset = dustDirection * 4f;
				for (int i = 0; i < 5; i++)
				{
					Vector2 position = dustOrigin;
					Vector2? velocity = dustVelocity;
					float scale = Main.rand.NextFloat(0.6f, 1f);
					Dust.NewDustPerfect(position, 229, velocity, 120, default(Color), scale).noGravity = true;
					dustOrigin += dustOriginOffset;
				}
			}
		}
		else
		{
			base.Projectile.active = false;
		}
	}

	public override bool? CanHitNPC(NPC target)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		if (Math.Sign((Owner.Center - target.Center).X) != Owner.direction)
		{
			return false;
		}
		if (target.CountsAsACritter || target.friendly || !target.chaseable)
		{
			return false;
		}
		return base.CanHitNPC(target);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.HitDirectionOverride = Math.Sign(-Owner.direction);
	}

	public override bool? CanCutTiles()
	{
		return false;
	}
}
