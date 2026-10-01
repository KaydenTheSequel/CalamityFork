using CalamityMod.Items.Weapons.DraedonsArsenal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.SmallAresArms;

public class ExoskeletonPlasmaCannon : ExoskeletonCannon
{
	public override int ShootRate => 30;

	public override float ShootSpeed => 13.5f;

	public override Vector2 OwnerRestingOffset
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return ExoskeletonCannon.HoverOffsetTable[base.HoverOffsetIndex];
		}
	}

	public override void ClampFirstLimbRotation(ref double limbRotation)
	{
		limbRotation = ExoskeletonCannon.RotationalClampTable[base.HoverOffsetIndex];
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void PostAI()
	{
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 5 % Main.projFrames[base.Type];
	}

	public override void ShootAtTarget(NPC target, Vector2 shootDirection)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = PlasmaCaster.FireSound with
		{
			Volume = 0.4f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		for (int i = 0; i < 40; i++)
		{
			float dustSpeed = Main.rand.NextFloat(1.8f, 3f);
			Vector2 dustVel = shootDirection * dustSpeed;
			dustVel = dustVel.RotatedBy(-0.3499999940395355);
			dustVel = dustVel.RotatedByRandom(0.699999988079071);
			int randomDustType = (Main.rand.NextBool() ? 107 : 110);
			Dust dust = Dust.NewDustDirect(base.Projectile.TopLeft, base.Projectile.width, base.Projectile.height, randomDustType, dustVel.X, dustVel.Y, 200, default(Color), 1.7f);
			dust.position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 4f;
			dust.position += shootDirection * 60f;
			dust.noGravity = true;
			dust.velocity *= base.Projectile.scale * 1.6f;
			Dust dust2 = Dust.NewDustDirect(base.Projectile.TopLeft, base.Projectile.width, base.Projectile.height, randomDustType, dustVel.X, dustVel.Y, 100, default(Color), 0.8f);
			dust2.position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 4f;
			dust2.position += shootDirection * 60f;
			dust2.velocity *= base.Projectile.scale * 0.9f;
			dust2.noGravity = true;
			dust2.fadeIn = 1f;
			dust2.color = Color.Green * 0.5f;
		}
		for (int j = 0; j < 20; j++)
		{
			float dustSpeed2 = Main.rand.NextFloat(1.8f, 3f);
			Vector2 dustVel2 = shootDirection * dustSpeed2;
			dustVel2 = dustVel2.RotatedBy(-0.3499999940395355);
			dustVel2 = dustVel2.RotatedByRandom(0.699999988079071);
			int randomDustType2 = (Main.rand.NextBool() ? 107 : 110);
			Dust dust3 = Dust.NewDustDirect(base.Projectile.TopLeft, base.Projectile.width, base.Projectile.height, randomDustType2, dustVel2.X, dustVel2.Y, 0, default(Color), 2f);
			dust3.position = base.Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(shootDirection.ToRotation()) * (float)base.Projectile.width / 5f;
			dust3.position += shootDirection * 60f;
			dust3.noGravity = true;
			dust3.velocity *= base.Projectile.scale * 0.27f;
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			Vector2 fireballVelocity = shootDirection * ShootSpeed;
			Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), base.Projectile.Center, fireballVelocity, ModContent.ProjectileType<MinionPlasmaBlast>(), (int)((float)base.Projectile.damage * 0.9f), 0f, base.Projectile.owner);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		DefaultDrawCannon(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SmallAresArms/ExoskeletonPlasmaCannonGlowmask", (AssetRequestMode)2).Value);
		return false;
	}
}
