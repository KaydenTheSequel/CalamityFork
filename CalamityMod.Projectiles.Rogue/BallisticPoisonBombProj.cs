using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class BallisticPoisonBombProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 200;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(6))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 14, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
		base.Projectile.StickToTiles(ignorePlatforms: true, stickToEverything: false);
		if (base.Projectile.owner == Main.myPlayer && base.Projectile.timeLeft <= 3)
		{
			base.Projectile.tileCollide = false;
			base.Projectile.ai[1] = 0f;
			base.Projectile.alpha = 255;
			base.Projectile.ExpandHitboxBy(128);
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 10f)
		{
			base.Projectile.ai[0] = 10f;
			if (base.Projectile.velocity.Y == 0f && base.Projectile.velocity.X != 0f)
			{
				base.Projectile.velocity.X *= 0.97f;
				if (Math.Abs(base.Projectile.velocity.X) < 0.01f)
				{
					base.Projectile.velocity.X = 0f;
					base.Projectile.netUpdate = true;
				}
			}
			base.Projectile.velocity.Y += 0.2f;
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(128);
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int s = 0; s < 3; s++)
			{
				Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<BallisticPoisonBombSpike>(), base.Projectile.damage, 0f, base.Projectile.owner);
			}
			int cloudAmt = (base.Projectile.Calamity().stealthStrike ? Main.rand.Next(7, 11) : Main.rand.Next(3, 6));
			for (int c = 0; c < cloudAmt; c++)
			{
				Vector2 velocity2 = CalamityUtils.RandomVelocity(100f, 10f, 200f, 0.01f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity2, ModContent.ProjectileType<BallisticPoisonCloud>(), (int)((float)base.Projectile.damage * 0.6f), 0f, base.Projectile.owner, 0f, base.Projectile.Calamity().stealthStrike ? 1f : 0f);
			}
		}
		for (int d = 0; d < 5; d++)
		{
			int boom = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 14, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[boom];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[boom].scale = 0.5f;
				Main.dust[boom].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int i = 0; i < 9; i++)
		{
			int fire = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
			Main.dust[fire].noGravity = true;
			Dust obj2 = Main.dust[fire];
			obj2.velocity *= 5f;
			fire = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[fire];
			obj3.velocity *= 2f;
		}
		if (!Main.dedServ)
		{
			Vector2 goreSource = base.Projectile.Center;
			int goreAmt = 3;
			Vector2 source = default(Vector2);
			((Vector2)(ref source))._002Ector(goreSource.X - 24f, goreSource.Y - 24f);
			for (int goreIndex = 1; goreIndex <= goreAmt; goreIndex++)
			{
				float velocityMult = 0.33f * (float)goreIndex;
				int type = Main.rand.Next(61, 64);
				int smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, Main.rand.NextVector2CircularEdge(2f, 2f), type);
				Gore obj4 = Main.gore[smoke];
				obj4.velocity *= velocityMult;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(70, 180);
		base.Projectile.Kill();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(70, 180);
		base.Projectile.Kill();
	}
}
