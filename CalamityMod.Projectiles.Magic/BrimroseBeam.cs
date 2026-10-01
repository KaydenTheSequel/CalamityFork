using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class BrimroseBeam : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 4);
		base.Projectile.alpha = 255;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 100;
		base.Projectile.timeLeft = 30;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		int numProj = 2;
		float rotation = MathHelper.ToRadians((float)((base.Projectile.ai[0] >= 3f) ? 20 : 10));
		if (base.Projectile.owner == Main.myPlayer && base.Projectile.ai[0] < 4f)
		{
			for (int i = 0; i < numProj; i++)
			{
				Vector2 perturbedSpeed = base.Projectile.velocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)(i / (numProj - 1))));
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, perturbedSpeed, base.Projectile.type, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, base.Projectile.ai[0] + 1f);
			}
		}
	}

	public override void AI()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] > 9f || base.Projectile.ai[0] > 0f)
		{
			int fire = Dust.NewDust(base.Projectile.Center, 1, 1, 235, 0f, 0f, 0, default(Color), 1.5f);
			Dust obj = Main.dust[fire];
			obj.velocity *= 0.1f;
			Main.dust[fire].noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 120);
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((double)base.Projectile.damage * 0.8);
		}
	}
}
