using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class TornadoHostile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public override string Texture => "CalamityMod/Projectiles/TornadoProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 1200;
	}

	public override void AI()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		float projTimer = ((base.Projectile.ai[1] == 1f) ? 900f : 600f);
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = -1;
			SoundEngine.PlaySound(in SoundID.Item122, base.Projectile.Center);
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= projTimer)
		{
			base.Projectile.Kill();
		}
		float expandSizeX = ((base.Projectile.ai[1] == 1f) ? 90f : 15f);
		float expandSizeY = ((base.Projectile.ai[1] == 1f) ? 90f : 15f);
		Point projCenterTile = base.Projectile.Center.ToTileCoordinates();
		Collision.ExpandVertically(projCenterTile.X, projCenterTile.Y, out var sizeMod, out var sizeMod2, (int)expandSizeX, (int)expandSizeY);
		sizeMod++;
		sizeMod2--;
		Vector2 sizeModVector = new Vector2((float)projCenterTile.X, (float)sizeMod) * 16f + new Vector2(8f);
		Vector2 sizeModVector2 = new Vector2((float)projCenterTile.X, (float)sizeMod2) * 16f + new Vector2(8f);
		Vector2 centering = Vector2.Lerp(sizeModVector, sizeModVector2, 0.5f);
		Vector2 sizeModPos = default(Vector2);
		((Vector2)(ref sizeModPos))._002Ector(0f, sizeModVector2.Y - sizeModVector.Y);
		sizeModPos.X = sizeModPos.Y * 0.2f;
		base.Projectile.width = (int)(sizeModPos.X * ((base.Projectile.ai[1] == 1f) ? 0.167f : 0.65f));
		base.Projectile.height = (int)sizeModPos.Y;
		base.Projectile.Center = centering;
		if (base.Projectile.owner == Main.myPlayer)
		{
			bool breakFlag = false;
			Vector2 playerCenter = Main.player[base.Projectile.owner].Center;
			Vector2 top = Main.player[base.Projectile.owner].Top;
			for (float i = 0f; i < 1f; i += 0.05f)
			{
				Vector2 position2 = Vector2.Lerp(sizeModVector, sizeModVector2, i);
				if (Collision.CanHitLine(position2, 0, 0, playerCenter, 0, 0) || Collision.CanHitLine(position2, 0, 0, top, 0, 0))
				{
					breakFlag = true;
					break;
				}
			}
			if (!breakFlag && base.Projectile.ai[0] < projTimer - 120f)
			{
				float aiDecrement = base.Projectile.ai[0] % 60f;
				base.Projectile.ai[0] = projTimer - 120f + aiDecrement;
				base.Projectile.netUpdate = true;
			}
		}
		if (!(base.Projectile.ai[0] < projTimer - 120f))
		{
			return;
		}
		Vector2 dustVelocity = default(Vector2);
		Vector2 dustCustomData = default(Vector2);
		for (int j = 0; j < 1; j++)
		{
			float lerpRandomizer = Main.rand.NextFloat();
			((Vector2)(ref dustVelocity))._002Ector(MathHelper.Lerp(0.1f, 1f, Main.rand.NextFloat()), MathHelper.Lerp(-0.5f, 0.9f, lerpRandomizer));
			dustVelocity.X *= MathHelper.Lerp(2.2f, 0.6f, lerpRandomizer);
			dustVelocity.X *= -1f;
			((Vector2)(ref dustCustomData))._002Ector(6f, 10f);
			Vector2 dustPosition = centering + sizeModPos * dustVelocity * 0.5f + dustCustomData;
			Dust cloudDust = Main.dust[Dust.NewDust(dustPosition, 0, 0, 16, 0f, 0f, 0, default(Color), 1.5f)];
			cloudDust.position = dustPosition;
			cloudDust.customData = centering + dustCustomData;
			cloudDust.fadeIn = 1f;
			cloudDust.scale = 0.3f;
			if (dustVelocity.X > -1.2f)
			{
				cloudDust.velocity.X = 1f + Main.rand.NextFloat();
			}
			cloudDust.velocity.Y = Main.rand.NextFloat() * -0.5f - 1f;
		}
	}

	public override bool CanHitPlayer(Player target)
	{
		if (base.Projectile.ai[0] >= 60f)
		{
			return base.Projectile.ai[0] <= ((base.Projectile.ai[1] == 1f) ? 840f : 540f);
		}
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		float aiTrackCheck = ((base.Projectile.ai[1] == 1f) ? 900f : 600f);
		float expandSizeX2 = ((base.Projectile.ai[1] == 1f) ? 90f : 15f);
		float expandSizeY2 = ((base.Projectile.ai[1] == 1f) ? 90f : 15f);
		float aiTracker = base.Projectile.ai[0];
		float trackerClamp = MathHelper.Clamp(aiTracker / 30f, 0f, 1f);
		if (aiTracker > aiTrackCheck - 60f)
		{
			trackerClamp = MathHelper.Lerp(1f, 0f, (aiTracker - (aiTrackCheck - 60f)) / 60f);
		}
		Point centerPoint = base.Projectile.Center.ToTileCoordinates();
		Collision.ExpandVertically(centerPoint.X, centerPoint.Y, out var sizeModding, out var sizeModding2, (int)expandSizeX2, (int)expandSizeY2);
		sizeModding++;
		sizeModding2--;
		float vectorMult = 0.2f;
		Vector2 sizeModdingVector = new Vector2((float)centerPoint.X, (float)sizeModding) * 16f + new Vector2(8f);
		Vector2 sizeModdingVector2 = new Vector2((float)centerPoint.X, (float)sizeModding2) * 16f + new Vector2(8f);
		Vector2.Lerp(sizeModdingVector, sizeModdingVector2, 0.5f);
		Vector2 sizeModdingPos = default(Vector2);
		((Vector2)(ref sizeModdingPos))._002Ector(0f, sizeModdingVector2.Y - sizeModdingVector.Y);
		sizeModdingPos.X = sizeModdingPos.Y * vectorMult;
		new Vector2(sizeModdingVector.X - sizeModdingPos.X / 2f, sizeModdingVector.Y);
		Texture2D texture2D23 = TextureAssets.Projectile[base.Type].Value;
		Rectangle drawRectangle = texture2D23.Frame();
		Vector2 smallRect = drawRectangle.Size() / 2f;
		float aiTrackMult = -(float)Math.PI / 50f * aiTracker;
		Vector2 spinningpoint2 = Vector2.UnitY.RotatedBy(aiTracker * 0.1f);
		float incrementStorage = 0f;
		float increment = 5.1f;
		Color cloudColor = default(Color);
		((Color)(ref cloudColor))._002Ector(225, 225, 225);
		Vector2 colorChangeVector = default(Vector2);
		for (float k = (int)sizeModdingVector2.Y; k > (float)(int)sizeModdingVector.Y; k -= increment)
		{
			incrementStorage += increment;
			float colorChanger = incrementStorage / sizeModdingPos.Y;
			float incStorageMult = incrementStorage * ((float)Math.PI * 2f) / -20f;
			float lowerColorChanger = colorChanger - 0.15f;
			Vector2 spinArea = spinningpoint2.RotatedBy(incStorageMult);
			((Vector2)(ref colorChangeVector))._002Ector(0f, colorChanger + 1f);
			colorChangeVector.X = colorChangeVector.Y * vectorMult;
			Color newCloudColor = Color.Lerp(Color.Transparent, cloudColor, colorChanger * 2f);
			if (colorChanger > 0.5f)
			{
				newCloudColor = Color.Lerp(Color.Transparent, cloudColor, 2f - colorChanger * 2f);
			}
			((Color)(ref newCloudColor)).A = (byte)((float)(int)((Color)(ref newCloudColor)).A * 0.5f);
			newCloudColor *= trackerClamp;
			spinArea *= colorChangeVector * 100f;
			spinArea.Y = 0f;
			spinArea.X = 0f;
			spinArea += new Vector2(sizeModdingVector2.X, k) - Main.screenPosition;
			Main.spriteBatch.Draw(texture2D23, spinArea, (Rectangle?)drawRectangle, newCloudColor, aiTrackMult + incStorageMult, smallRect, 1f + lowerColorChanger, (SpriteEffects)0, 0f);
		}
		return false;
	}
}
