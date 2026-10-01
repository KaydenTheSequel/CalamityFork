using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.BaseProjectiles;

public abstract class BaseWormProjectile : ModProjectile
{
	public enum SegmentFollowLogic
	{
		Regular,
		Exact
	}

	public List<Vector2> SegmentTypeDrawOffsets = new List<Vector2>();

	public float AnimationFrame;

	public WormAnimation ActiveAnimation;

	private List<Asset<Texture2D>> internalTexAssets = new List<Asset<Texture2D>>();

	private List<Asset<Texture2D>> internalGlowAssets = new List<Asset<Texture2D>>();

	public List<BaseWormSegment> Segments = new List<BaseWormSegment>();

	public SegmentFollowLogic SegmentFollowType;

	public float SegmentRigidity = 0.2f;

	public float SegmentMaxRotation = (float)Math.PI * 2f;

	private List<Vector2> segmentPoints = new List<Vector2>();

	public abstract int SegmentCount { get; }

	public abstract List<float> SegmentTypePositionOffsets { get; }

	public abstract List<string> SegmentTextures { get; }

	public virtual List<string?> GlowTextures { get; }

	public List<Asset<Texture2D>> SegmentTextureAssets
	{
		get
		{
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			if (internalTexAssets.Count == 0)
			{
				for (int i = 0; i < SegmentTextures.Count; i++)
				{
					internalTexAssets.Add(ModContent.Request<Texture2D>(SegmentTextures[i], (AssetRequestMode)2));
					if (SegmentTypeDrawOffsets.Count <= i)
					{
						SegmentTypeDrawOffsets.Add(Vector2.Zero);
					}
				}
			}
			return internalTexAssets;
		}
	}

	public List<Asset<Texture2D>> GlowTextureAssets
	{
		get
		{
			if (internalGlowAssets.Count == 0)
			{
				for (int i = 0; i < GlowTextures.Count; i++)
				{
					if (GlowTextures[i] != null)
					{
						internalGlowAssets.Add(ModContent.Request<Texture2D>(GlowTextures[i], (AssetRequestMode)2));
					}
					else
					{
						internalGlowAssets.Add(ModContent.Request<Texture2D>("CalamityMod/Projectiles/InvisibleProj", (AssetRequestMode)2));
					}
				}
			}
			return internalGlowAssets;
		}
	}

