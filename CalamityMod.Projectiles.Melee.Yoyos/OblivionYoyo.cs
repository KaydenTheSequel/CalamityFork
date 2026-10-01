using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Yoyos;

public class OblivionYoyo : ModProjectile
{
	public const int MaxUpdates = 2;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Oblivion>();

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.YoyosLifeTimeMultiplier[base.Type] = -1f;
		ProjectileID.Sets.YoyosMaximumRange[base.Type] = Oblivion.Reach;
		ProjectileID.Sets.YoyosTopSpeed[base.Type] = Oblivion.Speed / 2f;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.aiStyle = 99;
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
	}

	public override void AI()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.MagnetSphereHitscan(base.Projectile, 300f, 9f, 48f, 5, ModContent.ProjectileType<SeethingDischargeBrimstoneBarrage>(), 0.5, attackMultiple: false, base.Projectile.DamageType);
		Vector2 val = base.Projectile.position - Main.player[base.Projectile.owner].position;
		if (((Vector2)(ref val)).Length() > 3200f)
		{
			base.Projectile.Kill();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 240);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
