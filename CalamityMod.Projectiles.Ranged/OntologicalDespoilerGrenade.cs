using System;
using System.Collections.Generic;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class OntologicalDespoilerGrenade : ModProjectile, ILocalizedModType, IModType
{
	public bool explode;

	public Color baseColor;

	public Color color1;

	public Color color2;

	public Color color3;

	public Color color4;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Ranged/OntologicalDespoilerGrenade";

	public ref float time => ref base.Projectile.ai[0];

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 25;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.extraUpdates = 3;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		color1 = Owner.shirtColor;
		color2 = Color.Lerp(Owner.shirtColor, Color.Black, 0.3f);
		color3 = Color.Lerp(Owner.shirtColor, Color.White, 0.2f);
		color4 = Color.Lerp(Owner.shirtColor, Color.White, 0.4f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 12)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 5)
		{
			base.Projectile.frame = 0;
		}
		if (time < 60f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.982f;
		}
		if (Owner.shirtColor != Color.White)
		{
			float rate = Main.GlobalTimeWrappedHourly * 15f;
			List<Color> eColors = new List<Color> { color1, color2, color3, color4 };
			int colorIndex = (int)(rate / 2f % (float)eColors.Count);
			Color currentColor = eColors[colorIndex];
			Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
			baseColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		}
		else
		{
			baseColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
		}
		if (time > 20f)
		{
			float sine = (float)Math.Sin((float)base.Projectile.timeLeft * 0.575f / (float)Math.PI);
			base.Projectile.scale = MathHelper.Clamp(1f + sine, 0.7f, 1.3f);
			Vector2 offset = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * 12f;
			float scale = Main.rand.NextFloat(1.3f, 1.4f);
			if (Main.rand.NextBool(3))
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset, ModContent.DustType<VoidDust>(), -base.Projectile.velocity * Main.rand.NextFloat(0.3f, 0.8f));
				dust.noGravity = true;
				dust.scale = scale;
				dust.color = baseColor;
			}
			if (Main.rand.NextBool(3))
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center - offset, ModContent.DustType<VoidDustInverted>(), -base.Projectile.velocity * Main.rand.NextFloat(0.3f, 0.8f));
				dust2.noGravity = true;
				dust2.scale = scale;
				dust2.color = baseColor;
			}
		}
		if (base.Projectile.timeLeft == 1)
		{
			explode = false;
		}
		NPC targetedNPC = base.Projectile.Center.ClosestNPCAt(1200f);
		if (targetedNPC != null && time > 30f && base.Projectile.numHits < 1 && Vector2.Distance(targetedNPC.Center, base.Projectile.Center) < 1200f)
		{
			float moveSpeed = Utils.GetLerpValue(570f, 120f, base.Projectile.timeLeft, clamped: true);
			CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targetedNPC, ignoreTiles: true, moveSpeed, 7f, 0.98f, 0.95f, accelerate: true);
			explode = true;
		}
		time++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		if (explode)
		{
			Owner.SetScreenshake(8.5f);
			float power = 1.5f;
			for (int i = 0; i < 55; i++)
			{
				Color useColor = GetRandomColor();
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(6) ? ModContent.DustType<VoidDustInverted>() : ModContent.DustType<VoidDust>(), (base.Projectile.velocity * 6f * power).RotatedByRandom(100.0) * Main.rand.NextFloat(0.2f, 1f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.75f, 2.25f) * power;
				dust.color = useColor;
				if (Owner.shirtColor == Color.White)
				{
					((Color)(ref useColor))._002Ector(Main.DiscoR, Main.DiscoG, Main.DiscoB);
				}
				if (i % 2 == 0)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(0f, -40f * power), 100.0) * Main.rand.NextFloat(0.1f, 1f), "CalamityMod/Particles/Sparkle", affectedByGravity: false, 40, Main.rand.NextFloat(1.4f, 2.4f) * power, useColor, new Vector2(0.4f, 1.1f)));
				}
			}
			for (int j = 0; j < 3; j++)
			{
				Color useColor2 = GetRandomColor();
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, useColor2, "CalamityMod/Particles/SoftRoundExplosion", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.4f - (float)j * 0.03f * power, 13, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, baseColor, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.15f * power, 2.5f * power, 38, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			int parts = 8;
			float rot = Main.rand.NextFloat(-9f, 9f);
			for (int k = 0; k < parts; k++)
			{
				Color useColor3 = GetRandomColor();
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Utils.RotatedBy(new Vector2(0f, -15f * ((k % 2 == 0) ? 1.8f : 1f) * power), (double)((float)k * (MathHelper.ToRadians(360f) / (float)parts)), default(Vector2)).RotatedBy(rot), "CalamityMod/Particles/VerticalSmear", affectedByGravity: false, 19, 3f * power, useColor3, new Vector2(0.2f, 1f)));
			}
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Black, "CalamityMod/Particles/SmallBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 1.2f * power, 39, UseAdditiveBlend: false, 1f, fade: true, 1f, (SpriteEffects)0));
			SoundStyle style;
			for (int l = 0; l < 3; l++)
			{
				style = new SoundStyle("CalamityMod/Sounds/Item/EarthMeteor");
				style.Volume = 0.6f;
				style.Pitch = -0.1f * (float)(l + 1);
				style.MaxInstances = 3;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			style = new SoundStyle("CalamityMod/Sounds/Item/ShadowboltReflect");
			style.Volume = 0.9f;
			style.Pitch = -0.4f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<OntoligicalDespoilerBurst>(), (int)((float)base.Projectile.damage * 12f), 0f, base.Projectile.owner);
		}
		else
		{
			for (int m = 0; m < 20; m++)
			{
				Color useColor4 = GetRandomColor();
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool() ? ModContent.DustType<VoidDustInverted>() : ModContent.DustType<VoidDust>(), (base.Projectile.velocity * 3f).RotatedByRandom(100.0) * Main.rand.NextFloat(0.2f, 1f));
				dust2.noGravity = true;
				dust2.scale = Main.rand.NextFloat(1.85f, 2.45f);
				dust2.color = useColor4;
			}
		}
	}

	public Color GetRandomColor()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		Color useColor = (Color)(Main.rand.Next(4) switch
		{
			0 => color1, 
			1 => color2, 
			2 => color3, 
			_ => color4, 
		});
		if (Owner.shirtColor == Color.White)
		{
			((Color)(ref useColor))._002Ector(Main.DiscoR, Main.DiscoG, Main.DiscoB);
		}
		return useColor;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SetCrit();
		float critDamage = Math.Min(Main.player[base.Projectile.owner].GetTotalCritChance(base.Projectile.DamageType) * 0.01f, 1f);
		modifiers.SourceDamage *= 1f + critDamage;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		if (time < 2f)
		{
			return false;
		}
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float drawRotation = base.Projectile.rotation;
		Asset<Texture2D> tex = ModContent.Request<Texture2D>("CalamityMod/Particles/DrainLineBloom2", (AssetRequestMode)2);
		Asset<Texture2D> tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/DrainLine2", (AssetRequestMode)2);
		Projectile projectile = base.Projectile;
		int mode = ProjectileID.Sets.TrailingMode[base.Type];
		Color val = baseColor;
		((Color)(ref val)).A = 0;
		CalamityUtils.DrawAfterimagesCentered(projectile, mode, val * 0.8f, 1, tex.Value);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], Color.Black, 1, tex2.Value, drawCentered: true, shrink: true);
		Asset<Texture2D> tex3 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/OntologicalDespoilerGrenade", (AssetRequestMode)2);
		Rectangle frame = tex3.Frame(1, 6, 0, base.Projectile.frame);
		Vector2 rotationPoint = frame.Size() * 0.5f;
		for (int i = 0; i < 4; i++)
		{
			Texture2D value = tex3.Value;
			Vector2 position = drawPosition + Main.rand.NextVector2Circular(7f, 7f);
			Rectangle? sourceRectangle = frame;
			val = baseColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value, position, sourceRectangle, val, drawRotation, rotationPoint, 1f, (SpriteEffects)0);
		}
		Main.EntitySpriteDraw(tex3.Value, drawPosition, frame, baseColor, drawRotation, rotationPoint, 1f, (SpriteEffects)0);
		return false;
	}

	public OntologicalDespoilerGrenade()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		explode = true;
		baseColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
		base._002Ector();
	}
}