	public void UpdateSegments()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.position += base.Projectile.velocity;
		if (ActiveAnimation != null)
		{
			ActiveAnimation.ApplyAnimationFrame(base.Projectile, AnimationFrame);
			AnimationFrame++;
			if (AnimationFrame > (float)ActiveAnimation.AnimationKeyframes.Keys.Max())
			{
				AnimationFrame = 0f;
				ActiveAnimation = null;
			}
		}
		else
		{
			if (segmentPoints.Count < 1 || segmentPoints[0].Distance(base.Projectile.Center) > 8f)
			{
				segmentPoints.Insert(0, base.Projectile.Center);
			}
			while (segmentPoints.Count > 300)
			{
				segmentPoints.RemoveAt(segmentPoints.Count - 1);
			}
			switch (SegmentFollowType)
			{
			case SegmentFollowLogic.Regular:
				RegularSegmentLogic();
				break;
			case SegmentFollowLogic.Exact:
				ExactSegmentLogic();
				break;
			}
		}
		Projectile projectile2 = base.Projectile;
		projectile2.position -= base.Projectile.velocity;
	}

	private void RegularSegmentLogic()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < Segments.Count; i++)
		{
			float segmentDistance = SegmentTypePositionOffsets[0];
			BaseWormSegment thisSeg = Segments[i];
			BaseWormSegment aheadSeg = new BaseWormSegment(this);
			if (i != 0)
			{
				aheadSeg = Segments[i - 1];
				segmentDistance = SegmentTypePositionOffsets[Segments[i - 1].segmentType + 1];
			}
			segmentDistance *= base.Projectile.scale;
			Vector2 nexSegDir = aheadSeg.Center - thisSeg.Center;
			if (aheadSeg.rotation != thisSeg.rotation)
			{
				nexSegDir = nexSegDir.RotatedBy(MathHelper.WrapAngle(aheadSeg.rotation - thisSeg.rotation) * SegmentRigidity);
				nexSegDir = nexSegDir.MoveTowards((aheadSeg.rotation - thisSeg.rotation).ToRotationVector2(), 1f);
			}
			thisSeg.rotation = nexSegDir.ToRotation() + (float)Math.PI / 2f;
			float angledif = MathHelper.WrapAngle(thisSeg.rotation - aheadSeg.rotation);
			thisSeg.rotation = thisSeg.rotation.AngleLerp(aheadSeg.rotation + MathHelper.Clamp(angledif, (0f - SegmentMaxRotation) * 0.5f, SegmentMaxRotation * 0.5f), 0.25f);
			thisSeg.Center = aheadSeg.Center - (thisSeg.rotation - (float)Math.PI / 2f).ToRotationVector2() * segmentDistance;
		}
	}

	private void ExactSegmentLogic()
	{
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		float dist = 40f;
		int segmentPointInUse = 0;
		for (int i = 0; i < Segments.Count; i++)
		{
			BaseWormSegment thisSeg = Segments[i];
			BaseWormSegment aheadSeg = new BaseWormSegment(this);
			if (i != 0)
			{
				aheadSeg = Segments[i - 1];
			}
			bool hasMoved = false;
			while (segmentPointInUse < segmentPoints.Count)
			{
				if (segmentPointInUse == 0)
				{
					if (aheadSeg.Center.Distance(segmentPoints[0]) >= dist)
					{
						thisSeg.Center = aheadSeg.Center + aheadSeg.Center.DirectionTo(segmentPoints[0]) * dist;
						Segments[i].velocity = Segments[i].Center.DirectionTo(aheadSeg.Center);
						Segments[i].rotation = Segments[i].velocity.ToRotation() + (float)Math.PI / 2f;
						hasMoved = true;
						break;
					}
					segmentPointInUse++;
				}
				else
				{
					if (aheadSeg.Center.Distance(segmentPoints[segmentPointInUse]) >= dist)
					{
						thisSeg.Center = aheadSeg.Center + aheadSeg.Center.DirectionTo(segmentPoints[segmentPointInUse]) * dist;
						Segments[i].velocity = Segments[i].Center.DirectionTo(aheadSeg.Center);
						Segments[i].rotation = Segments[i].velocity.ToRotation() + (float)Math.PI / 2f;
						hasMoved = true;
						break;
					}
					segmentPointInUse++;
				}
			}
			if (!hasMoved && segmentPointInUse >= segmentPoints.Count)
			{
				thisSeg.Center = segmentPoints[segmentPoints.Count - 1];
				Segments[i].velocity = Segments[i].Center.DirectionTo(aheadSeg.Center);
				Segments[i].rotation = Segments[i].velocity.ToRotation() + (float)Math.PI / 2f;
				hasMoved = true;
				break;
			}
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 1600;
	}

	public override void SetDefaults()
	{
		for (int i = 0; i < SegmentCount - 1; i++)
		{
			Segments.Add(new BaseWormSegment(this));
		}
		Segments.Add(new BaseWormSegment(this, 1));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		for (int i = Segments.Count - 1; i >= 0; i--)
		{
			DrawSegment(ref lightColor, Segments[i]);
		}
		Main.spriteBatch.Draw(TextureAssets.Projectile[base.Type].Value, base.Projectile.Center - Main.screenPosition, (Rectangle?)null, lightColor * base.Projectile.Opacity, base.Projectile.rotation, TextureAssets.Projectile[base.Type].Value.Size() / 2f, base.Projectile.scale, (SpriteEffects)0, 1f);
		if (GlowTextures.Count > 0 && GlowTextures[0] != null)
		{
			Main.spriteBatch.Draw(GlowTextureAssets[0].Value, base.Projectile.Center - Main.screenPosition, (Rectangle?)null, Color.White * base.Projectile.Opacity, base.Projectile.rotation, GlowTextureAssets[0].Size() / 2f, base.Projectile.scale, (SpriteEffects)0, 1f);
		}
		return false;
	}

	public virtual void DrawSegment(ref Color lightColor, BaseWormSegment segment)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		Color color = Lighting.GetColor(segment.Center.ToTileCoordinates());
		if (SegmentTextureAssets.IndexInRange(segment.segmentType))
		{
			Texture2D tex = SegmentTextureAssets[segment.segmentType].Value;
			Main.spriteBatch.Draw(tex, segment.Center - Main.screenPosition, (Rectangle?)null, color * segment.Opacity, segment.rotation, tex.Size() / 2f + SegmentTypeDrawOffsets[segment.segmentType], base.Projectile.scale, (SpriteEffects)0, 1f);
			if (GlowTextures != null && GlowTextures.IndexInRange(segment.segmentType + 1) && GlowTextures[segment.segmentType + 1] != null)
			{
				tex = GlowTextureAssets[segment.segmentType + 1].Value;
				Main.spriteBatch.Draw(tex, segment.Center - Main.screenPosition, (Rectangle?)null, Color.White * segment.Opacity, segment.rotation, tex.Size() / 2f + SegmentTypeDrawOffsets[segment.segmentType], base.Projectile.scale, (SpriteEffects)0, 1f);
			}
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(AnimationFrame);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		AnimationFrame = reader.ReadSingle();
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < Segments.Count; i++)
		{
			BaseWormSegment prevSeg = new BaseWormSegment(this);
			if (i != 0)
			{
				prevSeg = Segments[i - 1];
			}
			float cpoint = 0f;
			if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), prevSeg.Center, Segments[i].Center, 16f, ref cpoint))
			{
				return true;
			}
		}
		return base.Colliding(projHitbox, targetHitbox);
	}
}
