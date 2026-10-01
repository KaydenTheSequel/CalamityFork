using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ProfanedPartisanProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/ProfanedPartisan";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 9;
		base.Projectile.timeLeft = 600;
		base.Projectile.extraUpdates = 3;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 12;
	}

	public override void AI()
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] < 0.4f)
		{
			base.Projectile.ai[0] += 0.1f;
		}
		else
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI));
		if (base.Projectile.spriteDirection == 1)
		{
			base.Projectile.rotation += MathHelper.ToRadians(45f);
			base.DrawOffsetX = -26;
			base.DrawOriginOffsetX = 13f;
			base.DrawOriginOffsetY = 2;
		}
		if (base.Projectile.spriteDirection == -1)
		{
			base.Projectile.rotation -= MathHelper.ToRadians(45f);
			base.DrawOffsetX = 2;
			base.DrawOriginOffsetX = -13f;
			base.DrawOriginOffsetY = 2;
		}
		if (Main.rand.NextBool(3))
		{
			int d = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, base.Projectile.velocity.X, base.Projectile.velocity.Y, 100, default(Color), 1.1f);
			Main.dust[d].position = base.Projectile.Center;
			Dust obj = Main.dust[d];
			obj.velocity *= 0.3f;
			Dust obj2 = Main.dust[d];
			obj2.velocity += base.Projectile.velocity * 0.85f;
		}
		Lighting.AddLight(base.Projectile.Center, 1f, 0.8f, 0.2f);
		if (base.Projectile.Calamity().stealthStrike)
		{
			Vector2 spearPosition = default(Vector2);
			((Vector2)(ref spearPosition))._002Ector(base.Projectile.Center.X + Main.rand.NextFloat(-15f, 15f), base.Projectile.Center.Y + Main.rand.NextFloat(-15f, 15f));
			Vector2 spearSpeed = base.Projectile.velocity;
			if (base.Projectile.timeLeft % 18 == 0)
			{
				int projID = ModContent.ProjectileType<ProfanedPartisanSpear>();
				int spearDamage = (int)((float)base.Projectile.damage * 0.4f);
				float spearKB = 1f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spearPosition, spearSpeed, projID, spearDamage, spearKB, base.Projectile.owner);
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 20; i++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 50, default(Color), 2.6f);
		}
		SoundEngine.PlaySound(in SoundID.Item45, base.Projectile.position);
		int projID = ModContent.ProjectileType<PartisanExplosion>();
		int explosionDamage = (int)((float)base.Projectile.damage * 0.8f);
		float explosionKB = 8f;
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, projID, explosionDamage, explosionKB, base.Projectile.owner);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
	}
}
