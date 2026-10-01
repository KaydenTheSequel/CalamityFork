using System;
using System.IO;
using CalamityMod.Buffs.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SageSpirit : ModProjectile, ILocalizedModType, IModType
{
	internal bool Initialized;

	internal Vector2 PlayerFlyStart;

	internal Vector2 PlayerFlyOffsetAtEnds;

	internal Vector2 PlayerFlyDestination;

	internal const int ShootRate = 40;

	public new string LocalizationCategory => "Projectiles.Summon";

	internal Player Owner => Main.player[base.Projectile.owner];

	internal ref float AttackTimer => ref base.Projectile.ai[0];

	internal ref float PlayerFlyTime => ref base.Projectile.ai[1];

	internal ref float SageSpiritIndex => ref base.Projectile.localAI[0];

	internal ref float GeneralTime => ref base.Projectile.localAI[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 48;
		base.Projectile.height = 72;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 90000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(Initialized);
		writer.Write(SageSpiritIndex);
		writer.Write(GeneralTime);
		writer.WritePackedVector2(PlayerFlyStart);
		writer.WritePackedVector2(PlayerFlyOffsetAtEnds);
		writer.WritePackedVector2(PlayerFlyDestination);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		Initialized = reader.ReadBoolean();
		SageSpiritIndex = reader.ReadSingle();
		GeneralTime = reader.ReadSingle();
		PlayerFlyStart = reader.ReadPackedVector2();
		PlayerFlyOffsetAtEnds = reader.ReadPackedVector2();
		PlayerFlyDestination = reader.ReadPackedVector2();
	}

	public override void AI()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		GeneralTime++;
		ProvidePlayerMinionBuffs();
		Initialize();
		DetermineFrames();
		NPC potentialTarget = base.Projectile.Center.MinionHoming(750f, Owner);
		if (potentialTarget == null)
		{
			FlyNearOwner();
			return;
		}
		ResetOwnerFlyValues();
		AttackTarget(potentialTarget);
	}

	internal void ProvidePlayerMinionBuffs()
	{
		Owner.AddBuff(ModContent.BuffType<SageSpiritBuff>(), 3600);
		if (base.Projectile.type == ModContent.ProjectileType<SageSpirit>())
		{
			if (Owner.dead)
			{
				Owner.Calamity().sageSpirit = false;
			}
			if (Owner.Calamity().sageSpirit)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	internal void Initialize()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if (!Initialized)
		{
			Initialized = true;
			PlayerFlyTime = 1f;
			PlayerFlyOffsetAtEnds = Vector2.UnitY;
			PlayerFlyStart = base.Projectile.Center;
			PlayerFlyDestination = base.Projectile.Center - Vector2.UnitY * 10f;
			base.Projectile.netUpdate = true;
		}
	}

	internal void DetermineFrames()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 5 == 4)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
		}
	}

	internal void ResetOwnerFlyValues()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		PlayerFlyTime = 0f;
		PlayerFlyOffsetAtEnds = Vector2.Zero;
		PlayerFlyStart = (PlayerFlyDestination = -Vector2.One);
		base.Projectile.netUpdate = true;
	}

	internal void FlyNearOwner()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		Vector2 destination = Owner.Center + Vector2.UnitX * (float)Owner.width * 1.6f * (float)Owner.direction;
		int totalSageSpirits = Owner.ownedProjectileCounts[base.Type];
		destination += (SageSpiritIndex * ((float)Math.PI * 2f) / (float)totalSageSpirits).ToRotationVector2() * 70f;
		if (PlayerFlyTime >= 1f)
		{
			base.Projectile.Center = PlayerFlyDestination;
			ResetOwnerFlyValues();
			return;
		}
		float distanceFromDestination = base.Projectile.Distance(destination);
		if (float.IsNaN(distanceFromDestination))
		{
			distanceFromDestination = 0f;
		}
		if (PlayerFlyTime <= 0f)
		{
			PlayerFlyStart = base.Projectile.Center;
			PlayerFlyOffsetAtEnds = Vector2.UnitY * distanceFromDestination;
			PlayerFlyDestination = destination;
			base.Projectile.netUpdate = true;
			PlayerFlyTime = 0.01f;
		}
		else
		{
			base.Projectile.position.Y += (float)Math.Cos(GeneralTime / 60f * ((float)Math.PI * 2f) + (float)base.Projectile.identity * 1.1f) * 0.5f;
			if (!(distanceFromDestination < 160f) || !(PlayerFlyTime <= 0f))
			{
				base.Projectile.Center = Vector2.CatmullRom(PlayerFlyStart + PlayerFlyOffsetAtEnds, PlayerFlyStart, PlayerFlyDestination, PlayerFlyDestination + PlayerFlyOffsetAtEnds, PlayerFlyTime);
				PlayerFlyTime = MathHelper.Clamp(PlayerFlyTime + 0.03f, 0f, 1f);
			}
		}
	}

	internal void AttackTarget(NPC target)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		AttackTimer++;
		int totalSageSpirits = Owner.ownedProjectileCounts[base.Type];
		Vector2 destinationOffsetFactor = Vector2.Max(target.Size, new Vector2(160f)) * new Vector2(0.3f, 0.2f);
		Vector2 destination = target.Center + (AttackTimer / 12f).ToRotationVector2() * destinationOffsetFactor;
		destination += (SageSpiritIndex * ((float)Math.PI * 2f) / (float)totalSageSpirits).ToRotationVector2() * 130f;
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, destination, 0.1f);
		if (base.Projectile.owner == Main.myPlayer && AttackTimer % 40f == 39f)
		{
			for (int i = 0; i < 3; i++)
			{
				Vector2 spikeVelocity = -Vector2.UnitY.RotatedBy(MathHelper.Lerp(-0.43f, 0.43f, (float)i / 3f)) * 6f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Top, spikeVelocity, ModContent.ProjectileType<SageNeedle>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
		}
	}
}
