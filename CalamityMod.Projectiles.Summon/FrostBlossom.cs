using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class FrostBlossom : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer ModdedOwner => Owner.Calamity();

	public NPC Target
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return Owner.Center.MinionHoming(500f, Owner, CalamityPlayer.areThereAnyDamnBosses);
		}
	}

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 36;
		base.Projectile.height = 40;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.coldDamage = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		bool num = base.Projectile.type == ModContent.ProjectileType<FrostBlossom>();
		Owner.AddBuff(ModContent.BuffType<FrostBlossomBuff>(), 3600);
		if (num)
		{
			if (Owner.dead)
			{
				ModdedOwner.frostBlossom = false;
			}
			if (ModdedOwner.frostBlossom)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		base.Projectile.Center = Owner.Center + Vector2.UnitY * (Owner.gfxOffY - 60f);
		if (Owner.gravDir == -1f)
		{
			base.Projectile.position.Y += 120f;
			base.Projectile.rotation = (float)Math.PI;
		}
		else
		{
			base.Projectile.rotation = 0f;
		}
		base.Projectile.position.X = (int)base.Projectile.position.X;
		base.Projectile.position.Y = (int)base.Projectile.position.Y;
		base.Projectile.scale = 1f + (float)Math.Sin(base.Projectile.ai[0]++ / 40f) * 0.085f;
		base.Projectile.Opacity = 1f - (float)Math.Sin(base.Projectile.ai[1] / 45f) * 0.075f - 0.075f;
		if (base.Projectile.localAI[0] == 0f)
		{
			for (int i = 0; i < 36; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 113);
				dust.noGravity = true;
				dust.velocity = Vector2.One.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(2f, 7f);
			}
			base.Projectile.localAI[0]++;
		}
		if (base.Projectile.owner == Main.myPlayer && Target != null && base.Projectile.ai[1]++ % 35f == 34f && Collision.CanHit(base.Projectile.position, base.Projectile.width, base.Projectile.height, Target.position, Target.width, Target.height))
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.SafeDirectionTo(Target.Center) * 20f, ModContent.ProjectileType<FrostBeam>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
	}
}
