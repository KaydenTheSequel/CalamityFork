using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class Nanotech : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 26;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.ai[1] >= 30f && target.CanBeChasedBy(base.Projectile);
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, new Vector3(0.075f, 0.4f, 0.15f));
		base.Projectile.rotation += base.Projectile.velocity.X * 0.2f;
		if (base.Projectile.velocity.X > 0f)
		{
			base.Projectile.rotation += 0.08f;
		}
		else
		{
			base.Projectile.rotation -= 0.08f;
		}
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] > 60f)
		{
			base.Projectile.alpha += 5;
			if (base.Projectile.alpha >= 255)
			{
				base.Projectile.alpha = 255;
				base.Projectile.Kill();
				return;
			}
		}
		if (base.Projectile.ai[1] >= 30f)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 200f, 12f, 20f);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		int cap = 5;
		float capDamageFactor = 0.05f;
		int excessCount = Main.player[base.Projectile.owner].ownedProjectileCounts[base.Type] - cap;
		modifiers.SourceDamage *= MathHelper.Clamp(1f - capDamageFactor * (float)excessCount, 0f, 1f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 2; i++)
		{
			int dustScale = (int)(10f * base.Projectile.scale);
			int greenDust = Dust.NewDust(base.Projectile.Center - Vector2.One * (float)dustScale, dustScale * 2, dustScale * 2, 107);
			Dust nanoDust = Main.dust[greenDust];
			Vector2 dustDirection = Vector2.Normalize(nanoDust.position - base.Projectile.Center);
			nanoDust.position = base.Projectile.Center + dustDirection * (float)dustScale * base.Projectile.scale;
			if (i < 30)
			{
				nanoDust.velocity = dustDirection * ((Vector2)(ref nanoDust.velocity)).Length();
			}
			else
			{
				nanoDust.velocity = dustDirection * (float)Main.rand.Next(45, 91) / 10f;
			}
			nanoDust.color = Main.hslToRgb((float)(0.4000000059604645 + Main.rand.NextDouble() * 0.20000000298023224), 0.9f, 0.5f);
			nanoDust.color = Color.Lerp(nanoDust.color, Color.White, 0.3f);
			nanoDust.noGravity = true;
			nanoDust.scale = 0.7f;
		}
	}
}
