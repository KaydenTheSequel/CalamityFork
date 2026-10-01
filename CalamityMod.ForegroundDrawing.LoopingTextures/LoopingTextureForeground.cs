using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.ForegroundDrawing.LoopingTextures;

public abstract class LoopingTextureForeground : ModSystem
{
	private float Progress;

	public float Intensity;

	private Vector2 Parallax;

	public virtual string Texture => "CalamityMod/ExtraTextures/Miscellaneous/NuclearTorrent";

	public virtual float Speed => 60f;

	public virtual float IntensityMaximum => 0.15f;

	public virtual Vector2 ParallaxDepth
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Vector2.Zero;
		}
	}

	public virtual bool DoesThisShow()
	{
		return false;
	}

	public override void PostDrawTiles()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		if (DoesThisShow())
		{
			Intensity = MathHelper.Lerp(Intensity, IntensityMaximum, 0.1f);
		}
		else
		{
			Intensity = MathHelper.Lerp(Intensity, 0f, 0.1f);
		}
		Intensity = MathHelper.Clamp(Intensity, 0f, 1f);
		Parallax += Main.LocalPlayer.velocity * ParallaxDepth;
		Progress += Speed;
		Main.spriteBatch.Begin();
		Draw();
		PostDraw();
		Main.spriteBatch.End();
		Update();
	}

	public override void PostUpdateEverything()
	{
		Update();
	}

	public virtual void PostDraw()
	{
	}

	public virtual void Update()
	{
	}

	public virtual void Draw()
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		Rectangle rect = default(Rectangle);
		Color col = default(Color);
		for (int i = -4; i < 14; i++)
		{
			float wid = tex.Width();
			float hei = tex.Height();
			((Rectangle)(ref rect))._002Ector(0, 0, (int)wid, (int)hei);
			float rot = 0f - Main.WindForVisuals;
			((Color)(ref col))._002Ector(Intensity, Intensity, Intensity, 0f);
			Main.EntitySpriteDraw(tex.Value, Utils.RotatedBy(new Vector2(Parallax.X % wid, 0f), (double)rot, default(Vector2)) + Utils.RotatedBy(new Vector2((float)(tex.Width() * i), 0f), (double)rot, default(Vector2)), rect, col, rot, new Vector2(0f, (hei - Progress) % hei), 1f, (SpriteEffects)0);
			Main.EntitySpriteDraw(tex.Value, Utils.RotatedBy(new Vector2(Parallax.X % wid, 0f), (double)rot, default(Vector2)) + Utils.RotatedBy(new Vector2((float)(tex.Width() * i), 0f - hei), (double)rot, default(Vector2)), rect, col, rot, new Vector2(0f, (hei - Progress) % hei), 1f, (SpriteEffects)0);
			Main.EntitySpriteDraw(tex.Value, Utils.RotatedBy(new Vector2(Parallax.X % wid, 0f), (double)rot, default(Vector2)) + Utils.RotatedBy(new Vector2((float)(tex.Width() * i), (0f - hei) * 2f), (double)rot, default(Vector2)), rect, col, rot, new Vector2(0f, (hei - Progress) % hei), 1f, (SpriteEffects)0);
		}
	}

	protected LoopingTextureForeground()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Parallax = Vector2.Zero;
		base._002Ector();
	}
}
