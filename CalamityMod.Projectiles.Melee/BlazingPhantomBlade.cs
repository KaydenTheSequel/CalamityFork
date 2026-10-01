using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class BlazingPhantomBlade : ModProjectile, ILocalizedModType, IModType
{
	internal const float StartingScale = 1f;

	internal const float SunlightBladeMaxVelocity = 32f;

	internal const int SunlightBladePierce = 10;

	internal const float FadeInTime = 30f;

	internal const float FadeOutTime = 30f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.NoMeleeSpeedVelocityScaling[base.Type] = true;
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.alpha = 255;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
		base.Projectile.timeLeft = 220;
		base.Projectile.noEnchantmentVisuals = true;
		base.Projectile.scale = 1f;
	}

	public override void AI()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		float fullyVisibleDuration = base.Projectile.ai[1];
		bool hyperBlade = fullyVisibleDuration == 48f;
		bool sunlightBlade = fullyVisibleDuration == 56f;
		float timeBeforeFadeOut = fullyVisibleDuration + 30f;
		float projectileDuration = timeBeforeFadeOut + 30f;
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item8, base.Projectile.Center);
		}
		base.Projectile.localAI[0]++;
		base.Projectile.Opacity = Utils.Remap(base.Projectile.localAI[0], 0f, fullyVisibleDuration, 0f, 1f) * Utils.Remap(base.Projectile.localAI[0], timeBeforeFadeOut, projectileDuration, 1f, 0f);
		if (base.Projectile.localAI[0] >= projectileDuration)
		{
			base.Projectile.localAI[1] = 1f;
			base.Projectile.Kill();
			return;
		}
		_ = Main.player[base.Projectile.owner];
		base.Projectile.direction = (base.Projectile.spriteDirection = (int)base.Projectile.ai[0]);
		base.Projectile.localAI[1]++;
		base.Projectile.rotation += base.Projectile.ai[0] * ((float)Math.PI * 2f) * (4f + base.Projectile.Opacity * 4f) / 90f;
		base.Projectile.scale = Utils.Remap(base.Projectile.localAI[0], fullyVisibleDuration + 2f, projectileDuration, 1.12f, 1f) * base.Projectile.ai[2] * 1f;
		float randomDustSpawnLocation = base.Projectile.rotation + Main.rand.NextFloatDirection() * ((float)Math.PI / 2f) * 0.7f;
		Vector2 dustPosition = base.Projectile.Center + randomDustSpawnLocation.ToRotationVector2() * 84f * base.Projectile.scale;
		if (Main.rand.NextBool(3))
		{
			Dust dust = Dust.NewDustPerfect(dustPosition, 171, null, 100, default(Color), 1.4f);
			dust.noGravity = true;
			dust.velocity *= 0f;
			dust.fadeIn = 1.5f;
		}
		for (int i = 0; (float)i < 3f * base.Projectile.Opacity; i++)
		{
			Vector2 dustVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX);
			int dustType1 = (sunlightBlade ? 169 : (hyperBlade ? 75 : 296));
			int dustType2 = (sunlightBlade ? 64 : (hyperBlade ? 61 : 60));
			int dustType3 = ((Main.rand.NextFloat() < base.Projectile.Opacity) ? dustType1 : dustType2);
			Dust dust2 = Dust.NewDustPerfect(dustPosition, dustType3, base.Projectile.velocity * 0.2f + dustVelocity * 3f, 100, default(Color), 1.4f);
			dust2.noGravity = true;
			dust2.customData = base.Projectile.Opacity * 0.2f;
		}
		if (!sunlightBlade)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, hyperBlade ? 500f : 250f, hyperBlade ? 16f : 8f, hyperBlade ? 10f : 15f);
		}
		else if (((Vector2)(ref base.Projectile.velocity)).Length() < 32f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.05f;
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 32f)
			{
				((Vector2)(ref base.Projectile.velocity)).Normalize();
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 32f;
			}
		}
		_ = base.Projectile.position;
		_ = base.Projectile.width;
		_ = base.Projectile.height;
		for (float i2 = -(float)Math.PI / 4f; i2 <= (float)Math.PI / 4f; i2 += (float)Math.PI / 2f)
		{
			Rectangle rect = Utils.CenteredRectangle(base.Projectile.Center + (base.Projectile.rotation + i2).ToRotationVector2() * 70f * base.Projectile.scale, new Vector2(60f * base.Projectile.scale, 60f * base.Projectile.scale));
			base.Projectile.EmitEnchantmentVisualsAt(rect.TopLeft(), rect.Width, rect.Height);
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		Vector2 distanceFromTarget = targetHitbox.ClosestPointInRect(base.Projectile.Center) - base.Projectile.Center;
		distanceFromTarget.SafeNormalize(Vector2.UnitX);
		float projectileSize = 100f * base.Projectile.scale;
		if (((Vector2)(ref distanceFromTarget)).Length() < projectileSize && Collision.CanHit(base.Projectile.Center, 0, 0, ((Rectangle)(ref targetHitbox)).Center.ToVector2(), 0, 0))
		{
			return true;
		}
		return null;
	}

	public override void CutTiles()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		Vector2 startPoint = (base.Projectile.rotation - (float)Math.PI / 4f).ToRotationVector2() * 60f * base.Projectile.scale;
		Vector2 endPoint = (base.Projectile.rotation + (float)Math.PI / 4f).ToRotationVector2() * 60f * base.Projectile.scale;
		float projectileSize = 60f * base.Projectile.scale;
		Utils.PlotTileLine(base.Projectile.Center + startPoint, base.Projectile.Center + endPoint, projectileSize, DelegateMethods.CutTiles);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0902: Unknown result type (might be due to invalid IL or missing references)
		//IL_0926: Unknown result type (might be due to invalid IL or missing references)
		//IL_093e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0945: Unknown result type (might be due to invalid IL or missing references)
		//IL_094c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0951: Unknown result type (might be due to invalid IL or missing references)
		//IL_0956: Unknown result type (might be due to invalid IL or missing references)
		//IL_0968: Unknown result type (might be due to invalid IL or missing references)
		//IL_0978: Unknown result type (might be due to invalid IL or missing references)
		//IL_0982: Unknown result type (might be due to invalid IL or missing references)
		//IL_0989: Unknown result type (might be due to invalid IL or missing references)
		//IL_098e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aaa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ace: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_069a: Unknown result type (might be due to invalid IL or missing references)
		//IL_069c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06da: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_059f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05df: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0759: Unknown result type (might be due to invalid IL or missing references)
		//IL_0760: Unknown result type (might be due to invalid IL or missing references)
		//IL_0767: Unknown result type (might be due to invalid IL or missing references)
		//IL_076e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0775: Unknown result type (might be due to invalid IL or missing references)
		//IL_077f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0791: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		//IL_0799: Unknown result type (might be due to invalid IL or missing references)
		//IL_079d: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0829: Unknown result type (might be due to invalid IL or missing references)
		//IL_083f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0846: Unknown result type (might be due to invalid IL or missing references)
		//IL_084d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0851: Unknown result type (might be due to invalid IL or missing references)
		float num = base.Projectile.ai[1];
		bool hyperBlade = num == 48f;
		bool sunlightBlade = num == 56f;
		Texture2D asset = TextureAssets.Projectile[base.Type].Value;
		Rectangle rectangle = asset.Frame(1, 4);
		Vector2 origin = rectangle.Size() / 2f;
		float num2 = base.Projectile.scale * 1.1f;
		SpriteEffects effects = (SpriteEffects)((!(base.Projectile.ai[0] >= 0f)) ? 2 : 0);
		float num3 = 0.975f;
		Color color = Lighting.GetColor(base.Projectile.Center.ToTileCoordinates());
		Vector3 val = ((Color)(ref color)).ToVector3();
		float fromValue = ((Vector3)(ref val)).Length() / (float)Math.Sqrt(3.0);
		fromValue = Utils.Remap(fromValue, 0.2f, 1f, 0f, 1f);
		float num4 = MathHelper.Min(0.15f + fromValue * 0.85f, Utils.Remap(base.Projectile.localAI[0], 30f, 96f, 1f, 0f));
		float num5 = 2f;
		for (float num6 = num5; num6 >= 0f; num6--)
		{
			if (!(base.Projectile.oldPos[(int)num6] == Vector2.Zero))
			{
				Vector2 val2 = base.Projectile.Center - base.Projectile.velocity * 0.5f * num6;
				float num7 = base.Projectile.oldRot[(int)num6] + base.Projectile.ai[0] * ((float)Math.PI * 2f) * 0.1f * (0f - num6);
				Vector2 position = val2 - Main.screenPosition;
				float num8 = 1f - num6 / num5;
				float num9 = base.Projectile.Opacity * num8 * num8 * 0.85f;
				float amount = base.Projectile.Opacity * base.Projectile.Opacity;
				Color colorOne = Color.Lerp(new Color(sunlightBlade ? 20 : (hyperBlade ? 60 : 40), sunlightBlade ? 40 : (hyperBlade ? 20 : 60), sunlightBlade ? 60 : (hyperBlade ? 40 : 20), 120), new Color(sunlightBlade ? 225 : (hyperBlade ? 25 : 225), sunlightBlade ? 225 : (hyperBlade ? 225 : 40), sunlightBlade ? 25 : (hyperBlade ? 40 : 25), 120), amount);
				Main.spriteBatch.Draw(asset, position, (Rectangle?)rectangle, colorOne * num4 * num9, num7 + base.Projectile.ai[0] * ((float)Math.PI / 4f) * -1f, origin, num2 * num3, effects, 0f);
				Color colorTwo = Color.Lerp(new Color(sunlightBlade ? 40 : (hyperBlade ? 180 : 80), sunlightBlade ? 80 : (hyperBlade ? 40 : 180), sunlightBlade ? 180 : (hyperBlade ? 80 : 40)), new Color(sunlightBlade ? 255 : (hyperBlade ? 100 : 155), sunlightBlade ? 255 : (hyperBlade ? 255 : 100), sunlightBlade ? 100 : (hyperBlade ? 155 : 255)), amount);
				Color color3 = Color.White * num9 * 0.5f;
				((Color)(ref color3)).A = (byte)((float)(int)((Color)(ref color3)).A * (1f - num4));
				Color color4 = color3 * num4 * 0.5f;
				if (sunlightBlade)
				{
					((Color)(ref color4)).B = (byte)((float)(int)((Color)(ref color4)).B * num4);
					((Color)(ref color4)).R = (byte)((float)(int)((Color)(ref color4)).R * (0.25f + num4 * 0.75f));
				}
				else if (hyperBlade)
				{
					((Color)(ref color4)).G = (byte)((float)(int)((Color)(ref color4)).G * num4);
					((Color)(ref color4)).B = (byte)((float)(int)((Color)(ref color4)).B * (0.25f + num4 * 0.75f));
				}
				else
				{
					((Color)(ref color4)).R = (byte)((float)(int)((Color)(ref color4)).R * num4);
					((Color)(ref color4)).G = (byte)((float)(int)((Color)(ref color4)).G * (0.25f + num4 * 0.75f));
				}
				float num10 = 3f;
				for (float num11 = (float)Math.PI * -2f + (float)Math.PI * 2f / num10; num11 < 0f; num11 += (float)Math.PI * 2f / num10)
				{
					float num12 = Utils.Remap(num11, (float)Math.PI * -2f, 0f, 0f, 0.5f);
					Main.spriteBatch.Draw(asset, position, (Rectangle?)rectangle, color4 * 0.15f * num12, num7 + base.Projectile.ai[0] * 0.01f + num11, origin, num2, effects, 0f);
					Main.spriteBatch.Draw(asset, position, (Rectangle?)rectangle, Color.Lerp(new Color(sunlightBlade ? 30 : (hyperBlade ? 160 : 80), sunlightBlade ? 80 : (hyperBlade ? 30 : 160), sunlightBlade ? 160 : (hyperBlade ? 80 : 30)), new Color(sunlightBlade ? 200 : (hyperBlade ? 255 : 200), sunlightBlade ? 200 : (hyperBlade ? 200 : 0), sunlightBlade ? 255 : ((!hyperBlade) ? 255 : 0)), amount) * fromValue * num9 * num12, num7 + num11, origin, num2 * 0.8f, effects, 0f);
					Main.spriteBatch.Draw(asset, position, (Rectangle?)rectangle, colorTwo * fromValue * num9 * MathHelper.Lerp(0.05f, 0.4f, fromValue) * num12, num7 + num11, origin, num2 * num3, effects, 0f);
					Main.spriteBatch.Draw(asset, position, (Rectangle?)asset.Frame(1, 4, 0, 3), new Color(sunlightBlade ? 150 : (hyperBlade ? 255 : 200), sunlightBlade ? 255 : (hyperBlade ? 200 : 150), sunlightBlade ? 200 : (hyperBlade ? 150 : 255)) * MathHelper.Lerp(0.05f, 0.5f, fromValue) * num9 * num12, num7 + num11, origin, num2, effects, 0f);
				}
				Main.spriteBatch.Draw(asset, position, (Rectangle?)rectangle, color4 * 0.15f, num7 + base.Projectile.ai[0] * 0.01f, origin, num2, effects, 0f);
				Main.spriteBatch.Draw(asset, position, (Rectangle?)rectangle, Color.Lerp(new Color(sunlightBlade ? 30 : (hyperBlade ? 160 : 80), sunlightBlade ? 80 : (hyperBlade ? 30 : 160), sunlightBlade ? 160 : (hyperBlade ? 80 : 30)), new Color(sunlightBlade ? 255 : ((!hyperBlade) ? 255 : 0), sunlightBlade ? 255 : (hyperBlade ? 255 : 100), (!sunlightBlade) ? (hyperBlade ? 100 : 0) : 0), amount) * num4 * num9, num7, origin, num2 * 0.8f, effects, 0f);
				Main.spriteBatch.Draw(asset, position, (Rectangle?)rectangle, colorTwo * fromValue * num9 * MathHelper.Lerp(0.05f, 0.4f, num4), num7, origin, num2 * num3, effects, 0f);
				Main.spriteBatch.Draw(asset, position, (Rectangle?)asset.Frame(1, 4, 0, 3), new Color(sunlightBlade ? 255 : (hyperBlade ? 100 : 255), sunlightBlade ? 255 : (hyperBlade ? 255 : 75), sunlightBlade ? 75 : (hyperBlade ? 75 : 100)) * MathHelper.Lerp(0.05f, 0.5f, num4) * num9, num7, origin, num2, effects, 0f);
			}
		}
		float num13 = 1f - base.Projectile.localAI[0] * 1f / 80f;
		if (num13 < 0.5f)
		{
			num13 = 0.5f;
		}
		float num14 = MathHelper.Min(num4, MathHelper.Lerp(1f, fromValue, Utils.Remap(base.Projectile.localAI[0], 0f, 80f, 0f, 1f)));
		Texture2D value = TextureAssets.Extra[98].Value;
		SpriteEffects dir = (SpriteEffects)0;
		Vector2 drawpos = base.Projectile.Center - Main.screenPosition + (base.Projectile.rotation + (float)Math.PI * 3f / 20f * base.Projectile.ai[0]).ToRotationVector2() * ((float)asset.Width * 0.5f - 4f) * num2 * num13;
		Color drawColor = new Color(255, 255, 255, 0) * base.Projectile.Opacity * 0.5f * num14;
		Color color5 = new Color(sunlightBlade ? 255 : (hyperBlade ? 50 : 255), sunlightBlade ? 255 : (hyperBlade ? 255 : 75), sunlightBlade ? 50 : (hyperBlade ? 75 : 50)) * num14 * base.Projectile.Opacity * 0.5f;
		((Color)(ref color5)).A = 0;
		float flareCounter = base.Projectile.Opacity;
		float fadeInStart = 0f;
		float fadeInEnd = 1f;
		float fadeOutStart = 1f;
		float fadeOutEnd = 2f;
		float rotation = (float)Math.PI / 4f;
		Vector2 scale = default(Vector2);
		((Vector2)(ref scale))._002Ector(2f, 2f);
		Vector2 one = Vector2.One;
		Vector2 origin2 = value.Size() / 2f;
		Color color6 = drawColor * 0.5f;
		float num15 = Utils.GetLerpValue(fadeInStart, fadeInEnd, flareCounter, clamped: true) * Utils.GetLerpValue(fadeOutEnd, fadeOutStart, flareCounter, clamped: true);
		Vector2 vector = new Vector2(one.X * 0.5f, scale.X) * num15;
		Vector2 vector2 = new Vector2(one.Y * 0.5f, scale.Y) * num15;
		color5 *= num15;
		color6 *= num15;
		Main.EntitySpriteDraw(value, drawpos, null, color5, (float)Math.PI / 2f + rotation, origin2, vector, dir);
		Main.EntitySpriteDraw(value, drawpos, null, color5, 0f + rotation, origin2, vector2, dir);
		Main.EntitySpriteDraw(value, drawpos, null, color6, (float)Math.PI / 2f + rotation, origin2, vector * 0.6f, dir);
		Main.EntitySpriteDraw(value, drawpos, null, color6, 0f + rotation, origin2, vector2 * 0.6f, dir);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(70, 120);
		target.AddBuff(323, 120);
		bool sunlightBlade = base.Projectile.ai[1] == 56f;
		if (base.Projectile.numHits >= (sunlightBlade ? 10 : 3))
		{
			base.Projectile.localAI[0] = base.Projectile.ai[1] + 30f;
		}
	}

	public override bool? CanDamage()
	{
		if (!(base.Projectile.localAI[0] > base.Projectile.ai[1] + 30f))
		{
			return null;
		}
		return false;
	}
}
