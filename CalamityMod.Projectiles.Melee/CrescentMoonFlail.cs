using System;
using System.Linq;
using CalamityMod.Particles;
using CalamityMod.Systems.Mechanic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class CrescentMoonFlail : ModProjectile, ILocalizedModType, IModType
{
	public int moonCounter = 6;

	public int burstStage = -1;

	private bool hasFired;

	private bool hasStarbits;

	private StarburstEntity starburst1;

	private StarburstEntity starburst2;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 8;
		base.Projectile.timeLeft = 6000;
		base.Projectile.MaxUpdates = 2;
	}

	public override void AI()
	{
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0591: Unknown result type (might be due to invalid IL or missing references)
		//IL_0596: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ade: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af3: Unknown result type (might be due to invalid IL or missing references)
		//IL_072b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0737: Unknown result type (might be due to invalid IL or missing references)
		//IL_073c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0746: Unknown result type (might be due to invalid IL or missing references)
		//IL_075e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0763: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_0655: Unknown result type (might be due to invalid IL or missing references)
		//IL_065f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0664: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a49: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0860: Unknown result type (might be due to invalid IL or missing references)
		//IL_086b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0872: Unknown result type (might be due to invalid IL or missing references)
		//IL_0881: Unknown result type (might be due to invalid IL or missing references)
		//IL_088b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0891: Unknown result type (might be due to invalid IL or missing references)
		//IL_0892: Unknown result type (might be due to invalid IL or missing references)
		//IL_0899: Unknown result type (might be due to invalid IL or missing references)
		//IL_089e: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08de: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0900: Unknown result type (might be due to invalid IL or missing references)
		//IL_0905: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0926: Unknown result type (might be due to invalid IL or missing references)
		//IL_0931: Unknown result type (might be due to invalid IL or missing references)
		//IL_0938: Unknown result type (might be due to invalid IL or missing references)
		//IL_0947: Unknown result type (might be due to invalid IL or missing references)
		//IL_0951: Unknown result type (might be due to invalid IL or missing references)
		//IL_0957: Unknown result type (might be due to invalid IL or missing references)
		//IL_0958: Unknown result type (might be due to invalid IL or missing references)
		//IL_095f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0964: Unknown result type (might be due to invalid IL or missing references)
		//IL_0969: Unknown result type (might be due to invalid IL or missing references)
		//IL_0977: Unknown result type (might be due to invalid IL or missing references)
		//IL_097c: Unknown result type (might be due to invalid IL or missing references)
		//IL_097e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0983: Unknown result type (might be due to invalid IL or missing references)
		//IL_098b: Unknown result type (might be due to invalid IL or missing references)
		//IL_099f: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a27: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		player.heldProj = base.Projectile.whoAmI;
		player.itemAnimation = 5;
		player.itemTime = 5;
		if (!player.channel && !hasFired)
		{
			base.Projectile.velocity = player.DirectionTo(player.Calamity().mouseWorld) * 25f;
			base.Projectile.Center = player.Center + base.Projectile.velocity * 5f;
			hasFired = true;
			base.Projectile.ai[0] = 0f;
			hasStarbits = player.Calamity().AvaliableStarburst >= 20;
			if (hasStarbits)
			{
				StarburstEntity star1 = player.Calamity().StarburstEntities.FirstOrDefault((StarburstEntity x) => x.AICooldown <= 0 && x.value == 10, null);
				if (star1 != null)
				{
					star1.AICooldown = 1;
					StarburstEntity star2 = player.Calamity().StarburstEntities.FirstOrDefault((StarburstEntity x) => x.AICooldown <= 0 && x.value == 10, null);
					if (star2 != null)
					{
						star2.AICooldown = 1;
						starburst1 = star1;
						starburst2 = star2;
					}
				}
			}
		}
		if (base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = player.DirectionTo(player.Calamity().mouseWorld);
		}
		if (!hasFired)
		{
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy(0.3f * (float)player.direction).SafeNormalize(Vector2.One) * 50f;
			base.Projectile.Center = player.Center;
			base.Projectile.ai[0]++;
			if (player.miscCounter % 13 == 0 && base.Projectile.FinalExtraUpdate())
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 5f, ModContent.ProjectileType<CrescentMoonProj>(), (int)((float)base.Projectile.damage * 0.1f), 0f, base.Projectile.owner);
			}
			if (player.miscCounter % 20 == 0 && base.Projectile.FinalExtraUpdate())
			{
				player.Calamity().StratusStarburst++;
			}
		}
		else
		{
			base.Projectile.ai[0]++;
			if (base.Projectile.ai[0] == 30f && burstStage == -1 && hasStarbits && player.Calamity().StratusStarburst >= 20 && player.controlUseItem)
			{
				burstStage = 10;
			}
			if (base.Projectile.ai[0] == 40f)
			{
				if (burstStage == -1)
				{
					for (int i = 0; i < 6; i++)
					{
						int moonDamage = (int)((float)base.Projectile.damage * 0.1f);
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy((float)Math.PI * 2f * ((float)i / 6f + 0.5f)) * 10f, ModContent.ProjectileType<CrescentMoonProj>(), moonDamage, 0f, base.Projectile.owner);
					}
				}
				else
				{
					burstStage = 0;
					SoundEngine.PlaySound(in SoundID.DD2_WitherBeastDeath);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ScorpiusConstellation>(), base.Projectile.damage * 3, 0f, base.Projectile.owner);
					base.Projectile.position = base.Projectile.Center;
					Projectile projectile = base.Projectile;
					projectile.Size *= 7f;
					base.Projectile.Center = base.Projectile.position;
					base.Projectile.damage *= 3;
					base.Projectile.Damage();
					base.Projectile.damage = 0;
					base.Projectile.position = base.Projectile.Center;
					Projectile projectile2 = base.Projectile;
					projectile2.Size *= 0.2f;
					base.Projectile.Center = base.Projectile.position;
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.SkyBlue, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat(-15f, 15f), 0f, 0.25f, 12, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.DeepSkyBlue, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat(-15f, 15f), 0f, 0.2f, 12, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					for (int i2 = 0; i2 < 30; i2++)
					{
						int dustType = Utils.SelectRandom<int>(Main.rand, 109, 111, 132);
						int dust = Dust.NewDust(base.Projectile.Center, 0, 0, dustType);
						Main.dust[dust].noGravity = true;
						Dust obj = Main.dust[dust];
						obj.velocity *= 7f;
					}
					player.Calamity().StratusStarburst -= 20;
					if (starburst1 != null)
					{
						player.Calamity().StarburstEntities.Remove(starburst1);
					}
					if (starburst2 != null)
					{
						player.Calamity().StarburstEntities.Remove(starburst2);
					}
				}
			}
			if (burstStage > 0)
			{
				burstStage--;
			}
			if (base.Projectile.ai[0] > 20f && base.Projectile.Distance(player.Center) < 100f)
			{
				base.Projectile.ai[0] = 501f;
			}
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.DirectionTo(player.Center) * 35f, base.Projectile.ai[0] * 0.00075f);
			if (base.Projectile.Distance(player.Center) < 32f && base.Projectile.ai[0] > 20f)
			{
				base.Projectile.Kill();
			}
			if (hasStarbits)
			{
				float rotation = base.Projectile.DirectionFrom(player.Center).ToRotation() - (float)Math.PI / 2f;
				float distanceMod = MathHelper.SmoothStep(0f, 1f, MathHelper.Min(base.Projectile.ai[0] / 30f, 1f));
				if (burstStage > 0)
				{
					distanceMod = (float)burstStage * 0.1f;
				}
				float playerLerpMod = MathHelper.Min(base.Projectile.ai[0] / 40f, 1f);
				if (base.Projectile.ai[0] <= 40f)
				{
					if (starburst1 != null)
					{
						Vector2 goal = Vector2.Lerp(Main.player[base.Projectile.owner].Center, base.Projectile.Center, playerLerpMod) + Utils.RotatedBy(new Vector2(200f, 0f), (double)rotation, default(Vector2)) * distanceMod;
						starburst1.Velocity = starburst1.Center.DirectionTo(goal) * MathHelper.Min(goal.Distance(starburst1.Center), 64f);
						starburst1.AICooldown = 2;
						StarburstEntity starburstEntity = starburst1;
						starburstEntity.Velocity *= 0.95f;
					}
					if (starburst2 != null)
					{
						Vector2 goal2 = Vector2.Lerp(Main.player[base.Projectile.owner].Center, base.Projectile.Center, playerLerpMod) - Utils.RotatedBy(new Vector2(200f, 0f), (double)rotation, default(Vector2)) * distanceMod;
						starburst2.Velocity = starburst2.Center.DirectionTo(goal2) * MathHelper.Min(goal2.Distance(starburst2.Center), 64f);
						starburst2.AICooldown = 2;
						StarburstEntity starburstEntity2 = starburst2;
						starburstEntity2.Velocity *= 0.95f;
					}
				}
				else if (base.Projectile.ai[0] <= 45f)
				{
					if (starburst2 != null)
					{
						StarburstEntity starburstEntity3 = starburst2;
						starburstEntity3.Velocity *= 0.8f;
					}
					if (starburst1 != null)
					{
						StarburstEntity starburstEntity4 = starburst1;
						starburstEntity4.Velocity *= 0.8f;
					}
				}
			}
			player.direction = player.DirectionTo(base.Projectile.Center + base.Projectile.velocity).X.DirectionalSign();
		}
		if (base.Projectile.FinalExtraUpdate() && burstStage != 0 && (base.Projectile.ai[0] <= 40f || !hasFired))
		{
			GeneralParticleHandler.SpawnParticle(new BloomParticle(base.Projectile.Center + base.Projectile.velocity, Vector2.Zero, Color.SkyBlue, 0.45f, 0.45f, 2, fade: false));
		}
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, player.DirectionTo(base.Projectile.Center + base.Projectile.velocity).ToRotation() - (float)Math.PI / 2f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		base.Projectile.localAI[1] = 4f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		Vector2 startPos = Main.player[base.Projectile.owner].Center + Main.player[base.Projectile.owner].DirectionTo(base.Projectile.Center) * 20f;
		Vector2 endPos = base.Projectile.Center;
		float rotation = base.Projectile.DirectionFrom(startPos).ToRotation() - (float)Math.PI / 2f;
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		for (int i = 1; i < 50; i++)
		{
			Main.EntitySpriteDraw(tex, Vector2.Lerp(startPos, endPos, (float)i / 50f) - Main.screenPosition, (Rectangle?)((i % 5 == 2) ? new Rectangle(0, 60, 54, 20) : new Rectangle(0, 86, 54, 18)), Color.White, rotation, ((i % 5 == 2) ? new Vector2(54f, 20f) : new Vector2(54f, 18f)) * 0.5f, base.Projectile.scale, (SpriteEffects)0, 0f);
		}
		Main.EntitySpriteDraw(tex, startPos - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, 54, 56), Color.White, rotation, new Vector2(54f, 56f) * 0.5f, base.Projectile.scale, (SpriteEffects)0, 0f);
		if (burstStage != 0)
		{
			Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, 108, 54, 50), Color.White, rotation, new Vector2(54f, 50f) * 0.5f, base.Projectile.scale, (SpriteEffects)0, 0f);
		}
		return false;
	}
}
