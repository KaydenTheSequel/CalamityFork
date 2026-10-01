using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class CosmicDashExplosion : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle Impact = new SoundStyle("CalamityMod/Sounds/NPCKilled/DevourerSegmentBreak1")
	{
		Volume = 0.3f
	};

	public new string LocalizationCategory => "Projectiles.Typeless";

	public Player Owner => Main.player[base.Projectile.owner];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 140;
		base.Projectile.height = 140;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 4;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = Impact with
		{
			PitchVariance = 0.3f
		};
		SoundEngine.PlaySound(in style, base.Projectile.position);
		style = SoundID.Item62 with
		{
			Volume = 0.5f,
			PitchVariance = 0.3f
		};
		SoundEngine.PlaySound(in style, base.Projectile.position);
		base.Projectile.position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2);
		base.Projectile.width = 140;
		base.Projectile.height = 140;
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int i = 0; i < 35; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 181, Utils.RotatedByRandom(new Vector2(4.5f, 4.5f), 100.0) * Main.rand.NextFloat(0.2f, 1.9f), 0, default(Color), Main.rand.NextFloat(1.5f, 2.8f));
			dust.shader = GameShaders.Armor.GetSecondaryShader(Owner.cShield, Owner);
			dust.noGravity = true;
		}
		for (int j = 0; j < 14; j++)
		{
			Vector2 dustVel = Utils.RotatedByRandom(new Vector2(6f, 6f), 100.0) * Main.rand.NextFloat(0.5f, 1.2f);
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + dustVel * 2f, 272, dustVel);
			dust2.shader = GameShaders.Armor.GetSecondaryShader(Owner.cShield, Owner);
			Dust.NewDustPerfect(base.Projectile.Center + dustVel * 2f, 226, dustVel);
			dust2.shader = GameShaders.Armor.GetSecondaryShader(Owner.cShield, Owner);
		}
	}
}
