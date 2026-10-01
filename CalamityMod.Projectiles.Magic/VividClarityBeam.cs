using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class VividClarityBeam : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 10;
		base.Projectile.extraUpdates = 100;
		base.Projectile.timeLeft = 300;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		Vector2 rotateVector = default(Vector2);
		((Vector2)(ref rotateVector))._002Ector(5f, 10f);
		if (base.Projectile.position.Y > Main.player[base.Projectile.owner].position.Y - 50f)
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] == 48f)
		{
			base.Projectile.ai[0] = 0f;
		}
		else
		{
			for (int i = 0; i < 2; i++)
			{
				Vector2 dustRotation = Vector2.UnitX * -12f;
				dustRotation = -Vector2.UnitY.RotatedBy(base.Projectile.ai[0] * ((float)Math.PI / 24f) + (float)i * (float)Math.PI) * rotateVector - base.Projectile.rotation.ToRotationVector2() * 10f;
				int exo = Dust.NewDust(base.Projectile.Center, 0, 0, 66, 0f, 0f, 160, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
				Main.dust[exo].scale = 0.75f;
				Main.dust[exo].noGravity = true;
				Main.dust[exo].position = base.Projectile.Center + dustRotation;
				Main.dust[exo].velocity = base.Projectile.velocity;
			}
		}
		base.Projectile.localAI[1]++;
		if (base.Projectile.localAI[1] >= 49f && base.Projectile.owner == Main.myPlayer)
		{
			base.Projectile.localAI[1] = 0f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<VividOrb>(), (int)((float)base.Projectile.damage * 0.66f), base.Projectile.knockBack, base.Projectile.owner);
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 4f)
		{
			for (int j = 0; j < 2; j++)
			{
				Vector2 projPos = base.Projectile.position;
				projPos -= base.Projectile.velocity * ((float)j * 0.25f);
				int exod = Dust.NewDust(projPos, 1, 1, 66, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
				Main.dust[exod].noGravity = true;
				Main.dust[exod].position = projPos;
				Main.dust[exod].scale = (float)Main.rand.Next(70, 110) * 0.013f;
				Dust obj = Main.dust[exod];
				obj.velocity *= 0.1f;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.penetrate--;
		if (base.Projectile.penetrate <= 0)
		{
			base.Projectile.Kill();
		}
		else
		{
			if (base.Projectile.velocity.X != oldVelocity.X)
			{
				base.Projectile.velocity.X = 0f - oldVelocity.X;
			}
			if (base.Projectile.velocity.Y != oldVelocity.Y)
			{
				base.Projectile.velocity.Y = 0f - oldVelocity.Y;
			}
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}
}
