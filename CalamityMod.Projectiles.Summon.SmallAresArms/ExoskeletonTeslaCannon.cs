using System.IO;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.SmallAresArms;

public class ExoskeletonTeslaCannon : ExoskeletonCannon
{
	public ref float TeslaOrbIndex => ref base.Projectile.localAI[0];

	public override int ShootRate => 36;

	public override float ShootSpeed => 16f;

	public override bool UsesSuperpredictiveness => true;

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

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(TeslaOrbIndex);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		TeslaOrbIndex = reader.ReadSingle();
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
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = CommonCalamitySounds.PlasmaBoltSound with
		{
			Volume = 0.4f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		if (Main.myPlayer == base.Projectile.owner)
		{
			int damage = (int)((float)base.Projectile.damage * 1f);
			Vector2 teslaOrbVelocity = shootDirection * ShootSpeed;
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromAI(), base.Projectile.Center, teslaOrbVelocity, ModContent.ProjectileType<MinionTeslaOrb>(), damage, 0f, base.Projectile.owner).ai[0] = TeslaOrbIndex++ % 6f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		DefaultDrawCannon(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SmallAresArms/ExoskeletonTeslaCannonGlowmask", (AssetRequestMode)2).Value);
		return false;
	}
}
