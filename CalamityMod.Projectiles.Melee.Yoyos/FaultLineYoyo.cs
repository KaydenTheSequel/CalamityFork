using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Yoyos;

public class FaultLineYoyo : ModProjectile
{
	public const int MaxUpdates = 2;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<FaultLine>();

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.YoyosLifeTimeMultiplier[base.Type] = -1f;
		ProjectileID.Sets.YoyosMaximumRange[base.Type] = FaultLine.Reach;
		ProjectileID.Sets.YoyosTopSpeed[base.Type] = FaultLine.Speed / 2f;
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
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, Main.rand.NextBool(3) ? 16 : 127, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
		Vector2 velocity;
		if (Main.rand.NextBool(8) && !Main.dedServ)
		{
			IEntitySource source_FromAI = base.Projectile.GetSource_FromAI();
			Vector2 position = base.Projectile.position;
			velocity = default(Vector2);
			int smoke = Gore.NewGore(source_FromAI, position, velocity, Main.rand.Next(375, 378), 0.5f);
			Main.gore[smoke].behindTiles = true;
		}
		velocity = base.Projectile.position - Main.player[base.Projectile.owner].position;
		if (((Vector2)(ref velocity)).Length() > 3200f)
		{
			base.Projectile.Kill();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(323, 240);
		if (base.Projectile.owner == Main.myPlayer)
		{
			int boom = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<FuckYou>(), (int)((float)base.Projectile.damage * 0.5f), base.Projectile.knockBack, base.Projectile.owner, 0f, 0.85f + Main.rand.NextFloat() * 1.15f);
			if (boom.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[boom].DamageType = DamageClass.MeleeNoSpeed;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
