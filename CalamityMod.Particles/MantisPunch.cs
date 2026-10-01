using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class MantisPunch : Particle
{
	private int Frame;

	private int FrameTimer;

	public override string Texture => "CalamityMod/Particles/MantisPunch";

	public override bool UseCustomDraw => true;

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		Main.EntitySpriteDraw(tex.Value, Position - Main.screenPosition, tex.Frame(1, 6, 0, Frame), Lighting.GetColor((Position / 16f).ToPoint()), Rotation, Origin, 1f, (SpriteEffects)0);
	}

	public MantisPunch(Vector2 position, float rotation)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Rotation = rotation;
		Origin = new Vector2(32f, 56f);
		AffectedByLight = true;
	}

	public override void Update()
	{
		FrameTimer++;
		if (FrameTimer % 3 == 0)
		{
			Frame++;
		}
		if (Frame >= 6)
		{
			Kill();
		}
	}
}
