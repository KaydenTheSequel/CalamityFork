using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;

namespace CalamityMod.Skies;

public class MonolithScreenShaderData : ScreenShaderData
{
	public MonolithScreenShaderData(string passName)
		: base(passName)
	{
	}

	public override void Apply()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Vector3 vec = ((Color)(ref Main.ColorOfTheSkies)).ToVector3();
		vec *= 0.4f;
		UseOpacity(Math.Max(vec.X, Math.Max(vec.Y, vec.Z)));
		base.Apply();
	}

	public override void Update(GameTime gameTime)
	{
		if (Main.LocalPlayer.Calamity().monolithAccursedShader < 1)
		{
			Filters.Scene["CalamityMod:MonolithAccursed"].Deactivate();
		}
	}
}
