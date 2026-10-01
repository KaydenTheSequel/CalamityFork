using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class GoldiePet : ModProjectile, ILocalizedModType, IModType
{
	public float MaxBloomTime = 30f;

	public float MaxSparkleTime = 150f;

	public static Color GoldColor;

	public new string LocalizationCategory => "Projectiles.Pets";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float RotationTimer => ref base.Projectile.ai[0];

	public ref float SparkleTimer => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.LightPet[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 16;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.friendly = true;
		base.Projectile.netImportant = true;
	}

	public override void AI()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		if (OwnerCheck())
		{
			return;
		}
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 5 % Main.projFrames[base.Type];
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref GoldColor)).ToVector3() * (0.9f + 0.45f * SparkleTimer / MaxSparkleTime));
		if (SparkleTimer < MaxBloomTime)
		{
			SparkleTimer++;
		}
		else if (SparkleTimer > MaxBloomTime)
		{
			SparkleTimer--;
		}
		if (Main.rand.NextBool(6))
		{
			Dust.QuickDust(base.Projectile.Center, GoldColor);
		}
		Vector2 targetLocation = base.Projectile.position;
		bool foundCoin = false;
		float range = 800f;
		float magnetRange = 128f;
		for (int itemIndex = 0; itemIndex < Main.maxItems; itemIndex++)
		{
			Item item = Main.item[itemIndex];
			if (!item.active || !ItemID.Sets.CommonCoin[item.type] || item.noGrabDelay != 0 || item.playerIndexTheItemIsReservedFor != base.Projectile.owner || !ItemLoader.CanPickup(item, Main.player[item.playerIndexTheItemIsReservedFor]) || !Main.player[item.playerIndexTheItemIsReservedFor].ItemSpace(item).CanTakeItemToPersonalInventory)
			{
				continue;
			}
			float itemDist = Vector2.Distance(item.Center, base.Projectile.Center);
			float distanceToPotential = Vector2.Distance(base.Projectile.Center, targetLocation);
			if (itemDist > range)
			{
				continue;
			}
			if (itemDist <= magnetRange)
			{
				item.velocity = item.SafeDirectionTo(base.Projectile.Center) * 6f;
			}
			if (base.Projectile.owner == Main.myPlayer)
			{
				Rectangle rect = base.Projectile.getRect();
				if (((Rectangle)(ref rect)).Intersects(new Rectangle((int)item.position.X, (int)item.position.Y, item.width, item.height)))
				{
					Main.item[itemIndex] = Owner.GetItem(base.Projectile.owner, item, default(GetItemSettings));
					if (Main.netMode == 1)
					{
						NetMessage.SendData(21, -1, -1, null, itemIndex);
					}
					SparkleTimer = MaxSparkleTime;
				}
			}
			if (distanceToPotential < itemDist)
			{
				range = itemDist;
				targetLocation = item.Center;
				foundCoin = true;
			}
		}
		if (foundCoin)
		{
			Vector2 destination = base.Projectile.SafeDirectionTo(targetLocation);
			destination *= 12f;
			base.Projectile.velocity = (base.Projectile.velocity * 40f + destination) / 41f;
			base.Projectile.rotation = base.Projectile.velocity.X * 0.05f;
			RotationTimer = -1f;
			return;
		}
		if (RotationTimer == -1f)
		{
			Vector2 restingSpot = Owner.Center + Vector2.UnitY * -80f;
			base.Projectile.velocity = (restingSpot - base.Projectile.Center) / 15f;
			base.Projectile.rotation = base.Projectile.velocity.X * 0.05f;
			if (Vector2.Distance(restingSpot, base.Projectile.Center) <= 2f)
			{
				RotationTimer++;
			}
			return;
		}
		Vector2 playerDist = Owner.Center - base.Projectile.Center;
		base.Projectile.rotation = playerDist.ToRotation() - (float)Math.PI / 2f;
		base.Projectile.Center = Owner.Center + Vector2.UnitY.RotatedBy(RotationTimer) * -80f;
		RotationTimer += MathHelper.ToRadians(4f);
		if (Main.rand.NextBool(150) && SparkleTimer <= MaxBloomTime)
		{
			SparkleTimer = MaxSparkleTime * 0.6f;
		}
	}

	public bool OwnerCheck()
	{
		if (!Owner.active)
		{
			base.Projectile.Kill();
			return true;
		}
		if (Owner.dead)
		{
			Owner.Calamity().thiefsDime = false;
		}
		if (Owner.Calamity().thiefsDime)
		{
			base.Projectile.timeLeft = 2;
		}
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D shineTex = ModContent.Request<Texture2D>("CalamityMod/Particles/Sparkle", (AssetRequestMode)2).Value;
		float shinePercent = Utils.SmoothStep(MaxBloomTime, MaxSparkleTime, SparkleTimer);
		float shineScale = (float)Math.Log10((double)shinePercent + 0.01) + 2f;
		Texture2D bloomTex = ModContent.Request<Texture2D>("CalamityMod/Particles/Light", (AssetRequestMode)2).Value;
		float bloomPercent = Math.Clamp(SparkleTimer / MaxBloomTime, 0f, 5f);
		Math.Clamp(bloomPercent, 0f, 2.5f);
		if (bloomPercent > 0f)
		{
			Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
			Main.EntitySpriteDraw(bloomTex, base.Projectile.Center - Main.screenPosition, null, GoldColor * bloomPercent * 0.2f, base.Projectile.rotation, bloomTex.Size() / 2f, bloomPercent * base.Projectile.scale * 0.3f, (SpriteEffects)0);
			Main.EntitySpriteDraw(shineTex, base.Projectile.Center - Main.screenPosition, null, GoldColor * shinePercent, base.Projectile.rotation, shineTex.Size() / 2f, shineScale * base.Projectile.scale, (SpriteEffects)0);
			Main.spriteBatch.ExitShaderRegion();
		}
		return true;
	}

	static GoldiePet()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		GoldColor = Color.Goldenrod;
	}
}
