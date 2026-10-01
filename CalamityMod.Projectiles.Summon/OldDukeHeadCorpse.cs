using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class OldDukeHeadCorpse : ModProjectile, ILocalizedModType, IModType
{
	public int GFBTimer = 3000;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 2;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 64;
		base.Projectile.height = 58;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = true;
		base.Projectile.sentry = true;
		base.Projectile.timeLeft = 36000;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0645: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0654: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		//IL_0678: Unknown result type (might be due to invalid IL or missing references)
		//IL_0682: Unknown result type (might be due to invalid IL or missing references)
		//IL_0688: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		NPC target = base.Projectile.Center.MinionHoming(845f, player, ignoreTiles: false);
		if (target != null && target.Bottom.Y > base.Projectile.Top.Y)
		{
			target = null;
		}
		if (Main.zenithWorld)
		{
			if (GFBTimer > 0)
			{
				if (Main.rand.NextBool((int)(400f * Utils.GetLerpValue(0f, 2000f, GFBTimer)) + 1))
				{
					Projectile projectile = base.Projectile;
					projectile.velocity += (Vector2.One * 0.3f).RotatedByRandom(100.0);
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/Heartbeat");
					style.Volume = 0.5f;
					style.MaxInstances = -1;
					SoundEngine.PlaySound(in style, player.Center);
					player.SetScreenshake(6.5f * Utils.GetLerpValue(600f, 0f, GFBTimer));
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= 0.99f;
				}
				GFBTimer--;
				if (GFBTimer == 0)
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/GFB/HeComes");
					style.Volume = 1f;
					style.SoundLimitBehavior = SoundLimitBehavior.IgnoreNew;
					SoundEngine.PlaySound(in style, player.Center);
				}
			}
			else
			{
				bool far = player.Center.Distance(base.Projectile.Center) > 2000f;
				if (player.Center.Distance(base.Projectile.Center) > 50f && base.Projectile.timeLeft % (far ? 2 : 10) == 0)
				{
					base.Projectile.scale = 1f;
					Vector2 moveDir2 = (base.Projectile.Center.DirectionTo(player.Center) * (far ? 40f : Main.rand.NextFloat(20f, 25f))).RotatedByRandom(0.4000000059604645);
					Projectile projectile3 = base.Projectile;
					projectile3.Center += moveDir2;
					Projectile projectile4 = base.Projectile;
					projectile4.velocity += (far ? Vector2.Zero : (moveDir2 * 0.4f));
					for (int j = 0; j < 8; j++)
					{
						Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(35f, 35f), ModContent.DustType<LightDust>());
						dust.velocity = -base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(3f, 7f);
						dust.scale = Main.rand.NextFloat(0.5f, 0.7f);
						dust.noGravity = true;
						dust.color = Color.Chartreuse;
						dust.noLightEmittence = true;
					}
					SoundEngine.PlaySound(SoundID.NPCDeath13 with
					{
						Volume = 0.85f,
						MaxInstances = -1
					}, base.Projectile.Center);
					if (far && base.Projectile.timeLeft % 30 == 0)
					{
						SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/GFB/YouAreNotSafe");
						style.Volume = 0.8f;
						style.SoundLimitBehavior = SoundLimitBehavior.IgnoreNew;
						SoundEngine.PlaySound(in style, player.Center);
						Main.NewText(CalamityUtils.GetTextValue("Misc.NotSafe"), Color.Chartreuse);
					}
				}
				else
				{
					Projectile projectile5 = base.Projectile;
					projectile5.velocity *= 0.9f;
				}
				if (player.Center.Distance(base.Projectile.Center) < 50f)
				{
					player.AddBuff(163, 30);
					player.AddBuff(22, 30);
					player.AddBuff(ModContent.BuffType<MiracleBlight>(), 15);
					if (player.statLife > 15)
					{
						player.statLife = (int)((float)player.statLife * 0.97f);
					}
					player.velocity *= 0.95f;
					player.Center += (Vector2.One * 7f).RotatedByRandom(100.0);
					if (base.Projectile.timeLeft % 20 == 0)
					{
						for (int i = 0; i < 4; i++)
						{
							SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/Heartbeat");
							style.Volume = 0.8f;
							style.MaxInstances = -1;
							SoundEngine.PlaySound(in style, player.Center);
						}
						player.SetScreenshake(4.5f);
					}
					base.Projectile.Center = player.Center;
					SoundEngine.PlaySound(SoundID.NPCDeath20 with
					{
						Volume = 0.35f,
						MaxInstances = -1
					}, base.Projectile.Center);
					if (player.dead)
					{
						base.Projectile.scale *= 1.02f;
					}
					else
					{
						base.Projectile.scale = 1f;
					}
				}
			}
		}
		base.Projectile.frame = (target != null).ToInt();
		if (target != null)
		{
			base.Projectile.ai[0]++;
			if (Main.myPlayer == base.Projectile.owner && base.Projectile.ai[0] % 8f == 0f)
			{
				float angle = (float)Math.Atan(Math.Abs(target.Center.X - base.Projectile.Center.X) / 450f);
				angle *= (float)Math.Sign(target.Center.X - base.Projectile.Center.X);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Top + Vector2.UnitY * 7f, Utils.RotatedBy(new Vector2(0f, 0f - Main.rand.NextFloat(21f, 30.5f)), (double)angle, default(Vector2)), ModContent.ProjectileType<OldDukeSharkVomit>(), base.Projectile.damage, 5f, base.Projectile.owner);
			}
		}
		base.Projectile.velocity.Y += 0.5f;
		if (base.Projectile.velocity.Y > 10f)
		{
			base.Projectile.velocity.Y = 10f;
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}
}
