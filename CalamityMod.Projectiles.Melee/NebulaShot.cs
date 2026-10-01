using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class NebulaShot : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 5;
		base.Projectile.timeLeft = 600;
	}

	public override void AI()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.ai[1] = 1f;
			SoundEngine.PlaySound(in SoundID.Item12, base.Projectile.position);
		}
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 15;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		Lighting.AddLight(base.Projectile.Center, 0.4f, 0.2f, 0.4f);
		for (int i = 0; i < 5; i++)
		{
			Dust obj = Main.dust[Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, Main.rand.NextBool(3) ? 56 : 242, base.Projectile.velocity.X, base.Projectile.velocity.Y, 100)];
			obj.velocity = Vector2.Zero;
			obj.position -= base.Projectile.velocity / 5f * (float)i;
			obj.noGravity = true;
			obj.scale = 0.8f;
			obj.noLight = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 90);
	}
}
