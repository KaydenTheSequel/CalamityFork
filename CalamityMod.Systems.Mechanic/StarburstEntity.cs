using System.Collections.Generic;
using System.Linq;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Utilities.Daybreak;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Mechanic;

public class StarburstEntity
{
	private sealed class StarburstManager : ModPlayer
	{
		public ref List<StarburstEntity> StarburstEntities => ref base.Player.Calamity().StarburstEntities;

		public override void PostUpdate()
		{
			for (int i = 0; i < StarburstEntities.Count; i++)
			{
				StarburstEntity starburstEntity = StarburstEntities[i];
				starburstEntity.AI(base.Player, i);
				starburstEntity.UpdatePosition();
				starburstEntity.UpdateAnimation();
				foreach (StarburstEntity item in starburstEntity.MergeChildren.ToList())
				{
					item.AI(base.Player, i);
					item.UpdatePosition();
					item.UpdateAnimation();
				}
			}
		}

		public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0086: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0102: Unknown result type (might be due to invalid IL or missing references)
			//IL_012a: Unknown result type (might be due to invalid IL or missing references)
			//IL_012f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0134: Unknown result type (might be due to invalid IL or missing references)
			//IL_0145: Unknown result type (might be due to invalid IL or missing references)
			//IL_014f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0162: Unknown result type (might be due to invalid IL or missing references)
			//IL_016c: Unknown result type (might be due to invalid IL or missing references)
			//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0205: Unknown result type (might be due to invalid IL or missing references)
			//IL_022d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0232: Unknown result type (might be due to invalid IL or missing references)
			//IL_0237: Unknown result type (might be due to invalid IL or missing references)
			//IL_0248: Unknown result type (might be due to invalid IL or missing references)
			//IL_0252: Unknown result type (might be due to invalid IL or missing references)
			//IL_0265: Unknown result type (might be due to invalid IL or missing references)
			//IL_026f: Unknown result type (might be due to invalid IL or missing references)
			if (StarburstEntities.Count <= 0 || drawInfo.shadow != 0f)
			{
				return;
			}
			using (Main.spriteBatch.Scope())
			{
				Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
				Texture2D tex = TextureAssets.Projectile[ModContent.ProjectileType<DracoConstellation>()].Value;
				Texture2D glowTex = DracoConstellation.GetGlowTex();
				for (int i = 0; i < StarburstEntities.Count; i++)
				{
					StarburstEntity star = StarburstEntities[i];
					_ = star.color;
					int value = star.value;
					value -= star.MergeChildren.Count;
					float ScaleMod = MathHelper.Lerp(0.4f, 0.8f, (float)value / 10f);
					Main.spriteBatch.Draw(glowTex, star.Center - Main.screenPosition, (Rectangle?)null, star.color * 0.66f, 0f, glowTex.Size() * 0.5f, 0.2f * star.scale * ScaleMod, (SpriteEffects)0, 1f);
					Main.spriteBatch.Draw(tex, star.Center - Main.screenPosition, (Rectangle?)null, star.color * 0.66f, MathHelper.WrapAngle(Main.GlobalTimeWrappedHourly + (float)i), tex.Size() * 0.5f, 0.75f * star.scale * ScaleMod, (SpriteEffects)0, 1f);
					for (int j = 0; j < star.MergeChildren.Count; j++)
					{
						StarburstEntity ministar = star.MergeChildren[j];
						ScaleMod = MathHelper.Lerp(0.4f, 0.8f, (float)ministar.value / 10f);
						Main.spriteBatch.Draw(glowTex, ministar.Center - Main.screenPosition, (Rectangle?)null, ministar.color * 0.66f, 0f, glowTex.Size() * 0.5f, 0.2f * ministar.scale * ScaleMod, (SpriteEffects)0, 1f);
						Main.spriteBatch.Draw(tex, ministar.Center - Main.screenPosition, (Rectangle?)null, ministar.color * 0.66f, MathHelper.WrapAngle(Main.GlobalTimeWrappedHourly + (float)i), tex.Size() * 0.5f, 0.75f * ministar.scale * ScaleMod, (SpriteEffects)0, 1f);
					}
				}
				Main.spriteBatch.End();
			}
		}
	}

	public Vector2 Center;

	public Vector2 Velocity;

	public int AICooldown;

	public int frameCounter;

	public int frame;

	public int value;

	public float scale;

	public float opacity;

	public Color color;

	public StarburstEntity MergeTarget;

	public List<StarburstEntity> MergeChildren;

	public bool ShouldRemoveFromList;

	public StarburstEntity(Vector2 position, bool shouldRandomize = true)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		Center = Vector2.Zero;
		Velocity = Vector2.Zero;
		value = 1;
		scale = 1f;
		opacity = 1f;
		color = Color.Black;
		MergeChildren = new List<StarburstEntity>();
		base._002Ector();
		Center = position;
		if (shouldRandomize)
		{
			Velocity = Vector2.UnitX.RotatedByRandom(6.2831854820251465) * 3f;
		}
		switch (Main.rand.Next(1, 7))
		{
		case 1:
			color = Color.HotPink;
			break;
		case 2:
			color = Color.Yellow;
			break;
		case 3:
			color = Color.LimeGreen;
			break;
		case 4:
			color = Color.SkyBlue;
			break;
		case 5:
			color = Color.Lavender;
			break;
		case 6:
			color = Color.White;
			break;
		}
		color = Color.Lerp(Color.White, color, 0.5f);
	}

	public void AI(Player owner, int index = 0)
	{
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		if (AICooldown > 0)
		{
			MergeChildren = new List<StarburstEntity>();
			AICooldown--;
		}
		else if (MergeTarget != null)
		{
			Velocity += Center.DirectionTo(MergeTarget.Center) * 2.5f;
			Velocity *= 0.925f;
			if (Center.Distance(MergeTarget.Center) < 12f)
			{
				MergeTarget.MergeChildren.Remove(this);
			}
		}
		else if (Center.Distance(owner.Center) > 100f)
		{
			Velocity += Center.DirectionTo(owner.Center + Utils.RotatedBy(new Vector2(16f, 0f), (double)index, default(Vector2))).RotatedByRandom(0.30000001192092896);
			Velocity *= 0.95f;
		}
	}

	public void UpdatePosition()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		Center += Velocity;
	}

	public void UpdateAnimation()
	{
		frameCounter++;
		if (frameCounter > 6)
		{
			frame++;
			frameCounter = 0;
		}
		if (frame > 5)
		{
			frame = 0;
		}
	}
}
