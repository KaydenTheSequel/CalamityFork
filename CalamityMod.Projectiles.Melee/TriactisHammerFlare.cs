using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TriactisHammerFlare : ModProjectile, ILocalizedModType, IModType
{
	public static Asset<Texture2D> Sparkle;

	public static Asset<Texture2D> Bloom;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/ExtraTextures/TinyGreyscaleCircle";

	public ref float FlareType => ref base.Projectile.ai[0];

	public ref float Target => ref base.Projectile.ai[1];

	public ref float OrbitRadius => ref base.Projectile.ai[2];

	public Player Owner => Main.player[base.Projectile.owner];

	public override void Load()
	{
		Sparkle = ModContent.Request<Texture2D>("CalamityMod/Particles/Sparkle", (AssetRequestMode)2);
		Bloom = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 16;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Melee;
	}

	public override void AI()
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.dead || !Owner.active)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.timeLeft = 2;
		float rotation = Main.GlobalTimeWrappedHourly * 2f + MathHelper.ToRadians(120f) * FlareType;
		if (Target == -2f)
		{
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<TriactisHammerProj>(), base.Projectile.damage, 0f, base.Projectile.owner, 0f, FlareType, OrbitRadius).scale = 0f;
			base.Projectile.Kill();
			return;
		}
		if (!Main.npc.IndexInRange((int)Target))
		{
			base.Projectile.Center = Owner.MountedCenter + Vector2.UnitX.RotatedBy(rotation) * 96f;
		}
		else
		{
			NPC enemy = Main.npc[(int)Target];
			if (enemy == null || enemy.life <= 0 || !enemy.active || enemy.dontTakeDamage || enemy.immortal)
			{
				Target = -1f;
				base.Projectile.netUpdate = true;
				for (int i = 0; i < 5; i++)
				{
					Vector2 velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(6f, 10f);
					GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, velocity, Color.White, GetColor(FlareType), 1f, 24, 0.1f, 2.4f));
				}
				return;
			}
			OrbitRadius = MathHelper.Clamp(MathF.Max(enemy.width, enemy.height) + 64f, 64f, 400f);
			base.Projectile.Center = enemy.Center + Vector2.UnitX.RotatedBy(rotation) * OrbitRadius;
		}
		if (Main.rand.NextBool(3))
		{
			Dust dust = Dust.QuickDust(base.Projectile.Center, GetColor(FlareType));
			dust.position += Main.rand.NextVector2Unit() * Main.rand.NextFloat(0f, 8f);
		}
	}

	public static Color GetColor(float type)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		if (type == 3f)
		{
			Vector3 blue = Main.rgbToHsl(new Color(117, 170, 239));
			return Main.hslToRgb(blue.X + 0.05f * MathF.Sin(Main.GlobalTimeWrappedHourly * 5f), blue.Y, blue.Z);
		}
		if (type == 2f)
		{
			Vector3 green = Main.rgbToHsl(new Color(132, 225, 26));
			return Main.hslToRgb(green.X + 0.05f * MathF.Sin(Main.GlobalTimeWrappedHourly * 5f), green.Y, green.Z);
		}
		Vector3 red = Main.rgbToHsl(Color.Red);
		return Main.hslToRgb(red.X + 0.05f * MathF.Sin(Main.GlobalTimeWrappedHourly * 5f), red.Y, red.Z);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return GetColor(FlareType);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
		Texture2D sparkleTex = Sparkle.Value;
		Texture2D bloomTex = Bloom.Value;
		float bloomScale = (float)sparkleTex.Height / (float)bloomTex.Height;
		float sparkleScale = 0.7f + CalamityUtils.Convert01To010(Main.GlobalTimeWrappedHourly % 2f / 2f) * 0.2f;
		Color color = base.Projectile.GetAlpha(lightColor);
		float rotation = Main.GlobalTimeWrappedHourly * 8f;
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(bloomTex, drawPos, null, color * 0.5f, 0f, bloomTex.Size() * 0.5f, 5f * bloomScale, (SpriteEffects)0);
		Main.EntitySpriteDraw(sparkleTex, drawPos, null, Color.Lerp(color, Color.White, 0.7f), rotation, sparkleTex.Size() * 0.5f, 2.2f * sparkleScale, (SpriteEffects)0);
		Main.EntitySpriteDraw(sparkleTex, drawPos, null, color, rotation + (float)Math.PI / 4f, sparkleTex.Size() * 0.5f, 1.6f * sparkleScale, (SpriteEffects)0);
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		for (int i = 0; i < base.Projectile.oldPos.Length; i++)
		{
			float completionRatio = (float)i / (float)base.Projectile.oldPos.Length;
			Vector2 trailPos = base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f - Main.screenPosition;
			Color trailColor = Color.Lerp(color, Color.Black, completionRatio);
			float trailScale = MathHelper.Lerp(0.15f, 1f, 1f - completionRatio);
			Main.EntitySpriteDraw(texture, trailPos, null, trailColor, 0f, texture.Size() * 0.5f, base.Projectile.scale * trailScale, (SpriteEffects)0);
		}
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
