using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class PenumbraBomb : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/Penumbra";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 250;
		base.Projectile.extraUpdates = 2;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
	}

	public override void AI()
	{
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0]++;
		if (base.Projectile.direction == 1)
		{
			base.DrawOffsetX = -4;
			base.DrawOriginOffsetX = -5f;
		}
		else
		{
			base.DrawOffsetX = -11;
			base.DrawOriginOffsetX = 5f;
		}
		if (base.Projectile.alpha > 10)
		{
			base.Projectile.alpha -= 7;
		}
		else
		{
			base.Projectile.alpha = 10;
		}
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI)) + MathHelper.ToRadians(180f) * (float)base.Projectile.direction;
		float dfreq = (base.Projectile.Calamity().stealthStrike ? 8f : 4f);
		if (base.Projectile.ai[0] == dfreq)
		{
			Vector2 dustspeed = base.Projectile.velocity * Main.rand.NextFloat(0.5f, 0.8f);
			int d = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 54, dustspeed.X, dustspeed.Y, 0, new Color(38, 30, 43), 1.4f);
			Main.dust[d].velocity = dustspeed;
			if (base.Projectile.Calamity().stealthStrike)
			{
				Vector2 dustspeed2 = default(Vector2);
				((Vector2)(ref dustspeed2))._002Ector(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f));
				int d2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 54, dustspeed2.X, dustspeed2.Y, 0, new Color(38, 30, 43), 1.3f);
				Main.dust[d2].velocity = dustspeed2;
			}
			base.Projectile.ai[0] = 0f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(189, 180);
		target.AddBuff(ModContent.BuffType<Nightwither>(), 180);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		int projAmt = (base.Projectile.Calamity().stealthStrike ? 8 : 6);
		for (int i = 0; i < projAmt; i++)
		{
			Vector2 SoulSpeed = Vector2.UnitX.RotatedBy((float)Math.PI * 2f * (float)i / (float)projAmt) * 13f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, SoulSpeed, ModContent.ProjectileType<PenumbraSoul>(), (int)((float)base.Projectile.damage * 0.15f), 3f, base.Projectile.owner);
		}
		int maxDust = (base.Projectile.Calamity().stealthStrike ? 100 : 70);
		Vector2 dustspeed = default(Vector2);
		for (int j = 0; j < maxDust; j++)
		{
			((Vector2)(ref dustspeed))._002Ector(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 54, dustspeed.X, dustspeed.Y, 0, new Color(38, 30, 43), 1.6f);
		}
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		base.Projectile.ExpandHitboxBy(110);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.damage /= 2;
		base.Projectile.Damage();
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
