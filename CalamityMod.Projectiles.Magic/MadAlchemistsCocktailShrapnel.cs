using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class MadAlchemistsCocktailShrapnel : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 180;
		base.Projectile.extraUpdates = 1;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.timeLeft < 150 && target.CanBeChasedBy(base.Projectile);
	}

	public override void AI()
	{
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		Vector2 rotationMult = default(Vector2);
		((Vector2)(ref rotationMult))._002Ector(6f, 12f);
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] == 48f)
		{
			base.Projectile.localAI[0] = 0f;
		}
		else
		{
			for (int i = 0; i < 2; i++)
			{
				Vector2 dustRotation = Vector2.UnitX * -15f;
				dustRotation = -Vector2.UnitY.RotatedBy(base.Projectile.localAI[0] * ((float)Math.PI / 24f) + (float)i * (float)Math.PI) * rotationMult * 0.75f;
				int shrapnelDust = Dust.NewDust(base.Projectile.Center, 0, 0, 173, 0f, 0f, 160, default(Color), 0.75f);
				Main.dust[shrapnelDust].noGravity = true;
				Main.dust[shrapnelDust].position = base.Projectile.Center + dustRotation;
				Main.dust[shrapnelDust].velocity = base.Projectile.velocity;
			}
		}
		int extraDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 100);
		Main.dust[extraDust].noGravity = true;
		if (base.Projectile.timeLeft < 150)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 600f, 12f, 20f);
		}
	}
}
