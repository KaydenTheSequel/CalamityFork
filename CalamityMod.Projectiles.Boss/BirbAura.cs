using System;
using System.IO;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class BirbAura : ModProjectile, ILocalizedModType, IModType
{
	private float timer = 135f;

	private float timeBeforeVanish;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.hostile = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 1200;
		base.Projectile.Calamity().DealsDefenseDamage = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
		writer.Write(timer);
		writer.Write(timeBeforeVanish);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
		timer = reader.ReadSingle();
		timeBeforeVanish = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		Vector2? vector78 = null;
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		Vector2 fireFrom = default(Vector2);
		((Vector2)(ref fireFrom))._002Ector(base.Projectile.ai[0], base.Projectile.ai[1]);
		base.Projectile.position = fireFrom - new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f;
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		if (timer > 0f)
		{
			timer--;
		}
		float projScale = 1f;
		if (timeBeforeVanish == 0f)
		{
			timeBeforeVanish = ((base.Projectile.timeLeft <= 900) ? 900f : 1200f);
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] >= timeBeforeVanish)
		{
			base.Projectile.Kill();
			return;
		}
		if (base.Projectile.localAI[0] % 4f == 0f)
		{
			base.Projectile.frameCounter++;
		}
		base.Projectile.scale = (float)Math.Sin((double)base.Projectile.localAI[0] * Math.PI / (double)timeBeforeVanish) * 10f * projScale;
		if (base.Projectile.scale > projScale)
		{
			base.Projectile.scale = projScale;
		}
		float projVelRotation = base.Projectile.velocity.ToRotation();
		base.Projectile.rotation = projVelRotation - (float)Math.PI / 2f;
		base.Projectile.velocity = projVelRotation.ToRotationVector2();
		float projWidth = base.Projectile.width;
		Vector2 samplingPoint = base.Projectile.Center;
		if (vector78.HasValue)
		{
			samplingPoint = vector78.Value;
		}
		float laserLength = base.Projectile.ai[1] - 160f;
		float[] array3 = new float[3];
		Collision.LaserScan(samplingPoint, base.Projectile.velocity, projWidth * base.Projectile.scale, laserLength, array3);
		float auraLength = 0f;
		for (int j = 0; j < array3.Length; j++)
		{
			auraLength += array3[j];
		}
		auraLength /= 3f;
		auraLength = MathHelper.Clamp(auraLength, 3600f, 4800f);
		base.Projectile.localAI[1] = MathHelper.Lerp(base.Projectile.localAI[1], auraLength, 0.5f);
		DelegateMethods.v3_1 = new Vector3(0.9f, 0.3f, 0.3f);
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CastLight);
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		if (timer <= 0f && (base.Projectile.localAI[0] >= 120f || base.Projectile.timeLeft <= 900) && base.Projectile.owner == Main.myPlayer)
		{
			timer = 15f;
			Vector2 fireFrom = default(Vector2);
			((Vector2)(ref fireFrom))._002Ector(target.Center.X, target.Center.Y - 900f);
			SoundEngine.PlaySound(in CommonCalamitySounds.LightningSound, target.Center - Vector2.UnitY * 300f);
			Vector2 ai0 = target.Center - fireFrom;
			float ai1 = Main.rand.Next(100);
			Vector2 velocity = Vector2.Normalize(ai0.RotatedByRandom(0.7853981852531433)) * 7f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), fireFrom.X, fireFrom.Y, velocity.X, velocity.Y, ModContent.ProjectileType<RedLightning>(), base.Projectile.damage, 0f, base.Projectile.owner, ai0.ToRotation(), ai1);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		Texture2D middleTex = TextureAssets.Projectile[base.Type].Value;
		Texture2D endpointTex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/BirbAuraEndpoints", (AssetRequestMode)1).Value;
		int middleFrame = base.Projectile.frameCounter % 12;
		int endpointFrame = base.Projectile.frameCounter % 3;
		float auraDrawLength = base.Projectile.localAI[1];
		Color grayColor = default(Color);
		((Color)(ref grayColor))._002Ector(128, 128, 128, 0);
		Rectangle? sourceRectangle2 = endpointTex.Frame(1, 3, 0, endpointFrame);
		Vector2 endPosition = base.Projectile.Center - Main.screenPosition - new Vector2(0f, 22f);
		Main.EntitySpriteDraw(endpointTex, endPosition, sourceRectangle2, grayColor, base.Projectile.rotation, endpointTex.Frame().Top(), base.Projectile.scale, (SpriteEffects)0);
		auraDrawLength -= (float)(endpointTex.Height / 2 + endpointTex.Height) * base.Projectile.scale;
		Vector2 projCenter = base.Projectile.Center;
		projCenter += base.Projectile.velocity * base.Projectile.scale * (float)endpointTex.Height / 2f;
		if (auraDrawLength > 0f)
		{
			float auraSegment = 0f;
			Rectangle drawRectangle = middleTex.Frame(12, 1, middleFrame);
			while (auraSegment + 1f < auraDrawLength)
			{
				if (auraDrawLength - auraSegment < (float)drawRectangle.Height)
				{
					drawRectangle.Height = (int)(auraDrawLength - auraSegment);
				}
				Main.EntitySpriteDraw(middleTex, projCenter - Main.screenPosition, drawRectangle, grayColor, base.Projectile.rotation, new Vector2((float)(drawRectangle.Width / 2), 0f), base.Projectile.scale, (SpriteEffects)0);
				auraSegment += (float)drawRectangle.Height * base.Projectile.scale;
				projCenter += base.Projectile.velocity * (float)drawRectangle.Height * base.Projectile.scale;
				drawRectangle.Y += middleTex.Height;
				if (drawRectangle.Y + drawRectangle.Height > middleTex.Height)
				{
					drawRectangle.Y = 0;
				}
			}
		}
		return false;
	}

	public override void CutTiles()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Vector2 unit = base.Projectile.velocity;
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + unit * base.Projectile.localAI[1], (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CutTiles);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		if (timer > 15f)
		{
			return false;
		}
		if (((Rectangle)(ref projHitbox)).Intersects(targetHitbox))
		{
			return true;
		}
		float useless = 0f;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], 80f * base.Projectile.scale, ref useless))
		{
			return true;
		}
		return false;
	}
}
