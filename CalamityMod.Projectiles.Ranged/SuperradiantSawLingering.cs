using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SuperradiantSawLingering : ModProjectile, ILocalizedModType, IModType
{
	public Particle SmallSlashSmear;

	public Particle LargeSlashSmear;

	public static Asset<Texture2D> SawOutline;

	public static Asset<Texture2D> SmallSlash;

	public static Asset<Texture2D> LargeSlash;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Ranged/SuperradiantSaw";

	public ref float Time => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 46);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.timeLeft = 270;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		base.Projectile.rotation += MathHelper.ToRadians(42f);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.955f;
		if (Time % 12f == 0f && Time > 30f)
		{
			Vector2 randVelocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(7.5f, 9f);
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, randVelocity, ModContent.ProjectileType<SuperradiantBolt>(), (int)((float)base.Projectile.damage * 0.5f), 0f, Main.myPlayer);
			}
		}
		if (base.Projectile.timeLeft <= 30)
		{
			base.Projectile.alpha += 8;
			if (base.Projectile.alpha > 255)
			{
				base.Projectile.Kill();
			}
		}
		if (LargeSlashSmear == null)
		{
			LargeSlashSmear = new CircularSmearVFX(base.Projectile.Center, Color.Black, Time * (0f - base.Projectile.rotation), 1.35f);
			GeneralParticleHandler.SpawnParticle(LargeSlashSmear);
		}
		else
		{
			LargeSlashSmear.Rotation = 0f - base.Projectile.rotation;
			LargeSlashSmear.Time = 0;
			LargeSlashSmear.Position = base.Projectile.Center;
			LargeSlashSmear.Scale = 1.35f;
			LargeSlashSmear.Color = Main.hslToRgb(0.5f + 0.5f * MathF.Sin(Main.GlobalTimeWrappedHourly * 5f), 1f, 0.6f) * 0.8f * base.Projectile.Opacity;
		}
		if (SmallSlashSmear == null)
		{
			SmallSlashSmear = new CircularSmearVFX(base.Projectile.Center, Color.Black, base.Projectile.rotation, 0.8f);
			GeneralParticleHandler.SpawnParticle(SmallSlashSmear);
			return;
		}
		SmallSlashSmear.Rotation = base.Projectile.rotation;
		SmallSlashSmear.Time = 0;
		SmallSlashSmear.Position = base.Projectile.Center;
		SmallSlashSmear.Scale = 0.8f;
		SmallSlashSmear.Color = Main.hslToRgb(0.5f + 0.5f * MathF.Cos(Main.GlobalTimeWrappedHourly * 5f), 1f, 0.6f) * 0.6f * base.Projectile.Opacity;
	}

	public override bool? CanDamage()
	{
		return base.Projectile.timeLeft > 30;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Laceration>(), 180);
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 90);
		SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/SwiftSlice"), base.Projectile.Center);
		for (int s = 0; s < 12; s++)
		{
			Vector2 sparkVel = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(14f, 18f);
			float sparkSize = Main.rand.NextFloat(1f, 1.4f);
			Color sparkColor = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.8f);
			GeneralParticleHandler.SpawnParticle(new AltLineParticle(target.Center, sparkVel, affectedByGravity: false, 30, sparkSize, sparkColor));
		}
		for (int sq = 0; sq < 5; sq++)
		{
			Vector2 squareVel = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(10f, 16f);
			float squareSize = Main.rand.NextFloat(3.2f, 4f);
			Color squareColor = Main.hslToRgb(Main.rand.NextFloat(), 0.6f, 0.8f);
			GeneralParticleHandler.SpawnParticle(new SquareParticle(target.Center, squareVel, affectedByGravity: true, 30, squareSize, squareColor));
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/CeramicImpact", 2), base.Projectile.Center);
		for (int i = 0; i < 32; i++)
		{
			Vector2 velocity = ((float)Math.PI * 2f * (float)i / 32f - (float)Math.PI / 32f).ToRotationVector2() * 32f;
			GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, velocity, Color.White, Color.Lime, 1.5f, 30, 0.1f, 3f, Main.rand.NextFloat(0f, 0.01f)));
		}
	}

	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		((Rectangle)(ref hitbox)).Inflate(70, 70);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		if (LargeSlash == null)
		{
			LargeSlash = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/SuperradiantSawLargeSlash", (AssetRequestMode)2);
		}
		Texture2D largeSlashTexture = LargeSlash.Value;
		if (SmallSlash == null)
		{
			SmallSlash = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/SuperradiantSawSmallSlash", (AssetRequestMode)2);
		}
		Texture2D smallSlashTexture = SmallSlash.Value;
		Color slashColor = new Color(200, 200, 200, 100) * base.Projectile.Opacity;
		Main.EntitySpriteDraw(largeSlashTexture, base.Projectile.Center - Main.screenPosition, null, slashColor, 0f - base.Projectile.rotation, largeSlashTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
		if (Time % 4f == 0f)
		{
			Vector2 randomParticleOffset = default(Vector2);
			((Vector2)(ref randomParticleOffset))._002Ector(Main.rand.NextFloat((float)(-base.Projectile.width) * 1.75f, (float)base.Projectile.width * 1.75f), Main.rand.NextFloat((float)(-base.Projectile.width) * 1.75f, (float)base.Projectile.width * 1.75f));
			float randomParticleScale = Main.rand.NextFloat(0.65f, 0.95f);
			Color bloomColor = Color.Lerp(new Color(29, 120, 30), new Color(56, 255, 59), MathF.Abs(MathF.Sin(Time)));
			GeneralParticleHandler.SpawnParticle(new BloomParticle(base.Projectile.Center + randomParticleOffset, base.Projectile.velocity, Main.rand.NextBool() ? Color.White : bloomColor, randomParticleScale, randomParticleScale, 4, fade: false));
		}
		Main.EntitySpriteDraw(smallSlashTexture, base.Projectile.Center - Main.screenPosition, null, slashColor, base.Projectile.rotation, smallSlashTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
		if (Time % 4f == 0f)
		{
			Vector2 randomParticleOffset2 = default(Vector2);
			((Vector2)(ref randomParticleOffset2))._002Ector(Main.rand.NextFloat(-base.Projectile.width, base.Projectile.width), Main.rand.NextFloat(-base.Projectile.width, base.Projectile.width));
			float randomParticleScale2 = Main.rand.NextFloat(0.35f, 0.65f);
			Color bloomColor2 = Color.Lerp(new Color(29, 120, 30), new Color(56, 255, 59), MathF.Abs(MathF.Sin(Time)));
			GeneralParticleHandler.SpawnParticle(new BloomParticle(base.Projectile.Center + randomParticleOffset2, base.Projectile.velocity, Main.rand.NextBool() ? Color.White : bloomColor2, randomParticleScale2, randomParticleScale2, 4, fade: false));
		}
		Texture2D buzzsawTexture = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(buzzsawTexture, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, buzzsawTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
		if (SawOutline == null)
		{
			SawOutline = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/SuperradiantSawOutline", (AssetRequestMode)2);
		}
		Texture2D outline = SawOutline.Value;
		Main.EntitySpriteDraw(outline, base.Projectile.Center - Main.screenPosition, null, Main.DiscoColor, base.Projectile.rotation, outline.Size() * 0.5f, 1f, (SpriteEffects)0);
		if (!CalamityClientConfig.Instance.Afterimages)
		{
			return false;
		}
		for (int i = 1; i < base.Projectile.oldPos.Length; i++)
		{
			float afterimageRot = base.Projectile.oldRot[i];
			Vector2 drawPos = base.Projectile.oldPos[i] + buzzsawTexture.Size() * 0.5f - Main.screenPosition;
			float intensity = MathHelper.Lerp(0.1f, 0.6f, 1f - (float)i / (float)base.Projectile.oldPos.Length);
			Main.EntitySpriteDraw(buzzsawTexture, drawPos, null, Color.White * intensity, afterimageRot, buzzsawTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
			Main.EntitySpriteDraw(largeSlashTexture, drawPos, null, slashColor * intensity, 0f - afterimageRot, largeSlashTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
			Main.EntitySpriteDraw(smallSlashTexture, drawPos, null, slashColor * intensity, afterimageRot, smallSlashTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
		}
		return false;
	}
}
