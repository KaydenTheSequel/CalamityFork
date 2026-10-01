using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SeraphimProjectile : ModProjectile, ILocalizedModType, IModType
{
	public const float InitialSpeed = 64f;

	public const float SlowdownSpeed = 7f;

	public const int SlowdownTime = 50;

	public static readonly float SlowdownFactor = (float)Math.Pow(7.0 / 64.0, 0.019999999552965164);

	public new string LocalizationCategory => "Projectiles.Rogue";

	public ref float Time => ref base.Projectile.ai[0];

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/Seraphim";

	public override void SetDefaults()
	{
		base.Projectile.width = 82;
		base.Projectile.height = 82;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 50;
		base.Projectile.alpha = 0;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 14;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		if (Time <= 50f)
		{
			base.Projectile.Opacity = (float)Math.Pow(1f - Time / 50f, 2.0);
			Projectile projectile = base.Projectile;
			projectile.velocity *= SlowdownFactor;
			int lightDustCount = (int)MathHelper.Lerp(8f, 1f, base.Projectile.Opacity);
			for (int i = 0; i < lightDustCount; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Unit() * (1f - base.Projectile.Opacity) * 45f, 267);
				dust.color = Color.Lerp(Color.Gold, Color.White, Main.rand.NextFloat(0.5f, 1f));
				dust.velocity = Main.rand.NextVector2Circular(10f, 10f);
				dust.scale = MathHelper.Lerp(1.3f, 0.8f, base.Projectile.Opacity) * Main.rand.NextFloat(0.8f, 1.2f);
				dust.noGravity = true;
			}
		}
		Time++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		int damage = base.Projectile.damage;
		float kb = base.Projectile.knockBack;
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<SeraphimAngelicLight>(), damage, kb, base.Projectile.owner);
		NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(1600f);
		for (int i = 0; i < 6; i++)
		{
			float offsetAngle = MathHelper.Lerp(-0.9f, 0.9f, (float)i / 5f);
			if (potentialTarget != null)
			{
				offsetAngle -= base.Projectile.AngleTo(potentialTarget.Center);
			}
			Vector2 fanVelocity = offsetAngle.ToRotationVector2() * 10f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, fanVelocity, ModContent.ProjectileType<SeraphimDagger>(), damage / 2, kb, base.Projectile.owner, i);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.4f;
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
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 origin = texture.Size() * 0.5f;
		Vector2 baseDrawPosition = base.Projectile.Center - Main.screenPosition;
		float endFade = Utils.GetLerpValue(0f, 12f, base.Projectile.timeLeft, clamped: true);
		Color mainColor = Color.White * base.Projectile.Opacity * endFade * 1.5f;
		((Color)(ref mainColor)).A = (byte)(255 - base.Projectile.alpha);
		Color afterimageLightColor = Color.White * endFade;
		((Color)(ref afterimageLightColor)).A = (byte)(255 - base.Projectile.alpha);
		for (int i = 0; i < 18; i++)
		{
			Vector2 drawPosition = baseDrawPosition + ((float)Math.PI * 2f * (float)i / 18f).ToRotationVector2() * (1f - base.Projectile.Opacity) * 16f;
			Main.EntitySpriteDraw(texture, drawPosition, null, afterimageLightColor, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		}
		for (int j = 0; j < 8; j++)
		{
			Vector2 drawPosition2 = baseDrawPosition - base.Projectile.velocity * (float)j * 0.3f;
			Color afterimageColor = mainColor * (1f - (float)j / 8f);
			Main.EntitySpriteDraw(texture, drawPosition2, null, afterimageColor, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}
}
