using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class MountedScannerSummon : ModProjectile, ILocalizedModType, IModType
{
	public const int LaserFireRate = 125;

	public const float OffsetDistanceFromPlayer = 60f;

	public new string LocalizationCategory => "Projectiles.Misc";

	public float AngularOffsetRelativeToPlayer
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public float Time
	{
		get
		{
			return base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		ProjectileID.Sets.NeedsUUID[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 18;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.Center = player.Center + AngularOffsetRelativeToPlayer.ToRotationVector2() * 60f + Vector2.UnitY * player.gfxOffY;
		GrantBuffs(player);
		NPC potentialTarget = base.Projectile.Center.MinionHoming(960f, player);
		if (potentialTarget == null)
		{
			AdjustVisualValues_Idle(player);
			base.Projectile.localAI[0] = 0f;
		}
		else
		{
			AttackTarget(potentialTarget);
			base.Projectile.localAI[0] = 1f;
		}
		Time++;
	}

	public void GrantBuffs(Player player)
	{
		bool num = base.Projectile.type == ModContent.ProjectileType<MountedScannerSummon>();
		player.AddBuff(ModContent.BuffType<MountedScannerBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				player.Calamity().mountedScanner = false;
			}
			if (player.Calamity().mountedScanner)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public void AdjustVisualValues_Idle(Player player)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref player.velocity)).Length() > 1.5f)
		{
			base.Projectile.spriteDirection = (player.velocity.X > 0f).ToDirectionInt();
			base.Projectile.rotation = player.velocity.ToRotation() + (float)(base.Projectile.spriteDirection == -1).ToInt() * (float)Math.PI;
		}
		else
		{
			base.Projectile.spriteDirection = 1;
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp((float)Math.PI / 2f, 0.075f);
		}
	}

	public void AttackTarget(NPC target)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.spriteDirection = 1;
		base.Projectile.rotation = base.Projectile.AngleTo(target.Center);
		if (Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, target.position, target.width, target.height) && Time % 125f == 124f)
		{
			if (base.Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.SafeDirectionTo(target.Center, Vector2.UnitY), ModContent.ProjectileType<MountedScannerLaser>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, base.Projectile.whoAmI);
			}
			SoundEngine.PlaySound(in CommonCalamitySounds.LaserCannonSound, base.Projectile.Center);
		}
	}
}
