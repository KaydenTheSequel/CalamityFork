using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class VoidofExtinctionCrit : ModProjectile, ILocalizedModType, IModType
{
	private static float ExplosionRadius = 300f;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 300;
		base.Projectile.height = 300;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = AverageDamageClass.Instance;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		for (int i = 0; i <= 30; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(4) ? 218 : ModContent.DustType<BrimstoneFlame>(), Utils.RotatedByRandom(new Vector2(5f, 5f), MathHelper.ToRadians(360f)) * Main.rand.NextFloat(1.1f, 2.2f), 0, default(Color), Main.rand.NextFloat(2.8f, 3.4f));
			dust.shader = GameShaders.Armor.GetSecondaryShader(player.cFace, player);
			dust.noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 360);
		SoundStyle style = SoundID.Item89 with
		{
			Volume = 0.5f,
			PitchVariance = 0.4f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, ExplosionRadius, targetHitbox);
	}

	public override bool? CanDamage()
	{
		return base.CanDamage();
	}

	public override bool? CanCutTiles()
	{
		return false;
	}
}
