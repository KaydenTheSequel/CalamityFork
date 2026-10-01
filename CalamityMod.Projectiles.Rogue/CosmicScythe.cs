using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class CosmicScythe : ModProjectile, ILocalizedModType, IModType
{
	private int originalDamage;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/Boss/SignusScythe";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 26;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 400;
		base.Projectile.alpha = 100;
		base.Projectile.penetrate = 5;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += 0.5f * (float)base.Projectile.direction;
		int shadow = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 100);
		Main.dust[shadow].noGravity = true;
		Dust obj = Main.dust[shadow];
		obj.velocity *= 0f;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.95f;
		if (base.Projectile.timeLeft == 400)
		{
			originalDamage = base.Projectile.damage;
			base.Projectile.damage = 0;
		}
		if (base.Projectile.timeLeft <= 375)
		{
			if (base.Projectile.timeLeft > 350)
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 1.06f;
			}
			base.Projectile.damage = (int)((double)originalDamage * 1.25);
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 300f, 12f, 20f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Laceration>(), 240);
		base.Projectile.Kill();
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		base.Projectile.ExpandHitboxBy(50);
		for (int d = 0; d < 4; d++)
		{
			int shadow = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 27, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[shadow];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[shadow].scale = 0.5f;
				Main.dust[shadow].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int i = 0; i < 12; i++)
		{
			int shadow2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 27, 0f, 0f, 100, default(Color), 3f);
			Main.dust[shadow2].noGravity = true;
			Dust obj2 = Main.dust[shadow2];
			obj2.velocity *= 5f;
			shadow2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 27, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[shadow2];
			obj3.velocity *= 2f;
		}
	}
}
