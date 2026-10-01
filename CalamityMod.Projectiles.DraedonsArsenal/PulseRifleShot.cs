using System;
using CalamityMod.Dusts;
using CalamityMod.Effects;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class PulseRifleShot : ModProjectile, ILocalizedModType, IModType
{
	public bool onSpawn = true;

	private NPC targeted;

	private NPC lastHitTarget;

	private int timesItCanHit = 3;

	public bool startAttackEffects = true;

	public bool dead;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float time => ref base.Projectile.ai[0];

	public bool isBeam => base.Projectile.ai[1] == 0f;

	public int attackTime => (int)(180f + base.Projectile.ai[1]);

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 7;
		base.Projectile.timeLeft = 900;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08af: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_067e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_070e: Unknown result type (might be due to invalid IL or missing references)
		//IL_071a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0725: Unknown result type (might be due to invalid IL or missing references)
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		//IL_074d: Unknown result type (might be due to invalid IL or missing references)
		//IL_075c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0909: Unknown result type (might be due to invalid IL or missing references)
		//IL_0914: Unknown result type (might be due to invalid IL or missing references)
		//IL_091e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0930: Unknown result type (might be due to invalid IL or missing references)
		//IL_093a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0941: Unknown result type (might be due to invalid IL or missing references)
		//IL_0959: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0804: Unknown result type (might be due to invalid IL or missing references)
		//IL_0809: Unknown result type (might be due to invalid IL or missing references)
		//IL_0817: Unknown result type (might be due to invalid IL or missing references)
		//IL_0830: Unknown result type (might be due to invalid IL or missing references)
		//IL_0835: Unknown result type (might be due to invalid IL or missing references)
		//IL_0842: Unknown result type (might be due to invalid IL or missing references)
		//IL_0847: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		float targetDist = Vector2.Distance(Owner.Center, base.Projectile.Center);
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref ArsenalEffects.ArsenalPulseColor)).ToVector3() * 0.5f);
		if (onSpawn)
		{
			float fxPower = (isBeam ? 2f : 0.5f);
			if (isBeam)
			{
				Projectile projectile = base.Projectile;
				projectile.Center += base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(-0.05f * (float)Math.Sign(base.Projectile.velocity.X)) * 60f;
				base.Projectile.extraUpdates = 100;
				timesItCanHit = 1;
				Owner.SetScreenshake(4f);
			}
			for (int i = 0; (float)i <= 8f * fxPower; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ArsenalEffects.ArsenalPulseDust);
				dust.scale = Main.rand.NextFloat(1.2f, 1.9f) * base.Projectile.scale;
				dust.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotateRandom(0.5) * Main.rand.NextFloat(2f, 9f) * fxPower;
				dust.noGravity = true;
				dust.color = ArsenalEffects.ArsenalPulseColor;
				dust.fadeIn = 1f;
			}
			for (int j = 0; (float)j <= 6f * fxPower; j++)
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>());
				dust2.scale = Main.rand.NextFloat(1.2f, 1.9f) * base.Projectile.scale;
				dust2.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotateRandom(0.30000001192092896) * Main.rand.NextFloat(4f, 15f) * fxPower;
				dust2.noGravity = true;
				dust2.color = ArsenalEffects.ArsenalPulseColor;
				dust2.fadeIn = 0.3f;
			}
			onSpawn = false;
		}
		if (isBeam)
		{
			if (targetDist < 1400f && time > 0f)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 0.01f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 35, 0.55f * base.Projectile.scale, ArsenalEffects.ArsenalPulseColor * 0.5f, new Vector2(0.6f, 1f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.2f, 0.7f, 0.4f));
				base.Projectile.scale += 0.007f;
				if (time % 3f == 0f)
				{
					Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(3f * base.Projectile.scale, 3f * base.Projectile.scale), ArsenalEffects.ArsenalPulseDust);
					dust3.scale = Main.rand.NextFloat(0.7f, 1.2f) * base.Projectile.scale;
					dust3.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(7f, 25f);
					dust3.noGravity = true;
					dust3.color = ArsenalEffects.ArsenalPulseColor;
					dust3.fadeIn = 0.6f;
				}
			}
		}
		else
		{
			if (time >= (float)attackTime)
			{
				if (targeted == null)
				{
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= 0.9f;
					startAttackEffects = true;
					NPC chosenTarget = null;
					float distance = 2500f;
					for (int index = 0; index < Main.npc.Length; index++)
					{
						NPC searchedTarget = Main.npc[index];
						if (searchedTarget.CanBeChasedBy() && Vector2.Distance(base.Projectile.Center, searchedTarget.Center) < distance && (lastHitTarget == null || searchedTarget != lastHitTarget) && searchedTarget.active && searchedTarget.life > 0)
						{
							distance = Vector2.Distance(base.Projectile.Center, searchedTarget.Center);
							chosenTarget = searchedTarget;
						}
					}
					if (chosenTarget == null)
					{
						if (lastHitTarget != null)
						{
							base.Projectile.localNPCImmunity[lastHitTarget.whoAmI] = 0;
						}
						for (int k = 0; k < Main.npc.Length; k++)
						{
							NPC searchedTarget2 = Main.npc[k];
							if (searchedTarget2.CanBeChasedBy() && Vector2.Distance(base.Projectile.Center, searchedTarget2.Center) < distance && searchedTarget2.active && searchedTarget2.life > 0)
							{
								distance = Vector2.Distance(base.Projectile.Center, searchedTarget2.Center);
								chosenTarget = searchedTarget2;
							}
						}
					}
					targeted = chosenTarget;
				}
				else
				{
					base.Projectile.extraUpdates = 7 + (int)((float)base.Projectile.numHits * 0.6f);
					if (base.Projectile.timeLeft < 110 * base.Projectile.extraUpdates)
					{
						base.Projectile.timeLeft = 110 * base.Projectile.extraUpdates;
					}
					CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targeted, ignoreTiles: true, 0.4f, 15f, 0.97f);
					if (startAttackEffects)
					{
						base.Projectile.velocity = base.Projectile.Center.DirectionTo(targeted.Center) * 10f;
						SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/PulseSound");
						style.Volume = 0.35f;
						style.Pitch = Math.Max(0.5f, Main.rand.NextFloat(0.1f, 0.3f) + (float)base.Projectile.numHits * 0.2f);
						style.MaxInstances = 5;
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 0.5f, "CalamityMod/Particles/HighResHollowCircleHardEdgeAlt", affectedByGravity: false, 13, 0.05f * base.Projectile.scale, ArsenalEffects.ArsenalPulseColor, new Vector2(1.2f, 0.7f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.4f));
						for (int l = 0; l < 6; l++)
						{
							Dust dust4 = Dust.NewDustPerfect(base.Projectile.Center, ArsenalEffects.ArsenalPulseDust);
							dust4.scale = Main.rand.NextFloat(1.2f, 1.9f) * base.Projectile.scale;
							dust4.velocity = base.Projectile.Center.DirectionTo(targeted.Center).RotatedByRandom(0.5) * Main.rand.NextFloat(5f, 9f);
							dust4.noGravity = true;
							dust4.color = ArsenalEffects.ArsenalPulseColor;
							dust4.fadeIn = 1f;
						}
						startAttackEffects = false;
					}
					if (targeted.life <= 0 || !targeted.active || !targeted.CanBeChasedBy())
					{
						targeted = null;
					}
				}
			}
			else
			{
				Projectile projectile3 = base.Projectile;
				projectile3.velocity *= 0.985f;
			}
			float squash = Utils.GetLerpValue(1f, 3f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true);
			if (targetDist < 1400f && squash > 0.1f && time > 5f)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 0.01f, "CalamityMod/Particles/DualTrail", affectedByGravity: false, 13, 0.075f, ArsenalEffects.ArsenalPulseColor * 0.6f * squash, new Vector2(1f - 0.15f * squash, 1.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.2f * squash));
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		time++;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (target != targeted && !isBeam)
		{
			return false;
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		bool onKill = target.life <= 0 && target.realLife == -1;
		timesItCanHit--;
		float fxVel = (isBeam ? 3f : 1f);
		for (int i = 0; (float)i <= 5f * fxVel; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>());
			dust.scale = Main.rand.NextFloat(1.7f, 2.4f) * base.Projectile.scale;
			dust.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotateRandom(0.30000001192092896) * Main.rand.NextFloat(4f, 9f) * fxVel;
			dust.noGravity = true;
			dust.color = ArsenalEffects.ArsenalPulseColor;
			dust.fadeIn = 0.3f * fxVel;
		}
		if (!isBeam)
		{
			base.Projectile.ai[1] = -50f;
			lastHitTarget = target;
			targeted = null;
			time = 0f;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.9f;
			if (onKill)
			{
				timesItCanHit += 3;
			}
		}
		else
		{
			if (Main.myPlayer == base.Projectile.owner)
			{
				int numProj = 4;
				int projectileDamage = (int)((float)base.Projectile.damage * 0.5f);
				for (int j = 1; j < numProj + 1; j++)
				{
					Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity.SafeNormalize(Vector2.Zero) * (float)(10 / numProj * j), base.Projectile.type, projectileDamage, base.Projectile.knockBack, base.Projectile.owner, 0f, 28 * j).scale = 1.4f - (float)j * 0.2f;
				}
			}
			Vector2 vel = base.Projectile.velocity.SafeNormalize(Vector2.UnitX);
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, vel * 7f, "CalamityMod/Particles/HighResHollowCircleHardEdgeAlt", affectedByGravity: false, 16, 0.065f, ArsenalEffects.ArsenalPulseColor, new Vector2(2f, 0.7f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.2f));
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, vel * 13f, "CalamityMod/Particles/HighResHollowCircleHardEdgeAlt", affectedByGravity: false, 12, 0.04f, ArsenalEffects.ArsenalPulseColor, new Vector2(2f, 0.7f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.2f));
		}
		if (timesItCanHit <= 0)
		{
			base.Projectile.Kill();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		if (isBeam)
		{
			return false;
		}
		Asset<Texture2D> orb = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		Vector2 squash = default(Vector2);
		((Vector2)(ref squash))._002Ector(Utils.Remap(((Vector2)(ref base.Projectile.velocity)).Length(), 5f, 10f, 1f, 0.6f), Utils.Remap(((Vector2)(ref base.Projectile.velocity)).Length(), 5f, 10f, 1f, 2f));
		float timeleftFade = (float)Math.Pow(Utils.GetLerpValue(0f, 40 * base.Projectile.extraUpdates, base.Projectile.timeLeft, clamped: true), 5.0);
		for (int i = 0; i < 7; i++)
		{
			Color val = Color.Lerp(ArsenalEffects.ArsenalPulseColor, Color.White, (float)i * 0.07f);
			((Color)(ref val)).A = 0;
			Color orbColor = val * 0.5f;
			Vector2 scale = base.Projectile.scale * timeleftFade * squash * (0.05f + (float)i * 0.01f) * 3f;
			Main.EntitySpriteDraw(orb.Value, base.Projectile.Center - Main.screenPosition, null, orbColor, base.Projectile.rotation, orb.Size() * 0.5f, scale, (SpriteEffects)0);
		}
		return false;
	}
}
