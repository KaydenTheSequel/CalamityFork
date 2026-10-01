using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.NPCs;

public class WormAnimation
{
	public class WormKeyframe
	{
		public float[] segmentOffsets = new float[1];

		public WormKeyframe(params (float, float)[] parameters)
		{
			segmentOffsets = new float[200];
			new List<float>();
			for (int i = 0; i < 200; i++)
			{
				bool broken = false;
				for (int j = 0; j < parameters.Length; j++)
				{
					if (parameters[j].Item1 == (float)i)
					{
						segmentOffsets[i] = parameters[j].Item2;
						broken = true;
						break;
					}
				}
				if (!broken)
				{
					segmentOffsets[i] = float.NaN;
				}
			}
		}

		public static WormKeyframe GetCurrent(BaseWormNPC HeadNPC)
		{
			WormKeyframe kf = new WormKeyframe();
			for (int i = 0; i < HeadNPC.Segments.Count(); i++)
			{
				if (i == 0)
				{
					float dif = ShortestAngle(HeadNPC.NPC.rotation, HeadNPC.Segments[i].rotation);
					kf.segmentOffsets[i] = dif;
				}
				else
				{
					float dif2 = ShortestAngle(HeadNPC.Segments[i - 1].rotation, HeadNPC.Segments[i].rotation);
					kf.segmentOffsets[i] = dif2;
				}
			}
			return kf;
		}

		private static float ShortestAngle(float from, float to)
		{
			float difference;
			for (difference = to - from; difference < -(float)Math.PI; difference += (float)Math.PI * 2f)
			{
			}
			while (difference > (float)Math.PI)
			{
				difference -= (float)Math.PI * 2f;
			}
			return difference;
		}

		public float[] ClearTheNaNs()
		{
			float[] offsets = new float[segmentOffsets.Length];
			for (int i = 0; i < segmentOffsets.Length; i++)
			{
				if (float.IsNaN(segmentOffsets[i]))
				{
					if (i == 0)
					{
						offsets[i] = 0f;
					}
					else
					{
						offsets[i] = offsets[i - 1];
					}
				}
				else
				{
					offsets[i] = segmentOffsets[i];
				}
			}
			return offsets;
		}
	}

	public Dictionary<int, (WormKeyframe, float)> AnimationKeyframes = new Dictionary<int, (WormKeyframe, float)>();

	public float segmentRigidity = 0.75f;

	public bool mirror = true;

	public bool applyRotation = true;

