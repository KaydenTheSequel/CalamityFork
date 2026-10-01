using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class UltimaSpark : ModProjectile, ILocalizedModType, IModType
{
	public const int DustType = 261;

	public const float MaxHomingDistance = 1200f;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public float Time
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 8);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.arrow = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 240;
		base.Projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ && Time > 5f)
		{
			for (int i = 0; i < 3; i++)
			{
				Dust dust = Dust.NewDustPerfect(Vector2.Lerp(base.Projectile.oldPosition, base.Projectile.position, (float)i / 3f), 261);
				dust.color = Main.hslToRgb((Main.rand.NextFloat(-0.04f, 0.04f) + Time / 80f) % 1f, 0.8f, 0.6f);
				dust.scale = 2.3f;
				dust.fadeIn = 1f;
				dust.rotation = Main.rand.NextFloat((float)Math.PI * 2f);
				dust.velocity = Vector2.Zero;
				dust.noGravity = true;
			}
		}
		NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(1200f);
		if (potentialTarget != null)
		{
			base.Projectile.velocity = (base.Projectile.velocity * 8f + base.Projectile.SafeDirectionTo(potentialTarget.Center) * 18f) / 9f;
			return;
		}
		if (Time > 30f)
		{
			float updatedTime = Time - 30f;
			if (updatedTime % 120f > 90f)
			{
				base.Projectile.velocity = base.Projectile.velocity.RotatedBy(0.07853981852531433);
			}
			else if (updatedTime % 120f > 30f)
			{
				base.Projectile.velocity = base.Projectile.velocity.RotatedBy((float)Math.Sin((updatedTime - 30f) % 60f / 60f * ((float)Math.PI * 2f)) * MathHelper.ToRadians(15f));
			}
		}
		Time++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(60, 60);
		base.Projectile.Damage();
		if (!Main.dedServ)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 261);
				dust.color = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.7f);
				dust.scale = Main.rand.NextFloat(0.9f, 1.25f);
				dust.velocity = Main.rand.NextVector2Circular(6f, 6f);
				dust.noGravity = true;
			}
		}
	}
}
