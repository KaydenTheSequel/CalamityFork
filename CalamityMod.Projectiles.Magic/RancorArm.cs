using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.Projectiles.Magic;

public class RancorArm : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 IdealPosition;

	public new string LocalizationCategory => "Projectiles.Magic";

	public Player Owner => Main.player[base.Projectile.owner];

	public float RotationDirection => base.Projectile.rotation + base.Projectile.ai[0];

	public ref float Time => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 4);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 240;
		base.Projectile.hide = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 12;
		base.Projectile.ignoreWater = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(base.Projectile.frame);
		writer.Write(base.Projectile.rotation);
		writer.WriteVector2(IdealPosition);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frame = reader.ReadInt32();
		base.Projectile.rotation = reader.ReadSingle();
		IdealPosition = reader.ReadVector2();
	}

	public override void AI()
	{
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundEngine.PlaySound((Time == 0f) ? SoundID.NPCDeath52 : SoundID.Item20, base.Projectile.Center);
			base.Projectile.frame = Main.rand.Next(Main.projFrames[base.Type]);
			Vector2 newSize = default(Vector2);
			switch (base.Projectile.frame)
			{
			case 0:
				((Vector2)(ref newSize))._002Ector(16f, 48f);
				break;
			case 1:
				((Vector2)(ref newSize))._002Ector(28f, 120f);
				break;
			case 2:
				((Vector2)(ref newSize))._002Ector(30f, 82f);
				break;
			case 3:
				((Vector2)(ref newSize))._002Ector(22f, 90f);
				break;
			case 4:
				((Vector2)(ref newSize))._002Ector(36f, 110f);
				break;
			default:
				((Vector2)(ref newSize))._002Ector(28f, 50f);
				break;
			}
			Vector2 idealCenter = base.Projectile.Center;
			if (WorldUtils.Find(idealCenter.ToTileCoordinates(), Searches.Chain(new Searches.Down(25), new CustomConditions.SolidOrPlatform()), out var result))
			{
				idealCenter = result.ToWorldCoordinates();
			}
			idealCenter.Y += 36f;
			base.Projectile.ExpandHitboxBy((int)newSize.X, (int)newSize.Y);
			if (Main.rand.NextBool(3))
			{
				base.Projectile.rotation = Main.rand.NextFloatDirection() * 0.3f;
			}
			IdealPosition = idealCenter;
			base.Projectile.localAI[0] = 1f;
			base.Projectile.netUpdate = true;
		}
		float descendCompletion = Utils.GetLerpValue(0f, 24f, base.Projectile.timeLeft, clamped: true);
		float riseCompletion = Utils.GetLerpValue(0f, 30f, Time, clamped: true);
		float heightOffset = -58f;
		if ((float)base.Projectile.height > 90f)
		{
			heightOffset += 22f;
		}
		base.Projectile.Bottom = IdealPosition + Vector2.UnitY.RotatedBy(RotationDirection) * ((1f - riseCompletion * descendCompletion) * 64f + heightOffset);
		base.Projectile.Opacity = (float)Math.Pow(riseCompletion, 2.4) * descendCompletion;
		base.Projectile.scale = descendCompletion;
		Time++;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		float _ = 0f;
		Vector2 bottom = base.Projectile.Bottom;
		Vector2 top = base.Projectile.Bottom - Vector2.UnitY.RotatedBy(RotationDirection) * (float)base.Projectile.height;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), bottom, top, (float)base.Projectile.width * base.Projectile.scale, ref _);
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindNPCsAndTiles.Add(index);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Color drawColor = Lighting.GetColor((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16) * base.Projectile.Opacity;
		Color fadedColor = Color.Red * base.Projectile.Opacity * base.Projectile.scale * 0.4f;
		((Color)(ref fadedColor)).A = 0;
		Vector2 scale = default(Vector2);
		((Vector2)(ref scale))._002Ector(base.Projectile.scale, 1f);
		float afterimageOffset = base.Projectile.Opacity * base.Projectile.scale * 5f;
		for (int i = 0; i < 6; i++)
		{
			Vector2 afterimageOffsetVector = ((float)Math.PI * 2f * (float)i / 6f).ToRotationVector2() * afterimageOffset;
			Main.EntitySpriteDraw(texture, drawPosition + afterimageOffsetVector, frame, fadedColor, RotationDirection, origin, scale, (SpriteEffects)0);
		}
		Main.EntitySpriteDraw(texture, drawPosition, frame, drawColor, RotationDirection, origin, scale, (SpriteEffects)0);
		return false;
	}
}
