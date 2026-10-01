using System;
using System.IO;
using CalamityMod.Buffs.Summon.Whips;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.NPCs.Providence;
using CalamityMod.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class MiniGuardianHealer : ModProjectile, ILocalizedModType, IModType
{
	internal const int starTimer = 180;

	internal const int laserTimer = 1200;

	private bool hasSetTimers;

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public bool SpawnedFromPSC => base.Projectile.ai[0] == 1f;

	public bool ForcedVanity
	{
		get
		{
			if (SpawnedFromPSC)
			{
				return !Owner.Calamity().profanedCrystalBuffs;
			}
			return false;
		}
	}

	public bool isEmpowered => Main.player[base.Projectile.owner].Calamity().pscState == 3;

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.tileCollide = false;
		base.Projectile.width = 68;
		base.Projectile.height = 82;
		base.Projectile.friendly = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.minion = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 18000;
		base.Projectile.timeLeft *= 5;
	}

	public override bool? CanCutTiles()
	{
		CalamityPlayer modPlayer = Main.player[base.Projectile.owner].Calamity();
		if ((!modPlayer.pSoulArtifact || modPlayer.profanedCrystal) && !modPlayer.profanedCrystalBuffs)
		{
			return false;
		}
		return null;
	}

	public override void AI()
	{
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0978: Unknown result type (might be due to invalid IL or missing references)
		//IL_097d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0985: Unknown result type (might be due to invalid IL or missing references)
		//IL_098a: Unknown result type (might be due to invalid IL or missing references)
		//IL_098f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0724: Unknown result type (might be due to invalid IL or missing references)
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0733: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0764: Unknown result type (might be due to invalid IL or missing references)
		//IL_0768: Unknown result type (might be due to invalid IL or missing references)
		//IL_076d: Unknown result type (might be due to invalid IL or missing references)
		//IL_077f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0849: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0caa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d06: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0591: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (modPlayer.pSoulGuardians)
		{
			base.Projectile.timeLeft = 2;
		}
		if (!modPlayer.pSoulArtifact || player.dead || !player.active)
		{
			modPlayer.pSoulGuardians = false;
			base.Projectile.active = false;
			return;
		}
		if (!hasSetTimers)
		{
			base.Projectile.ai[1] = 180f;
			base.Projectile.ai[2] = 1200f;
			hasSetTimers = true;
			base.Projectile.netUpdate = true;
		}
		base.Projectile.MinionAntiClump();
		Player owner = Owner;
		bool psc = owner.Calamity().profanedCrystal;
		if ((psc && !SpawnedFromPSC) || (!psc && SpawnedFromPSC))
		{
			base.Projectile.active = false;
		}
		Vector2 playerDestination = owner.Center - base.Projectile.Center;
		bool num = isEmpowered;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 5)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		NPC target = null;
		if (num && player.whoAmI == Main.myPlayer)
		{
			target = base.Projectile.Center.MinionHoming(2000f, player);
			if (target != null)
			{
				base.Projectile.spriteDirection = ((base.Projectile.DirectionTo(owner.Center).X > 0f) ? 1 : (-1));
				if (base.Projectile.ai[1] <= 0f)
				{
					SoundEngine.PlaySound(in SoundID.DD2_BetsyFireballImpact, base.Projectile.Center);
					int totalFlameProjectiles = 30;
					int totalRings = 3;
					double radians = (float)Math.PI * 2f / (float)totalFlameProjectiles;
					double angleA = radians * 0.5;
					double angleB = (double)MathHelper.ToRadians(90f) - angleA;
					for (int i = 0; i < totalRings; i++)
					{
						bool num2 = i % 2 == 0;
						float starVelocity = (float)i + 2f;
						float velocityX = (float)((double)starVelocity * Math.Sin(angleA) / Math.Sin(angleB));
						Vector2 spinningPoint = (num2 ? new Vector2(0f - velocityX, 0f - starVelocity) : new Vector2(0f, 0f - starVelocity));
						for (int j = 0; j < totalFlameProjectiles; j++)
						{
							Vector2 vector2 = spinningPoint.RotatedBy(radians * (double)j);
							int type = ModContent.ProjectileType<MiniGuardianStars>();
							int dmgAmt = base.Projectile.originalDamage;
							Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, vector2 * 2.5f, type, dmgAmt, 0f, Main.myPlayer).originalDamage = base.Projectile.originalDamage;
							Color dustColor = ProfanedSoulCrystal.GetColorForPsc(modPlayer.pscState, Main.dayTime);
							((Color)(ref dustColor)).A = byte.MaxValue;
							int maxDust = 3;
							for (int k = 0; k < maxDust; k++)
							{
								int dust = Dust.NewDust(base.Projectile.Center, 0, 0, 267, 0f, 0f, 0, dustColor);
								Main.dust[dust].position = base.Projectile.Center;
								Main.dust[dust].velocity = vector2 * starVelocity * ((float)k * 0.5f + 1f);
								Main.dust[dust].noGravity = true;
								Main.dust[dust].scale = 1f + (float)k;
								Main.dust[dust].fadeIn = Main.rand.NextFloat() * 2f;
								Dust dust2 = DustExtensions.BetterCloneDust(dust);
								dust2.scale /= 2f;
								dust2.fadeIn /= 2f;
								dust2.color = new Color(255, 255, 255, 255);
							}
						}
					}
					base.Projectile.ai[1] = 180f;
				}
				bool num3 = player.HasBuff<ProfanedCrystalWhipBuff>();
				if (num3 && base.Projectile.ai[2] <= 0f)
				{
					SoundEngine.PlaySound(in Providence.HolyRaySound, player.Center);
					float rotation = 435f;
					Vector2 velocity = target.Center - player.Center;
					((Vector2)(ref velocity)).Normalize();
					float beamDirection = -1f;
					if (velocity.X < 0f)
					{
						beamDirection = 1f;
					}
					int holyLaserDamage = base.Projectile.originalDamage * 5;
					velocity = velocity.RotatedBy((0.0 - (double)beamDirection) * 6.2831854820251465 / 4.0);
					int projectile = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), player.Center.X, player.Center.Y, velocity.X, velocity.Y, ModContent.ProjectileType<MiniGuardianHolyRay>(), holyLaserDamage, 0f, Main.myPlayer, beamDirection * ((float)Math.PI * 2f) / rotation, player.whoAmI);
					if (Main.projectile.IndexInRange(projectile))
					{
						Main.projectile[projectile].originalDamage = holyLaserDamage;
					}
					projectile = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f - velocity.X, 0f - velocity.Y, ModContent.ProjectileType<MiniGuardianHolyRay>(), holyLaserDamage, 0f, Main.myPlayer, (0f - beamDirection) * ((float)Math.PI * 2f) / rotation, player.whoAmI);
					if (Main.projectile.IndexInRange(projectile))
					{
						Main.projectile[projectile].originalDamage = holyLaserDamage;
					}
					base.Projectile.ai[2] = 1200f;
				}
				base.Projectile.ai[1]--;
				if (num3)
				{
					base.Projectile.ai[2]--;
				}
			}
		}
		if (target == null)
		{
			playerDestination.X += Main.rand.NextFloat(-5f, 5f);
			playerDestination.Y += Main.rand.NextFloat(-10f, 10f);
			float playerDist = ((Vector2)(ref playerDestination)).Length();
			float acceleration = 0.5f;
			float returnSpeed = 28f;
			if (playerDist > 2000f)
			{
				base.Projectile.position = owner.position;
				base.Projectile.netUpdate = true;
			}
			else if (playerDist < 50f)
			{
				acceleration = 0.01f;
				if (Math.Abs(base.Projectile.velocity.X) > 2f || Math.Abs(base.Projectile.velocity.Y) > 2f)
				{
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= 0.9f;
				}
			}
			else
			{
				if (playerDist < 100f)
				{
					acceleration = 0.1f;
				}
				if (playerDist > 300f)
				{
					acceleration = 1f;
				}
				playerDist = returnSpeed / playerDist;
				playerDestination *= playerDist;
				if (base.Projectile.velocity.X < playerDestination.X)
				{
					base.Projectile.velocity.X += acceleration;
					if (acceleration > 0.05f && base.Projectile.velocity.X < 0f)
					{
						base.Projectile.velocity.X += acceleration;
					}
				}
				if (base.Projectile.velocity.X > playerDestination.X)
				{
					base.Projectile.velocity.X -= acceleration;
					if (acceleration > 0.05f && base.Projectile.velocity.X > 0f)
					{
						base.Projectile.velocity.X -= acceleration;
					}
				}
				if (base.Projectile.velocity.Y < playerDestination.Y)
				{
					base.Projectile.velocity.Y += acceleration;
					if (acceleration > 0.05f && base.Projectile.velocity.Y < 0f)
					{
						base.Projectile.velocity.Y += acceleration * 2f;
					}
				}
				if (base.Projectile.velocity.Y > playerDestination.Y)
				{
					base.Projectile.velocity.Y -= acceleration;
					if (acceleration > 0.05f && base.Projectile.velocity.Y > 0f)
					{
						base.Projectile.velocity.Y -= acceleration * 2f;
					}
				}
			}
			if (Math.Abs(base.Projectile.velocity.X) > 0.2f)
			{
				base.Projectile.direction = (base.Projectile.spriteDirection = Math.Sign(base.Projectile.velocity.X));
			}
		}
		else
		{
			if (base.Projectile.ai[2] <= 120f)
			{
				playerDestination = player.Center;
				Rectangle hitbox = base.Projectile.Hitbox;
				if (((Rectangle)(ref hitbox)).Intersects(player.Hitbox))
				{
					base.Projectile.ai[2] = 0f;
				}
			}
			else
			{
				playerDestination = target.Center;
				playerDestination.X += Main.rand.NextFloat(-5f, 5f);
				playerDestination.Y += Main.rand.NextFloat(-155f, -160f);
			}
			float dist = base.Projectile.Center.Distance(playerDestination);
			float x = playerDestination.X;
			float num544 = playerDestination.Y;
			float num550 = 40f;
			Vector2 vector43 = base.Projectile.Center;
			float num551 = x - vector43.X;
			float num552 = num544 - vector43.Y;
			float num553 = (float)Math.Sqrt(num551 * num551 + num552 * num552);
			if (num553 < 100f)
			{
				num550 = 28f;
			}
			num553 = num550 / num553;
			num551 *= num553;
			num552 *= num553;
			base.Projectile.velocity.X = (base.Projectile.velocity.X * 14f + num551) / 13.5f;
			base.Projectile.velocity.Y = (base.Projectile.velocity.Y * 14f + num552) / 13.5f;
			if (base.Projectile.ai[2] <= 120f)
			{
				Projectile projectile3 = base.Projectile;
				projectile3.velocity *= ((dist > 10f) ? MathHelper.SmoothStep(0.65f, 0.95f, 120f - Utils.GetLerpValue(120f, base.Projectile.ai[2], 60f)) : 0.1f);
			}
			else
			{
				Projectile projectile4 = base.Projectile;
				projectile4.velocity *= ((dist > 10f) ? 0.9f : 0.3f);
			}
		}
		if (base.Projectile.ai[2] <= 120f && owner.HasBuff<ProfanedCrystalWhipBuff>())
		{
			int dustCount = (int)Math.Round(MathHelper.SmoothStep(1f, 5f, base.Projectile.ai[2] / 120f));
			float outwardness = MathHelper.SmoothStep(40f, 60f, base.Projectile.ai[2] / 120f);
			float dustScale = MathHelper.Lerp(1.15f, 1.425f, base.Projectile.ai[2] / 120f);
			for (int l = 0; l < dustCount; l++)
			{
				Vector2 spawnPosition = player.Center + Main.rand.NextVector2Unit() * outwardness * Main.rand.NextFloat(0.75f, 1.1f);
				Vector2 dustVelocity = (player.Center - spawnPosition) * 0.085f + owner.velocity;
				Dust dust3 = Dust.NewDustPerfect(spawnPosition, ProvUtils.GetDustID(!Main.dayTime));
				dust3.velocity = dustVelocity;
				dust3.scale = dustScale * Main.rand.NextFloat(0.75f, 1.15f);
				dust3.color = Color.Lerp(Color.LightCoral, Color.White, base.Projectile.ai[2] / 120f * Main.rand.NextFloat(0.65f, 1f));
				dust3.noGravity = true;
				dust3.noLight = true;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (SpawnedFromPSC && !ForcedVanity && Owner.Calamity().pscState == 3)
		{
			int dye = Owner?.cMinion ?? 0;
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 1, null, drawCentered: true, shrink: false, dye);
			return false;
		}
		return true;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(hasSetTimers);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		hasSetTimers = reader.ReadBoolean();
	}
}
