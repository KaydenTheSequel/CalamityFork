using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SiriusQuasar : ModProjectile, ILocalizedModType, IModType
{
	private bool hasExploded;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float Time => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 4);
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 30;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.timeLeft = 1000;
		base.Projectile.friendly = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		Time++;
		bool isDrawingUpdate = base.Projectile.numUpdates % 6 == 0;
		if ((Time > 6f) & isDrawingUpdate)
		{
			Color outerSparkColor = default(Color);
			((Color)(ref outerSparkColor))._002Ector(8, 35, 156);
			float scaleBoost = MathHelper.Clamp(Time * 0.005f, 0f, 2f);
			float outerSparkScale = 3.2f + scaleBoost;
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, base.Projectile.velocity, affectedByGravity: false, 7, outerSparkScale, outerSparkColor));
			Color innerSparkColor = default(Color);
			((Color)(ref innerSparkColor))._002Ector(184, 215, 245);
			float innerSparkScale = 1.6f + scaleBoost;
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, base.Projectile.velocity, affectedByGravity: false, 7, innerSparkScale, innerSparkColor));
		}
		for (int d = 0; d < 1; d++)
		{
			Vector2 projPos = base.Projectile.position;
			projPos -= base.Projectile.velocity * ((float)d * 0.25f);
			base.Projectile.alpha = 255;
			int trailDust = Dust.NewDust(projPos, 1, 1, 20);
			Main.dust[trailDust].position = projPos;
			Main.dust[trailDust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
			Dust obj = Main.dust[trailDust];
			obj.velocity *= 0.2f;
			Main.dust[trailDust].noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Voidfrost>(), 600);
		Main.rgbToHsl(new Color(103, 203, Main.DiscoB));
		if (hasExploded || target.Calamity().IsArmored())
		{
			return;
		}
		hasExploded = true;
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ScorpioNukeHit");
		style.Volume = 0.75f;
		style.Pitch = 0.6f;
		style.PitchVariance = 0.2f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		float numberOfDusts = 156f;
		float rotFactor = 360f / numberOfDusts;
		for (int i = 0; (float)i < numberOfDusts; i++)
		{
			float rot = MathHelper.ToRadians((float)i * rotFactor);
			float intensity = Main.rand.NextFloat(0.2f, 0.5f);
			Vector2 offset = Utils.RotatedBy(new Vector2(30f, 5.8f), (double)rot, default(Vector2));
			Vector2 velOffset = Utils.RotatedBy(new Vector2(40.8f, 10.5f), (double)rot, default(Vector2));
			if (i % 2 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + offset, velOffset * intensity * 0.7f, "CalamityMod/Particles/Sparkle", affectedByGravity: false, (int)(40f * intensity), intensity, Main.rand.NextBool(3) ? Color.DarkSlateBlue : Color.SlateBlue, new Vector2(1f, 2f), useAddativeBlend: true, glowCenter: true));
				continue;
			}
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset, ModContent.DustType<LightDust>(), velOffset);
			dust.noGravity = true;
			dust.velocity = velOffset * intensity;
			dust.scale = Main.rand.NextFloat(2.5f, 2.8f) * intensity;
			dust.color = (Main.rand.NextBool(3) ? Color.DarkSlateBlue : Color.SlateBlue);
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.DarkSlateBlue, "CalamityMod/Particles/BloomRing", new Vector2(0.6f, 0.8f), base.Projectile.velocity.ToRotation(), 0f, 3f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.SlateBlue, "CalamityMod/Particles/BloomRing", new Vector2(0.3f, 0.7f), base.Projectile.velocity.ToRotation(), 0f, 4f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
	}
}
