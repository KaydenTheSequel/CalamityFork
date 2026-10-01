using System;
using CalamityMod.NPCs.AstrumDeus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class DeusRitualDrama : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle PulseSound;

	public const int TotalSinePeriods = 6;

	public const int PulseTime = 45;

	public const int TotalRitualTime = 420;

	public const float MaxUpwardRise = 540f;

	public static readonly Point PulseSize;

	public new string LocalizationCategory => "Projectiles.Boss";

	public float Time
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public bool CreatedWithStarcore => base.Projectile.ai[1] == 1f;

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 2);
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 420;
	}

	public override void AI()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.extraUpdates = CreatedWithStarcore.ToInt();
		Time++;
		if (Time == 375f && Main.netMode != 1)
		{
			int idx = NPC.NewNPC(base.Projectile.GetSource_FromThis(), (int)base.Projectile.Center.X, (int)base.Projectile.Center.Y - 540, ModContent.NPCType<AstrumDeusHead>(), 1);
			if (idx != -1)
			{
				CalamityUtils.BossAwakenMessage(idx);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		float upwardnessRatio = Utils.GetLerpValue(60f, 420f, Time, clamped: true);
		float upwardness = MathHelper.Lerp(0f, 540f, upwardnessRatio);
		if (Time >= 375f)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, (Effect)null, Main.GameViewMatrix.ZoomMatrix);
			float pulseCompletionRatio = Utils.GetLerpValue(375f, 420f, Time, clamped: true);
			Vector2 scale = base.Projectile.scale * (3f + pulseCompletionRatio * 5f) * new Vector2(1.5f, 1f);
			DrawData drawData = new DrawData(ModContent.Request<Texture2D>("Terraria/Images/Misc/Perlin", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition + PulseSize.ToVector2() * scale * 0.5f - Vector2.UnitY * upwardness, (Rectangle?)new Rectangle(0, 0, PulseSize.X, PulseSize.Y), new Color(new Vector4(1f - (float)Math.Sqrt(pulseCompletionRatio))) * 0.66f, base.Projectile.rotation, PulseSize.ToVector2(), scale, (SpriteEffects)0, 0f);
			Color pulseColor = Color.Lerp(Color.Cyan * 1.5f, Color.OrangeRed, MathHelper.Clamp(pulseCompletionRatio * 1.5f, 0f, 1f));
			GameShaders.Misc["ForceField"].UseColor(pulseColor);
			GameShaders.Misc["ForceField"].Apply(drawData);
			drawData.Draw(Main.spriteBatch);
			return false;
		}
		float outwardnessRatio = Utils.GetLerpValue(60f, 220f, Time, clamped: true);
		if (Time > 250f)
		{
			outwardnessRatio = 1f - Utils.GetLerpValue(250f, 375f, Time, clamped: true);
		}
		float outwardness = MathHelper.Lerp(0f, 140f, outwardnessRatio);
		Vector2 offset = default(Vector2);
		((Vector2)(ref offset))._002Ector((float)Math.Sin(Time / 375f * ((float)Math.PI * 2f) * 6f) * outwardness, 0f - upwardness);
		if (!Main.dedServ && Math.Abs(offset.X) < 6f && Time > 60f)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Vector2.UnitY * offset.Y, 261);
				dust.color = Utils.SelectRandom(Main.rand, (Color[])(object)new Color[2]
				{
					Color.Cyan,
					Color.OrangeRed
				});
				dust.scale = 1.15f;
				dust.velocity = Main.rand.NextVector2CircularEdge(3f, 3f) * Main.rand.NextFloat(0.7f, 1.4f);
				dust.noGravity = true;
				float angle = (float)Math.PI * 2f * (float)i / 20f;
				dust = Dust.NewDustPerfect(base.Projectile.Center + Vector2.UnitY * offset.Y, 261);
				dust.color = Utils.SelectRandom(Main.rand, (Color[])(object)new Color[2]
				{
					Color.Cyan,
					Color.OrangeRed
				});
				dust.scale = 1.15f;
				dust.velocity = angle.ToRotationVector2() * 7f;
				dust.noGravity = true;
			}
			SoundEngine.PlaySound(in PulseSound, base.Projectile.Center);
		}
		DrawStars(Main.spriteBatch, offset);
		return false;
	}

	public void DrawStars(SpriteBatch spriteBatch, Vector2 offset)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		Texture2D starTexture = TextureAssets.Projectile[base.Type].Value;
		for (int i = 0; i < 6; i++)
		{
			Vector2 angularOffset = ((float)Math.PI * 2f * (float)i / 6f + Time / 15f).ToRotationVector2() * 4f;
			Main.EntitySpriteDraw(starTexture, base.Projectile.Center + angularOffset + offset - Main.screenPosition, null, Color.Cyan * 0.5f, 0f, starTexture.Size() * 0.5f, 0.6f, (SpriteEffects)0);
			Main.EntitySpriteDraw(starTexture, base.Projectile.Center + angularOffset + offset * new Vector2(-1f, 1f) - Main.screenPosition, null, Color.OrangeRed * 0.5f, 0f, starTexture.Size() * 0.5f, 0.6f, (SpriteEffects)0);
		}
		Main.EntitySpriteDraw(starTexture, base.Projectile.Center + offset - Main.screenPosition, null, Color.Cyan * 1.4f, 0f, starTexture.Size() * 0.5f, 0.6f, (SpriteEffects)0);
		Main.EntitySpriteDraw(starTexture, base.Projectile.Center + offset * new Vector2(-1f, 1f) - Main.screenPosition, null, Color.OrangeRed * 1.1f, 0f, starTexture.Size() * 0.5f, 0.6f, (SpriteEffects)0);
		if (!Main.dedServ)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset, 261);
			dust.color = Color.Cyan;
			dust.scale = 1.15f;
			dust.velocity = Vector2.Zero;
			dust.noGravity = true;
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + offset * new Vector2(-1f, 1f), 261);
			dust2.color = Color.OrangeRed;
			dust2.scale = 1.15f;
			dust2.velocity = Vector2.Zero;
			dust2.noGravity = true;
		}
	}

	static DeusRitualDrama()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		PulseSound = new SoundStyle("CalamityMod/Sounds/Custom/AstralBeaconOrbPulse");
		PulseSize = new Point(300, 300);
	}
}
