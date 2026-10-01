using System;
using CalamityMod.CalPlayer;
using CalamityMod.Projectiles.Healing;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SandElementalHealer : ModProjectile, ILocalizedModType, IModType
{
	public int dust = 3;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 42;
		base.Projectile.height = 98;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0600: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_063e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0654: Unknown result type (might be due to invalid IL or missing references)
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_0658: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f5: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!modPlayer.rareSandElemental && !modPlayer.allElementals && !modPlayer.rareSandElementalVanity && !modPlayer.allElementalsVanity)
		{
			base.Projectile.active = false;
			return;
		}
		if (base.Projectile.type == ModContent.ProjectileType<SandElementalHealer>())
		{
			if (player.dead)
			{
				modPlayer.rareSandEleBuff = false;
			}
			if (modPlayer.rareSandEleBuff)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		dust--;
		if (dust >= 0)
		{
			int dustAmt = 50;
			for (int d = 0; d < dustAmt; d++)
			{
				int sand = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y + 16f), base.Projectile.width, base.Projectile.height - 16, 32);
				Dust obj = Main.dust[sand];
				obj.velocity *= 2f;
				Main.dust[sand].scale *= 1.15f;
			}
		}
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
		if (Math.Abs(base.Projectile.velocity.X) > 0.2f)
		{
			base.Projectile.spriteDirection = -base.Projectile.direction;
		}
		if (!modPlayer.rareSandElementalVanity && !modPlayer.allElementalsVanity)
		{
			float lightScalar = (float)Main.rand.Next(90, 111) * 0.01f;
			lightScalar *= Main.essScale;
			Lighting.AddLight(base.Projectile.Center, 0.7f * lightScalar, 0.6f * lightScalar, 0f * lightScalar);
		}
		base.Projectile.MinionAntiClump();
		if (Vector2.Distance(player.Center, base.Projectile.Center) > 400f)
		{
			base.Projectile.ai[0] = 1f;
			base.Projectile.tileCollide = false;
			base.Projectile.netUpdate = true;
		}
		float safeDist = 100f;
		bool returning = false;
		if (!returning)
		{
			returning = base.Projectile.ai[0] == 1f;
		}
		float returnSpeed = 7f;
		if (returning)
		{
			returnSpeed = 18f;
		}
		Vector2 playerVec = player.Center - base.Projectile.Center + new Vector2(-250f, -60f);
		float num = ((Vector2)(ref playerVec)).Length();
		if (num > 200f && returnSpeed < 10f)
		{
			returnSpeed = 10f;
		}
		if (((num < safeDist) & returning) && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
		{
			base.Projectile.ai[0] = 0f;
			base.Projectile.netUpdate = true;
		}
		if (num > 2000f)
		{
			base.Projectile.position.X = player.Center.X - (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = player.Center.Y - (float)(base.Projectile.height / 2);
			base.Projectile.netUpdate = true;
		}
		if (num > 70f)
		{
			((Vector2)(ref playerVec)).Normalize();
			playerVec *= returnSpeed;
			base.Projectile.velocity = (base.Projectile.velocity * 40f + playerVec) / 41f;
		}
		else if (base.Projectile.velocity.X == 0f && base.Projectile.velocity.Y == 0f)
		{
			base.Projectile.velocity.X = -0.22f;
			base.Projectile.velocity.Y = -0.12f;
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.ai[1] += Main.rand.Next(1, 4);
		}
		if (base.Projectile.ai[1] > 220f)
		{
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.localAI[0] < 120f)
		{
			base.Projectile.localAI[0]++;
		}
		if (base.Projectile.ai[0] != 0f || modPlayer.rareSandElementalVanity || modPlayer.allElementalsVanity)
		{
			return;
		}
		int healProj = ModContent.ProjectileType<CactusHealOrb>();
		if (base.Projectile.ai[1] != 0f || !(base.Projectile.localAI[0] >= 120f))
		{
			return;
		}
		base.Projectile.ai[1]++;
		if (Main.myPlayer == base.Projectile.owner && player.statLife < player.statLifeMax2)
		{
			SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.position);
			int dustAmt2 = 36;
			for (int i = 0; i < dustAmt2; i++)
			{
				Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(i - (dustAmt2 / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt2) + base.Projectile.Center;
				Vector2 dustVel = val - base.Projectile.Center;
				int green = Dust.NewDust(val + dustVel, 0, 0, 107, dustVel.X * 1.5f, dustVel.Y * 1.5f, 100, new Color(0, 200, 0));
				Main.dust[green].noGravity = true;
				Main.dust[green].noLight = true;
				Main.dust[green].velocity = dustVel;
			}
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, -Vector2.UnitY * 6f, healProj, 0, 0f, Main.myPlayer);
		}
	}

	public override bool? CanDamage()
	{
		CalamityPlayer modPlayer = Main.player[base.Projectile.owner].Calamity();
		if (modPlayer.rareSandElementalVanity || modPlayer.allElementalsVanity)
		{
			return false;
		}
		return null;
	}
}
