using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ExoFire : ModProjectile, ILocalizedModType, IModType
{
	public Color sparkColor;

	public int Time;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float LightPower => ref base.Projectile.ai[2];

	public ref int audioCooldown => ref Main.player[base.Projectile.owner].Calamity().PhotoAudioCooldown;

	public ref int PhotoTimer => ref Main.player[base.Projectile.owner].Calamity().PhotoTimer;

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 180;
		base.Projectile.timeLeft = 240;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 6;
	}

	public override void AI()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		Player obj = Main.player[base.Projectile.owner];
		bool photosens = CalamityClientConfig.Instance.Photosensitivity;
		float targetDist = Vector2.Distance(obj.Center, base.Projectile.Center);
		List<Color> eColors = new List<Color>
		{
			Color.OrangeRed,
			photosens ? Color.DodgerBlue : Color.MediumTurquoise,
			Color.Orange,
			Color.LawnGreen
		};
		float rate = Main.GlobalTimeWrappedHourly * (float)(photosens ? 2 : 8);
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		sparkColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		if (photosens)
		{
			((Color)(ref sparkColor)).A = 64;
		}
		Time++;
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.White;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.2f);
		if (targetDist < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * Main.rand.NextFloat(0.5f, 4f), "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 4, (37f - (float)Time * ((PhotoTimer == 0) ? 0.165f : 0.088f) - (float)PhotoTimer * 0.2f + (float)((PhotoTimer == 1) ? 20 : 0)) * 0.005f, sparkColor, new Vector2(1f, 1f + (float)Time * 0.1f)));
			if (!photosens)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * Main.rand.NextFloat(0.5f, 4f), "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 4, (37f - (float)Time * ((PhotoTimer == 0) ? 0.165f : 0.088f) - (float)PhotoTimer * 0.2f + (float)((PhotoTimer == 1) ? 20 : 0)) * 0.003f, Color.Lerp(Color.Blue, sparkColor, 0.5f), new Vector2(1f, 1f + (float)Time * 0.1f)));
			}
		}
		if (Main.rand.NextBool(35) && targetDist < 1400f && Time > 5)
		{
			Vector2 center2 = base.Projectile.Center;
			Vector2? velocity = Utils.RotatedByRandom(new Vector2(0f, -5f), 0.05000000074505806) * Main.rand.NextFloat(0.3f, 1.6f);
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(center2, 263, velocity, 0, newColor);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.3f, 1f);
			dust.color = sparkColor;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 1200);
		if (audioCooldown == 0)
		{
			SoundEngine.PlaySound(in Photoviscerator.HitSound, target.Center);
			audioCooldown = 16;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 1200);
	}
}
