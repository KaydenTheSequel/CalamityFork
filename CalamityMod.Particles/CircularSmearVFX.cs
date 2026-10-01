using CalamityMod.ExtraTextures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;

namespace CalamityMod.Particles;

public class CircularSmearVFX : Particle
{
	public Asset<Texture2D> LoadedAsset;

	public override string Texture => "CalamityMod/Particles/CircularSmear";

	public override bool UseAdditiveBlend => true;

	public override bool SetLifetime => true;

	public override bool UseCustomDraw => true;

	public CircularSmearVFX(Vector2 position, Color color, float rotation, float scale, Asset<Texture2D>? texture = null)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = Vector2.Zero;
		Color = color;
		Scale = scale;
		Rotation = rotation;
		Lifetime = 2;
		LoadedAsset = texture;
		if (LoadedAsset == null)
		{
			LoadedAsset = ExtraTextureRefs.CircularSmear;
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		spriteBatch.Draw(LoadedAsset.Value, Position - Main.screenPosition, (Rectangle?)null, Color, Rotation, LoadedAsset.Value.Size() * 0.5f, Scale, (SpriteEffects)0, 0f);
	}
}
