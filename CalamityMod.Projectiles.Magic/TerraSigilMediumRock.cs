using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class TerraSigilMediumRock : ModProjectile, ILocalizedModType, IModType
{
	public bool goToCursor;

	public Vector2 mousePos;

	public float CenterX;

	public float CenterY;

	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 75;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		if (Time < 69f)
		{
			base.Projectile.friendly = false;
		}
		else
		{
			base.Projectile.friendly = true;
		}
		if (Time == 0f)
		{
			base.Projectile.scale = 0.5f;
		}
		if (!goToCursor)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.96f;
		}
		if (Time < 11f)
		{
			base.Projectile.scale *= 1.08f;
		}
		if (Time == 60f)
		{
			base.Projectile.velocity = Vector2.Zero;
			mousePos = Main.player[base.Projectile.owner].ClampedMouseWorld();
			goToCursor = true;
		}
		if (goToCursor)
		{
			if (Time == 60f)
			{
				CenterX = base.Projectile.Center.X;
				CenterY = base.Projectile.Center.Y;
			}
			if (Time > 60f)
			{
				base.Projectile.Center = new Vector2(MathHelper.Lerp(CenterX, mousePos.X, Utils.GetLerpValue(60f, 73f, Time, clamped: true)), MathHelper.Lerp(CenterY, mousePos.Y, Utils.GetLerpValue(60f, 73f, Time, clamped: true)));
			}
		}
		base.Projectile.rotation += 0.05f;
		Time++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/Ravager/RavagerJump2");
		style.Volume = 0.7f;
		style.PitchVariance = 0.1f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		int rockCount = Main.rand.Next(1, 3);
		Vector2 randomVelocity = default(Vector2);
		for (int i = 0; i < rockCount; i++)
		{
			((Vector2)(ref randomVelocity))._002Ector(Main.rand.NextFloat(-10f, 10f), Main.rand.NextFloat(-3f, -10f));
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, randomVelocity, ModContent.ProjectileType<TerraSigilSmallRock>(), 0, 0f, base.Projectile.owner);
		}
		for (int j = 0; j < 14; j++)
		{
			if (Main.rand.NextBool(3))
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(6) ? ModContent.DustType<TerraSigilDust>() : 262, Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.1f, 0.8f));
				dust.noGravity = false;
				dust.scale = Main.rand.NextFloat(0.6f, 1f);
				if (dust.type == 262)
				{
					dust.noGravity = true;
					dust.fadeIn = 0.3f;
					dust.velocity *= 1.5f;
				}
				dust.alpha = 100;
			}
			else
			{
				Color clr = Color.Lerp(Main.rand.NextBool() ? Color.Peru : Color.PeachPuff, Color.Black, Main.rand.NextFloat(0.25f, 0.45f));
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, (Vector2.One * Main.rand.NextFloat(3f, 8f)).RotatedByRandom(6.2831854820251465), "CalamityMod/Particles/SmallSmoke", affectedByGravity: true, Main.rand.Next(15, 31), base.Projectile.scale * Main.rand.NextFloat(0.05f, 0.1f) * 4f, clr, new Vector2(1f, Main.rand.NextFloat(0.2f, 1f)), useAddativeBlend: false, glowCenter: false, Main.rand.NextFloat(-2f, 2f), fadeIn: false, affectedByLight: true, 0f, 1f, 1f, flipHorizontal: false, noShrink: false, Main.rand.NextFloat(-0.5f, 0.5f)));
			}
		}
		for (int k = 0; k < 3; k++)
		{
			Vector2 randVel = Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.4f, 1f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + randVel, randVel, Color.Peru, Main.rand.Next(15, 26), Main.rand.NextFloat(0.6f, 1.5f), 0.3f));
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, randVel * 0.5f, Color.SandyBrown, Color.Beige, Main.rand.NextFloat(0.2f, 0.8f), 80f, Main.rand.NextFloat(0.02f, -0.02f)));
		}
	}
}
