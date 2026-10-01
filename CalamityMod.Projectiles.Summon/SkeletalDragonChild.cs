using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SkeletalDragonChild : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 52;
		base.Projectile.height = 48;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 7;
		base.Projectile.minionSlots = 0f;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (base.Projectile.ai[0] < 0f || base.Projectile.ai[0] >= (float)Main.projectile.Length)
		{
			base.Projectile.Kill();
			return;
		}
		Projectile mother = Main.projectile[(int)base.Projectile.ai[0]];
		if (!mother.active || mother.type != ModContent.ProjectileType<SkeletalDragonMother>())
		{
			base.Projectile.Kill();
			return;
		}
		NPC target = base.Projectile.Center.MinionHoming(732.60004f, player);
		if (target != null)
		{
			base.Projectile.ai[1]++;
			if (base.Projectile.ai[1] % 55f == 54f && Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.SafeDirectionTo(target.Center) * 19f, ModContent.ProjectileType<BloodSpit>(), mother.damage, mother.knockBack, mother.owner);
			}
		}
		if (mother.ai[1] == 1f)
		{
			if (target != null)
			{
				if (base.Projectile.Distance(mother.Center) > 620f)
				{
					base.Projectile.velocity = (mother.Center - base.Projectile.Center) / 45f;
				}
				else
				{
					base.Projectile.velocity = (base.Projectile.velocity * 19f + base.Projectile.SafeDirectionTo(mother.Center) * 18f) / 20f;
				}
			}
			else if (base.Projectile.Distance(mother.Center) > 250f)
			{
				base.Projectile.velocity = (mother.Center - base.Projectile.Center) / 28f;
			}
			else
			{
				Projectile projectile = base.Projectile;
				projectile.velocity += new Vector2((float)Math.Sign(mother.Center.X - base.Projectile.Center.X), (float)Math.Sign(mother.Center.Y - base.Projectile.Center.Y)) * new Vector2(0.04f, 0.0225f);
				if (((Vector2)(ref base.Projectile.velocity)).Length() > 8f)
				{
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= 8f / ((Vector2)(ref base.Projectile.velocity)).Length();
				}
			}
		}
		else if (base.Projectile.Distance(mother.Center) > 250f)
		{
			base.Projectile.velocity = (mother.Center - base.Projectile.Center) / 28f;
		}
		else
		{
			Projectile projectile3 = base.Projectile;
			projectile3.velocity += new Vector2((float)Math.Sign(mother.Center.X - base.Projectile.Center.X), (float)Math.Sign(mother.Center.Y - base.Projectile.Center.Y)) * new Vector2(0.04f, 0.0225f);
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 8f)
			{
				Projectile projectile4 = base.Projectile;
				projectile4.velocity *= 8f / ((Vector2)(ref base.Projectile.velocity)).Length();
			}
		}
		base.Projectile.direction = (base.Projectile.spriteDirection = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
	}
}