	public void ApplyAnimationFrame(Entity HeadEntity, float frame)
	{
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_0635: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0692: Unknown result type (might be due to invalid IL or missing references)
		//IL_0697: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0834: Unknown result type (might be due to invalid IL or missing references)
		//IL_0849: Unknown result type (might be due to invalid IL or missing references)
		//IL_084e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_070e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0702: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_070c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_073a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0740: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		//IL_0757: Unknown result type (might be due to invalid IL or missing references)
		//IL_075c: Unknown result type (might be due to invalid IL or missing references)
		//IL_075e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0763: Unknown result type (might be due to invalid IL or missing references)
		//IL_0773: Unknown result type (might be due to invalid IL or missing references)
		//IL_0775: Unknown result type (might be due to invalid IL or missing references)
		//IL_0777: Unknown result type (might be due to invalid IL or missing references)
		//IL_0779: Unknown result type (might be due to invalid IL or missing references)
		//IL_0784: Unknown result type (might be due to invalid IL or missing references)
		//IL_078b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0790: Unknown result type (might be due to invalid IL or missing references)
		//IL_0795: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e3: Unknown result type (might be due to invalid IL or missing references)
		IOrderedEnumerable<KeyValuePair<int, (WormKeyframe, float)>> orderedEnumerable = AnimationKeyframes.OrderBy((KeyValuePair<int, (WormKeyframe, float)> x) => x.Key);
		(int, float[], float)? prev = null;
		(int, float[], float)? next = null;
		float[] goalSegmentRotOffsets = new float[200];
		float goalRotation = float.NaN;
		foreach (KeyValuePair<int, (WormKeyframe, float)> item in orderedEnumerable)
		{
			if ((float)item.Key < frame)
			{
				prev = (item.Key, item.Value.Item1.ClearTheNaNs(), item.Value.Item2);
				continue;
			}
			next = (item.Key, item.Value.Item1.ClearTheNaNs(), item.Value.Item2);
			break;
		}
		if (next.HasValue && prev.HasValue)
		{
			float completion = (frame - (float)prev.Value.Item1) / (float)(next.Value.Item1 - prev.Value.Item1);
			for (int i = 0; i < goalSegmentRotOffsets.Length; i++)
			{
				goalSegmentRotOffsets[i] = MathHelper.Lerp(prev.Value.Item2[i], next.Value.Item2[i], completion);
			}
			if (float.IsNaN(next.Value.Item3) && frame == (float)next.Value.Item1)
			{
				AnimationKeyframes[next.Value.Item1] = (AnimationKeyframes[next.Value.Item1].Item1, next.Value.Item3);
			}
			if (!float.IsNaN(prev.Value.Item3))
			{
				goalRotation = MathHelper.Lerp(prev.Value.Item3, next.Value.Item3, completion);
			}
		}
		else
		{
			if (prev.HasValue)
			{
				goalSegmentRotOffsets = prev.Value.Item2;
			}
			if (next.HasValue)
			{
				goalSegmentRotOffsets = next.Value.Item2;
				if (float.IsNaN(next.Value.Item3) && frame == (float)next.Value.Item1)
				{
					AnimationKeyframes[next.Value.Item1] = (AnimationKeyframes[next.Value.Item1].Item1, next.Value.Item3);
				}
			}
		}
		if (HeadEntity is NPC)
		{
			BaseWormNPC HeadNPC = ((NPC)HeadEntity).ModNPC<BaseWormNPC>();
			if (HeadNPC.Segments.Count <= 0)
			{
				return;
			}
			if (applyRotation && !float.IsNaN(goalRotation))
			{
				HeadNPC.NPC.rotation = goalRotation * (float)((!mirror) ? 1 : HeadNPC.NPC.velocity.X.DirectionalSign());
			}
			for (int i2 = 0; i2 < HeadNPC.Segments.Count; i2++)
			{
				Vector2 pos1 = HeadNPC.NPC.Center;
				float dist = HeadNPC.SegmentTypePositionOffsets[0];
				if (i2 != 0)
				{
					dist = HeadNPC.SegmentTypePositionOffsets[HeadNPC.Segments[i2 - 1].segmentType + 1];
				}
				dist *= HeadNPC.NPC.scale;
				Vector2 rot = (HeadNPC.NPC.rotation - (float)Math.PI / 2f).ToRotationVector2();
				if (rot == Vector2.Zero)
				{
					rot = HeadNPC.Segments[0].Center.AngleTo(HeadNPC.NPC.Center).ToRotationVector2();
				}
				if (i2 >= 1)
				{
					pos1 = HeadNPC.Segments[i2 - 1].Center;
					rot = HeadNPC.Segments[i2 - 1].velocity.SafeNormalize(Vector2.Zero);
				}
				rot = rot.RotatedBy(goalSegmentRotOffsets[i2] * (float)((!mirror) ? 1 : HeadNPC.NPC.velocity.X.DirectionalSign()));
				Vector2 dir = HeadNPC.Segments[i2].Center.DirectionFrom(pos1);
				HeadNPC.Segments[i2].Center = pos1 + Vector2.Lerp(dir, -rot, segmentRigidity) * dist;
				float rotationOffset = Vector2.Lerp(dir, -rot, segmentRigidity).ToRotation();
				float finalOffset = (-rot).ToRotation().AngleLerp(rotationOffset, 0f);
				HeadNPC.Segments[i2].Center = pos1 + finalOffset.ToRotationVector2() * dist;
				HeadNPC.Segments[i2].velocity = HeadNPC.Segments[i2].Center.DirectionTo(pos1);
				HeadNPC.Segments[i2].rotation = HeadNPC.Segments[i2].velocity.ToRotation() + (float)Math.PI / 2f;
			}
			for (int i3 = 1; i3 < HeadNPC.Segments.Count - 1; i3++)
			{
				HeadNPC.Segments[i3].rotation = HeadNPC.Segments[i3 + 1].Center.DirectionTo(HeadNPC.Segments[i3 - 1].Center).ToRotation() + (float)Math.PI / 2f;
			}
		}
		else
		{
			if (!(HeadEntity is Projectile))
			{
				return;
			}
			BaseWormProjectile HeadProjectile = ((Projectile)HeadEntity).ModProjectile<BaseWormProjectile>();
			if (HeadProjectile.Segments.Count <= 0)
			{
				return;
			}
			if (applyRotation && !float.IsNaN(goalRotation))
			{
				HeadProjectile.Projectile.rotation = goalRotation * (float)((!mirror) ? 1 : HeadProjectile.Projectile.velocity.X.DirectionalSign());
			}
			for (int i4 = 0; i4 < HeadProjectile.Segments.Count; i4++)
			{
				Vector2 pos2 = HeadProjectile.Projectile.Center;
				float dist2 = HeadProjectile.SegmentTypePositionOffsets[0];
				if (i4 != 0)
				{
					dist2 = HeadProjectile.SegmentTypePositionOffsets[HeadProjectile.Segments[i4 - 1].segmentType + 1];
				}
				dist2 *= HeadProjectile.Projectile.scale;
				Vector2 rot2 = (HeadProjectile.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2();
				if (rot2 == Vector2.Zero)
				{
					rot2 = HeadProjectile.Segments[0].Center.AngleTo(HeadProjectile.Projectile.Center).ToRotationVector2();
				}
				if (i4 >= 1)
				{
					pos2 = HeadProjectile.Segments[i4 - 1].Center;
					rot2 = HeadProjectile.Segments[i4 - 1].velocity.SafeNormalize(Vector2.Zero);
				}
				rot2 = rot2.RotatedBy(goalSegmentRotOffsets[i4] * (float)((!mirror) ? 1 : HeadProjectile.Projectile.velocity.X.DirectionalSign()));
				Vector2 dir2 = HeadProjectile.Segments[i4].Center.DirectionFrom(pos2);
				HeadProjectile.Segments[i4].Center = pos2 + Vector2.Lerp(dir2, -rot2, segmentRigidity) * dist2;
				HeadProjectile.Segments[i4].velocity = HeadProjectile.Segments[i4].Center.DirectionTo(pos2);
				HeadProjectile.Segments[i4].rotation = HeadProjectile.Segments[i4].velocity.ToRotation() + (float)Math.PI / 2f;
			}
			for (int i5 = 1; i5 < HeadProjectile.Segments.Count - 1; i5++)
			{
				HeadProjectile.Segments[i5].rotation = HeadProjectile.Segments[i5 + 1].Center.DirectionTo(HeadProjectile.Segments[i5 - 1].Center).ToRotation() + (float)Math.PI / 2f;
			}
		}
	}
}
