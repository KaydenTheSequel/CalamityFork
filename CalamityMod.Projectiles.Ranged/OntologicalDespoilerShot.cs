using System;
using System.Collections.Generic;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class OntologicalDespoilerShot : ModProjectile, ILocalizedModType, IModType
{
	public Color baseColor;

	public int sineDir;

	public Color color1;

	public Color color2;

	public Color color3;

	public Color color4;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Ranged/OntologicalDespoilerShot";

	public ref float time => ref base.Projectile.ai[0];

	public Player Owner => Main.player[base.Projectile.owner];

	public bool Positive => base.Projectile.ai[2] < 5f;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 25;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 750;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.extraUpdates = 4;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
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
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_0632: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		color1 = Owner.shirtColor;
		color2 = Color.Lerp(Owner.shirtColor, Color.Black, 0.2f);
		color3 = Color.Lerp(Owner.shirtColor, Color.White, 0.1f);
		color4 = Color.Lerp(Owner.shirtColor, Color.White, 0.2f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 10)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 5)
		{
			base.Projectile.frame = 0;
		}
		if (time == 0f)
		{
			base.Projectile.scale = 1f;
			sineDir = (Main.rand.NextBool() ? 1 : (-1));
			base.Projectile.frame = Main.rand.Next(0, 7);
			if (!Positive)
			{
				base.Projectile.penetrate = 1;
			}
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
		if (!Positive)
		{
			baseColor = Color.White;
			base.Projectile.extraUpdates = 3;
		}
		else
		{
			base.Projectile.extraUpdates = 5;
		}
		if (time > 20f)
		{
			if (!Positive)
			{
				float sine = (float)Math.Sin((float)base.Projectile.timeLeft * 0.575f / (float)Math.PI);
				Vector2 offset = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * 13f;
				float scale = Main.rand.NextFloat(0.45f, 0.55f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset * (float)sineDir, (time % 2f == 0f) ? ModContent.DustType<VoidDustInverted>() : ModContent.DustType<VoidDust>(), -base.Projectile.velocity * Main.rand.NextFloat(0.3f, 0.8f));
				dust.noGravity = true;
				dust.scale = scale;
				dust.color = baseColor;
			}
			else
			{
				if (Main.rand.NextBool(21))
				{
					Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(10f, 10f), ModContent.DustType<LightDust>(), -base.Projectile.velocity * Main.rand.NextFloat(0.3f, 1.8f));
					dust2.noGravity = true;
					dust2.scale = Main.rand.NextFloat(0.8f, 1.15f);
					dust2.color = baseColor;
				}
				if ((time < 15f || time % 25f <= 15f) && base.Projectile.ai[2] != 1f)
				{
					base.Projectile.velocity = base.Projectile.velocity.RotatedBy(1f / 30f * (float)((base.Projectile.ai[2] == 2f) ? 1 : (-1)));
					if (time % 25f == 0f || time == 15f)
					{
						base.Projectile.ai[2] = ((base.Projectile.ai[2] != 2f) ? 2 : 0);
					}
				}
			}
		}
		if (!Positive)
		{
			NPC targetedNPC = base.Projectile.Center.ClosestNPCAt(700f);
			if (targetedNPC != null && time > 30f && base.Projectile.numHits < 1 && Vector2.Distance(targetedNPC.Center, base.Projectile.Center) < 700f)
			{
				float moveSpeed = 0.42f + Utils.GetLerpValue(650f, 450f, base.Projectile.timeLeft, clamped: true);
				CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targetedNPC, ignoreTiles: true, moveSpeed, 8f, 0.95f, 0.95f, accelerate: true);
			}
		}
		else
		{
			if (base.Projectile.timeLeft < 100)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.96f;
				base.Projectile.scale *= 0.98f;
			}
			base.Projectile.timeLeft--;
			if (base.Projectile.timeLeft <= 1)
			{
				for (int i = 0; i < 2; i++)
				{
					GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center, (base.Projectile.velocity * 5f).RotatedByRandom(0.05000000074505806) * Main.rand.NextFloat(0.1f, 1f), affectedByGravity: false, Main.rand.Next(20, 29), Main.rand.NextFloat(0.6f, 1.3f), baseColor));
				}
			}
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		if (!Positive)
		{
			for (int i = 0; i < 3; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, (!Positive) ? (Main.rand.NextBool() ? ModContent.DustType<VoidDustInverted>() : ModContent.DustType<VoidDust>()) : ModContent.DustType<LightDust>(), (base.Projectile.velocity * 3f).RotatedByRandom(0.699999988079071) * Main.rand.NextFloat(0.2f, 1f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.15f, 1.45f);
				dust.color = baseColor;
			}
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.15f, 0.65f, 8, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Black, "CalamityMod/Particles/SmallBloomRingLayered", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.2f, 0.75f, 8, UseAdditiveBlend: false, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Black, "CalamityMod/Particles/SmallBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.4f, 0.05f, 9, UseAdditiveBlend: false, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		else
		{
			for (int j = 0; j < MathHelper.Clamp((int)(3f - (float)base.Projectile.numHits * 0.3f), 1, 3); j++)
			{
				GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center, (base.Projectile.velocity * 4f).RotatedByRandom(0.15000000596046448) * Main.rand.NextFloat(0.1f, 1f), affectedByGravity: false, Main.rand.Next(20, 29), Main.rand.NextFloat(0.6f, 1.3f), baseColor));
			}
		}
		SoundStyle obj = (Positive ? new SoundStyle("CalamityMod/Sounds/Item/OntologicalDespoilerSmallImpact") : OntologicalDespoiler.SmallImpact);
		SoundEngine.PlaySound(obj with
		{
			Volume = ((!Positive) ? 1f : 0.3f),
			Pitch = Main.rand.NextFloat(0.05f, 0.15f) * (float)((!Positive) ? 3 : 5),
			MaxInstances = ((!Positive) ? 1 : 6)
		}, base.Projectile.Center);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.3f;
		int hitsToMinMult = 3;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
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
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
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
		Projectile projectile2 = base.Projectile;
		int mode2 = ProjectileID.Sets.TrailingMode[base.Type];
		Color lightColor2;
		if (!Positive)
		{
			lightColor2 = Color.Black;
		}
		else
		{
			val = Color.Lerp(Color.White, baseColor, MathHelper.Clamp(Utils.GetLerpValue(50f, 175f, time, clamped: true), 0f, 0.5f));
			((Color)(ref val)).A = 0;
			lightColor2 = val;
		}
		CalamityUtils.DrawAfterimagesCentered(projectile2, mode2, lightColor2, 1, tex2.Value, drawCentered: true, shrink: true);
		Asset<Texture2D> tex3 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/OntologicalDespoilerShot", (AssetRequestMode)2);
		Asset<Texture2D> tex4 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/OntologicalDespoilerShot2", (AssetRequestMode)2);
		_ = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/BasicCircle", (AssetRequestMode)2).Value;
		Rectangle frame = tex3.Frame(1, 6, 0, base.Projectile.frame);
		Vector2 rotationPoint = frame.Size() * 0.5f;
		if (Positive)
		{
			for (int i = 0; i < 5; i++)
			{
				Texture2D texture = (Positive ? tex4.Value : tex3.Value);
				Rectangle? sourceRectangle = frame;
				Color color;
				if (!Positive)
				{
					color = baseColor;
				}
				else
				{
					val = baseColor;
					((Color)(ref val)).A = 0;
					color = val;
				}
				Main.EntitySpriteDraw(texture, drawPosition, sourceRectangle, color, drawRotation, rotationPoint, new Vector2(1f + (float)i * 0.45f, 1f - (float)i * 0.25f) * base.Projectile.scale * 0.9f, (SpriteEffects)0);
				Texture2D value = tex4.Value;
				Rectangle? sourceRectangle2 = frame;
				val = Color.Lerp(baseColor, Color.White, 0.7f);
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(value, drawPosition, sourceRectangle2, val, drawRotation, rotationPoint, new Vector2(1f + (float)i * 0.45f, 1f - (float)i * 0.25f) * base.Projectile.scale * 0.8f, (SpriteEffects)0);
			}
		}
		else
		{
			Texture2D texture2 = (Positive ? tex4.Value : tex3.Value);
			Rectangle? sourceRectangle3 = frame;
			Color color2;
			if (!Positive)
			{
				color2 = baseColor;
			}
			else
			{
				val = baseColor;
				((Color)(ref val)).A = 0;
				color2 = val;
			}
			Main.EntitySpriteDraw(texture2, drawPosition, sourceRectangle3, color2, drawRotation, rotationPoint, base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}

	public OntologicalDespoilerShot()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		baseColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
		sineDir = 1;
		base._002Ector();
	}
}
