using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

public class ChumBone : Particle
{
	private bool TexVariant;

	public override string Texture => "CalamityMod/Particles/ChumBone1";

	public override bool UseCustomDraw => true;

	public override bool SetLifetime => true;

	public static string Texture2 => "CalamityMod/Particles/ChumBone2";

	public ChumBone(Vector2 position, Vector2 velocity, Color color, float rotation, float scale, int lifeTime, bool variant)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Position = position;
		Velocity = velocity;
		Scale = scale;
		Lifetime = lifeTime;
		Rotation = rotation;
		Color = color;
		TexVariant = variant;
	}

	public override void Update()
	{
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		Tile below = CalamityUtils.ParanoidTileRetrieval((int)(Position.X / 16f), (int)(Position.Y / 16f));
		if (!below.HasTile || !below.IsTileSolid() || below.Slope > SlopeType.Solid)
		{
			if (Velocity.Y < 10f)
			{
				Velocity.Y++;
			}
		}
		else
		{
			Velocity.Y = 0f;
		}
		if (Time == Lifetime)
		{
			for (int i = 0; i < 4; i++)
			{
				Dust.NewDust(Position, 10, 10, 26, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f));
			}
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = (TexVariant ? ModContent.Request<Texture2D>(Texture2, (AssetRequestMode)2).Value : ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value);
		SpriteEffects spriteDir = (SpriteEffects)1;
		spriteBatch.Draw(tex, Position - Main.screenPosition, (Rectangle?)null, Lighting.GetColor(Position.ToTileCoordinates()), Rotation, new Vector2((float)(tex.Width / 2), (float)tex.Height), Scale, spriteDir, 0f);
	}
}
