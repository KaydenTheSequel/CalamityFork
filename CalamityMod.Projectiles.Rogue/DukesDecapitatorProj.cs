using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class DukesDecapitatorProj : ModProjectile, ILocalizedModType, IModType
{
	private float rotationAmount = 1.5f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/DukesDecapitator";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 600;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		Main.player[base.Projectile.owner].Calamity();
		if (base.Projectile.velocity.X != 0f || base.Projectile.velocity.Y != 0f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.99f;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] == 5f)
		{
			base.Projectile.tileCollide = true;
		}
		if (base.Projectile.ai[0] % 15f == 0f && rotationAmount > 0f)
		{
			rotationAmount -= 0.05f;
			if (base.Projectile.Calamity().stealthStrike && base.Projectile.owner == Main.myPlayer)
			{
				float velocityX = Main.rand.NextFloat(-0.8f, 0.8f);
				float velocityY = Main.rand.NextFloat(-0.8f, -0.8f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, velocityX, velocityY, ModContent.ProjectileType<DukesDecapitatorBubble>(), (int)((double)base.Projectile.damage * 0.8), base.Projectile.knockBack, base.Projectile.owner);
			}
		}
		if (rotationAmount <= 0f)
		{
			base.Projectile.Kill();
		}
		base.Projectile.rotation += rotationAmount;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 120);
		base.Projectile.velocity = Vector2.Zero;
		rotationAmount -= 0.05f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 120);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 20; i++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 49, base.Projectile.velocity.X * 0.25f, base.Projectile.velocity.Y * 0.25f, 0, new Color(255, 255, 255), 0.75f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
