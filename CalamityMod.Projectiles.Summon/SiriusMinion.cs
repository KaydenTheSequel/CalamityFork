using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Enums;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Systems.Mechanic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SiriusMinion : ModProjectile, ILocalizedModType, IModType
{
	public bool CheckForSpawning;

	public List<StarburstEntity> starburstsToFire = new List<StarburstEntity>();

	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer moddedOwner => Owner.Calamity();

	public ref float TimerForShooting => ref base.Projectile.ai[0];

	public int MinionSlotsToAdd
	{
		get
		{
			return (int)base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 38;
		base.Projectile.height = 48;
		base.Projectile.minionSlots = 1f;
		base.Projectile.penetrate = -1;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		NPC target = base.Projectile.Center.MinionHoming(5000f, Owner);
		if (MinionSlotsToAdd > 0)
		{
			float minionSlotsAvaliable = Owner.maxMinions;
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile item = enumerator.Current;
				if (item.owner == base.Projectile.owner)
				{
					minionSlotsAvaliable -= item.minionSlots;
				}
			}
			while (minionSlotsAvaliable >= 1f && MinionSlotsToAdd > 0)
			{
				base.Projectile.minionSlots++;
				minionSlotsAvaliable--;
				MinionSlotsToAdd--;
				base.Projectile.netUpdate = true;
			}
			MinionSlotsToAdd = 0;
		}
		CheckMinionExistince();
		SpawnEffect();
		ShootTarget(target);
		if (target != null)
		{
			moddedOwner.StarburstSpawnFrameCounter += base.Projectile.minionSlots / (float)CalamityUtils.SecondsToFrames(3f);
			while (moddedOwner.StarburstSpawnFrameCounter >= 1f && moddedOwner.StratusStarburst <= CalamityPlayer.MaxStratusStarburst)
			{
				moddedOwner.StratusStarburst++;
				moddedOwner.StarburstEntities.Add(new StarburstEntity(base.Projectile.Center));
				moddedOwner.StarburstSpawnFrameCounter--;
			}
		}
		Lighting.AddLight(base.Projectile.Center, 0.5f, 0.5f, 1f);
		TimerForShooting++;
		base.Projectile.scale = MathHelper.Lerp(0.3f, 0.33f, 1f + MathF.Sin((float)base.Projectile.frameCounter * 0.01f) * 0.5f);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 31415)
		{
			base.Projectile.frameCounter = 0;
		}
		base.Projectile.spriteDirection = Owner.direction;
		base.Projectile.Center = Owner.oldPosition + Owner.Size * 0.5f - new Vector2((float)(64 * base.Projectile.spriteDirection), 96f - Owner.gfxOffY);
		base.Projectile.velocity = Owner.velocity * 0f;
		Vector2 SiriusPos = base.Projectile.Center + base.Projectile.velocity;
		float value = 0f;
		if (base.Projectile.ai[2] > 2f)
		{
			foreach (StarburstEntity item2 in starburstsToFire)
			{
				value += (float)item2.value;
			}
		}
		float SiriusScale = 0.055f + 0.001f * ((float)moddedOwner.AvaliableStarburst + value);
		SpawnStar(0f, new Vector2(0f, 0f), 1.5f, 0, 300);
		SpawnStar(2f, new Vector2(-118f, 217f), 0.75f, 40);
		SpawnStar(3f, new Vector2(-67f, 272f), 0.75f, 120);
		SpawnStar(4f, new Vector2(119f, 32f), 0.75f, 5);
		SpawnStar(5f, new Vector2(-192f, 284f), 0.75f, 10);
		SpawnStar(6f, new Vector2(-62f, 11f), 0.5f, 75);
		SpawnStar(7f, new Vector2(-50f, -103f), 0.5f, 130);
		SpawnStar(8f, new Vector2(-101f, -23f), 0.5f, 20);
		SpawnStar(9f, new Vector2(46f, 59f), 0.5f, 100);
		SpawnStar(10f, new Vector2(-49f, 166f), 0.5f, 60);
		void SpawnStar(float SlotRequirement, Vector2 offset, float intensity, int flashOffset = 0, int flashMod = 100)
		{
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0115: Unknown result type (might be due to invalid IL or missing references)
			//IL_011a: Unknown result type (might be due to invalid IL or missing references)
			//IL_011f: Unknown result type (might be due to invalid IL or missing references)
			//IL_013f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0145: Unknown result type (might be due to invalid IL or missing references)
			//IL_0146: Unknown result type (might be due to invalid IL or missing references)
			//IL_0150: Unknown result type (might be due to invalid IL or missing references)
			//IL_016b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0170: Unknown result type (might be due to invalid IL or missing references)
			if (!(SlotRequirement > 0f) || !(base.Projectile.minionSlots < SlotRequirement))
			{
				offset.X *= base.Projectile.spriteDirection;
				BloomParticle star = new BloomParticle(SiriusPos + offset * base.Projectile.scale - Owner.oldVelocity * Math.Clamp(((Vector2)(ref offset)).Length() * 0.001f, 0f, 1f), Vector2.Zero, Color.SlateBlue * (((Owner.miscCounter + flashOffset) % flashMod < 5) ? 0.75f : 1f), 2f * SiriusScale * intensity, 2f * SiriusScale * intensity, 2, fade: false);
				CustomSpark particle = new CustomSpark(SiriusPos + offset * base.Projectile.scale - Owner.oldVelocity * Math.Clamp(((Vector2)(ref offset)).Length() * 0.001f, 0f, 1f), Vector2.UnitX.RotatedBy((float)Math.PI * ((float)Owner.miscCounter / 300f)) * 0.1f, "CalamityMod/Particles/Sparkle", affectedByGravity: false, 2, 10f * SiriusScale * intensity, Color.SkyBlue, Vector2.One);
				GeneralParticleHandler.SpawnParticle(star, pixelate: false, GeneralDrawLayer.AfterProjectiles);
				GeneralParticleHandler.SpawnParticle(particle, pixelate: false, GeneralDrawLayer.AfterProjectiles);
			}
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.minionSlots);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.minionSlots = reader.ReadSingle();
	}

	public void CheckMinionExistince()
	{
		Owner.AddBuff(ModContent.BuffType<SiriusBuff>(), 3600);
		if (base.Projectile.type == ModContent.ProjectileType<SiriusMinion>())
		{
			if (Owner.dead)
			{
				moddedOwner.sirius = false;
			}
			if (moddedOwner.sirius)
			{
				base.Projectile.timeLeft = 2;
				moddedOwner.StratusStarburstResetTimer = (int)MathHelper.Max((float)moddedOwner.StratusStarburstResetTimer, 60f);
			}
		}
	}

	public void SpawnEffect()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		if (!CheckForSpawning)
		{
			int dustAmt = 50;
			for (int d = 0; d < dustAmt; d++)
			{
				Vector2 dustVelocity = ((float)Math.PI * 2f / (float)dustAmt * (float)d).ToRotationVector2() * 20f;
				Dust.NewDustPerfect(Owner.Center - Vector2.UnitY * 60f, 20, dustVelocity).noGravity = true;
			}
			CheckForSpawning = true;
		}
	}

	public void ShootTarget(NPC target)
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			return;
		}
		float timer = 90f * (10f / (10f + base.Projectile.minionSlots));
		if (TimerForShooting >= timer && base.Projectile.owner == Main.myPlayer)
		{
			TimerForShooting = 0f;
			SoundStyle style = FrigidflashBolt.UseSound with
			{
				Volume = 1f,
				Pitch = -0.15f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			int dustAmt = 50;
			for (int d = 0; d < dustAmt; d++)
			{
				Vector2 dustVelocity = ((float)Math.PI * 2f / (float)dustAmt * (float)d).ToRotationVector2() * 20f;
				Dust.NewDustPerfect(base.Projectile.Center, 20, dustVelocity).noGravity = true;
			}
			for (int i = 0; i < 2; i++)
			{
				Vector2 velocity = Utils.RotatedByRandom(new Vector2(25f, 0f), 3.1415927410125732);
				float damageMod = 1f + MathF.Pow(0.2f * base.Projectile.minionSlots, 1.5f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + velocity, velocity, ModContent.ProjectileType<SiriusBeam>(), (int)((float)base.Projectile.damage * damageMod), base.Projectile.knockBack, base.Projectile.owner);
			}
		}
		if (moddedOwner.AvaliableStarburst >= CalamityPlayer.MaxStratusStarburst)
		{
			base.Projectile.ai[2]++;
		}
		if (!(base.Projectile.ai[2] > 0f))
		{
			return;
		}
		float value = 0f;
		foreach (StarburstEntity item in starburstsToFire)
		{
			value += (float)item.value;
		}
		foreach (StarburstEntity star in moddedOwner.StarburstEntities)
		{
			if (value >= 50f)
			{
				break;
			}
			value += (float)star.value;
			starburstsToFire.Add(star);
		}
		foreach (StarburstEntity item3 in starburstsToFire)
		{
			item3.Center = Vector2.Lerp(item3.Center, base.Projectile.Center + base.Projectile.velocity, base.Projectile.ai[2] / 15f);
			item3.AICooldown = 2;
		}
		base.Projectile.ai[2]++;
		if (!(base.Projectile.ai[2] > 15f))
		{
			return;
		}
		if (Main.LocalPlayer.whoAmI == base.Projectile.owner)
		{
			for (int j = 0; j < 2; j++)
			{
				Vector2 velocity2 = base.Projectile.Center.DirectionTo(target.Center) * 10f;
				float damageMod2 = 40f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + velocity2, velocity2, ModContent.ProjectileType<SiriusQuasar>(), (int)((float)base.Projectile.damage * damageMod2), base.Projectile.knockBack, base.Projectile.owner, 1f);
			}
		}
		SoundEngine.PlaySound(in Exoblade.BeamHitSound, Owner.Center);
		GeneralParticleHandler.SpawnParticle(new DetailedExplosion(base.Projectile.Center, Vector2.Zero, Color.SkyBlue, Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, 0.75f, Main.rand.Next(15, 22)));
		GeneralParticleHandler.SpawnParticle(new DetailedExplosion(base.Projectile.Center, Vector2.Zero, Color.SlateBlue, Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, 0.55f, Main.rand.Next(10, 19), UseAdditiveBlend: false));
		GeneralParticleHandler.SpawnParticle(new DetailedExplosion(base.Projectile.Center, Vector2.Zero, Color.SlateBlue, Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, 0.4f, Main.rand.Next(10, 19), UseAdditiveBlend: false));
		for (int k = 0; k < 4; k++)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.SkyBlue, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.55f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		moddedOwner.StratusStarburst -= 50;
		base.Projectile.ai[2] = 0f;
		foreach (StarburstEntity item2 in starburstsToFire)
		{
			moddedOwner.StarburstEntities.Remove(item2);
		}
		starburstsToFire = new List<StarburstEntity>();
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, 200);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		Vector2 SiriusPos = base.Projectile.Center;
		ConnectStars(4f, new Vector2(0f, 0f), new Vector2(119f, 32f));
		ConnectStars(2f, new Vector2(0f, 0f), new Vector2(-118f, 217f));
		ConnectStars(6f, new Vector2(0f, 0f), new Vector2(-62f, 11f));
		ConnectStars(9f, new Vector2(119f, 32f), new Vector2(46f, 59f));
		ConnectStars(10f, new Vector2(46f, 59f), new Vector2(-49f, 166f));
		ConnectStars(10f, new Vector2(-49f, 166f), new Vector2(-67f, 272f));
		ConnectStars(3f, new Vector2(-67f, 272f), new Vector2(-118f, 217f));
		ConnectStars(5f, new Vector2(-118f, 217f), new Vector2(-192f, 284f));
		ConnectStars(8f, new Vector2(-62f, 11f), new Vector2(-101f, -23f));
		ConnectStars(8f, new Vector2(-101f, -23f), new Vector2(-50f, -103f));
		ConnectStars(7f, new Vector2(-50f, -103f), new Vector2(-62f, 11f));
		return false;
		void ConnectStars(float SlotRequirement, Vector2 point1, Vector2 point2)
		{
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_008e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0093: Unknown result type (might be due to invalid IL or missing references)
			//IL_009e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_010d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0112: Unknown result type (might be due to invalid IL or missing references)
			//IL_0117: Unknown result type (might be due to invalid IL or missing references)
			if (!(SlotRequirement > 0f) || !(base.Projectile.minionSlots < SlotRequirement))
			{
				point1.X *= base.Projectile.spriteDirection;
				point2.X *= base.Projectile.spriteDirection;
				Color color = Color.SkyBlue * 0.75f * ((MathF.Sin(Main.GlobalTimeWrappedHourly) + 1f) * 0.25f + 0.5f);
				Main.spriteBatch.DrawLineBetter(SiriusPos + point1 * base.Projectile.scale - Owner.oldVelocity * Math.Clamp(((Vector2)(ref point1)).Length() * 0.001f, 0f, 1f), SiriusPos + point2 * base.Projectile.scale - Owner.oldVelocity * Math.Clamp(((Vector2)(ref point2)).Length() * 0.001f, 0f, 1f), color, 2f);
			}
		}
	}
}
