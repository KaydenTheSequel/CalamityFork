using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SeraphimDagger : ModProjectile, ILocalizedModType, IModType
{
	public const int SlowdownTime = 45;

	public const int AimTime = 25;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 150;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.alpha = 0;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 15;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.ai[1] = Main.rand.NextFloat(-0.8f, 0.8f);
			base.Projectile.localAI[0] = 1f;
		}
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 8f, Time, clamped: true) * Utils.GetLerpValue(0f, 8f, base.Projectile.timeLeft, clamped: true);
		if (Time <= 45f)
		{
			float angularVelocity = (float)Math.Pow(1f - Utils.GetLerpValue(0f, 45f, Time, clamped: true), 2.0) * (float)Math.PI / 6f;
			base.Projectile.rotation += angularVelocity;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.95f;
		}
		else if (Time <= 70f)
		{
			if (Time == 70f)
			{
				SoundEngine.PlaySound(in SoundID.DD2_WyvernDiveDown, base.Projectile.Center);
				SoundEngine.PlaySound(in SoundID.DD2_DarkMageHealImpact, base.Projectile.Center);
			}
			NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(1600f);
			float idealRotation = base.Projectile.AngleTo(potentialTarget?.Center ?? (base.Projectile.Center - Vector2.UnitY)) + (float)Math.PI / 4f;
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp(idealRotation, 0.08f).AngleTowards(idealRotation, 0.1f);
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.9f;
			if (Time == 70f)
			{
				base.Projectile.rotation = idealRotation;
				base.Projectile.velocity = (idealRotation - (float)Math.PI / 4f).ToRotationVector2() * 14f;
			}
		}
		else if (((Vector2)(ref base.Projectile.velocity)).Length() < 28f)
		{
			Projectile projectile3 = base.Projectile;
			projectile3.velocity *= 1.045f;
		}
		Time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		base.Projectile.damage = Math.Max(1, (int)((double)base.Projectile.damage * 0.5));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 origin = texture.Size() * 0.5f;
		Vector2 baseDrawPosition = base.Projectile.Center - Main.screenPosition;
		float endFade = Utils.GetLerpValue(0f, 12f, base.Projectile.timeLeft, clamped: true);
		Color mainColor = Color.Goldenrod * base.Projectile.Opacity * endFade * 1.5f;
		((Color)(ref mainColor)).A = 74;
		Color afterimageLightColor = Color.White * endFade;
		((Color)(ref afterimageLightColor)).A = 74;
		for (int i = 0; i < 12; i++)
		{
			Vector2 drawPosition = baseDrawPosition + ((float)Math.PI * 2f * (float)i / 12f).ToRotationVector2() * (1f - base.Projectile.Opacity) * 6f;
			Main.EntitySpriteDraw(texture, drawPosition, null, afterimageLightColor, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		}
		for (int j = 0; j < 10; j++)
		{
			Vector2 drawPosition2 = baseDrawPosition - base.Projectile.velocity * (float)j * 0.45f;
			Color afterimageColor = mainColor * (1f - (float)j / 10f);
			Main.EntitySpriteDraw(texture, drawPosition2, null, afterimageColor, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}
}
