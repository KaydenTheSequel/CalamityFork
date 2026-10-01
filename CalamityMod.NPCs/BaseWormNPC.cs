using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs;

public abstract class BaseWormNPC : ModNPC
{
	public enum SegmentFollowLogic
	{
		Regular,
		Exact
	}

	public List<Vector2> SegmentTypeDrawOffsets = new List<Vector2>();

	public float AnimationFrame;

	public WormAnimation ActiveAnimation;

	public List<BaseWormSegment> Segments = new List<BaseWormSegment>();

	private List<Asset<Texture2D>> internalTexAssets = new List<Asset<Texture2D>>();

	private List<Asset<Texture2D>> internalGlowAssets = new List<Asset<Texture2D>>();

	public SegmentFollowLogic SegmentFollowType;

	public float SegmentRigidity = 0.2f;

	public float SegmentMaxRotation = (float)Math.PI * 2f;

	private List<Vector2> segmentPoints = new List<Vector2>();

	public abstract int SegmentCount { get; }

	public abstract List<float> SegmentTypePositionOffsets { get; }

	public abstract List<string> SegmentTextures { get; }

	public virtual List<string?> GlowTextures { get; }

	public abstract int WormHitboxNpcType { get; }

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
		NPC nPC = base.NPC;
		nPC.position += base.NPC.velocity;
		if (ActiveAnimation != null)
		{
			ActiveAnimation.ApplyAnimationFrame(base.NPC, AnimationFrame);
			AnimationFrame++;
			if (AnimationFrame > (float)ActiveAnimation.AnimationKeyframes.Keys.Max())
			{
				AnimationFrame = 0f;
				ActiveAnimation = null;
			}
		}
		else
		{
			if (segmentPoints.Count < 1 || segmentPoints[0].Distance(base.NPC.Center) > 8f)
			{
				segmentPoints.Insert(0, base.NPC.Center);
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
		NPC nPC2 = base.NPC;
		nPC2.position -= base.NPC.velocity;
		SpawnHitboxes();
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
			segmentDistance *= base.NPC.scale;
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

	public virtual void SpawnHitboxes()
	{
		if (Main.netMode == 1)
		{
			return;
		}
		int i;
		for (i = 0; i < Segments.Count; i++)
		{
			if (Main.npc.Count((NPC x) => x.active) >= Main.maxNPCs - 5)
			{
				break;
			}
			if (!Main.player.Any(delegate(Player x)
			{
				//IL_001e: Unknown result type (might be due to invalid IL or missing references)
				//IL_0024: Unknown result type (might be due to invalid IL or missing references)
				return x.active && Segments[i].Center.Distance(x.Center) < 1200f;
			}))
			{
				continue;
			}
			if (Main.npc.Any((NPC x) => x.active && x.type == WormHitboxNpcType && x.ai[1] == (float)i && x.ai[0] == (float)base.NPC.whoAmI))
			{
				Main.npc.First((NPC x) => x.active && x.type == WormHitboxNpcType && x.ai[1] == (float)i && x.ai[0] == (float)base.NPC.whoAmI).ai[2] = 0f;
			}
			else
			{
				NPC.NewNPC(base.NPC.GetSource_Misc("Hitbox"), (int)Segments[i].Center.X, (int)Segments[i].Center.Y + 100, WormHitboxNpcType, 0, base.NPC.whoAmI, i);
			}
		}
	}

	public override void SetStaticDefaults()
	{
		NPCID.Sets.MustAlwaysDraw[base.Type] = true;
	}

	public override void SetDefaults()
	{
		for (int i = 0; i < SegmentCount - 1; i++)
		{
			Segments.Add(new BaseWormSegment(this));
		}
		Segments.Add(new BaseWormSegment(this, 1));
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.rotation = MathF.Sin(Main.GlobalTimeWrappedHourly) * 0.2f + (float)Math.PI / 2f;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			drawColor = Color.White;
			SegmentRigidity = 0.1f;
			UpdateSegments();
		}
		for (int i = Segments.Count - 1; i >= 0; i--)
		{
			DrawSegment(spriteBatch, screenPos, drawColor, Segments[i]);
		}
		spriteBatch.Draw(TextureAssets.Npc[base.Type].Value, base.NPC.Center - screenPos, (Rectangle?)null, drawColor * base.NPC.Opacity, base.NPC.rotation, TextureAssets.Npc[base.Type].Value.Size() / 2f, base.NPC.scale, (SpriteEffects)0, 1f);
		if (GlowTextures.Count > 0 && GlowTextures[0] != null)
		{
			spriteBatch.Draw(GlowTextureAssets[0].Value, base.NPC.Center - screenPos, (Rectangle?)null, Color.White * base.NPC.Opacity, base.NPC.rotation, GlowTextureAssets[0].Size() / 2f, base.NPC.scale, (SpriteEffects)0, 1f);
		}
		return false;
	}

	public virtual void DrawSegment(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor, BaseWormSegment segment)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		Color color = Lighting.GetColor(segment.Center.ToTileCoordinates());
		if (base.NPC.IsABestiaryIconDummy)
		{
			color = Color.White;
		}
		if (SegmentTextureAssets.IndexInRange(segment.segmentType))
		{
			Texture2D tex = SegmentTextureAssets[segment.segmentType].Value;
			spriteBatch.Draw(tex, segment.Center - screenPos, (Rectangle?)null, color * segment.Opacity, segment.rotation, tex.Size() / 2f + SegmentTypeDrawOffsets[segment.segmentType], base.NPC.scale, (SpriteEffects)0, 1f);
			if (GlowTextures.IndexInRange(segment.segmentType + 1) && GlowTextures[segment.segmentType + 1] != null)
			{
				tex = GlowTextureAssets[segment.segmentType + 1].Value;
				spriteBatch.Draw(tex, segment.Center - screenPos, (Rectangle?)null, Color.White * segment.Opacity, segment.rotation, tex.Size() / 2f + SegmentTypeDrawOffsets[segment.segmentType], base.NPC.scale, (SpriteEffects)0, 1f);
			}
		}
	}
}
