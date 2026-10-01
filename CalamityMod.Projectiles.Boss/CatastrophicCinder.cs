using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Graphics.Metaballs;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class CatastrophicCinder : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = (CalamityWorld.death ? 4 : 3);
		base.Projectile.timeLeft = 300;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += 0.2f * (float)base.Projectile.direction;
		Lighting.AddLight(base.Projectile.Center, 0.25f, 0f, 0f);
		CatastropheMetaball.Particle particle = CatastropheMetaball.SpawnParticle(base.Projectile.Center + base.Projectile.velocity, -base.Projectile.velocity, TextureAssets.Projectile[base.Type].Width() * 2);
		particle.rotation = base.Projectile.rotation;
		particle.TextureToUse = TextureAssets.Projectile[base.Type].Value;
		particle.SizeScaling = 0.5f;
		base.Projectile.Opacity = 0f;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 10; i++)
		{
			CatastropheMetaball.SpawnParticle(base.Projectile.Center + base.Projectile.velocity, Main.rand.NextVector2Circular(4f, 4f), 16f).SizeScaling = 0.9f;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		return base.Projectile.penetrate-- == 0;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 120);
		}
	}
}
