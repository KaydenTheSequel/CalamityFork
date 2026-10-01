using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class TacticalPlagueJet : ModProjectile, ILocalizedModType, IModType
{
	public static Item FalseGun;

	public static Item PlagueEngine;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 52;
		base.Projectile.height = 32;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	private static void DefineFalseGun(int baseDamage)
	{
		int p90ID = ModContent.ItemType<P90>();
		int TPEID = ModContent.ItemType<TacticalPlagueEngine>();
		FalseGun = new Item();
		PlagueEngine = new Item();
		FalseGun.SetDefaults(p90ID, noMatCheck: true);
		PlagueEngine.SetDefaults(TPEID, noMatCheck: true);
		FalseGun.damage = baseDamage;
		FalseGun.knockBack = PlagueEngine.knockBack;
		FalseGun.shootSpeed = PlagueEngine.shootSpeed;
		FalseGun.consumeAmmoOnFirstShotOnly = false;
		FalseGun.consumeAmmoOnLastShotOnly = false;
		FalseGun.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (base.Projectile.localAI[0] == 0f)
		{
			for (int i = 0; i < 45; i++)
			{
				Vector2 velocity = ((float)Math.PI * 2f / 45f * (float)i).ToRotationVector2() * 4f;
				Dust.NewDustPerfect(base.Projectile.Center + velocity * 2f, 46, velocity).noGravity = true;
			}
			if (FalseGun == null)
			{
				DefineFalseGun(base.Projectile.originalDamage);
			}
			base.Projectile.localAI[0] = 1f;
		}
		int usedAmmoItemId = base.Projectile.frameCounter++;
		if ((float)usedAmmoItemId > 6f)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		player.AddBuff(ModContent.BuffType<TacticalPlagueEngineBuff>(), 3600);
		if (base.Projectile.type == ModContent.ProjectileType<TacticalPlagueJet>())
		{
			if (player.dead)
			{
				modPlayer.plagueEngine = false;
			}
			if (modPlayer.plagueEngine)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		NPC potentialTarget = base.Projectile.Center.MinionHoming(1560f, player);
		if (potentialTarget == null || !player.HasAmmo(FalseGun))
		{
			Vector2 val = player.Center - base.Projectile.Center;
			float distanceToOwner = ((Vector2)(ref val)).Length();
			float acceleration = 0.1f;
			if (distanceToOwner < 140f)
			{
				acceleration = 0.035f;
			}
			else if (distanceToOwner < 200f)
			{
				acceleration = 0.07f;
			}
			if (distanceToOwner > 100f)
			{
				if (Math.Abs(player.Center.X - base.Projectile.Center.X) > 20f)
				{
					base.Projectile.velocity.X += acceleration * (float)Math.Sign(player.Center.X - base.Projectile.Center.X);
				}
				if (Math.Abs(player.Center.Y - base.Projectile.Center.Y) > 10f)
				{
					base.Projectile.velocity.Y += acceleration * (float)Math.Sign(player.Center.Y - base.Projectile.Center.Y);
				}
			}
			else if (((Vector2)(ref base.Projectile.velocity)).Length() > 4f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.95f;
			}
			if (Math.Abs(base.Projectile.velocity.Y) < 2f)
			{
				base.Projectile.velocity.Y += 0.1f * (float)Math.Sign(player.Center.Y - base.Projectile.Center.Y);
			}
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 9f)
			{
				base.Projectile.velocity = Vector2.Normalize(base.Projectile.velocity) * 9f;
			}
			if (base.Projectile.velocity.X > 0.25f)
			{
				base.Projectile.spriteDirection = 1;
			}
			else if (base.Projectile.velocity.X < -0.25f)
			{
				base.Projectile.spriteDirection = -1;
			}
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(0f, 0.2f);
			if (distanceToOwner > 2700f)
			{
				base.Projectile.Center = player.Center;
				base.Projectile.netUpdate = true;
			}
			return;
		}
		base.Projectile.spriteDirection = 1;
		base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.SafeDirectionTo(potentialTarget.Center - Vector2.UnitY * 195f) * 17f, 0.035f);
		base.Projectile.rotation = base.Projectile.rotation.AngleTowards(base.Projectile.AngleTo(potentialTarget.Center), 0.1f);
		if (base.Projectile.ai[0]++ % 125f == 24f)
		{
			bool shootRocket = ++base.Projectile.ai[1] % 2f == 0f;
			bool dontConsumeAmmo = Main.rand.NextBool() | shootRocket;
			player.PickAmmo(FalseGun, out var projID, out var shootSpeed, out var damage, out var kb, out usedAmmoItemId, dontConsumeAmmo);
			int projIndex;
			if (shootRocket)
			{
				int rocketDamage = (int)((float)damage * 1.5f);
				float rocketKB = kb + 5f;
				projIndex = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.SafeDirectionTo(potentialTarget.Center) * 18f, ModContent.ProjectileType<MK2RocketHoming>(), rocketDamage, rocketKB, base.Projectile.owner);
			}
			else
			{
				projIndex = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.SafeDirectionTo(potentialTarget.Center) * shootSpeed, projID, damage, kb, base.Projectile.owner);
			}
			if (projIndex.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[projIndex].DamageType = DamageClass.Summon;
				Main.projectile[projIndex].minion = false;
			}
		}
		base.Projectile.MinionAntiClump(0.25f);
	}
}
