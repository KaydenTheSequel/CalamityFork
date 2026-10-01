using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class FestiveWingsOrnament : ModProjectile, ILocalizedModType, IModType
{
	internal static readonly SoundStyle JingleSound = new SoundStyle("CalamityMod/Sounds/Item/FestiveJingle")
	{
		Volume = 0.25f,
		PitchVariance = 0.5f
	};

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "Terraria/Images/Projectile_" + (short)335;

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = Main.projFrames[335];
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 22);
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 420;
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frame = Main.rand.Next(Main.projFrames[base.Type]);
		SoundEngine.PlaySound(in JingleSound, base.Projectile.Bottom);
		Player owner = Main.player[base.Projectile.owner];
		if (owner == null || !owner.active)
		{
			return;
		}
		Vector2 direction = owner.SafeDirectionTo(base.Projectile.Bottom);
		base.Projectile.rotation = direction.ToRotation() + (float)Math.PI / 2f;
		int totalPoints = (int)Utils.Remap(Vector2.Distance(base.Projectile.Bottom, owner.Center), 320f, 512f, 8f, 13f);
		Vector2[] trailPoints = (Vector2[])(object)new Vector2[totalPoints + 1];
		trailPoints[0] = owner.Center;
		trailPoints[totalPoints] = base.Projectile.Bottom;
		for (int i = 1; i < totalPoints; i++)
		{
			trailPoints[i] = Vector2.Lerp(owner.Center, base.Projectile.Bottom, (float)i / (float)totalPoints) + Main.rand.NextVector2Circular(12f, 12f) + direction.RotatedBy(1.5707963705062866) * Main.rand.NextFloat(-48f, 48f);
		}
		int dustType = 90 - base.Projectile.frame;
		for (int j = 0; j < totalPoints; j++)
		{
			for (int d = 0; d < 8; d++)
			{
				Dust dust = Dust.NewDustPerfect(Vector2.Lerp(trailPoints[j], trailPoints[j + 1], (float)d / 8f), dustType);
				dust.noGravity = true;
				dust.noLight = true;
			}
		}
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Bottom, 0.5f, 0.5f, 0.5f);
		if (base.Projectile.timeLeft <= 120)
		{
			base.Projectile.velocity.Y += 0.75f;
			if (base.Projectile.velocity.Y > 16f)
			{
				base.Projectile.velocity.Y = 16f;
			}
		}
		for (int i = 0; i < 255; i++)
		{
			Player player = Main.player[i];
			if (Vector2.Distance(player.Center, base.Projectile.Bottom) < 32f && (float)player.wingTimeMax > 0f)
			{
				player.wingTime = MathHelper.Clamp(player.wingTime + 75f, 0f, (float)player.wingTimeMax);
				SoundStyle style = SoundID.Item29 with
				{
					Volume = 0.25f
				};
				SoundEngine.PlaySound(in style, player.Center);
				base.Projectile.Kill();
				int dustType = 90 - base.Projectile.frame;
				for (int d = 0; d < 36; d++)
				{
					Dust dust = Dust.NewDustPerfect(base.Projectile.Bottom, dustType);
					dust.velocity = ((float)Math.PI * 2f * (float)d / 36f).ToRotationVector2() * 6f;
					dust.noGravity = true;
					dust.noLight = true;
				}
				break;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.Bottom);
		int dustType = 90 - base.Projectile.frame;
		for (int i = 0; i < 20; i++)
		{
			Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType);
			dust.noLight = true;
			dust.scale = 0.8f;
		}
		return true;
	}
}
