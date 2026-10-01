using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TruePurityProjection : ModProjectile, ILocalizedModType, IModType
{
	public NPC target;

	public new string LocalizationCategory => "Projectiles.Melee";

	public Player Owner => Main.player[base.Projectile.owner];

	public override string Texture => "CalamityMod/Projectiles/Melee/BrokenBiomeBlade_PurityProjection";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.aiStyle = 27;
		base.AIType = 156;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = TrueBiomeBlade.DefaultAttunement_BeamTime;
		base.Projectile.extraUpdates = 1;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			Projectile[] projectile = Main.projectile;
			foreach (Projectile proj in projectile)
			{
				if (proj.active && proj.type == ModContent.ProjectileType<PurityProjectionSigil>() && proj.owner == Owner.whoAmI)
				{
					target = Main.npc[(int)proj.ai[0]];
					break;
				}
			}
		}
		else
		{
			Vector2 val = base.Projectile.Center - target.Center;
			float num = ((Vector2)(ref val)).Length();
			val = base.Projectile.Center + base.Projectile.velocity - target.Center;
			if (num >= ((Vector2)(ref val)).Length() && base.Projectile.velocity.AngleBetween(target.Center - base.Projectile.Center) < TrueBiomeBlade.DefaultAttunement_HomingAngle)
			{
				base.Projectile.timeLeft = 30;
				float angularTurnSpeed = MathHelper.ToRadians(MathHelper.Lerp(12.5f, 2.5f, MathHelper.Clamp(base.Projectile.Distance(target.Center) / 10f, 0f, 1f)));
				float idealDirection = base.Projectile.AngleTo(target.Center);
				float updatedDirection = base.Projectile.velocity.ToRotation().AngleTowards(idealDirection, angularTurnSpeed);
				base.Projectile.velocity = updatedDirection.ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length();
			}
		}
		if ((float)base.Projectile.timeLeft < (float)TrueBiomeBlade.DefaultAttunement_BeamTime - 5f)
		{
			base.Projectile.tileCollide = true;
		}
		Lighting.AddLight(base.Projectile.Center, 0.75f, 1f, 0.24f);
		int dustParticle = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 75, 0f, 0f, 100, default(Color), 0.9f);
		Main.dust[dustParticle].noGravity = true;
		Dust obj = Main.dust[dustParticle];
		obj.velocity *= 0.5f;
		Dust obj2 = Main.dust[dustParticle];
		obj2.velocity += base.Projectile.velocity * 0.1f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		if ((float)base.Projectile.timeLeft > (float)TrueBiomeBlade.DefaultAttunement_BeamTime - 5f)
		{
			return false;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 15; i++)
		{
			Vector2 displace = (base.Projectile.rotation - (float)Math.PI / 4f).ToRotationVector2() * (-0.5f + (float)i / 15f) * 88f;
			int dustParticle = Dust.NewDust(base.Projectile.Center + displace, base.Projectile.width, base.Projectile.height, 75, 0f, 0f, 100, default(Color), 2f);
			Main.dust[dustParticle].noGravity = true;
			Main.dust[dustParticle].velocity = base.Projectile.oldVelocity;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 90);
	}
}
