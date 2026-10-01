using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class InfernalKrisProjectile : ModProjectile, ILocalizedModType, IModType
{
	private bool hasSpawnedCinders;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/InfernalKris";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 2;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 280)
		{
			base.Projectile.rotation += 0.4f * (float)base.Projectile.direction;
			float minScale = 1.9f;
			float maxScale = 2.5f;
			int dust = Dust.NewDust(base.Projectile.position - new Vector2(10f, 10f), 30, 30, 6, base.Projectile.velocity.X, base.Projectile.velocity.Y, 0, default(Color), Main.rand.NextFloat(minScale, maxScale));
			Main.dust[dust].noGravity = true;
		}
		else
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		}
		base.Projectile.velocity.Y += 0.01f;
		if (base.Projectile.velocity.Y > 10f)
		{
			base.Projectile.velocity.Y = 10f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		int debuffTime = 60 * (base.Projectile.Calamity().stealthStrike ? Main.rand.Next(4, 8) : Main.rand.Next(3, 6));
		target.AddBuff(24, debuffTime);
		if (base.Projectile.Calamity().stealthStrike)
		{
			StealthEffect();
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		int debuffTime = 60 * (base.Projectile.Calamity().stealthStrike ? Main.rand.Next(4, 8) : Main.rand.Next(3, 6));
		target.AddBuff(24, debuffTime);
		if (base.Projectile.Calamity().stealthStrike)
		{
			StealthEffect();
		}
	}

	private void StealthEffect()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		hasSpawnedCinders = true;
		int sparkCount = Main.rand.Next(4, 6);
		for (int i = 0; i < sparkCount; i++)
		{
			Vector2 sparkVelocity = Main.rand.NextVector2Circular(1f, 3f);
			sparkVelocity = sparkVelocity.SafeNormalize(Vector2.UnitY) * 3f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, sparkVelocity, ModContent.ProjectileType<InfernalKrisCinder>(), (int)((float)base.Projectile.damage * 0.5f), 0f, base.Projectile.owner);
		}
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<InfernalKrisExplosion>(), (int)((float)base.Projectile.damage * 0.5f), 0f, base.Projectile.owner);
		SoundEngine.PlaySound(in SoundID.Item74, base.Projectile.position);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().stealthStrike)
		{
			Color glowColour = default(Color);
			((Color)(ref glowColour))._002Ector(255, 215, 100, 100);
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], glowColour);
			float minScale = 1.9f;
			float maxScale = 2.5f;
			int dust = Dust.NewDust(base.Projectile.position, 10, 10, 6, base.Projectile.velocity.X, base.Projectile.velocity.Y, 0, default(Color), Main.rand.NextFloat(minScale, maxScale));
			Main.dust[dust].noGravity = true;
		}
		else
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		}
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position + base.Projectile.velocity, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		base.Projectile.ai[0] = 0f;
		base.Projectile.ai[1] = 0f;
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			if (oldVelocity.X < 0f)
			{
				base.Projectile.ai[0] = 1f;
			}
			if (oldVelocity.X > 0f)
			{
				base.Projectile.ai[0] = -1f;
			}
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			if (oldVelocity.Y < 0f)
			{
				base.Projectile.ai[1] = 1f;
			}
			if (oldVelocity.Y > 0f)
			{
				base.Projectile.ai[1] = -1f;
			}
		}
		SoundEngine.PlaySound(in SoundID.Item74, base.Projectile.position);
		base.Projectile.Kill();
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		if (!base.Projectile.Calamity().stealthStrike || hasSpawnedCinders)
		{
			return;
		}
		int sparkCount = Main.rand.Next(4, 6);
		for (int i = 0; i < sparkCount; i++)
		{
			Vector2 sparkVelocity = Main.rand.NextVector2Circular(1f, 3f);
			sparkVelocity = sparkVelocity.SafeNormalize(Vector2.UnitY) * 3f;
			if (base.Projectile.ai[0] != 0f)
			{
				sparkVelocity.X *= -1f;
			}
			if (base.Projectile.ai[1] != 0f)
			{
				sparkVelocity.Y *= -1f;
			}
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, sparkVelocity, ModContent.ProjectileType<InfernalKrisCinder>(), (int)((float)base.Projectile.damage * 0.5f), 0f, base.Projectile.owner);
		}
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<InfernalKrisExplosion>(), (int)((float)base.Projectile.damage * 0.5f), 0f, base.Projectile.owner);
		SoundEngine.PlaySound(in SoundID.Item74, base.Projectile.position);
	}
}
