using System;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.NPCs.ExoMechs.Ares;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class CorinthPrimeAirburstGrenade : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 44;
		base.Projectile.height = 28;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = true;
		base.Projectile.timeLeft = 130;
		base.Projectile.DamageType = DamageClass.Ranged;
	}

	public override void AI()
	{
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.velocity.Y < 12f && base.Projectile.timeLeft < 115)
		{
			base.Projectile.velocity.Y += 0.1f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		Vector2 pos = base.Projectile.Center;
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 7f)
		{
			for (int i = 0; i < 5; i++)
			{
				pos -= base.Projectile.velocity * ((float)i * 0.25f);
				int idx = Dust.NewDust(pos, base.Projectile.width, base.Projectile.height, Main.rand.NextBool() ? 206 : 187);
				Main.dust[idx].noGravity = true;
				Main.dust[idx].position = pos;
				Main.dust[idx].scale = (float)Main.rand.Next(70, 110) * 0.013f;
				Dust obj = Main.dust[idx];
				obj.velocity *= 0.3f;
			}
			return;
		}
		for (int j = 0; j < 30; j++)
		{
			int dustID;
			switch (Main.rand.Next(6))
			{
			case 0:
				dustID = 186;
				break;
			case 1:
			case 2:
				dustID = 20;
				break;
			default:
				dustID = 56;
				break;
			}
			float num = Main.rand.NextFloat(3f, 13f);
			float angleRandom = 0.06f;
			Vector2 dustVel = Utils.RotatedBy(new Vector2(num, 0f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
			dustVel = dustVel.RotatedBy(0f - angleRandom);
			dustVel = dustVel.RotatedByRandom(2f * angleRandom);
			float scale = Main.rand.NextFloat(0.5f, 1.6f);
			int idx2 = Dust.NewDust(pos, base.Projectile.width, base.Projectile.height, dustID, dustVel.X, dustVel.Y, 0, default(Color), scale);
			Main.dust[idx2].noGravity = true;
			Main.dust[idx2].position = pos;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		bool weakExplosion = base.Projectile.localAI[0] < 90f;
		if (weakExplosion)
		{
			SoundEngine.PlaySound(in TeslaCannon.FireSound, base.Projectile.Center);
		}
		else
		{
			SoundEngine.PlaySound(in AresGaussNuke.NukeExplosionSound, base.Projectile.Center);
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			if (weakExplosion)
			{
				for (int i = 0; i < 2; i++)
				{
					Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<CorinthPrimeAirburst>(), 1, 0f, base.Projectile.owner);
					projectile.ai[1] = Main.rand.NextFloat(64f, 174f) + (float)i * 20f;
					projectile.localAI[1] = Main.rand.NextFloat(0.18f, 0.3f);
					projectile.netUpdate = true;
				}
			}
			else
			{
				for (int j = 0; j < 7; j++)
				{
					Projectile explosion = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<CorinthPrimeAirburst>(), (int)((double)base.Projectile.damage * 0.5), base.Projectile.knockBack, base.Projectile.owner);
					if (explosion.whoAmI.WithinBounds(Main.maxProjectiles))
					{
						explosion.ai[1] = Main.rand.NextFloat(256f, 696f) + (float)j * 45f;
						explosion.localAI[1] = Main.rand.NextFloat(0.08f, 0.25f);
						explosion.Opacity = MathHelper.Lerp(0.18f, 0.6f, (float)j / 7f) + Main.rand.NextFloat(-0.08f, 0.08f);
						explosion.netUpdate = true;
					}
				}
			}
		}
		int totalDust = (weakExplosion ? 100 : 400);
		for (int k = 0; k < totalDust; k++)
		{
			int dustType = 206;
			float circleSize = 32f;
			if (k < 300)
			{
				dustType = 187;
				circleSize = 16f;
			}
			if (k < 200)
			{
				circleSize = 8f;
			}
			if (k < 100)
			{
				circleSize = 4f;
			}
			int circleDustID = Dust.NewDust(base.Projectile.Center, 6, 6, dustType, 0f, 0f, 100);
			float dustX = Main.dust[circleDustID].velocity.X;
			float dustY = Main.dust[circleDustID].velocity.Y;
			if (dustX == 0f && dustY == 0f)
			{
				dustX = 1f;
			}
			float dustCircle = (float)Math.Sqrt(dustX * dustX + dustY * dustY);
			dustCircle = circleSize / dustCircle;
			dustX *= dustCircle;
			dustY *= dustCircle;
			float scale = 1f;
			switch ((int)circleSize)
			{
			case 4:
				scale = 1.1f;
				break;
			case 8:
				scale = 1.2f;
				break;
			case 16:
				scale = 1.4f;
				break;
			case 32:
				scale = 1.8f;
				break;
			}
			Dust obj = Main.dust[circleDustID];
			obj.velocity *= 0.5f;
			obj.velocity.X += dustX;
			obj.velocity.Y += dustY;
			obj.scale = scale;
			obj.noGravity = true;
		}
	}
}
