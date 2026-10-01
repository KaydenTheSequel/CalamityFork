using System;
using CalamityMod.NPCs.Providence;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class MiniGuardianSpear : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/Boss/HolySpear";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.SummonTagDamageMultiplier[base.Type] = 0.5f;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.alpha = 100;
		base.Projectile.timeLeft = 300;
		base.Projectile.extraUpdates = 1;
		base.Projectile.minion = true;
		base.Projectile.scale = 0.9f;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	private void handleAI(bool miniGuardianPscAttack)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		if (miniGuardianPscAttack)
		{
			return;
		}
		if (base.Projectile.timeLeft == 300)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.01f;
		}
		else if (base.Projectile.timeLeft > 250)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 1.1f;
		}
		else if (base.Projectile.timeLeft == 250)
		{
			Projectile projectile3 = base.Projectile;
			projectile3.velocity *= 5f;
		}
		if (base.Projectile.timeLeft > 250)
		{
			return;
		}
		float num535 = base.Projectile.position.X;
		float num536 = base.Projectile.position.Y;
		float num537 = 3000f;
		bool flag19 = false;
		NPC ownerMinionAttackTargetNPC2 = base.Projectile.OwnerMinionAttackTargetNPC;
		if (ownerMinionAttackTargetNPC2 != null && ownerMinionAttackTargetNPC2.CanBeChasedBy(base.Projectile))
		{
			float num539 = ownerMinionAttackTargetNPC2.position.X + (float)(ownerMinionAttackTargetNPC2.width / 2);
			float num540 = ownerMinionAttackTargetNPC2.position.Y + (float)(ownerMinionAttackTargetNPC2.height / 2);
			float num541 = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - num539) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - num540);
			if (num541 < num537)
			{
				num537 = num541;
				num535 = num539;
				num536 = num540;
				flag19 = true;
			}
		}
		if (!flag19)
		{
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Main.npc[i].CanBeChasedBy(base.Projectile))
				{
					float num543 = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
					float num544 = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
					float num545 = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - num543) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - num544);
					if (num545 < num537)
					{
						num537 = num545;
						num535 = num543;
						num536 = num544;
						flag19 = true;
					}
				}
			}
		}
		if (flag19 && base.Projectile.ai[1] == 0f)
		{
			float num550 = 24f;
			Vector2 vector43 = base.Projectile.Center;
			float num551 = num535 - vector43.X;
			float num552 = num536 - vector43.Y;
			float num553 = (float)Math.Sqrt(num551 * num551 + num552 * num552);
			if (num553 < 100f)
			{
				num550 = 28f;
			}
			num553 = num550 / num553;
			num551 *= num553;
			num552 *= num553;
			base.Projectile.velocity.X = (base.Projectile.velocity.X * 14f + num551) / 15f;
			base.Projectile.velocity.Y = (base.Projectile.velocity.Y * 14f + num552) / 15f;
		}
	}

	public override void AI()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		base.Projectile.damage = (int)Owner.GetTotalDamage<SummonDamageClass>().ApplyTo(base.Projectile.originalDamage);
		bool psc = base.Projectile.ai[0] > 0f;
		int num469 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ProvUtils.GetDustID(!Main.dayTime & psc), 0f, 0f, 100, default(Color), (!Main.dayTime & psc) ? 0.5f : 1f);
		Main.dust[num469].noGravity = true;
		Dust obj = Main.dust[num469];
		obj.velocity *= 0f;
		handleAI(psc && base.Projectile.ai[1] > 0f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (base.Projectile.ai[1] == 0f && base.Projectile.timeLeft > 250)
		{
			return false;
		}
		return null;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		bool psc = base.Projectile.ai[0] > 0f;
		base.Projectile.DrawBackglow(ProvUtils.GetColorBasedOnEnrage(!Main.dayTime & psc, base.Projectile.alpha, Outline: true), 4f, TextureAssets.Projectile[base.Type].Value, null, (SpriteEffects)0);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], ProvUtils.GetColorBasedOnEnrage(!Main.dayTime & psc, base.Projectile.alpha));
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
		if (!Main.rand.NextBool(3))
		{
			return;
		}
		base.Projectile.position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2);
		base.Projectile.width = (base.Projectile.height = 200);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		bool psc = base.Projectile.ai[0] > 0f;
		bool shouldAdjust = !Main.dayTime & psc;
		int dustID = ProvUtils.GetDustID(shouldAdjust);
		for (int num621 = 0; num621 < 4; num621++)
		{
			int num622 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID, 0f, 0f, 100, default(Color), (!Main.dayTime & psc) ? 0.5f : 2f);
			Dust obj = Main.dust[num622];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[num622].scale = 0.5f;
				Main.dust[num622].fadeIn = (shouldAdjust ? 0.9f : (1f + (float)Main.rand.Next(10) * 0.1f));
			}
		}
		for (int i = 0; i < (shouldAdjust ? 8 : 12); i++)
		{
			int num624 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID, 0f, 0f, 100, default(Color), (!Main.dayTime & psc) ? 1.25f : 3f);
			Main.dust[num624].noGravity = true;
			Dust obj2 = Main.dust[num624];
			obj2.velocity *= 5f;
			Main.dust[num624].fadeIn = (shouldAdjust ? 0.9f : Main.dust[num624].fadeIn);
			num624 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID, 0f, 0f, 100, default(Color), (!Main.dayTime & psc) ? 1f : 2f);
			Dust obj3 = Main.dust[num624];
			obj3.velocity *= 2f;
			Main.dust[num624].fadeIn = (shouldAdjust ? 0.9f : Main.dust[num624].fadeIn);
		}
	}
}
