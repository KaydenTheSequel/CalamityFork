using System;
using CalamityMod.Particles;
using CalamityMod.Utilities.Daybreak;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class LyraConstellation : ModProjectile, ILocalizedModType, IModType
{
	public static int lifetime = 150;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		lifetime = 300;
		base.Projectile.timeLeft = lifetime;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
		base.Projectile.MaxUpdates = 2;
	}

	public override void AI()
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref base.Projectile.velocity)).Length() > 0f)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		}
		Player Owner = Main.player[base.Projectile.owner];
		Vector2 DrawCenter = base.Projectile.Center + base.Projectile.velocity;
		float StarScale = 0.2f;
		base.Projectile.scale = MathF.Min((float)base.Projectile.timeLeft / 20f, MathF.Min((float)(lifetime - base.Projectile.timeLeft) / 20f, 1f));
		if (base.Projectile.FinalExtraUpdate())
		{
			SpawnStar(new Vector2(0f, 0f), 0.75f);
			SpawnStar(new Vector2(75f, -60f), 1.25f, 40);
			SpawnStar(new Vector2(3f, -102f), 0.75f, 120);
			SpawnStar(new Vector2(-52f, 207f), 0.75f, 5);
			SpawnStar(new Vector2(-144f, 239f), 0.75f, 10);
			SpawnStar(new Vector2(-96f, 35f), 0.75f, 75);
		}
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.Size = new Vector2(225f, 375f) * base.Projectile.scale;
		base.Projectile.Center = base.Projectile.position;
		void SpawnStar(Vector2 offset, float intensity, int flashOffset = 0, int flashMod = 100)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Unknown result type (might be due to invalid IL or missing references)
			//IL_0097: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_010d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0112: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Unknown result type (might be due to invalid IL or missing references)
			//IL_013f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0144: Unknown result type (might be due to invalid IL or missing references)
			//IL_0149: Unknown result type (might be due to invalid IL or missing references)
			//IL_016c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0172: Unknown result type (might be due to invalid IL or missing references)
			//IL_0173: Unknown result type (might be due to invalid IL or missing references)
			//IL_017d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0198: Unknown result type (might be due to invalid IL or missing references)
			//IL_019d: Unknown result type (might be due to invalid IL or missing references)
			offset += new Vector2(35.666f, -53.166f);
			offset.X *= base.Projectile.spriteDirection;
			BloomParticle star = new BloomParticle(DrawCenter + offset.RotatedBy(base.Projectile.rotation) * base.Projectile.scale - Owner.oldVelocity * Math.Clamp(((Vector2)(ref offset)).Length() * 0.001f, 0f, 1f), Vector2.Zero, Color.SkyBlue * (((Owner.miscCounter + flashOffset) % flashMod < 5) ? 0.75f : 1f), StarScale * intensity, StarScale * intensity, 2, fade: false);
			CustomSpark particle = new CustomSpark(DrawCenter + offset.RotatedBy(base.Projectile.rotation) * base.Projectile.scale - Owner.oldVelocity * Math.Clamp(((Vector2)(ref offset)).Length() * 0.001f, 0f, 1f), Vector2.UnitX.RotatedBy((float)Math.PI * ((float)(Owner.miscCounter + flashOffset) / 300f)) * 0.1f, "CalamityMod/Particles/Sparkle", affectedByGravity: false, 2, 4f * StarScale * intensity, Color.White, Vector2.One);
			GeneralParticleHandler.SpawnParticle(star);
			GeneralParticleHandler.SpawnParticle(particle);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		Vector2 SiriusPos = base.Projectile.Center;
		Player Owner = Main.player[base.Projectile.owner];
		using (Main.spriteBatch.Scope())
		{
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			ConnectStars(new Vector2(0f, 0f), new Vector2(75f, -60f));
			ConnectStars(new Vector2(0f, 0f), new Vector2(3f, -102f));
			ConnectStars(new Vector2(0f, 0f), new Vector2(-96f, 35f));
			ConnectStars(new Vector2(0f, 0f), new Vector2(-52f, 207f));
			ConnectStars(new Vector2(75f, -60f), new Vector2(3f, -102f));
			ConnectStars(new Vector2(-144f, 239f), new Vector2(-96f, 35f));
			ConnectStars(new Vector2(-144f, 239f), new Vector2(-52f, 207f));
			Main.spriteBatch.End();
		}
		return false;
		void ConnectStars(Vector2 point1, Vector2 point2)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0093: Unknown result type (might be due to invalid IL or missing references)
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_00be: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_010e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0114: Unknown result type (might be due to invalid IL or missing references)
			//IL_0115: Unknown result type (might be due to invalid IL or missing references)
			//IL_0125: Unknown result type (might be due to invalid IL or missing references)
			//IL_012a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0135: Unknown result type (might be due to invalid IL or missing references)
			//IL_0156: Unknown result type (might be due to invalid IL or missing references)
			//IL_015b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0160: Unknown result type (might be due to invalid IL or missing references)
			point1 += new Vector2(35.666f, -53.166f);
			point2 += new Vector2(35.666f, -53.166f);
			point1.X *= base.Projectile.spriteDirection;
			point2.X *= base.Projectile.spriteDirection;
			Color color = Color.SkyBlue * 0.75f * ((MathF.Sin(Main.GlobalTimeWrappedHourly) + 1f) * 0.25f + 0.5f);
			Main.spriteBatch.DrawLineBetter(SiriusPos + point1.RotatedBy(base.Projectile.rotation) * base.Projectile.scale - Owner.oldVelocity * Math.Clamp(((Vector2)(ref point1)).Length() * 0.001f, 0f, 1f), SiriusPos + point2.RotatedBy(base.Projectile.rotation) * base.Projectile.scale - Owner.oldVelocity * Math.Clamp(((Vector2)(ref point2)).Length() * 0.001f, 0f, 1f), color, 3f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 5; i++)
		{
			int dustType = Utils.SelectRandom<int>(Main.rand, 109, 111, 132);
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, dustType, base.Projectile.velocity.X, base.Projectile.velocity.Y, 0, default(Color), 1.5f);
			Main.dust[dust].noGravity = true;
		}
	}
}
