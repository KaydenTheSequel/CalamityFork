using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class StarnightBeam : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.aiStyle = 27;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 600;
		base.AIType = 156;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.4f, 0f, 0.4f);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 50, 128, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 595)
		{
			return false;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.position);
		for (int i = 4; i < 31; i++)
		{
			float projOldX = base.Projectile.oldVelocity.X * (30f / (float)i);
			float projOldY = base.Projectile.oldVelocity.Y * (30f / (float)i);
			int starnight = Dust.NewDust(new Vector2(base.Projectile.oldPosition.X - projOldX, base.Projectile.oldPosition.Y - projOldY), 8, 8, 73, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, 100, default(Color), 1.8f);
			Dust obj = Main.dust[starnight];
			obj.noGravity = true;
			obj.velocity *= 0.1f;
			starnight = Dust.NewDust(new Vector2(base.Projectile.oldPosition.X - projOldX, base.Projectile.oldPosition.Y - projOldY), 8, 8, 73, base.Projectile.oldVelocity.X, base.Projectile.oldVelocity.Y, 100, default(Color), 1.4f);
			obj.velocity *= 0.01f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(324, 120);
	}
}
