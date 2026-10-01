using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class VividBolt : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.extraUpdates = 100;
		base.Projectile.penetrate = -1;
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 30;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 6;
	}

	public override void AI()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		Vector2 rotateVector = default(Vector2);
		((Vector2)(ref rotateVector))._002Ector(5f, 10f);
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
				Main.dust[exo].scale = 0.33f;
				Main.dust[exo].noGravity = true;
				Main.dust[exo].position = base.Projectile.Center + dustRotation;
				Main.dust[exo].velocity = base.Projectile.velocity;
			}
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
				Main.dust[exod].scale = (float)Main.rand.Next(70, 110) * 0.01f;
				Dust obj = Main.dust[exod];
				obj.velocity *= 0.1f;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 180);
	}
}
