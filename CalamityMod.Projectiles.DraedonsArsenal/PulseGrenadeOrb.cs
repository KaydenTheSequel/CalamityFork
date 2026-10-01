using System;
using CalamityMod.Dusts;
using CalamityMod.Effects;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class PulseGrenadeOrb : ModProjectile, ILocalizedModType, IModType
{
	public bool onSpawn = true;

	public Vector2 lastPos;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 5;
		base.Projectile.timeLeft = 600;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.ArmorPenetration = 15;
	}

	public override bool ShouldUpdatePosition()
	{
		return time < 120f;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref ArsenalEffects.ArsenalPulseColor)).ToVector3() * 0.5f);
		float targetDist = Vector2.Distance(Owner.Center, base.Projectile.Center);
		if (onSpawn)
		{
			lastPos = base.Projectile.Center;
			base.Projectile.scale = (base.Projectile.Calamity().stealthStrike ? 1f : 0.7f);
			for (int i = 0; i <= 4; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ArsenalEffects.ArsenalPulseDust);
				dust.scale = Main.rand.NextFloat(1.2f, 1.9f) * base.Projectile.scale;
				dust.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotateRandom(0.5) * Main.rand.NextFloat(5f, 7f);
				dust.noGravity = true;
				dust.color = ArsenalEffects.ArsenalPulseColor;
				dust.fadeIn = 1f;
			}
			for (int j = 0; j <= 2; j++)
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>());
				dust2.scale = Main.rand.NextFloat(1.2f, 1.9f) * base.Projectile.scale;
				dust2.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotateRandom(0.30000001192092896) * Main.rand.NextFloat(7f, 9f);
				dust2.noGravity = true;
				dust2.color = ArsenalEffects.ArsenalPulseColor;
				dust2.fadeIn = 0.3f;
			}
			onSpawn = false;
		}
		if (time < 120f)
		{
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy(0.023f * base.Projectile.ai[1] * (float)((time > 25f) ? 1 : 0)) * 0.997f * ((time > 25f) ? 1f : 0.98f);
		}
		else
		{
			Vector2 goal = Vector2.Lerp(Owner.GetFrontHandPosition(Player.CompositeArmStretchAmount.None, Owner.compositeFrontArm.rotation), base.Projectile.Center, 0.5f);
			base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, goal, (float)Math.Pow(Utils.GetLerpValue(120f, 250f, time, clamped: true), 5.0));
			Vector2 direction = base.Projectile.Center.DirectionTo(Owner.Center);
			float power = base.Projectile.Center.Distance(lastPos);
			base.Projectile.velocity = direction * power;
			if (time == 250f || base.Projectile.Center.Distance(Owner.Center) < 10f)
			{
				if (base.Projectile.ai[2] == 0f && Owner.HeldItem.type == ModContent.ItemType<PulseGrenade>())
				{
					Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), goal, Vector2.Zero, ModContent.ProjectileType<PulseGrenadeProjectile>(), base.Projectile.damage, 0f, Owner.whoAmI);
					projectile.ai[1] = 5f;
					projectile.ai[0] = (float)Owner.HeldItem.useTime * 0.7f;
					projectile.Opacity = 0f;
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/LightCatch");
					style.Volume = 1f;
					style.Pitch = 0f;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					style = new SoundStyle("CalamityMod/Sounds/Item/PulseSound");
					style.Volume = 0.1f;
					style.Pitch = 0.4f;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, ArsenalEffects.ArsenalPulseColor, "CalamityMod/Items/Weapons/DraedonsArsenal/PulseGrenade", Vector2.One, Main.rand.NextFloat(-0.2f, 0.2f), 1.5f, 0.9f, 45, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, ArsenalEffects.ArsenalPulseColor, "CalamityMod/Particles/BloomRing", Vector2.One, Main.rand.NextFloat(-0.2f, 0.2f), 0.55f, 0.05f, 30, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					base.Projectile.Kill();
				}
				else
				{
					base.Projectile.Kill();
				}
				return;
			}
		}
		float squash = Utils.GetLerpValue(1f, 3f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true);
		if (targetDist < 1400f && squash > 0.2f && time > 5f)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 0.01f, "CalamityMod/Particles/DualTrail", affectedByGravity: false, 13, 0.075f * base.Projectile.scale, ArsenalEffects.ArsenalPulseColor * 0.6f * squash, new Vector2(1f - 0.15f * squash, 1.3f + Utils.GetLerpValue(11f, 15f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true) * 1.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.2f * squash));
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		time++;
		lastPos = base.Projectile.Center;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.23f;
		int hitsToMinMult = 5;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= (base.Projectile.Calamity().stealthStrike ? 0.57f : 0.33f) * damageMult;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return null;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> orb = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		Vector2 squash = default(Vector2);
		((Vector2)(ref squash))._002Ector(Utils.Remap(((Vector2)(ref base.Projectile.velocity)).Length(), 5f, 10f, 1f, 0.6f), Utils.Remap(((Vector2)(ref base.Projectile.velocity)).Length(), 5f, 10f, 1f, 2f));
		float timeleftFade = (float)Math.Pow(Utils.GetLerpValue(0f, 40 * base.Projectile.extraUpdates, base.Projectile.timeLeft, clamped: true), 5.0);
		for (int i = 0; i < 6; i++)
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
