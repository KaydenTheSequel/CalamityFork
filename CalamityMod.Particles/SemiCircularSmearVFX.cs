using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class SemiCircularSmearVFX : Particle
{
	public Player player;

	public bool PlayerCentered;

	public Vector2 Squish;

	public override string Texture => "CalamityMod/Particles/SemiCircularSmear";

	public override bool UseAdditiveBlend => true;

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public SemiCircularSmearVFX(Vector2 position, Color color, float rotation, float scale, Vector2 squish, bool playerCentered = false)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		player = Main.LocalPlayer;
		base._002Ector();
		Position = position;
		Velocity = Vector2.Zero;
		Color = color;
		Scale = scale;
		Squish = squish;
		Rotation = rotation;
		Lifetime = 2;
		PlayerCentered = playerCentered;
	}

	public override void Update()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		if (PlayerCentered)
		{
			Position = player.MountedCenter;
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		spriteBatch.Draw(tex, Position - Main.screenPosition, (Rectangle?)null, Color, Rotation, tex.Size() / 2f, Squish * Scale, (SpriteEffects)0, 0f);
	}
}
