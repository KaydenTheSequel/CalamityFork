using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class PuddleSplash : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float time => ref base.Projectile.ai[0];

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 165;
		base.Projectile.height = 80;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = AverageDamageClass.Instance;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
		base.Projectile.timeLeft = 25;
	}

	public override void AI()
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		if (time == 0f)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/WaterSplash" + (Main.rand.NextBool() ? "1" : "2"));
			style.Volume = 0.2f;
			style.Pitch = Main.rand.NextFloat(0.9f, 1f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			for (int i = 0; i < 6; i++)
			{
				GeneralParticleHandler.SpawnParticle(new BloodParticle(base.Projectile.Center, (-Vector2.UnitY * Main.rand.NextFloat(6f, 12f)).RotatedByRandom(0.6499999761581421), 30, Main.rand.NextFloat(0.7f, 1.2f), Main.rand.NextBool() ? Color.CornflowerBlue : Color.SkyBlue));
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), (-Vector2.UnitY * Main.rand.NextFloat(2f, 5f)).RotatedByRandom(0.6499999761581421), 0, default(Color), Main.rand.NextFloat(1.1f, 1.4f));
				dust.noGravity = false;
				dust.color = (Main.rand.NextBool() ? Color.CornflowerBlue : Color.SkyBlue);
				dust.noLight = true;
				dust.noLightEmittence = true;
				dust.alpha = 100;
			}
			for (int j = 0; j < 2; j++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -Vector2.UnitY * 0.1f, "CalamityMod/Particles/HighResHollowCircleHardEdgeAlt", affectedByGravity: false, (j == 0) ? 15 : 22, (j == 0) ? 0.07f : 0.05f, (j == 0) ? Color.CornflowerBlue : Color.SkyBlue, new Vector2(0.7f, (j == 0) ? 1.3f : 0.9f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, (j == 0) ? (-0.6f) : (-0.5f)));
			}
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(103, 180);
		Vector2 launchVel = Owner.Center.DirectionTo(target.Center) + Vector2.UnitY * -0.75f;
		target.MoveNPC(launchVel, 12f);
	}

	public override bool? CanCutTiles()
	{
		return false;
	}
}
