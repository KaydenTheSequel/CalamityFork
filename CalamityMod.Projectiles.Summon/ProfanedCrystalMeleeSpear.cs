using System;
using System.IO;
using CalamityMod.NPCs.Providence;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class ProfanedCrystalMeleeSpear : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/Boss/HolySpear";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.SummonTagDamageMultiplier[base.Type] = 0.6f;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 50;
		base.Projectile.minion = true;
		base.Projectile.alpha = 100;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 1.06f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Lighting.AddLight(base.Projectile.Center, 1f, 0.2f, 0f);
		Player Owner = Main.player[base.Projectile.owner];
		base.Projectile.damage = (int)Owner.GetTotalDamage<SummonDamageClass>().ApplyTo(base.Projectile.originalDamage);
	}

	public override bool PreAI()
	{
		base.Projectile.Calamity().overridesMinionDamagePrevention = true;
		if (base.Projectile.timeLeft == 50 && base.Projectile.ai[1] == 1f)
		{
			base.Projectile.penetrate = 3;
		}
		return true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (base.Projectile.timeLeft > 133 && base.Projectile.ai[0] < 2f)
		{
			return false;
		}
		return null;
	}

	public override bool CanHitPvp(Player target)
	{
		if (base.Projectile.timeLeft > 133)
		{
			return base.Projectile.ai[0] == 2f;
		}
		return true;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 1f && base.Projectile.penetrate == 1)
		{
			handleSpecialHit(target.Center);
		}
		int chance = ((base.Projectile.ai[0] == 2f) ? 20 : (10 * base.Projectile.penetrate));
		Main.player[base.Projectile.owner].Calamity().rollBabSpears(chance, target.chaseable);
	}

	private void handleSpecialHit(Vector2 targCenter)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int i = 0; i < 10; i++)
			{
				float startDist = Main.rand.NextFloat(450f, 500f);
				Vector2 startDir = Main.rand.NextVector2Unit();
				Vector2 startPoint = targCenter + startDir * startDist;
				float speed = Main.rand.NextFloat(15f, 18f);
				Vector2 velocity = startDir * (0f - speed);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), startPoint, velocity, ModContent.ProjectileType<ProfanedCrystalMeleeSpear>(), (int)(0.25f * (float)base.Projectile.damage), 0f, base.Projectile.owner, 2f);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.DrawBackglow(ProvUtils.GetColorBasedOnEnrage(!Main.dayTime, base.Projectile.alpha, Outline: true), 4f, TextureAssets.Projectile[base.Type].Value, null, (SpriteEffects)0);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], ProvUtils.GetColorBasedOnEnrage(!Main.dayTime, base.Projectile.alpha));
		return false;
	}

	private void onHit()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item74, base.Projectile.position);
		if (!Main.rand.NextBool() && !Main.rand.NextBool(3))
		{
			return;
		}
		base.Projectile.position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2);
		base.Projectile.width = (base.Projectile.height = 200);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		int dust = ProvUtils.GetDustID(!Main.dayTime);
		for (int num621 = 0; num621 < 4; num621++)
		{
			int num622 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dust, 0f, 0f, 100, default(Color), Main.dayTime ? 2f : 0.5f);
			Dust obj = Main.dust[num622];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[num622].scale = 0.5f;
				Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int i = 0; i < 12; i++)
		{
			int num624 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dust, 0f, 0f, 100, default(Color), Main.dayTime ? 3f : 0.75f);
			Main.dust[num624].noGravity = true;
			Dust obj2 = Main.dust[num624];
			obj2.velocity *= 5f;
			num624 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dust, 0f, 0f, 100, default(Color), Main.dayTime ? 2f : 0.5f);
			Dust obj3 = Main.dust[num624];
			obj3.velocity *= 2f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		onHit();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		onHit();
	}
}
