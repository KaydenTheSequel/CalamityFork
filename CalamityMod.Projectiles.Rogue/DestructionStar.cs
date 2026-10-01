using System;
using CalamityMod.Dusts;
using CalamityMod.Enums;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class DestructionStar : ModProjectile, ILocalizedModType, IModType
{
	private float Radius = 47f;

	public float fade;

	public int beepTimerMax = 50;

	public int beepTimer;

	public int stealthHitCooldown;

	public bool makeReadySound;

	public int rotDirection = 1;

	public float rot = 0.01f;

	public bool setRot = true;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/StarofDestruction";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 94;
		base.Projectile.height = 94;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 25;
	}

	public override void AI()
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Unknown result type (might be due to invalid IL or missing references)
		//IL_059f: Unknown result type (might be due to invalid IL or missing references)
		if (setRot)
		{
			rotDirection = (Main.rand.NextBool() ? 1 : (-1));
			setRot = false;
		}
		base.Projectile.rotation += rot;
		rot += Utils.Remap(base.Projectile.timeLeft, 0f, 240f, 0.02f, 0.0015f) * (float)rotDirection;
		Vector2 moveToMouse = (Main.player[base.Projectile.owner].ClampedMouseWorld() - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
		if (base.Projectile.numHits == 0 || base.Projectile.Calamity().stealthStrike)
		{
			if (time > 8f)
			{
				if (((Vector2)(ref base.Projectile.velocity)).Length() < (float)(base.Projectile.Calamity().stealthStrike ? 20 : 13))
				{
					Projectile projectile = base.Projectile;
					projectile.velocity += moveToMouse * (base.Projectile.Calamity().stealthStrike ? 1f : 0.4f);
				}
				else
				{
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= 0.9f;
				}
			}
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 3f)
			{
				Vector2 placement = base.Projectile.Center + Main.rand.NextVector2Circular(70f, 70f);
				float speed = Main.rand.NextFloat(0.6f, 0.9f);
				if (Main.rand.NextBool())
				{
					int dustStyle = ModContent.DustType<VoidDustInverted>();
					Dust dust = Dust.NewDustPerfect(placement, dustStyle);
					dust.scale = Main.rand.NextFloat(0.8f, 1.2f);
					dust.velocity = -base.Projectile.velocity * speed;
					dust.noGravity = true;
					dust.color = Color.LightGreen;
				}
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + Main.rand.NextVector2Circular(20f, 20f), -base.Projectile.velocity * speed, Color.Black, 22, Main.rand.NextFloat(-0.2f, 0.2f) + speed, 0.4f, Main.rand.NextFloat(-0.05f, 0.05f)));
			}
			fade = MathHelper.Lerp(fade, 0f, 0.088f);
			if (base.Projectile.timeLeft < 240)
			{
				if (beepTimer >= beepTimerMax)
				{
					beepTimerMax = (int)((float)beepTimerMax * 0.81f);
					fade = 1f;
					rot *= -0.8f;
					rotDirection *= -1;
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/PlagueSounds/PBGAttackSwitchShort");
					style.Volume = 0.3f;
					style.Pitch = 0.7f * Utils.GetLerpValue(240f, 0f, base.Projectile.timeLeft, clamped: true);
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					beepTimer = 0;
				}
				beepTimer++;
			}
		}
		else if (!base.Projectile.Calamity().stealthStrike)
		{
			fade = 0f;
			base.Projectile.velocity = Vector2.Zero;
			if (time == 8f)
			{
				base.Projectile.Kill();
			}
		}
		time++;
		if (stealthHitCooldown > 0)
		{
			stealthHitCooldown--;
		}
		if (base.Projectile.Calamity().stealthStrike && base.Projectile.numHits > 0)
		{
			base.Projectile.velocity = Vector2.Zero;
			if (time == 8f)
			{
				base.Projectile.numHits = 0;
				stealthHitCooldown = 45;
				makeReadySound = true;
				Explode(big: false);
				Projectile projectile3 = base.Projectile;
				projectile3.velocity += moveToMouse * 30f;
			}
		}
		if (stealthHitCooldown == 0 && makeReadySound)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DeadSunRicochet");
			style.Volume = 0.7f;
			style.Pitch = 0.2f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			for (int i = 0; i <= 15; i++)
			{
				int dustStyle2 = (Main.rand.NextBool() ? 66 : 263);
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, dustStyle2, base.Projectile.velocity);
				dust2.scale = Main.rand.NextFloat(0.8f, 1f);
				dust2.velocity = Utils.RotatedByRandom(new Vector2(13f, 13f), 100.0) * Main.rand.NextFloat(0.2f, 1f);
				dust2.noGravity = true;
				dust2.color = Color.LightGreen;
			}
			makeReadySound = false;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		time = 0f;
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DudFire");
		style.Volume = 0.6f;
		style.Pitch = -0.4f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Black, "CalamityMod/Particles/SmallBloomRing", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 3f, 0f, 12, UseAdditiveBlend: false, 1f, fade: true, 1f, (SpriteEffects)0));
	}

	public override void OnKill(int timeLeft)
	{
		Explode(base.Projectile.Calamity().stealthStrike);
	}

	internal void Explode(bool big)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		float sizeBonus = ((!big) ? 1 : 2);
		float bigExplosionDamage = 3f;
		Player Owner = Main.player[base.Projectile.owner];
		Vector2 center = base.Projectile.Center;
		Vector2 zero = Vector2.Zero;
		Color val = Color.LightGreen;
		((Color)(ref val)).A = 0;
		CustomPulse customPulse = new CustomPulse(center, zero, val, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.82f * sizeBonus, 11, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0);
		GeneralParticleHandler.SpawnParticle(customPulse);
		customPulse.DrawLayer = GeneralDrawLayer.AfterEverything;
		CustomPulse customPulse2 = new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.74f * sizeBonus, 11, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0);
		GeneralParticleHandler.SpawnParticle(customPulse2);
		customPulse2.DrawLayer = GeneralDrawLayer.AfterEverything;
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<DestructionExplosion>(), (int)((float)base.Projectile.damage * (big ? bigExplosionDamage : (base.Projectile.Calamity().stealthStrike ? 0.25f : 1f))), base.Projectile.knockBack * 1.5f, base.Projectile.owner);
		if (!big)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MeldExplosion");
			style.Volume = 0.9f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		else
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/AresGaussNukeExplosion");
			style.Volume = 1f;
			style.Pitch = -0.5f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (big)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/EarthMeteor");
			style.Volume = 0.9f;
			style.Pitch = -0.2f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			style = new SoundStyle("CalamityMod/Sounds/Item/ExobladeDashImpact");
			style.Volume = 0.9f;
			style.Pitch = -0.2f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		CustomPulse customPulse3 = new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.LightGreen, "CalamityMod/Particles/BloomRing", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 3.3279998f * sizeBonus, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0);
		GeneralParticleHandler.SpawnParticle(customPulse3);
		customPulse3.DrawLayer = GeneralDrawLayer.AfterEverything;
		CustomPulse customPulse4 = new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.LightGreen, "CalamityMod/Particles/SoftRoundExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.3f * sizeBonus, 30, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0);
		GeneralParticleHandler.SpawnParticle(customPulse4);
		customPulse4.DrawLayer = GeneralDrawLayer.AfterEverything;
		CustomPulse customPulse5 = new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.LightGreen * 0.55f, "CalamityMod/Particles/SoftRoundExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.40500003f * sizeBonus, 30, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0);
		GeneralParticleHandler.SpawnParticle(customPulse5);
		customPulse5.DrawLayer = GeneralDrawLayer.AfterEverything;
		for (int i = 0; i < 2; i++)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Black, "CalamityMod/Particles/SmallBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), (2.2f + (float)i * 0.5f) * sizeBonus, 0f, 80, UseAdditiveBlend: false, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		for (int j = 0; (float)j < 19f * sizeBonus; j++)
		{
			int dustStyle = ModContent.DustType<VoidDustInverted>();
			Vector2 center2 = base.Projectile.Center;
			val = default(Color);
			Dust dust = Dust.NewDustPerfect(center2, dustStyle, null, 0, val);
			dust.scale = Main.rand.NextFloat(1.4f, 2.2f) * sizeBonus;
			dust.velocity = Utils.RotatedByRandom(new Vector2(21f, 21f), 100.0) * Main.rand.NextFloat(0.1f, 1f) * sizeBonus;
			dust.noGravity = true;
			dust.color = Color.LightGreen;
		}
		for (int k = 0; (float)k < 25f * sizeBonus; k++)
		{
			Vector2 center3 = base.Projectile.Center;
			val = default(Color);
			Dust dust2 = Dust.NewDustPerfect(center3, 278, null, 0, val);
			dust2.noGravity = false;
			dust2.velocity = Utils.RotatedByRandom(new Vector2(18f, 18f), 100.0) * Main.rand.NextFloat(0.3f, 1f) * sizeBonus;
			dust2.scale = Main.rand.NextFloat(0.8f, 1.2f);
			dust2.color = Color.LightGreen;
		}
		Owner.SetScreenshake(6f * (big ? 2.5f : 1f));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		Asset<Texture2D> tex2 = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Rogue/StarofDestructionGhost", (AssetRequestMode)2);
		Asset<Texture2D> portal = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		Color lightGreen;
		if (base.Projectile.Calamity().stealthStrike)
		{
			Texture2D value = portal.Value;
			Vector2 position = base.Projectile.Center - Main.screenPosition;
			lightGreen = Color.LightGreen;
			((Color)(ref lightGreen)).A = 0;
			Main.EntitySpriteDraw(value, position, null, lightGreen * Utils.GetLerpValue(45f, 0f, stealthHitCooldown), 0f, portal.Size() * 0.5f, 1.1f * Utils.GetLerpValue(45f, 0f, stealthHitCooldown), (SpriteEffects)0);
		}
		for (int i = 0; i < 15; i++)
		{
			Vector2 rotationalDrawOffset2 = ((float)Math.PI * 2f * (float)i / 15f).ToRotationVector2() * 5f * (fade + 0.2f);
			Texture2D value2 = tex2.Value;
			Vector2 position2 = base.Projectile.Center - Main.screenPosition + rotationalDrawOffset2;
			lightGreen = Color.LightGreen;
			((Color)(ref lightGreen)).A = 0;
			Main.EntitySpriteDraw(value2, position2, null, lightGreen * 0.4f, base.Projectile.rotation, tex2.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		}
		Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition, null, lightColor, base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		Texture2D value3 = tex2.Value;
		Vector2 position3 = base.Projectile.Center - Main.screenPosition;
		lightGreen = Color.LightGreen;
		((Color)(ref lightGreen)).A = 0;
		Main.EntitySpriteDraw(value3, position3, null, lightGreen * fade, base.Projectile.rotation, tex2.Size() / 2f, base.Projectile.scale * 1.1f, (SpriteEffects)0);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, Radius, targetHitbox);
	}

	public override bool? CanDamage()
	{
		if (stealthHitCooldown <= 0)
		{
			if (base.Projectile.numHits <= 0)
			{
				return null;
			}
			return false;
		}
		return false;
	}
}
