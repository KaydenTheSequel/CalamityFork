using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Yoyos;

public class ObliteratorYoyo : ModProjectile
{
	public SlotId GFB;

	public int GFBCounter;

	public int time;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<TheObliterator>();

	private static int DashStartup => 60;

	private static int DashCooldown => 15;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.YoyosLifeTimeMultiplier[base.Type] = -1f;
		ProjectileID.Sets.YoyosMaximumRange[base.Type] = TheObliterator.Reach;
		ProjectileID.Sets.YoyosTopSpeed[base.Type] = TheObliterator.Speed / 3f;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.aiStyle = 99;
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 7 * base.Projectile.MaxUpdates;
	}

	public override bool PreAI()
	{
		if (base.Projectile.FinalExtraUpdate())
		{
			if (base.Projectile.localAI[1] != (float)DashStartup)
			{
				base.Projectile.localAI[1]++;
			}
			if (base.Projectile.localAI[1] > (float)(DashStartup + DashCooldown))
			{
				base.Projectile.localAI[1] = 0f;
			}
		}
		if (base.Projectile.localAI[1] <= (float)DashStartup)
		{
			return true;
		}
		base.Projectile.aiStyle = -1;
		base.Projectile.timeLeft++;
		return true;
	}

	public override void AI()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		time++;
		if (Main.zenithWorld)
		{
			if (time == 1)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/BoomBoomKawaii")
				{
					IsLooped = true
				};
				GFB = SoundEngine.PlaySound(in style);
				GFBCounter++;
			}
			if (time % 30 == 0 && GFBCounter > 0)
			{
				GFBCounter--;
			}
			for (int i = 0; i < 13; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + ((float)i * (float)Math.PI + base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * 2f, Main.rand.Next(130, 135), ((float)i * (float)Math.PI + base.Projectile.rotation * (float)Math.Sign(base.Projectile.velocity.X)).ToRotationVector2() * Main.rand.NextFloat(1f, 90f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.1f, 1.7f);
			}
			if (SoundEngine.TryGetActiveSound(GFB, out ActiveSound RumblePitch) && RumblePitch.IsPlaying)
			{
				RumblePitch.Pitch = MathHelper.Lerp(0f, 1f, MathHelper.Clamp((float)GFBCounter * 0.1f, 0f, 1f));
				RumblePitch.Volume = MathHelper.Lerp(1f, 1.5f, (float)GFBCounter * 0.1f);
			}
		}
		if (base.Projectile.FinalExtraUpdate() && base.Projectile.localAI[1] == (float)DashStartup)
		{
			List<NPC> targets = new List<NPC>();
			float laserRange = 600f;
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			Vector2 center;
			while (enumerator.MoveNext())
			{
				NPC n = enumerator.Current;
				if (n.CanBeChasedBy(base.Projectile))
				{
					center = n.Center - base.Projectile.Center;
					if (((Vector2)(ref center)).Length() <= laserRange && Collision.CanHit(base.Projectile.Center, 1, 1, n.Center, 1, 1))
					{
						targets.Add(n);
					}
				}
			}
			if (targets.Count == 0)
			{
				return;
			}
			targets = targets.OrderBy(delegate(NPC x)
			{
				//IL_0007: Unknown result type (might be due to invalid IL or missing references)
				return x.Distance(base.Projectile.Center);
			}).ToList();
			base.Projectile.velocity = base.Projectile.DirectionTo(targets[0].Center);
			base.Projectile.Center = targets[0].Center - new Vector2(base.Projectile.velocity.X * (float)targets[0].width * 0.5f, base.Projectile.velocity.Y * (float)targets[0].height * 0.5f);
			Projectile projectile = base.Projectile;
			projectile.velocity *= 15f;
			for (int i2 = 0; i2 < 20; i2++)
			{
				Projectile projectile2 = base.Projectile;
				projectile2.Center -= base.Projectile.velocity;
				if (Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
				{
					Projectile projectile3 = base.Projectile;
					projectile3.Center += base.Projectile.velocity;
					break;
				}
			}
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<DoGWeaponTeleportRift>(), 0, 0f, base.Projectile.owner);
			}
			int laserAmount = 8;
			for (int i3 = 0; i3 < laserAmount; i3++)
			{
				int sparkLifetime = Main.rand.Next(30, 45);
				float sparkScale = Main.rand.NextFloat(0.8f, 1f) + 0.05f;
				Color sparkColor = Color.Lerp(Color.Fuchsia, Color.AliceBlue, Main.rand.NextFloat(0.5f));
				sparkColor = Color.Lerp(sparkColor, Color.Cyan, Main.rand.NextFloat());
				if (Main.rand.NextBool(5))
				{
					sparkScale *= 1.4f;
				}
				Vector2 spinningpoint = base.Projectile.velocity.SafeNormalize(Vector2.UnitX);
				double radians = (float)Math.PI * 2f * ((float)i3 / (float)laserAmount);
				center = default(Vector2);
				Vector2 sparkVelocity = spinningpoint.RotatedBy(radians, center) * 4f;
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, sparkVelocity, affectedByGravity: false, sparkLifetime, sparkScale, sparkColor));
			}
			base.Projectile.localAI[1]++;
			base.Projectile.ResetLocalNPCHitImmunity();
		}
		base.Projectile.aiStyle = 99;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(10f, 10f);
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/Yoyos/ObliteratorYoyoGlow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, origin, 2f, (SpriteEffects)0);
		if (!(base.Projectile.localAI[1] <= (float)DashStartup))
		{
			Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Particles/Jaws", (AssetRequestMode)2).Value;
			Main.spriteBatch.SetBlendState(BlendState.Additive);
			Main.spriteBatch.Draw(tex, base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 16f - Main.screenPosition, (Rectangle?)null, Color.Fuchsia, base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f, tex.Size() * 0.5f, 0.33f, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(tex, base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 16f - Main.screenPosition, (Rectangle?)null, Color.Aqua, base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f, tex.Size() * 0.5f, 0.25f, (SpriteEffects)0, 0f);
			Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld && SoundEngine.TryGetActiveSound(GFB, out ActiveSound RumblePlaying) && RumblePlaying.IsPlaying)
		{
			RumblePlaying?.Stop();
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.localAI[1] > (float)DashStartup)
		{
			modifiers.SourceDamage *= 6f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[1] > (float)DashStartup)
		{
			base.Projectile.localAI[1] = 0f;
			target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 180);
			for (int i = 0; i < 10; i++)
			{
				Vector2 sparkVelocity = -base.Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedByRandom(MathHelper.ToRadians(20f)) * Main.rand.NextFloat(26f, 32f);
				int sparkLifetime = Main.rand.Next(10, 20);
				float sparkScale = Main.rand.NextFloat(1.4f, 1.8f);
				Color sparkColor = Color.Lerp(Main.rand.NextBool() ? Color.Cyan : Color.Purple, Color.White, Main.rand.NextFloat(0f, 0.3f));
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, sparkVelocity, affectedByGravity: false, sparkLifetime, sparkScale, sparkColor));
			}
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/OtherworldlyHit");
			style.Pitch = -0.45f;
			style.Volume = 0.33f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		else
		{
			target.AddBuff(ModContent.BuffType<WhisperingDeath>(), 180);
		}
		GFBCounter = 15;
	}
}
