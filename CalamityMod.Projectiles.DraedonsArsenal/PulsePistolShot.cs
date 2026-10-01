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

public class PulsePistolShot : ModProjectile, ILocalizedModType, IModType
{
	public bool onSpawn = true;

	private NPC targeted;

	private NPC lastHitTarget;

	private int timesItCanHit = 1;

	public bool startAttackEffects = true;

	public int attackTime = 160;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 5;
		base.Projectile.timeLeft = 600;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0616: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0723: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_0682: Unknown result type (might be due to invalid IL or missing references)
		//IL_068d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0697: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref ArsenalEffects.ArsenalPulseColor)).ToVector3() * 0.5f);
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (onSpawn)
		{
			if (base.Projectile.ai[1] == 0f)
			{
				SoundStyle style = SoundID.DD2_DarkMageCastHeal with
				{
					Volume = 1.7f,
					Pitch = 0.3f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			for (int i = 0; i <= 8; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ArsenalEffects.ArsenalPulseDust);
				dust.scale = Main.rand.NextFloat(1.2f, 1.9f) * base.Projectile.scale;
				dust.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotateRandom(0.5) * Main.rand.NextFloat(5f, 7f);
				dust.noGravity = true;
				dust.color = ArsenalEffects.ArsenalPulseColor;
				dust.fadeIn = 1f;
			}
			for (int j = 0; j <= 6; j++)
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
		if (time >= (float)attackTime)
		{
			if (targeted == null)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.9f;
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
				base.Projectile.extraUpdates = 5 + (int)((float)base.Projectile.numHits * 0.6f);
				if (base.Projectile.timeLeft < 110 * base.Projectile.extraUpdates)
				{
					base.Projectile.timeLeft = 110 * base.Projectile.extraUpdates;
				}
				CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targeted, ignoreTiles: true, 0.5f, 15f, 0.97f);
				if (startAttackEffects)
				{
					base.Projectile.velocity = base.Projectile.Center.DirectionTo(targeted.Center) * 10f;
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/PulseSound");
					style.Volume = 0.25f;
					style.Pitch = Math.Max(0.6f, Main.rand.NextFloat(0.3f, 0.4f) + (float)base.Projectile.numHits * 0.1f);
					style.MaxInstances = 5;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					for (int l = 0; l < 6; l++)
					{
						Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center, ArsenalEffects.ArsenalPulseDust);
						dust3.scale = Main.rand.NextFloat(1.2f, 1.9f) * base.Projectile.scale;
						dust3.velocity = base.Projectile.Center.DirectionTo(targeted.Center).RotatedByRandom(0.5) * Main.rand.NextFloat(5f, 9f);
						dust3.noGravity = true;
						dust3.color = ArsenalEffects.ArsenalPulseColor;
						dust3.fadeIn = 1f;
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
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= ((base.Projectile.numHits > 0) ? 0.955f : 0.97f);
		}
		float squash = Utils.GetLerpValue(1f, 3f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true);
		if (targetDist < 1400f && squash > 0.2f && (base.Projectile.ai[1] == 0f || time > 5f))
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 0.01f, "CalamityMod/Particles/DualTrail", affectedByGravity: false, 13, 0.075f * base.Projectile.scale, ArsenalEffects.ArsenalPulseColor * 0.6f * squash, new Vector2(1f - 0.15f * squash, 1.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.2f * squash));
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		time++;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (target != targeted)
		{
			return false;
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		bool onKill = target.life <= 0 && target.realLife == -1;
		lastHitTarget = target;
		targeted = null;
		time = 0f;
		timesItCanHit--;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.8f;
		for (int i = 0; i <= 5; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>());
			dust.scale = Main.rand.NextFloat(1.7f, 2.4f) * base.Projectile.scale;
			dust.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotateRandom(0.30000001192092896) * Main.rand.NextFloat(4f, 9f);
			dust.noGravity = true;
			dust.color = ArsenalEffects.ArsenalPulseColor;
			dust.fadeIn = 0.3f;
		}
		if (onKill)
		{
			timesItCanHit++;
		}
		if (timesItCanHit <= 0)
		{
			if (base.Projectile.ai[1] == 0f && Main.myPlayer == base.Projectile.owner)
			{
				Projectile pulseOrb1 = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, (base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 10f).RotatedBy(0.30000001192092896) * 0.5f, base.Projectile.type, base.Projectile.damage / 4, base.Projectile.knockBack / 2f, base.Projectile.owner, 0f, 1f);
				Projectile projectile2 = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, (base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 10f).RotatedBy(-0.30000001192092896) * 0.5f, base.Projectile.type, base.Projectile.damage / 4, base.Projectile.knockBack / 2f, base.Projectile.owner, 0f, -1f);
				pulseOrb1.penetrate = 1;
				pulseOrb1.scale = 0.7f;
				projectile2.penetrate = 1;
				projectile2.scale = 0.7f;
			}
			base.Projectile.Kill();
		}
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
