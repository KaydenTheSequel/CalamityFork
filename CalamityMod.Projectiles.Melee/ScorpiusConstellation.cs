using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using CalamityMod.Utilities.Daybreak;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class ScorpiusConstellation : ModProjectile, ILocalizedModType, IModType
{
	public static int lifetime = 150;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		lifetime = 90;
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = lifetime;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.scale = 0f;
	}

	public override void AI()
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref base.Projectile.velocity)).Length() > 0f)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		}
		Player Owner = Main.player[base.Projectile.owner];
		Vector2 DrawCenter = base.Projectile.Center + base.Projectile.velocity;
		float StarScale = 0.2f;
		base.Projectile.scale = 0.75f * MathF.Min(MathF.Pow((float)(lifetime - base.Projectile.timeLeft) / 20f, 2f), 1f);
		base.Projectile.Opacity = MathF.Min((float)base.Projectile.timeLeft / 20f, 1f);
		if (base.Projectile.FinalExtraUpdate())
		{
			SpawnStar(new Vector2(0f, 0f), 0.75f, 25);
			SpawnStar(new Vector2(99f, -166f), 1.25f, 20);
			SpawnStar(new Vector2(224f, -300f), 0.75f, 15);
			SpawnStar(new Vector2(243f, -237f), 0.75f, 10);
			SpawnStar(new Vector2(243f, -163f), 0.75f, 5);
			SpawnStar(new Vector2(246f, -97f), 0.75f);
			SpawnStar(new Vector2(-255f, 77f), 0.75f, 55);
			SpawnStar(new Vector2(-176f, 71f), 0.75f, 50);
			SpawnStar(new Vector2(-236f, 142f), 0.75f, 45);
			SpawnStar(new Vector2(-188f, 197f), 0.75f, 40);
			SpawnStar(new Vector2(-85f, 193f), 0.75f, 35);
			SpawnStar(new Vector2(-19f, 172f), 0.75f, 30);
		}
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.Size = new Vector2(510f, 510f) * base.Projectile.scale;
		base.Projectile.Center = base.Projectile.position;
		void SpawnStar(Vector2 offset, float intensity, int flashOffset = 0, int flashMod = 60)
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
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0106: Unknown result type (might be due to invalid IL or missing references)
			//IL_010c: Unknown result type (might be due to invalid IL or missing references)
			//IL_010d: Unknown result type (might be due to invalid IL or missing references)
			//IL_011d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0122: Unknown result type (might be due to invalid IL or missing references)
			//IL_012e: Unknown result type (might be due to invalid IL or missing references)
			//IL_014f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0154: Unknown result type (might be due to invalid IL or missing references)
			//IL_0159: Unknown result type (might be due to invalid IL or missing references)
			//IL_017c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0182: Unknown result type (might be due to invalid IL or missing references)
			//IL_0183: Unknown result type (might be due to invalid IL or missing references)
			//IL_018d: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
			offset += new Vector2(5f, 49.25f);
			offset.X *= base.Projectile.spriteDirection;
			BloomParticle star = new BloomParticle(DrawCenter + offset.RotatedBy(base.Projectile.rotation) * base.Projectile.scale - Owner.oldVelocity * Math.Clamp(((Vector2)(ref offset)).Length() * 0.001f, 0f, 1f), Vector2.Zero, Color.SkyBlue * base.Projectile.Opacity * (((Owner.miscCounter + flashOffset) % flashMod < 5) ? 0.75f : 1f), StarScale * intensity, StarScale * intensity, 2, fade: false);
			CustomSpark particle = new CustomSpark(DrawCenter + offset.RotatedBy(base.Projectile.rotation) * base.Projectile.scale - Owner.oldVelocity * Math.Clamp(((Vector2)(ref offset)).Length() * 0.001f, 0f, 1f), Vector2.UnitX.RotatedBy((float)Math.PI * ((float)(Owner.miscCounter + flashOffset) / 300f)) * 0.1f, "CalamityMod/Particles/Sparkle", affectedByGravity: false, 2, 4f * StarScale * intensity, Color.White * base.Projectile.Opacity, Vector2.One);
			GeneralParticleHandler.SpawnParticle(star);
			GeneralParticleHandler.SpawnParticle(particle);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Voidfrost>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Voidfrost>(), 180);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		Vector2 SiriusPos = base.Projectile.Center;
		_ = Main.player[base.Projectile.owner];
		using (Main.spriteBatch.Scope())
		{
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			ConnectStars(new Vector2(-255f, 77f), new Vector2(-176f, 71f));
			ConnectStars(new Vector2(-176f, 71f), new Vector2(-236f, 142f));
			ConnectStars(new Vector2(-236f, 142f), new Vector2(-188f, 197f));
			ConnectStars(new Vector2(-188f, 197f), new Vector2(-85f, 193f));
			ConnectStars(new Vector2(-85f, 193f), new Vector2(-19f, 172f));
			ConnectStars(new Vector2(-19f, 172f), new Vector2(0f, 0f));
			ConnectStars(new Vector2(0f, 0f), new Vector2(99f, -166f));
			ConnectStars(new Vector2(99f, -166f), new Vector2(224f, -300f));
			ConnectStars(new Vector2(99f, -166f), new Vector2(243f, -237f));
			ConnectStars(new Vector2(99f, -166f), new Vector2(243f, -163f));
			ConnectStars(new Vector2(99f, -166f), new Vector2(246f, -97f));
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
			//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_010a: Unknown result type (might be due to invalid IL or missing references)
			point1 += new Vector2(5f, 49.25f);
			point2 += new Vector2(5f, 49.25f);
			point1.X *= base.Projectile.spriteDirection;
			point2.X *= base.Projectile.spriteDirection;
			Color color = Color.SkyBlue * 0.75f * ((MathF.Sin(Main.GlobalTimeWrappedHourly) + 1f) * 0.25f + 0.5f);
			Main.spriteBatch.DrawLineBetter(SiriusPos + point1.RotatedBy(base.Projectile.rotation) * base.Projectile.scale, SiriusPos + point2.RotatedBy(base.Projectile.rotation) * base.Projectile.scale, color * base.Projectile.Opacity, 3f);
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
