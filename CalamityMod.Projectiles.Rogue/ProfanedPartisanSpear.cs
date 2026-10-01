using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ProfanedPartisanSpear : ModProjectile, ILocalizedModType, IModType
{
	public int timer;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.timeLeft = 600;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] != 1f)
		{
			if (base.Projectile.velocity.X != oldVelocity.X)
			{
				base.Projectile.velocity.X = 0f - oldVelocity.X;
			}
			if (base.Projectile.velocity.Y != oldVelocity.Y)
			{
				base.Projectile.velocity.Y = 0f - oldVelocity.Y;
			}
			base.Projectile.ai[1] = 1f;
			base.Projectile.ai[0] = 1f;
			base.Projectile.extraUpdates = 2;
			if (base.Projectile.timeLeft > 280)
			{
				base.Projectile.timeLeft = 280;
			}
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
		OnHitEffects();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
		OnHitEffects();
	}

	private void OnHitEffects()
	{
		if (base.Projectile.ai[1] != 1f)
		{
			base.Projectile.velocity.X *= -1f;
			base.Projectile.velocity.Y *= -1f;
			base.Projectile.ai[1] = 1f;
			base.Projectile.ai[0] = 1f;
			base.Projectile.extraUpdates = 2;
			if (base.Projectile.timeLeft > 280)
			{
				base.Projectile.timeLeft = 280;
			}
		}
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.ai[0] == 1f)
		{
			timer++;
		}
		if (timer >= 5)
		{
			base.Projectile.penetrate = 1;
		}
		if (timer >= 10)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 300f, 7f, 20f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
