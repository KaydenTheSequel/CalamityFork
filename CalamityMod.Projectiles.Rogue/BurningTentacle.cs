using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class BurningTentacle : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 3;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.scale = 1f - base.Projectile.localAI[0];
		base.Projectile.height = (base.Projectile.width = (int)(20f * base.Projectile.scale));
		if (base.Projectile.localAI[0] < 0.1f)
		{
			base.Projectile.localAI[0] += 0.01f;
		}
		else
		{
			base.Projectile.localAI[0] += 0.025f;
		}
		if (base.Projectile.localAI[0] >= 0.95f)
		{
			base.Projectile.Kill();
		}
		base.Projectile.velocity.X += base.Projectile.ai[0];
		base.Projectile.velocity.Y += base.Projectile.ai[1];
		if (((Vector2)(ref base.Projectile.velocity)).Length() > 10f)
		{
			base.Projectile.velocity = Vector2.Normalize(base.Projectile.velocity) * 10f;
		}
		base.Projectile.ai[0] *= 1.04f;
		base.Projectile.ai[1] *= 1.04f;
		if (base.Projectile.scale < 1f)
		{
			for (int i = 0; (float)i < base.Projectile.scale * 8f; i++)
			{
				Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 27, base.Projectile.velocity.X, base.Projectile.velocity.Y, 100, default(Color), 1.1f);
				dust.position = (dust.position + base.Projectile.Center) / 2f;
				dust.noGravity = true;
				dust.velocity *= 0.1f;
				dust.velocity -= base.Projectile.velocity * (1.3f - base.Projectile.scale);
				dust.fadeIn = 100f;
				dust.scale += base.Projectile.scale * 0.75f;
			}
		}
	}
}
