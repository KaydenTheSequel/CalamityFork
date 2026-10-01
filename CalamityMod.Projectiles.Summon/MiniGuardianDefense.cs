using System;
using System.IO;
using CalamityMod.NPCs.Providence;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class MiniGuardianDefense : ModProjectile, ILocalizedModType, IModType
{
	public enum MiniDefenderAIState
	{
		ShieldActive,
		ShieldInactive,
		Vanity
	}

	public bool shieldActiveBefore;

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

	public bool shieldActive
	{
		get
		{
			if (!ForcedVanity)
			{
				return Owner.Calamity().pSoulShieldDurability > 0;
			}
			return false;
		}
	}

	public MiniDefenderAIState AIState
	{
		get
		{
			if (!ForcedVanity)
			{
				if (!shieldActive)
				{
					return MiniDefenderAIState.ShieldInactive;
				}
				return MiniDefenderAIState.ShieldActive;
			}
			return MiniDefenderAIState.Vanity;
		}
	}

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
		base.Projectile.width = 62;
		base.Projectile.height = 80;
		base.Projectile.minion = true;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
	}

	private void HandleRocks(bool spawnRocks = false, bool yeetRocks = false)
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		if (spawnRocks)
		{
			bool profanedCrystalBuffs = Owner.Calamity().profanedCrystalBuffs;
			int rockCount = (profanedCrystalBuffs ? 10 : 5);
			int[] validRockTypes = ((!profanedCrystalBuffs) ? new int[3] { 3, 5, 6 } : new int[5] { 1, 3, 4, 5, 6 });
			float angleVariance = (float)Math.PI * 2f / (float)rockCount;
			float angle = 0f;
			for (int i = 0; i < rockCount; i++)
			{
				int rockType = validRockTypes[Main.rand.Next(0, validRockTypes.Length)];
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), Owner.position, angle.ToRotationVector2() * 8f, ModContent.ProjectileType<MiniGuardianRock>(), 1, 2f, Owner.whoAmI, 0f, angle, rockType).originalDamage = base.Projectile.originalDamage;
				angle += angleVariance;
			}
		}
		else
		{
			if (!yeetRocks)
			{
				return;
			}
			int rock = ModContent.ProjectileType<MiniGuardianRock>();
			Projectile[] projectile = Main.projectile;
			foreach (Projectile proj in projectile)
			{
				if (proj.active && proj.owner == Owner.whoAmI && proj.type == rock)
				{
					proj.ai[0] = 1f;
				}
			}
		}
	}

	public override void AI()
	{
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0682: Unknown result type (might be due to invalid IL or missing references)
		//IL_0687: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_0600: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0733: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_074a: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0632: Unknown result type (might be due to invalid IL or missing references)
		//IL_0637: Unknown result type (might be due to invalid IL or missing references)
		//IL_07af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0814: Unknown result type (might be due to invalid IL or missing references)
		//IL_087f: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.Calamity().pSoulGuardians)
		{
			base.Projectile.timeLeft = 2;
		}
		if (!Owner.Calamity().pSoulArtifact || Owner.dead || !Owner.active)
		{
			Owner.Calamity().pSoulGuardians = false;
			base.Projectile.active = false;
			return;
		}
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 6 % Main.projFrames[base.Type];
		bool psc = Owner.Calamity().profanedCrystal;
		if ((psc && !SpawnedFromPSC) || (!psc && SpawnedFromPSC))
		{
			int rock = ModContent.ProjectileType<MiniGuardianRock>();
			Projectile[] projectile = Main.projectile;
			foreach (Projectile proj in projectile)
			{
				if (proj.active && proj.owner == Owner.whoAmI && proj.type == rock)
				{
					proj.active = false;
				}
			}
			base.Projectile.active = false;
		}
		bool shieldIsActive = shieldActive;
		bool shouldSpawnRocks = !shieldActiveBefore & shieldIsActive;
		bool shouldYeetRocks = shieldActiveBefore && !shieldIsActive;
		HandleRocks(shouldSpawnRocks, shouldYeetRocks);
		if (shouldSpawnRocks | shouldYeetRocks)
		{
			Vector2 dustPos = default(Vector2);
			for (int j = 0; j < 20; j++)
			{
				((Vector2)(ref dustPos))._002Ector(Owner.Center.X + Main.rand.NextFloat(-10f, 10f), Owner.Center.Y + Main.rand.NextFloat(-10f, 10f));
				Vector2 velocity = (Owner.Center - dustPos).SafeNormalize(Vector2.Zero);
				velocity *= ((Main.dayTime || !SpawnedFromPSC) ? 3f : 6.9f);
				Dust dust = Dust.NewDustPerfect(Owner.Center, ProvUtils.GetDustID(!Main.dayTime && SpawnedFromPSC), velocity, 0, default(Color), 2f);
				if (!Main.dayTime && SpawnedFromPSC)
				{
					dust.noGravity = true;
				}
			}
		}
		NPC potentialTarget = base.Projectile.Center.MinionHoming(1500f, Owner);
		Vector2 playerDestination = Owner.Center - base.Projectile.Center;
		switch (AIState)
		{
		case MiniDefenderAIState.ShieldActive:
		case MiniDefenderAIState.ShieldInactive:
			if (AIState == MiniDefenderAIState.ShieldInactive)
			{
				for (int k = 0; k < 2; k++)
				{
					if (Main.rand.NextBool(3))
					{
						Dust dust2 = Dust.NewDustDirect(Owner.position, Owner.width, Owner.height, ProvUtils.GetDustID(!Main.dayTime && SpawnedFromPSC));
						dust2.velocity = Main.rand.NextVector2Circular(3.5f, 3.5f);
						dust2.velocity.Y -= Main.rand.NextFloat(1f, 3f);
						dust2.scale = Main.rand.NextFloat(1.15f, 1.45f);
						dust2.noGravity = true;
					}
				}
			}
			if (potentialTarget != null)
			{
				playerDestination = Owner.Center + Owner.SafeDirectionTo(potentialTarget.Center) * ((!shieldIsActive) ? (-50f) : (Owner.Calamity().profanedCrystalBuffs ? 125f : 75f));
				playerDestination.X += Main.rand.NextFloat(-5f, 5f);
				playerDestination.Y += Main.rand.NextFloat(-5f, 5f);
			}
			else
			{
				playerDestination.X += Main.rand.NextFloat(-10f, 10f) + 75f * (float)(shieldIsActive ? Owner.direction : (-Owner.direction));
				playerDestination.Y += Main.rand.NextFloat(-10f, 10f);
			}
			break;
		case MiniDefenderAIState.Vanity:
			playerDestination.X += Main.rand.NextFloat(-10f, 20f) - 60f * (float)Owner.direction;
			playerDestination.Y += Main.rand.NextFloat(-10f, 20f) - 60f;
			break;
		}
		if (potentialTarget != null && AIState != MiniDefenderAIState.Vanity)
		{
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
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= ((dist > 10f) ? 0.9f : 0.3f);
			base.Projectile.spriteDirection = ((base.Projectile.DirectionTo(potentialTarget.Center).X > 0f) ? 1 : (-1));
		}
		else
		{
			float playerDist = ((Vector2)(ref playerDestination)).Length();
			float acceleration = 0.5f;
			float returnSpeed = 28f;
			if (playerDist > 2000f)
			{
				base.Projectile.position = Owner.position;
				base.Projectile.netUpdate = true;
			}
			else if (playerDist < 50f)
			{
				acceleration = 0.01f;
				if (Math.Abs(base.Projectile.velocity.X) > 2f || Math.Abs(base.Projectile.velocity.Y) > 2f)
				{
					Projectile projectile3 = base.Projectile;
					projectile3.velocity *= 0.9f;
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
		base.Projectile.netUpdate = base.Projectile.netUpdate || shieldIsActive != shieldActiveBefore;
		shieldActiveBefore = shieldIsActive;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(shieldActiveBefore);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		shieldActiveBefore = reader.ReadBoolean();
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (SpawnedFromPSC && !ForcedVanity)
		{
			int dye = Owner?.cMinion ?? 0;
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 1, null, drawCentered: true, shrink: false, dye);
			return false;
		}
		return true;
	}
}
