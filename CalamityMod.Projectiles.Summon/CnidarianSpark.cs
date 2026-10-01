using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class CnidarianSpark : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 initialVelocity;

	public new string LocalizationCategory => "Projectiles.Summon";

	public ref float Target => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 15;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (Main.npc[(int)Target] != target)
		{
			return false;
		}
		return base.CanHitNPC(target);
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity != Vector2.Zero)
		{
			initialVelocity = base.Projectile.velocity;
		}
		base.Projectile.velocity = Vector2.Zero;
		if (Main.npc[(int)Target] != null)
		{
			base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, Main.npc[(int)Target].Center, 0.4f);
			base.Projectile.rotation = (base.Projectile.Center - Main.npc[(int)Target].Center).ToRotation() - (float)Math.PI / 2f;
		}
		Color bloomColor = ((!Main.rand.NextBool()) ? Color.SpringGreen : (Main.rand.NextBool() ? Color.Gold : Color.Cyan));
		GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, (base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * 4f, Color.LightSkyBlue, bloomColor, 0.8f, 10, 1f, 2f));
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in CnidarianJellyfishOnTheString.ZapSound, base.Projectile.Center);
		for (int i = 0; i < 4; i++)
		{
			Color bloomColor = (Main.rand.NextBool() ? (Main.rand.NextBool() ? Color.Gold : Color.Cyan) : Color.SpringGreen);
			GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, initialVelocity * (float)i, Color.LightSkyBlue, bloomColor, 0.8f, 10, 1f, 2f));
		}
	}
}
