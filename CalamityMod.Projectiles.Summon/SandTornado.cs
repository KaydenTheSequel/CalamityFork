using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SandTornado : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/TornadoProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 1200;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		float lifeSpan = 900f;
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = -1;
			SoundEngine.PlaySound(in SoundID.Item122, base.Projectile.position);
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= lifeSpan)
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.localAI[0] >= 30f)
		{
			base.Projectile.damage = 0;
			if (base.Projectile.ai[0] < lifeSpan - 120f)
			{
				float aiDecrement = base.Projectile.ai[0] % 60f;
				base.Projectile.ai[0] = lifeSpan - 120f + aiDecrement;
				base.Projectile.netUpdate = true;
			}
		}
		Point point8 = base.Projectile.Center.ToTileCoordinates();
		Collision.ExpandVertically(point8.X, point8.Y, out var sizeMod, out var sizeMod2, 15, 15);
		sizeMod++;
		sizeMod2--;
		Vector2 sizeModVector = new Vector2((float)point8.X, (float)sizeMod) * 16f + new Vector2(8f);
		Vector2 sizeModVector2 = new Vector2((float)point8.X, (float)sizeMod2) * 16f + new Vector2(8f);
		Vector2 centering = Vector2.Lerp(sizeModVector, sizeModVector2, 0.5f);
		Vector2 sizeModPos = default(Vector2);
		((Vector2)(ref sizeModPos))._002Ector(0f, sizeModVector2.Y - sizeModVector.Y);
		sizeModPos.X = sizeModPos.Y * 0.2f;
		base.Projectile.width = (int)(sizeModPos.X * 0.65f);
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
			if (!breakFlag && base.Projectile.ai[0] < lifeSpan - 120f)
			{
				float aiDecrement2 = base.Projectile.ai[0] % 60f;
				base.Projectile.ai[0] = lifeSpan - 120f + aiDecrement2;
				base.Projectile.netUpdate = true;
			}
		}
		_ = base.Projectile.ai[0];
		_ = lifeSpan - 120f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		float aiTracker = base.Projectile.ai[0];
		float trackerClamp = MathHelper.Clamp(aiTracker / 30f, 0f, 1f);
		if (aiTracker > 540f)
		{
			trackerClamp = MathHelper.Lerp(1f, 0f, (aiTracker - 540f) / 60f);
		}
		Point centerPoint = base.Projectile.Center.ToTileCoordinates();
		Collision.ExpandVertically(centerPoint.X, centerPoint.Y, out var sizeModding, out var sizeModding2, 15, 15);
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
		Color sandYellow = default(Color);
		((Color)(ref sandYellow))._002Ector(225, 225, 100);
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
			Color newSandYellow = Color.Lerp(Color.Transparent, sandYellow, colorChanger * 2f);
			if (colorChanger > 0.5f)
			{
				newSandYellow = Color.Lerp(Color.Transparent, sandYellow, 2f - colorChanger * 2f);
			}
			((Color)(ref newSandYellow)).A = (byte)((float)(int)((Color)(ref newSandYellow)).A * 0.5f);
			newSandYellow *= trackerClamp;
			spinArea *= colorChangeVector * 100f;
			spinArea.Y = 0f;
			spinArea.X = 0f;
			spinArea += new Vector2(sizeModdingVector2.X, k) - Main.screenPosition;
			Main.EntitySpriteDraw(texture2D23, spinArea, drawRectangle, newSandYellow, aiTrackMult + incStorageMult, smallRect, 1f + lowerColorChanger, (SpriteEffects)0);
		}
		return false;
	}
}
