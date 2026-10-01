using System;
using CalamityMod.Items.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class CrystylCrusherRay : ModProjectile, ILocalizedModType, IModType
{
	private const float MAX_CHARGE = 100f;

	private const float MOVE_DISTANCE = 100f;

	private bool playedSound;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Magic/YharimsCrystalBeam";

	public float Distance
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

	public float Charge
	{
		get
		{
			return base.Projectile.localAI[0];
		}
		set
		{
			base.Projectile.localAI[0] = value;
		}
	}

	public bool IsAtMaxCharge => Charge == 100f;

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.scale = 7f;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.hide = true;
		base.Projectile.timeLeft = 300;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		Player owner = Main.player[base.Projectile.owner];
		if (IsAtMaxCharge)
		{
			Vector2 maxLength = owner.ClampedMouseWorld() - owner.Center;
			DrawLaser(TextureAssets.Projectile[base.Type].Value, owner.Center, base.Projectile.velocity, 15f, -(float)Math.PI / 2f, base.Projectile.scale, ((Vector2)(ref maxLength)).Length(), new Color(Main.DiscoR, 0, 255), 100);
		}
		return false;
	}

	public void DrawLaser(Texture2D texture, Vector2 start, Vector2 unit, float step, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), int transDist = 50)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		float r = unit.ToRotation() + rotation;
		for (float i = transDist; i <= Distance; i += step)
		{
			Vector2 origin = start + i * unit;
			Main.EntitySpriteDraw(texture, origin - Main.screenPosition, (Rectangle?)new Rectangle(0, 26, 28, 26), (i < (float)transDist) ? Color.Transparent : color, r, new Vector2(14f, 13f), MathHelper.Lerp(1f, scale, i / Distance), (SpriteEffects)0, 0f);
		}
		Main.EntitySpriteDraw(texture, start + unit * ((float)transDist - step) - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, 28, 26), color, r, new Vector2(14f, 13f), 1f, (SpriteEffects)0, 0f);
		Main.EntitySpriteDraw(texture, start + (Distance + step) * unit - Main.screenPosition, (Rectangle?)new Rectangle(0, 52, 28, 26), color, r, new Vector2(14f, 13f), scale, (SpriteEffects)0, 0f);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		if (!IsAtMaxCharge)
		{
			return false;
		}
		Player player = Main.player[base.Projectile.owner];
		Vector2 unit = base.Projectile.velocity;
		float point = 0f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center, player.Center + unit * Distance, 22f, ref point);
	}

	public override void AI()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (base.Projectile.timeLeft == 300)
		{
			SoundEngine.PlaySound(in CrystylCrusher.ChargeSound, player.Center);
		}
		base.Projectile.Center = player.Center + base.Projectile.velocity * 100f;
		base.Projectile.timeLeft = 2;
		UpdatePlayer(player);
		ChargeLaser(player);
		if (!(Charge < 100f))
		{
			if (!playedSound)
			{
				SoundEngine.PlaySound(in SoundID.Item68, base.Projectile.Center);
				playedSound = true;
			}
			SetLaserPosition(player);
			CastLights();
			DestroyTiles();
		}
	}

	private void SetLaserPosition(Player player)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		Distance = MathHelper.Max(player.Distance(player.ClampedMouseWorld()) - 20f, 105f);
	}

	private void ChargeLaser(Player player)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		if (!player.Calamity().mouseRight || !player.active || player.dead)
		{
			base.Projectile.Kill();
			return;
		}
		Vector2 offset = base.Projectile.velocity;
		offset *= 80f;
		Vector2 pos = player.Center + offset - new Vector2(15f, 15f);
		if (Charge < 100f)
		{
			Charge++;
		}
		int chargeFact = (int)(Charge / 20f);
		Vector2 dustVelocity = Vector2.UnitX * 18f;
		dustVelocity = dustVelocity.RotatedBy(base.Projectile.rotation - (float)Math.PI / 2f);
		Vector2 spawnPos = base.Projectile.Center + dustVelocity;
		for (int k = 0; k < chargeFact + 1; k++)
		{
			int dustType = Main.rand.Next(3);
			switch (dustType)
			{
			case 0:
				dustType = 173;
				break;
			case 1:
				dustType = 57;
				break;
			case 2:
				dustType = 58;
				break;
			}
			Vector2 spawn = spawnPos + ((float)Main.rand.NextDouble() * 6.28f).ToRotationVector2() * (12f - (float)(chargeFact * 2));
			Dust obj = Main.dust[Dust.NewDust(pos, 20, 20, dustType, base.Projectile.velocity.X / 2f, base.Projectile.velocity.Y / 2f)];
			obj.velocity = Vector2.Normalize(spawnPos - spawn) * 1.5f * (10f - (float)chargeFact * 2f) / 10f;
			obj.noGravity = true;
			obj.color = new Color(Main.DiscoR, 0, 255);
			obj.scale = (float)Main.rand.Next(10, 20) * 0.05f;
		}
	}

	private void UpdatePlayer(Player player)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			Vector2 diff = Main.MouseWorld - player.Center;
			((Vector2)(ref diff)).Normalize();
			base.Projectile.velocity = diff;
			base.Projectile.direction = ((Main.MouseWorld.X > player.position.X) ? 1 : (-1));
			base.Projectile.netUpdate = true;
		}
		int dir = base.Projectile.direction;
		player.ChangeDir(dir);
		player.heldProj = base.Projectile.whoAmI;
		player.itemTime = 2;
		player.itemAnimation = 2;
		player.itemRotation = (float)Math.Atan2(base.Projectile.velocity.Y * (float)dir, base.Projectile.velocity.X * (float)dir);
	}

	private void CastLights()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * (Distance - 100f), 26f, DelegateMethods.CastLight);
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override bool? CanCutTiles()
	{
		if (!IsAtMaxCharge)
		{
			return false;
		}
		return null;
	}

	public override void CutTiles()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Vector2 unit = base.Projectile.velocity;
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + unit * Distance, base.Projectile.width + 16, DelegateMethods.CutTiles);
	}

	private void DestroyTiles()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		Vector2 destroyVector = base.Projectile.Center + base.Projectile.velocity * (Distance - 100f);
		int radius = 7;
		int mineXLeft = (int)(destroyVector.X / 16f - (float)radius);
		int mineXRight = (int)(destroyVector.X / 16f + (float)radius);
		int mineXUp = (int)(destroyVector.Y / 16f - (float)radius);
		int mineXDown = (int)(destroyVector.Y / 16f + (float)radius);
		if (mineXLeft < 0)
		{
			mineXLeft = 0;
		}
		if (mineXRight > Main.maxTilesX)
		{
			mineXRight = Main.maxTilesX;
		}
		if (mineXUp < 0)
		{
			mineXUp = 0;
		}
		if (mineXDown > Main.maxTilesY)
		{
			mineXDown = Main.maxTilesY;
		}
		AchievementsHelper.CurrentlyMining = true;
		for (int i = mineXLeft; i <= mineXRight; i++)
		{
			for (int j = mineXUp; j <= mineXDown; j++)
			{
				float num = Math.Abs((float)i - destroyVector.X / 16f);
				float destroyTileY = Math.Abs((float)j - destroyVector.Y / 16f);
				if (Math.Sqrt(num * num + destroyTileY * destroyTileY) < (double)radius && Main.tile[i, j] != null && Main.tile[i, j].HasTile)
				{
					WorldGen.KillTile(i, j);
					DustExplosion(destroyVector);
					if (!Main.tile[i, j].HasTile && Main.netMode != 0)
					{
						NetMessage.SendData(17, -1, -1, null, 0, i, j);
					}
				}
			}
		}
		AchievementsHelper.CurrentlyMining = false;
	}

	private void DustExplosion(Vector2 vector)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		int dustAmt = 12;
		for (int k = 0; k < dustAmt; k++)
		{
			int dustType = Main.rand.Next(3);
			switch (dustType)
			{
			case 0:
				dustType = 173;
				break;
			case 1:
				dustType = 57;
				break;
			case 2:
				dustType = 58;
				break;
			}
			Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(k - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt) + vector;
			Vector2 faceDirection = val - vector;
			int explosionDust = Dust.NewDust(val + faceDirection, 0, 0, dustType, faceDirection.X * 0.5f, faceDirection.Y * 0.5f, 100, new Color(Main.DiscoR, 0, 255));
			Main.dust[explosionDust].noGravity = true;
			Main.dust[explosionDust].noLight = true;
			Main.dust[explosionDust].velocity = faceDirection;
		}
	}
}
