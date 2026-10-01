using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class Tornado : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/TornadoProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 600;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
	}

	public override void AI()
	{
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[1]++;
		if (base.Projectile.localAI[1] >= 10f)
		{
			base.Projectile.localAI[1] = 0f;
			int projCount = 0;
			int oldestTornado = 0;
			float tornadoAge = 0f;
			int projType = base.Projectile.type;
			for (int projIndex = 0; projIndex < Main.maxProjectiles; projIndex++)
			{
				Projectile proj = Main.projectile[projIndex];
				if (proj.active && proj.owner == base.Projectile.owner && proj.type == projType && proj.ai[0] < 900f)
				{
					projCount++;
					if (proj.ai[0] > tornadoAge)
					{
						oldestTornado = projIndex;
						tornadoAge = proj.ai[0];
					}
				}
			}
			if (projCount > 3)
			{
				Main.projectile[oldestTornado].netUpdate = true;
				Main.projectile[oldestTornado].ai[0] = 36000f;
				Main.projectile[oldestTornado].damage = 0;
				return;
			}
		}
		float projTimer = 900f;
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
		if (base.Projectile.localAI[0] >= 30f)
		{
			base.Projectile.damage = 0;
			if (base.Projectile.ai[0] < projTimer - 120f)
			{
				float timeModulo = base.Projectile.ai[0] % 60f;
				base.Projectile.ai[0] = projTimer - 120f + timeModulo;
				base.Projectile.netUpdate = true;
			}
		}
		float projX = base.Projectile.Center.X;
		float projY = base.Projectile.Center.Y;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (!npc.CanBeChasedBy(base.Projectile) || !Collision.CanHit(base.Projectile.Center, 1, 1, npc.Center, 1, 1) || CalamityPlayer.areThereAnyDamnBosses)
			{
				continue;
			}
			float npcCenterX = npc.position.X + (float)(npc.width / 2);
			float npcCenterY = npc.position.Y + (float)(npc.height / 2);
			if (Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - npcCenterX) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - npcCenterY) < 600f)
			{
				if (npc.position.X < projX)
				{
					npc.velocity.X += 0.02f;
				}
				else
				{
					npc.velocity.X -= 0.02f;
				}
				if (npc.position.Y < projY)
				{
					npc.velocity.Y += 0.02f;
				}
				else
				{
					npc.velocity.Y -= 0.02f;
				}
			}
		}
		Point projCenterTile = base.Projectile.Center.ToTileCoordinates();
		Collision.ExpandVertically(projCenterTile.X, projCenterTile.Y, out var sizeMod, out var sizeMod2, 15, 15);
		sizeMod++;
		sizeMod2--;
		Vector2 sizeModVector = new Vector2((float)projCenterTile.X, (float)sizeMod) * 16f + new Vector2(8f);
		Vector2 sizeModVector2 = new Vector2((float)projCenterTile.X, (float)sizeMod2) * 16f + new Vector2(8f);
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
			if (!breakFlag && base.Projectile.ai[0] < projTimer - 120f)
			{
				float aiDecrement = base.Projectile.ai[0] % 60f;
				base.Projectile.ai[0] = projTimer - 120f + aiDecrement;
				base.Projectile.netUpdate = true;
			}
		}
		_ = base.Projectile.ai[0];
		_ = projTimer - 120f;
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
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
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
		Vector2 spinningpoint = Vector2.UnitY.RotatedBy(aiTracker * 0.1f);
		float incrementStorage = 0f;
		float increment = 5.1f;
		Color cloudColor = default(Color);
		((Color)(ref cloudColor))._002Ector(225, 225, 225);
		Vector2 colorChangeVector = default(Vector2);
		for (float j = (int)sizeModdingVector2.Y; j > (float)(int)sizeModdingVector.Y; j -= increment)
		{
			incrementStorage += increment;
			float colorChanger = incrementStorage / sizeModdingPos.Y;
			float incStorageMult = incrementStorage * ((float)Math.PI * 2f) / -20f;
			float lowerColorChanger = colorChanger - 0.15f;
			Vector2 spinArea = spinningpoint.RotatedBy(incStorageMult);
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
			spinArea += new Vector2(sizeModdingVector2.X, j) - Main.screenPosition;
			Main.spriteBatch.Draw(texture2D23, spinArea, (Rectangle?)drawRectangle, newCloudColor, aiTrackMult + incStorageMult, smallRect, 1f + lowerColorChanger, (SpriteEffects)0, 0f);
		}
		return false;
	}
}
