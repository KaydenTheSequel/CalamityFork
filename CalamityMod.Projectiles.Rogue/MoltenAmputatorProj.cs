using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class MoltenAmputatorProj : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public Vector2 squash;

	public float fakeRot;

	public int returnTime;

	public bool pulled;

	public bool returning;

	public int direction;

	public int pulledTimer;

	public bool AMPUTATE;

	public float effectScale;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/MoltenAmputator";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 74;
		base.Projectile.height = 74;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 35 * base.Projectile.MaxUpdates;
		base.Projectile.timeLeft = 900;
		base.Projectile.extraUpdates = 4;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		//IL_0606: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_0616: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_077d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0782: Unknown result type (might be due to invalid IL or missing references)
		//IL_0791: Unknown result type (might be due to invalid IL or missing references)
		//IL_079b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07be: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0907: Unknown result type (might be due to invalid IL or missing references)
		//IL_0911: Unknown result type (might be due to invalid IL or missing references)
		//IL_0917: Unknown result type (might be due to invalid IL or missing references)
		//IL_0919: Unknown result type (might be due to invalid IL or missing references)
		//IL_091e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0923: Unknown result type (might be due to invalid IL or missing references)
		//IL_092e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0935: Unknown result type (might be due to invalid IL or missing references)
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_081d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0827: Unknown result type (might be due to invalid IL or missing references)
		//IL_082d: Unknown result type (might be due to invalid IL or missing references)
		//IL_082f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0834: Unknown result type (might be due to invalid IL or missing references)
		//IL_0839: Unknown result type (might be due to invalid IL or missing references)
		//IL_0844: Unknown result type (might be due to invalid IL or missing references)
		//IL_084b: Unknown result type (might be due to invalid IL or missing references)
		//IL_086c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0871: Unknown result type (might be due to invalid IL or missing references)
		//IL_088a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0899: Unknown result type (might be due to invalid IL or missing references)
		//IL_096a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0963: Unknown result type (might be due to invalid IL or missing references)
		//IL_0979: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b07: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb9: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		if (base.Projectile.ai[2] == 5f && base.Projectile.ai[2] < 10f && !base.Projectile.Calamity().stealthStrike)
		{
			if ((float)time >= (float)returnTime * 0.07f && (float)time < (float)returnTime * 1.4f)
			{
				if (!pulled)
				{
					base.Projectile.localNPCHitCooldown = -1;
					for (int i = 0; i < Main.maxNPCs; i++)
					{
						base.Projectile.localNPCImmunity[i] = 0;
					}
					base.Projectile.ai[2] = 10f;
					base.Projectile.numHits = 0;
					base.Projectile.velocity = base.Projectile.Center.DirectionTo(Owner.Center) * 3f;
					base.Projectile.extraUpdates += 4;
					pulled = true;
				}
			}
			else
			{
				base.Projectile.ai[2] = 0f;
			}
		}
		if (time >= returnTime)
		{
			returning = true;
		}
		fakeRot += 0.13f * (float)direction;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 5f)
		{
			effectScale = MathHelper.Lerp(effectScale, 1f, 0.05f);
		}
		else if (returning)
		{
			effectScale = MathHelper.Lerp(effectScale, 0f, 0.05f);
		}
		float x = MathHelper.Clamp(Utils.GetLerpValue(16f, 2f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true), 0.3f, 1f);
		float y = 1f;
		squash = new Vector2(x, y);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 7)
		{
			base.Projectile.frame = 0;
		}
		if (pulled)
		{
			if ((float)time < (float)returnTime * 1.5f)
			{
				time = (int)((float)returnTime * 1.5f);
			}
			if (pulledTimer > 20)
			{
				if (time % 2 == 0)
				{
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 19, 0.05f, Color.Orchid * 0.3f, new Vector2(0.7f, 1.2f), quickShrink: true, glow: false, 0.35f));
				}
				else
				{
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 11, 0.07f, Color.Goldenrod * 0.45f, new Vector2(0.7f, 1.2f), quickShrink: true, glow: false, 0.55f));
				}
			}
			pulledTimer++;
		}
		if ((float)direction == 0f)
		{
			direction = ((base.Projectile.Center.DirectionTo(Owner.Calamity().mouseWorld).X > 0f) ? 1 : (-1));
		}
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.Gold;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 1.5f);
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = 7 * base.Projectile.MaxUpdates;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/SwooshMid");
			style.MaxInstances = -1;
			style.Volume = (base.Projectile.Calamity().stealthStrike ? 0.15f : 0.3f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		base.Projectile.ai[0]++;
		if (time == returnTime)
		{
			base.Projectile.netUpdate = true;
		}
		if (returning)
		{
			Vector2 moveToTrackingPos = (Owner.Center - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 3f + 8f * Utils.GetLerpValue((float)returnTime * 1.2f, (float)returnTime * 1.5f, time, clamped: true))
			{
				Projectile projectile = base.Projectile;
				projectile.velocity += moveToTrackingPos * (0.02f + 4f * Utils.GetLerpValue((float)returnTime * 1.2f, (float)returnTime * 2.5f, time, clamped: true));
			}
			else
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.95f;
			}
			if (Main.myPlayer == base.Projectile.owner)
			{
				Rectangle hitbox = base.Projectile.Hitbox;
				if (((Rectangle)(ref hitbox)).Intersects(Owner.Hitbox))
				{
					base.Projectile.Kill();
				}
			}
		}
		else
		{
			if (base.Projectile.Calamity().stealthStrike && (float)time > (float)returnTime * 0.2f)
			{
				Vector2 moveToTrackingPos2 = (Owner.ClampedMouseWorld() - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
				if (((Vector2)(ref base.Projectile.velocity)).Length() < 16f * Utils.GetLerpValue((float)returnTime * 0.7f, 0f, time, clamped: true))
				{
					Projectile projectile3 = base.Projectile;
					projectile3.velocity += moveToTrackingPos2 * (0.5f * Utils.GetLerpValue((float)returnTime * 0.7f, 0f, time, clamped: true));
				}
				else
				{
					Projectile projectile4 = base.Projectile;
					projectile4.velocity *= 0.95f;
				}
			}
			Projectile projectile5 = base.Projectile;
			projectile5.velocity *= (((float)time > (float)returnTime * 0.4f) ? 0.97f : 0.982f);
		}
		if (Main.rand.NextBool(5 * ((!base.Projectile.Calamity().stealthStrike) ? 1 : 2)))
		{
			int numParts = 2;
			for (int j = 0; j < numParts; j++)
			{
				float fade = (Utils.GetLerpValue(5f, 2f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true) * 3f + 1f) * squash.X;
				float rot = fakeRot + (float)Math.PI * 2f * (float)j / (float)numParts;
				Vector2 vel = (-base.Projectile.velocity).MoveTowards(Utils.RotatedBy(new Vector2(0f, -130f), (double)rot, default(Vector2)).RotatedBy(-1.3f * (float)direction), Utils.GetLerpValue(5f, 2f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true));
				if (Main.rand.NextBool(6))
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, -70f * squash.X), (double)rot, default(Vector2)), vel.RotatedByRandom(0.4000000059604645) * fade, "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 17, Main.rand.NextFloat(1.15f, 1.3f), Color.Lerp(Color.Orchid, Color.White, Main.rand.NextFloat(0f, 0.7f)), new Vector2(1.3f, 0.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.3f, 0.4f)));
				}
				else
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, -70f * squash.X), (double)rot, default(Vector2)), vel.RotatedByRandom(0.4000000059604645) * fade, "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 17, Main.rand.NextFloat(0.75f, 0.82f), Main.rand.NextBool(4) ? Color.Khaki : Color.Orange, new Vector2(1.3f, 0.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.3f, 0.4f)));
				}
				if (Main.rand.NextBool(6))
				{
					Vector2 position = base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, -70f * squash.X), (double)rot, default(Vector2));
					int type = (Main.rand.NextBool(4) ? 278 : ModContent.DustType<LightDust>());
					newColor = default(Color);
					Dust dust = Dust.NewDustPerfect(position, type, null, 0, newColor);
					dust.noGravity = dust.type != 278;
					dust.scale = ((dust.type == 278) ? 0.95f : 1.2f);
					dust.color = Color.Lerp(Color.Orchid, Color.White, Main.rand.NextFloat(0f, 0.7f));
					dust.velocity = (vel * 2f).RotatedByRandom(0.4000000059604645) * fade;
				}
				else
				{
					Vector2 position2 = base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, -70f * squash.X), (double)rot, default(Vector2));
					int type2 = (Main.rand.NextBool(4) ? 278 : ModContent.DustType<LightDust>());
					newColor = default(Color);
					Dust dust2 = Dust.NewDustPerfect(position2, type2, null, 0, newColor);
					dust2.noGravity = dust2.type != 278;
					dust2.scale = ((dust2.type == 278) ? 0.75f : 0.9f);
					dust2.color = (Main.rand.NextBool(4) ? Color.Khaki : Color.Goldenrod);
					dust2.velocity = (vel * 2f).RotatedByRandom(0.4000000059604645) * fade;
				}
			}
		}
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0604: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_067a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0898: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08df: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_071c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_072c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_073b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0754: Unknown result type (might be due to invalid IL or missing references)
		//IL_075c: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_077d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0776: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_079d: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07de: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0839: Unknown result type (might be due to invalid IL or missing references)
		//IL_0832: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 120);
		Player Owner = Main.player[base.Projectile.owner];
		if (base.Projectile.Calamity().stealthStrike)
		{
			if (base.Projectile.numHits == 0)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/FinalDawnSlash");
				style.Volume = 0.5f;
				style.Pitch = Main.rand.NextFloat(0.4f, 0.85f);
				style.MaxInstances = -1;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			if (base.Projectile.numHits < 8)
			{
				int numParts = 2;
				for (int i = 0; i < numParts; i++)
				{
					float fade = 4f;
					float rot = fakeRot + (float)Math.PI * 2f * (float)i / (float)numParts * 0.4f + (float)(base.Projectile.numHits * 5);
					Vector2 vel = Utils.RotatedBy(new Vector2(0f, -130f), (double)rot, default(Vector2)).RotatedBy(-1.3f * (float)direction) * squash.X;
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, -70f * squash.X), (double)rot, default(Vector2)), (vel * 0.02f).RotatedByRandom(1.399999976158142) * fade, "CalamityMod/Particles/GlowSpark", affectedByGravity: false, 18, Main.rand.NextFloat(0.015f, 0.025f), Color.Goldenrod, new Vector2(2f, 0.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.6f, 0.7f)));
					GeneralParticleHandler.SpawnParticle(new BloodParticle(base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, -70f * squash.X), (double)rot, default(Vector2)), (vel * 0.04f).RotatedByRandom(1.399999976158142) * fade, Main.rand.Next(13, 25), Main.rand.NextFloat(0.85f, 1.2f), Main.rand.NextBool() ? Color.Goldenrod : Color.Orange));
					if (Main.rand.NextBool(6))
					{
						Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, -70f * squash.X), (double)rot, default(Vector2)), Main.rand.NextBool(4) ? 278 : ModContent.DustType<LightDust>());
						dust.noGravity = true;
						dust.scale = ((dust.type == 278) ? 0.95f : 1.2f);
						dust.color = Color.Lerp(Color.Orchid, Color.White, Main.rand.NextFloat(0f, 0.7f));
						dust.velocity = (vel * 0.04f).RotatedByRandom(1.399999976158142) * fade;
					}
					else
					{
						Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, -70f * squash.X), (double)rot, default(Vector2)), Main.rand.NextBool(4) ? 278 : ModContent.DustType<LightDust>());
						dust2.noGravity = true;
						dust2.scale = ((dust2.type == 278) ? 0.75f : 0.9f);
						dust2.color = (Main.rand.NextBool(4) ? Color.Khaki : Color.Goldenrod);
						dust2.velocity = (vel * 0.04f).RotatedByRandom(1.399999976158142) * fade;
					}
				}
			}
		}
		if (pulled && (pulledTimer < 10 || Owner.Calamity().focusFlurryAttackCount > 0) && !AMPUTATE && !base.Projectile.Calamity().stealthStrike)
		{
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), base.Projectile.damage * 7, 0f, Owner.whoAmI, target.whoAmI, 1f);
			GeneralParticleHandler.SpawnParticle(new CustomPulse(target.Center, Vector2.Zero, Color.Orange, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 1.1f, 1f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			for (int j = 0; j < 3; j++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(target.Center, Vector2.Zero, Color.Orchid, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.7f * (float)(j + 1), 1f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(target.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.35f * (float)(j + 1), 0.5f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			for (int k = 0; k < 6; k++)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(target.Center, base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * -5f * (float)((k % 2 != 0) ? 1 : (-1)), affectedByGravity: false, 15, 0.08f - (float)k * 0.01f, Color.Goldenrod, new Vector2(5f, 0.8f), quickShrink: true, glow: false, 1.2f));
			}
			for (int l = 0; l < 20; l++)
			{
				int dir = ((l < 10) ? 1 : (-1));
				GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center + Main.rand.NextVector2Circular(13f, 13f), base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * -25f * Main.rand.NextFloat(0.4f, 2f) * (float)dir, affectedByGravity: false, 12, 1.1f, Main.rand.NextBool(5) ? Color.Khaki : Color.Goldenrod));
				GeneralParticleHandler.SpawnParticle(new BloodParticle(base.Projectile.Center, ((base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * -25f).RotatedByRandom(0.6000000238418579) + new Vector2(0f, -1.5f)) * Main.rand.NextFloat(0.4f, 2f) * (float)dir, Main.rand.Next(13, 25), Main.rand.NextFloat(0.85f, 1.2f), Main.rand.NextBool() ? Color.Goldenrod : Color.Orange));
			}
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HellkiteFullCharge");
			style.Volume = 1f;
			style.Pitch = Main.rand.NextFloat(0.5f, 0.6f);
			SoundEngine.PlaySound(in style, target.Center);
			style = new SoundStyle("CalamityMod/Sounds/NPCKilled/PerfLargeDeath");
			style.Volume = 1f;
			style.Pitch = Main.rand.NextFloat(0.5f, 0.6f);
			SoundEngine.PlaySound(in style, target.Center);
			AMPUTATE = true;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 80f, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float drawRotation = base.Projectile.rotation;
		Asset<Texture2D> p = ModContent.Request<Texture2D>("CalamityMod/Particles/CircularSmearFire2", (AssetRequestMode)2);
		Asset<Texture2D> p2 = ModContent.Request<Texture2D>("CalamityMod/Particles/CircularSmearFire3", (AssetRequestMode)2);
		for (int i = 0; i < 3; i++)
		{
			Texture2D value = p2.Value;
			Color val = Color.Orchid;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value, drawPosition, null, val * 0.25f * effectScale, fakeRot * (Main.rand.NextFloat(1.5f, 1.55f) * ((float)i * 0.5f + 0.2f)), p2.Size() * 0.5f, 1.1f * Main.rand.NextFloat(0.8f, 1.15f) * effectScale, (SpriteEffects)(direction == -1));
			Texture2D value2 = p.Value;
			val = Color.Orange;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value2, drawPosition, null, val * 0.35f * effectScale, fakeRot * (Main.rand.NextFloat(1.1f, 1.15f) * ((float)i * 0.5f + 0.2f)), p.Size() * 0.5f, 0.9f * effectScale, (SpriteEffects)(direction == -1));
		}
		Asset<Texture2D> obj = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/MoltenAmputatorAnimated", (AssetRequestMode)2);
		Rectangle frame = obj.Frame(1, 8, 0, base.Projectile.frame);
		Main.EntitySpriteDraw(origin: frame.Size() * 0.5f, texture: obj.Value, position: drawPosition, sourceRectangle: frame, color: lightColor, rotation: drawRotation, scale: squash * base.Projectile.scale, effects: (SpriteEffects)(direction == -1));
		return false;
	}

	public MoltenAmputatorProj()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		squash = new Vector2(1f, 1f);
		returnTime = 180;
		base._002Ector();
	}
}
